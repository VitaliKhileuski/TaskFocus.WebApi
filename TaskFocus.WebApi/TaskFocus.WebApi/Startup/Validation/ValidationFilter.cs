using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TaskFocus.WebApi.Core.Models.ErrorModels;

namespace TaskFocus.WebApi.Startup.Validation
{
    public class ValidationFilter : IActionFilter
    {
        private const int ValidationErrorStatus = (int)HttpStatusCode.UnprocessableEntity;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var error = context.ModelState.FirstOrDefault(x =>
                    x.Value.ValidationState == ModelValidationState.Invalid);

                var validationErrorDetails = new ValidationErrorDetails
                {
                    FieldName = error.Key,
                    Message = error.Value.Errors.FirstOrDefault()?.ErrorMessage
                };

                var badRequest = new BadRequestObjectResult(new ValidationErrorResponse
                {
                    StatusCode = ValidationErrorStatus,
                    Error = validationErrorDetails
                })
                {
                    StatusCode = ValidationErrorStatus
                };

                context.Result = badRequest;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}