namespace Application.Hubs
{
    public  interface ICommunicationHub
    {
        Task SendMessageAsync(string UserName, string message);
        //Task SendMessageAsync(string method, string UserName, string message);
        Task UpdateUsersAsync(IEnumerable<string> UserNames);

        Task OnGeneratedWordContextAsync(string message);
    }
}
