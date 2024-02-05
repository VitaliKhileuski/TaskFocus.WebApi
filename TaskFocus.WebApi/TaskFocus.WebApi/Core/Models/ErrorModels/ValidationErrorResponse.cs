namespace TaskFocus.WebApi.Core.Models.ErrorModels
{
    public class ValidationErrorResponse
    {
        public int StatusCode { get; set; }
        
        public ValidationErrorDetails Error { get; set; }
    }
}
