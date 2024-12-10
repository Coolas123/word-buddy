namespace Domain.Shared
{
    public interface IValidationResult
    {
        public static readonly Error ValidationError = new(
            "e",
            "ee");
        Error[] Errors { get; }
    }
}
