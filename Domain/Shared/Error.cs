namespace Domain.Shared
{
    public class Error
    {
        public Error(string code, string message) {
            (Code, Message) = (code, message);
        }

        public string Code { get; }

        public string Message { get; }

        public static readonly Error None = new(string.Empty, string.Empty);
    }
}
