using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Hubs
{
    public  interface ICommunicationHub
    {
        Task SendMessageAsync(string UserName, string message);
        Task UpdateUsersAsync(IEnumerable<string> UserNames);
    }
}
