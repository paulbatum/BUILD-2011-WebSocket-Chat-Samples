using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using SignalR.Hubs;

namespace chat
{
    public class ChatHub : Hub, IDisconnect
    {
        #region privatevars
        private static readonly Dictionary<string, string> _clients = new Dictionary<string, string>();
        private static readonly JavaScriptSerializer serializer = new JavaScriptSerializer();
        #endregion

        public Task Join()
        {
            var user = StoreUser(Context, Context.ClientId);

            return Clients.messageFromServer(user, user + " joined the server.");
        }

        public Task Send(string message)
        {
            var user = GetUser(Context.ClientId);

            return Clients.messageFromServer(user, message);
        }

        private string StoreUser(HubContext context, string clientId)
        {
            var cookie = context.Cookies["username"];
            string user;
            if (cookie != null)
            {
                _clients[clientId] = cookie.Value;
                user = cookie.Value;

            }
            else { user = clientId; }

            return user;
        }

        private string GetUser(string clientId)
        {
            string user;
            if (!_clients.TryGetValue(clientId, out user))
            {
                return clientId;
            }
            return user;
        }

        void IDisconnect.Disconnect()
        {
            var user = GetUser(Context.ClientId);
            _clients.Remove(Context.ClientId);

            Clients.messageFromServer(user, user + " has left the server.");
        }

        enum MessageType
        {
            Send,
            Broadcast,
            Join,
            Leave
        }

        class Message
        {
            public MessageType Type { get; set; }
            public string Value { get; set; }
        }


    }
}