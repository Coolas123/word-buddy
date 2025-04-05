using Application.SignalR.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.ML.OnnxRuntimeGenAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Hubs
{
    public class CommunicationHub : Hub<ICommunicationHub>
    {
        private readonly ManagerConnection _managerConnection;
        public CommunicationHub(ManagerConnection managerConnection) {
            _managerConnection = managerConnection;
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
            var users = _managerConnection.Users.Select(x=>x.UserName).ToArray();
            await Clients.All.UpdateUsersAsync(users);
        }

        public async Task SendMessageAsync(string userName, string message) {
            foreach(var str in df.Rune(message)) {
                await Clients.Caller.SendMessageAsync(userName, str);
            }
        }
    }

    static class df {
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
