using System.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IO;
using Serilog;
using Serilog.Context;
using TaskFocus.WebApi.Core.Models.ErrorModels;
using TaskFocus.WebApi.Startup.Attributes;

namespace TaskFocus.WebApi.Startup.Logger
{
    public class RequestHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;

        private static readonly string[] Types = { "html", "text", "xml", "json", "txt", "x-www-form-urlencoded" };

        public RequestHandlerMiddleware(RequestDelegate next)
        {
            _next = next; 
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            Stream originalBodyStream = null;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            
            try
            {
                await LogRequestBody(context);

                originalBodyStream = context.Response.Body;

                context.Response.Body = responseBody;

                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
            finally
            {
                await LogResponseBody(context);

                if (originalBodyStream != null)
                {
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
        }

        private async Task LogResponseBody(HttpContext context)
        {
            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var endpointLoggingAttribute = endpoint?.Metadata.GetMetadata<EndpointLoggingAttribute>();

            if (endpointLoggingAttribute != null)
            {
                context.Request.EnableBuffering();
                var responseBody = await GetResponseBody(context);
                using (LogContext.PushProperty("ResponseBody", responseBody))
                {
                    Log.Logger.Information($"Track response body for {endpoint?.DisplayName}");
                }
            }

            context.Response.Body.Seek(0, SeekOrigin.Begin);
        }

        private async Task LogRequestBody(HttpContext context)
        {
            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var endpointLoggingAttribute = endpoint?.Metadata.GetMetadata<EndpointLoggingAttribute>();

            if (endpointLoggingAttribute != null)
            {
                context.Request.EnableBuffering();
                var requsetBody = await GetRequestBody(context);
                using (LogContext.PushProperty("RequestBody", requsetBody))
                {
                    Log.Logger.Information($"Track request body for {endpoint?.DisplayName}");
                }
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            context.Request.EnableBuffering();
            var requsetBody = await GetRequestBody(context);
            var traceId = Activity.Current?.Id ?? context?.TraceIdentifier;

            using (LogContext.PushProperty("RequestBody", requsetBody))
            {
                Log.Logger.Error(exception, $"Internal Server Error request body for {endpoint?.DisplayName}");
            }

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorLogModel 
            {
                StatusCode = context.Response.StatusCode,
                Message = $"Something went wrong. TraceId: {traceId}"
            }));
        }

        private async Task<string> GetRequestBody(HttpContext context)
        {
            if (IsTextBasedContentType(context.Request.Headers))
            {
                byte[] buffer = new byte[Convert.ToInt32(context.Request.ContentLength)];
                await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
                var requestBody = Encoding.UTF8.GetString(buffer);
                context.Request.Body.Seek(0, SeekOrigin.Begin);
                return requestBody;
            }
            return string.Empty;
        }

        private async Task<string> GetResponseBody(HttpContext context)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();

            return text;
        }

        private bool IsTextBasedContentType(IHeaderDictionary headers)
        {
            if (!headers.TryGetValue("Content-Type", out var values))
                return false;
            var header =  string.Join(" ", values.ToArray()).ToLowerInvariant();

            return Types.Any(t => header.Contains(t));
        }
    }
}