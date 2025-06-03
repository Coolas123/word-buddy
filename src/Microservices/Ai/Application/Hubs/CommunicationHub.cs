using Application.SignalR.Models;
using Application.TextGenerator.Queries.GenerateText;
using Infrastructure.MassTransit.ViewModels;
using MassTransit;
using Microsoft.AspNetCore.SignalR;
using Microsoft.ML.OnnxRuntimeGenAI;
using System;
using System.Text;

namespace Application.Hubs
{
    public class CommunicationHub : Hub<ICommunicationHub>
    {
        private readonly ManagerConnection _managerConnection;
        private readonly IRequestClient<SaveWordContextRequest> requestClient;
        public CommunicationHub(ManagerConnection managerConnection, IRequestClient<SaveWordContextRequest> requestClient) {
            _managerConnection = managerConnection;
            this.requestClient = requestClient;
        }
        public override async Task OnConnectedAsync() {
            var userName = Context.User.Identity.Name ?? "Anon";
            var connectionId = Context.ConnectionId;
            _managerConnection.ConnectUser(userName, connectionId);
            await UpdateUsersAsync();
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception) {
            var isUserRemoved = _managerConnection.DisconnectUser(Context.ConnectionId);
            if(isUserRemoved)
                await base.OnDisconnectedAsync(exception);

            await UpdateUsersAsync();
            await base.OnDisconnectedAsync(exception);
        }

        public async Task UpdateUsersAsync() {
            var users = _managerConnection.Users.Select(x => x.UserName).ToArray();
            await Clients.All.UpdateUsersAsync(users);
        }

        public async Task SendMessageAsync(string userName, GenerateTextQuery generateTextQuery) {
            var wordContext = new StringBuilder(32);
            foreach (var str in Rune(generateTextQuery.Prompt)) {
                wordContext.Append(str);
                await Clients.Caller.SendMessageAsync(userName, str);
            }
            await OnGeneratedWordContextAsync(wordContext.ToString());
        }

        public async Task OnGeneratedWordContextAsync(string message) {
            await Clients.Caller.OnGeneratedWordContextAsync(message);
        }

        public async Task RemoveConntextion() {
            var isUserRemoved = _managerConnection.DisconnectUser(Context.ConnectionId);
            await UpdateUsersAsync();

        }


        //-----------------------------------------
        public static IEnumerable<string> Rune(string message) {
            var modelPath = @"D:\CSharp\ms text3\Phi-3-mini-4k-instruct-onnx\cpu_and_mobile\cpu-int4-rtn-block-32";
            var model = new Model(modelPath);
            var tokenizer = new Tokenizer(model);

            var systemPrompt = "You are an AI assistant that helps people find information. Answer questions using a direct style. Do not share more information that the requested by the users.";

            // chat start
            Console.WriteLine(@"Ask your question. Type an empty string to Exit.");




            // show phi3 response
            Console.Write("Phi3: ");
            var fullPrompt = $"<|system|>{systemPrompt}<|end|><|user|>{message}<|end|><|assistant|>";
            var tokens = tokenizer.Encode(fullPrompt);

            var generatorParams = new GeneratorParams(model);
            generatorParams.SetSearchOption("max_length", 2048);
            generatorParams.SetSearchOption("past_present_share_buffer", false);
            generatorParams.SetInputSequences(tokens);

            var generator = new Generator(model, generatorParams);
            while (!generator.IsDone()) {
                generator.ComputeLogits();
                generator.GenerateNextToken();
                var outputTokens = generator.GetSequence(0);
                var newToken = outputTokens.Slice(outputTokens.Length - 1, 1);
                var output = tokenizer.Decode(newToken);
                yield return output;
            }
            Console.WriteLine();

        }
    }
}
