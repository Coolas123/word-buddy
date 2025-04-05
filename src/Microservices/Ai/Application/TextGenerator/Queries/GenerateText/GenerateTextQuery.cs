using Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.TextGenerator.Queries.GenerateText
{
    public sealed class GenerateTextQuery : IQuery<string>
    {
        public string Prompt {  get; set; }
    }
}
