using Application.Abstractions.Messaging;
using Domain.Shared;
using Microsoft.AspNetCore.SignalR.Client;

namespace Application.TextGenerator.Queries.GenerateText
{
    internal class GenerateTextQueryHandler : IQueryHandler<GenerateTextQuery, string>
    {
        public async Task<Result<string>> Handle(GenerateTextQuery request, CancellationToken cancellationToken) {
            var connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5031/chathub", opt => {
                    //opt.Headers.Add(Authorization, $"Bearer {token}")
                })
                .WithAutomaticReconnect()
                .Build();

            connection.On<IEnumerable<string>>("UpdateUsersAsync", users => {

            });

            connection.On<string, string>("SendMessageAsync", (user, message) => {
                Console.Write($"{message}");
            });

            try {
                connection.StartAsync().Wait();
                //
                await connection.SendAsync("SendMessageAsync", "username",request.Prompt);
            }
            catch { }
            return Result.Success("ds");
        }
    }
}
