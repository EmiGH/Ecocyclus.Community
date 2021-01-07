using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Threading;

namespace CSI.WebUI.Registration
{
    public partial class RegistrationVerifier : System.Web.UI.Page
    {
        String _Token="";
        String _Language="";

        protected override void OnPreInit(EventArgs e)
        {
            _Token = Request.QueryString["token"].ToString();
            _Language = Request.QueryString["language"].ToString();

            CultureInfo ci = new CultureInfo(_Language);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            base.OnPreInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {            
            try
            {                
                
                Library.Operation.AccountVerify(_Token, _Language);
                lblMessage.Text = Resources.Messages.AccountActivationSuccessful;
            }
            catch (Exception ex)
            {
                String _error = Resources.Messages.AccountActivationError;
                _error = _error.Replace("[error]", ex.Message);
                _error = _error.Replace("[mail]", Resources.Data.HelpDeskMailAddress);

                lblMessage.Text = _error;

            }
        }
    }
}