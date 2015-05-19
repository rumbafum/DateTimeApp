using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DateTimeOffsetApp
{
    /// <summary>
    /// Summary description for SetClientOffsetHandler
    /// </summary>
    public class SetClientOffsetHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string offset = context.Request["offset"];
            string momentDate = HttpUtility.HtmlDecode(context.Request["momentDate"]);
            Client.ClientOffset = Convert.ToInt16(offset);
            Client.ClientMomentDate = DateTimeOffset.Parse(momentDate);
            context.Response.ContentType = "text/plain";
            context.Response.Write("");
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