using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSI.WebUI.Registration
{
    public partial class PasswordRecovery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            btnRecover.Click += new EventHandler(btnRecover_Click);
            LoadTexts();
        }

        #region Methods

        private void LoadTexts()
        {
            lblMessage.Text = Resources.Messages.AccountRecoveryMessage;
            btnRecover.Text = Resources.Data.Recover;
            rfvEmail.Text = revEmail.Text = Resources.Messages.SummaryErrorCharacter;
        }

        #endregion

        #region Page events

        void btnRecover_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    Library.Operation.AccountRecover(txtEmail.Text, Session["CurrentLanguage"].ToString());

                    btnRecover.Enabled = false;

                    hdnMessageTitle.Value = Resources.Data.Success;
                    hdnMessage.Value = Resources.Messages.AccountRecoverySuccessful;
                    
                }
                catch (Exception exception)
                {
                    String _error = Resources.Messages.AccountRecoveryError;
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