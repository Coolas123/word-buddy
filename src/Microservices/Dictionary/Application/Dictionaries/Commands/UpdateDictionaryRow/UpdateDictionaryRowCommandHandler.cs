using Application.Abstractions.Messaging;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dictionaries.Commands.UpdateDictionaryRow
{
    internal class UpdateDictionaryRowCommandHandler : ICommandHandler<UpdateDictionaryRowCommand>
    {
        
        public Task<Result> Handle(UpdateDictionaryRowCommand request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }
}
