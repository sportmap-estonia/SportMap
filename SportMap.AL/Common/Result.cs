using SportMap.AL.Constants;

namespace SportMap.AL.Common
{
    public class Result
    {
        public bool IsSucceed { get; init; }
        public string? Message { get; init; }
        public Exception? Error { get; init; }

        public static Result WithMessage(string message) => new() { IsSucceed = true, Message = message };
        public static Result WithError(string errorMsg) => new() { IsSucceed = false, Message = errorMsg };
    }

    public class Result<T> : Result
    {
        public T? Value { get; init; }

        public static Result<T> WithValue(T value) => new() { IsSucceed = true, Value = value };
        public static Result<T> NotFound(string resourceName) => new() { IsSucceed = true, Message = string.Format(ResultConstants.NotFound, resourceName) };
        public static Result<T> WithError(Exception ex) => new() { IsSucceed = false, Error = ex };
    }
}