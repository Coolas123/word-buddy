namespace Domain.EntityServices
{
    public interface IEmailUniqueCheck
    {
        Task<bool> IsUnique(string email);
    }
}
