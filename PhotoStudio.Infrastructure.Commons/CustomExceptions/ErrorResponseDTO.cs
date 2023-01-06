using System.Text.Json;

namespace PhotoStudio.Infrastructure.Commons.CustomExceptions
{
    public class ErrorResponseDTO
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public int StatusCode { get; set; }

        public string ErrorDetails { get; set; }

        public string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
