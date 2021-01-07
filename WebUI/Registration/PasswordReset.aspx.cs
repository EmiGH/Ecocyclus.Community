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
    public partial class PasswordReset : System.Web.UI.Page
    {
        String _Token = "";
        String _Language = "";

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
            btnReset.Click += new EventHandler(btnReset_Click);
            LoadTexts();
        }

        #region Methods

        private void LoadTexts()
        {
            lblMessage.Text = Resources.Messages.AccountResetMessage;
            btnReset.Text = Resources.Data.Reset;
            rfvPassword.Text = rfvPassword2.Text = revPassword.Text = Resources.Messages.SummaryErrorCharacter;
            
        }

        #endregion

        #region Page events

        void btnReset_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    Library.Operation.AccountReset(_Token, txtPassword.Text, _Language);
                    
                    hdnMessageTitle.Value = Resources.Data.Success;
                    hdnMessage.Value = Resources.Messages.AccountResetSuccessful;
                }
                catch (Exception exception)
                {
                    String _error = Resources.Messages.AccountResetError;
                    _error = _error.Replace("[error]", exception.Message);
                    _error = _error.Replace("[mail]", Resources.Data.HelpDeskMailAddress);
                    
                    hdnMessageTitle.Value = Resources.Data.Error;
                    hdnMessage.Value = _error;
                }
            }
            else
            {
                hdnMessageTitle.Value = Resources.Data.Information;
                hdnMessage.Value = Resources.Messages.SummaryCheck;
            }
        }

        #endregion
    }
}