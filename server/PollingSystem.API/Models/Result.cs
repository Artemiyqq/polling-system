namespace PollingSystem.API.Models
{
    public class Result
    {
        public required string Message { get; set; }
        public bool Success { get; set; }

        public static Result SuccessResult(string message)
        {
            return new Result
            {
                Success = true,
                Message = message
            };
        }

        public static Result FailureResult(string message)
        {
            return new Result
            {
                Success = false,
                Message = message
            };
        }
    }
    public class Result<T> : Result
    {
        public T? Data { get; set; }

        public static Result<T> SuccessResult(T data, string message = "")
        {
            return new Result<T>
            {
                Success = true,
                Data = data,
                Message = message
            };
        }

        public static new Result<T> FailureResult(string message)
        {
            return new Result<T>
            {
                Success = false,
                Message = message
            };
        }
    }
}