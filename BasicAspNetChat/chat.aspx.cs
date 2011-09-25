using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace chat
{
    public partial class ChatPage : System.Web.UI.Page
    {
        public string HostUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            HostUrl = Request.Url.Host + ":" + Request.Url.Port + Response.ApplyAppPathModifier("~/chat.ashx");
        }
    }
}