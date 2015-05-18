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
            Client.ClientOffset = Convert.ToInt16(offset);
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