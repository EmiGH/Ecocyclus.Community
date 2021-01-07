using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSI.WebUI.Console.Directory
{
    public partial class ProfileChangePassword : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SetMenu();
            if (!IsPostBack)
            {
                LoadTexts();
                LoadData();
            }
        }

        #region Private Methods

        private void BindControls()
        {
            btnSave.Click += new EventHandler(btnSave_Click);
        }

        private void LoadTexts()
        {
            String _helpPostfix = Resources.Data.HelpPostfix;
            String _mandatoryField = Resources.Data.MandatoryField;

            lblSiteProperties.Text = Resources.Data.Properties;
            lblMandatoryFieldsExplanation.Text = Resources.Data.MandatoryFieldExplanation;

            lblDate.Text = Resources.Data.Date;
            lblName.Text = Resources.Data.Fullname;
            lblEmail.Text = Resources.Data.Email;
            lblIsManager.Text = Resources.Data.IsCompanyManager;

            lblOldPassowrd.Text = Resources.Data.PasswordCurrent + _mandatoryField + _helpPostfix;
            lblNewPassword.Text = Resources.Data.PasswordNew + _mandatoryField + _helpPostfix;
            lblRetypePassword.Text = Resources.Data.PasswordRetype + _mandatoryField + _helpPostfix;

            rfvNewPassword.Text = rfvOldPassword.Text = cvRetypePassword.Text =
            rfvRetypePassword.Text = Resources.Messages.SummaryErrorCharacter;

            btnSave.Text = Resources.Data.Save;

        }
        private void LoadData()
        {
            Library.Objects.Auxiliaries.Files.File _picture = I.Picture;

            imgPicture.Src = WebUI.Common.GetPath(WebUI.Common.eFolders.Common, Request) + "ImageViewer.aspx?IdFile=" + (_picture != null ? _picture.IdFile.ToString() : "-1");
            Pair _size;
            if (_picture != null)
                _size = WebUI.Common.GetImageSize(_picture.Stream, 150);
            else
                _size = WebUI.Common.GetDefaultImageSize(150);
            imgPicture.Style.Add("height", _size.Second.ToString() + "px");
            imgPicture.Style.Add("width", _size.First.ToString() + "px");

            lblNameValue.Text = I.Fullname;
            lblEmailValue.Text = I.Email;
            lblDateValue.Text = I.Timestamp.ToShortDateString();
            lblIsManagerValue.Text = WebUI.Common.GetBooleanTranslation(I.IsManager);
        }
        private void SetMenu()
        {
        }

        #endregion

        #region Page Events

        void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    I.ChangePassword(txtOldPassword.Text, txtNewPassword.Text);

                    ((Main)Page.Master).ErrorHandler.SetMessage(Resources.Data.Success, Resources.Messages.StandardSuccessful);
                }
                catch (Exception exception)
                {
                    String _error = Resources.Messages.StandardError;
                    _error = _error.Replace("[error]", exception.Message);
                    _error = _error.Replace("[mail]", Resources.Data.HelpDeskMailAddress);

                    ((Main)Page.Master).ErrorHandler.SetMessage(Resources.Data.Error, _error);
                }
            }
            else
            {
                ((Main)Page.Master).ErrorHandler.SetMessage(Resources.Data.Information, Resources.Messages.SummaryCheck);
            }
        }

        #endregion
    }
}