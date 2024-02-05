namespace TaskFocus.WebApi.Core.Constants
{
    public class SwaggerConstants
    {
        public const string SwaggerAuthorizationDescription =
            "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"";

        public const string SecurityDefinitionName = "Bearer";

        public const string SecurityScheme = "Bearer";

        public const string DocumentTitle = "TaskFocus.WebApi";

        public const string Version = "v1";

        public const string RoutePrefix = "Swagger";

        public const string Endpoint = "/swagger/v1/swagger.json";

        public const string SecuritySchemeName = "Authorization";
    }
}