namespace KarmaBook.Models
{
    public class ResponseResult
    {
        public bool Success { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public object? Pagination { get; set; }

        public ResponseResult() { }
        public ResponseResult(bool success, int statusCode, string message, object data, object pagination)
        {
            Success = success;
            StatusCode = statusCode;
            Message = message;
            Data = data;
            Pagination = pagination;
        }

    }
}
