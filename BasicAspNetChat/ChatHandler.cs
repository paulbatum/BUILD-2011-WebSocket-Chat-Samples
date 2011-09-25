using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using Microsoft.Web.WebSockets;

namespace chat
{
    public class ChatHandler : WebSocketHandler
    {
        private string user;
        private JavaScriptSerializer serializer = new JavaScriptSerializer();

        private static WebSocketCollection chatapp = new WebSocketCollection();

        public ChatHandler(string username)
        {
            user = username;
        }

        public override void OnOpen()
        {
            chatapp.Add(this);

            chatapp.Broadcast(serializer.Serialize(new
            {
                type = MessageType.Join,
                from = user,
                value = user + " joined the server."
            }));

        }

        public override void OnMessage(string message)
        {
            var m = serializer.Deserialize<Message>(message);

            switch (m.Type)
            {
                case MessageType.Broadcast:
                    chatapp.Broadcast(serializer.Serialize(new
                    {
                        type = m.Type,
                        from = user,
                        value = m.Value
                    }));

                    break;
                default:
                    return;
            }
        }

        public override void OnClose()
        {
            chatapp.Remove(this);

            chatapp.Broadcast(serializer.Serialize(new
            {
                type = MessageType.Leave,
                from = user,
                value = user + " has left the server."
            }));
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