using Microsoft.AspNet.SignalR;

namespace ERP.WebApi.Hubs
{
    
    public class ChatHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}