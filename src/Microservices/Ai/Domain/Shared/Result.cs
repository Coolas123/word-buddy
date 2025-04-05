namespace Domain.Shared
{
    public class Result
    {
        public Result(bool isSuccess,  Error error) {
            (IsSuccess, Error) = (isSuccess, error);
        }

        public Error Error { get; }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public static Result Success () {
            return new(true,Error.None);
        }

        public static Result<T> Success<T>(T value) {
            return new(value, true, Error.None);
        }

        public static Result Failure() {
            return new(false, Error.None);
        }

        public static Result<T> Failure<T>(Error error) {
            return new(default, false, error);
        }
    }
}
