using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DateTimeOffsetApp
{
    public partial class Default : System.Web.UI.Page
    {
        public DateTime dt;

        protected void Page_Load(object sender, EventArgs e)
        {
            dt = DateTime.Now;
            dt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc);
            var dispDt = dt.ToLocalTime();
            var dtString = dispDt.ToString(@"dd/MM/yyyy HH:mm:ss tt");
            ASPxDateEdit1.Value = dtString;
        }

        public string GetClientOffset()
        {
            return Client.ClientDateTime.ToString();
        }

        protected void OKButton_Click(object sender, EventArgs e)
        {
            string locale = "";
            DateTimeStyles styles = DateTimeStyles.AllowInnerWhite | DateTimeStyles.AllowLeadingWhite |
                                           DateTimeStyles.AllowTrailingWhite;
            DateTime localDate = DateTime.Now;
            DateTimeOffset localDateOffset = DateTimeOffset.Now;
            int integerOffset;
            bool result = false;

            // Hide form elements.
            this.DateForm.Visible = false;

            // Create array of CultureInfo objects
            CultureInfo[] cultures = new CultureInfo[Request.UserLanguages.Length + 1];
            for (int ctr = Request.UserLanguages.GetLowerBound(0); ctr <= Request.UserLanguages.GetUpperBound(0);
                 ctr++)
            {
                locale = Request.UserLanguages[ctr];
                if (!string.IsNullOrEmpty(locale))
                {

                    // Remove quality specifier, if present.
                    if (locale.Contains(";"))
                        locale = locale.Substring(0, locale.IndexOf(';'));
                    try
                    {
                        cultures[ctr] = new CultureInfo(locale, false);
                    }
                    catch (Exception) { }
                }
                else
                {
                    cultures[ctr] = CultureInfo.CurrentCulture;
                }
            }
            cultures[Request.UserLanguages.Length] = CultureInfo.InvariantCulture;

            // Get date and time information from hidden field.
            string[] dates = Request.Form["DateInfo"].Split(';');

            // Parse local date using each culture.
            foreach (CultureInfo culture in cultures)
            {
                result = DateTime.TryParse(dates[0], culture, styles, out localDate);
                if (result) break;
            }
            // Parse offset 
            result = int.TryParse(dates[1], out integerOffset);
            // Instantiate DateTimeOffset object representing user's local time
            if (result)
            {
                try
                {
                    localDateOffset = new DateTimeOffset(localDate, new TimeSpan(0, -integerOffset, 0));
                }
                catch (Exception)
                {
                    result = false;
                }
            }
            // Display result to user.
            if (result)
            {
                Response.Write("<P />");
                Response.Write("Your local date and time is " + localDateOffset.ToString() + ".<BR />");
                Response.Write("The date and time on the server is " +
                               TimeZoneInfo.ConvertTime(localDateOffset,
                                                        TimeZoneInfo.Local).ToString() + ".<BR />");
                Response.Write("Coordinated Universal Time is " + localDateOffset.ToUniversalTime().ToString() + ".<BR />");
            }
            else
            {
                Response.Write("<P />");
                Response.Write("Unable to recognize " + Server.HtmlEncode(dates[0]) + ".<BR />");
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            string script = "function AddDateInformation() { \n" +
                      "var today = new Date();\n" +
                      "document.getElementsByTagName('form')[0].DateInfo.value = today.toLocaleString() + \";\" + today.getTimezoneOffset();\n" +
                      " }";
            // Register client script
            ClientScriptManager scriptMgr = Page.ClientScript;
            scriptMgr.RegisterClientScriptBlock(this.GetType(), "SubmitOnClick", script, true);
        }

    }
}