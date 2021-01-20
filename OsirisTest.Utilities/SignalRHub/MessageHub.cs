using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using OsirisTest.Contracts;
using OsirisTest.Utilities.DataAccess.Models;

namespace OsirisTest.Utilities.SignalRHub
{
    public class MessageHub : Hub
    {
        public async Task SendCustomerMessage(BaseMessage<Customer> message)
        {
            await Clients.All.SendAsync("ReceiveCustomerMessage", message);
        }

        public async Task SendWagerMessage(BaseMessage<Wager> message)
        {
            await Clients.All.SendAsync("ReceiveWagerMessage", message);
        }
    }
}
