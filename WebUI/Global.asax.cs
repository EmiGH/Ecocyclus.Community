using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;

namespace WebUI
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Código que se ejecuta cuando se cierra la aplicación

        }

        void Application_Error(object sender, EventArgs e)
        {
            //// Code that runs when an unhandled error occurs
            //if (null != Context && null != Context.AllErrors)
            //    System.Diagnostics.Debug.WriteLine(Context.AllErrors.Length);

            ////bool isUnexpectedException = true;
            //HttpContext context = ((HttpApplication)sender).Context;

            //Exception ex = context.Server.GetLastError();
            //if (ex.InnerException != null)
            //    ex = ex.InnerException;

            //if (System.Web.HttpContext.Current != null)
            //{
            //    if (System.Web.HttpContext.Current.Session != null)
            //    {
            //        if (Session["Operation"] != null)
            //        {
            //            ((CSI.Library.Operation)Session["Operation"]).LogAuthenticatedError(ex);
            //            Response.Redirect(CSI.WebUI.Common.GetPath(CSI.WebUI.Common.eFolders.Console, Request) + "Error.aspx");
            //        }
            //        else
            //        {
            //            CSI.Library.Operation.LogUnauthenticatedError(ex);
            //            Response.Redirect(CSI.WebUI.Common.GetPath(CSI.WebUI.Common.eFolders.Console, Request) + "Error.aspx");
            //        }
            //    }
            //    else
            //    {
            //        CSI.Library.Operation.LogUnauthenticatedError(ex);
            //        Response.Redirect(CSI.WebUI.Common.GetPath(CSI.WebUI.Common.eFolders.Console, Request) + "Error.aspx");
            //    }
            //}
            //else
            //{
            //    CSI.Library.Operation.LogUnauthenticatedError(ex);
            //    Response.Redirect(CSI.WebUI.Common.GetPath(CSI.WebUI.Common.eFolders.Console, Request) + "Error.aspx");
            //}
        }

        void Session_Start(object sender, EventArgs e)
        {
        }

        void Session_End(object sender, EventArgs e)
        {
            // Código que se ejecuta cuando finaliza una sesión.
            // Nota: el evento Session_End se desencadena sólo cuando el modo sessionstate
            // se establece como InProc en el archivo Web.config. Si el modo de sesión se establece como StateServer 
            // o SQLServer, el evento no se genera.

        }

    }
}
