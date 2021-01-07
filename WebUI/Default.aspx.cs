using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web.Services;

namespace CSI.WebUI
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static void Contact(String from, String name, String company, String message)
        {
            try
            {
                Library.Operation.Contact(from, name, company, message);
            }
            catch
            {
                
            }
        }
    }
}