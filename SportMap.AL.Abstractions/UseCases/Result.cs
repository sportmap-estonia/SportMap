namespace SportMap.AL.Abstractions.UseCases
{
    public class Result(string errorMessage)
    {
        public string ErrorMessage { get; init; } = errorMessage;
        public bool HasError { get; init; } = !String.IsNullOrEmpty(errorMessage);

        public static Result WithError(string message) => new(message);
        public static Result Succeeded() => new(string.Empty);
    }

    public class Result<T>(T? data, string errorMessage) : Result(errorMessage)
    {
        public T? Data { get; init; } = data;
        public new static Result<T> WithError(string message) => new(default, message);
        public static Result<T> WithData(T data) => new(data, string.Empty);
    }
}
