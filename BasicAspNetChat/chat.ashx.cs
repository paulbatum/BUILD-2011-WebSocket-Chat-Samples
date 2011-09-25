using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Web.WebSockets;

namespace chat
{
    /// <summary>
    /// Summary description for chat_sockets
    /// </summary>
    public class ChatHttpHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var user = context.Request.Cookies["username"].Value;

            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(new ChatHandler(user));
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}