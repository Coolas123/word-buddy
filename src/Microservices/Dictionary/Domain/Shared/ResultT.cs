namespace Domain.Shared
{
    public class Result<TResult> : Result
    {
        private readonly TResult value;
        public Result(TResult value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            this.value = value;
        }
        public TResult Value() {
        return
            IsSuccess ?
            value :
            throw new InvalidOperationException("The value of a failure result can not be accessed");
    }
}
}
