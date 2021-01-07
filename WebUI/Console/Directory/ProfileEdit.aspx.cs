using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Security;
using CSI.Library.Objects.Users;


namespace CSI.WebUI.Console.Directory
{
    public partial class ProfileEdit : BasePage
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

        #region Methods

        private void BindControls()
        {
            btnSave.Click += new EventHandler(btnSave_Click);
        }

        private void SetMenu()
        {
        }
        private void LoadTexts()
        {
            String _helpPostfix = Resources.Data.HelpPostfix;
            String _mandatoryField = Resources.Data.MandatoryField;

            lblSiteProperties.Text = Resources.Data.Properties;
            lblMandatoryFieldsExplanation.Text = Resources.Data.MandatoryFieldExplanation;
            
            //Labels
            lblImageFormatMessage.Text = Resources.Data.ImageFormat.Replace("[size]", WebUI.Common.GetImageMaxSizeInBytes().ToString());
            lblEmailFormatMessage.Text = Resources.Data.EmailUniqueMessage;

            lblChoosePicture.Text = Resources.Data.ChoosePicture;
            lblFirstname.Text = Resources.Data.Firstname + _mandatoryField + _helpPostfix;
            lblLastname.Text = Resources.Data.Lastname + _mandatoryField + _helpPostfix;
            lblEmail.Text = Resources.Data.Email + _mandatoryField + _helpPostfix;
            lblLanguage.Text = Resources.Data.Language + _mandatoryField + _helpPostfix;
            lblIsManager.Text = Resources.Data.IsCompanyManager + _mandatoryField + _helpPostfix;

            rfvFirstname.Text = rfvLastname.Text = revEmail.Text = cvLanguage.Text = 
            rfvEmail.Text = Resources.Messages.SummaryErrorCharacter;
            revPicture.Text = Resources.Messages.SummaryCheck;

            btnSave.Text = Resources.Data.Save;

            LoadLanguages();
        }
        private void LoadData()
        {
           
            Library.Objects.Auxiliaries.Files.File _picture = I.Picture;

            imgPicture.ImageUrl = WebUI.Common.GetPath(WebUI.Common.eFolders.Common, Request) + "ImageViewer.aspx?IdFile=" + (_picture != null ? _picture.IdFile.ToString() : "-1");
            Pair _size;
            if (_picture != null)
                _size = WebUI.Common.GetImageSize(_picture.Stream, 150);
            else
                _size = WebUI.Common.GetDefaultImageSize(150);
            imgPicture.Style.Add("height", _size.Second.ToString() + "px");
            imgPicture.Style.Add("width", _size.First.ToString() + "px");

            txtFirstname.Text = I.Firstname;
            txtLastname.Text = I.Lastname;
            txtEmail.Text = I.Email;
            lblIsManagerValue.Text = WebUI.Common.GetBooleanTranslation(I.IsManager);
            
            ddlLanguage.SelectedValue = I.Language.IdLanguage;
        }
        private void LoadLanguages()
        {
            //Clear language list
            ddlLanguage.Items.Clear();
            
            //Empty item for registration language
            ddlLanguage.Items.Add(new ListItem(Resources.Data.ChooseLanguage, "0"));

            foreach (CSI.Library.Objects.Auxiliaries.Globalization.Language _language in I.GetLanguagesAvailable().Values)
            {
                //Add to register language list
                ddlLanguage.Items.Add(new ListItem(_language.Name, _language.IdLanguage));
            }

        }
        private void SaveData()
        {
            if (IsValid)
            {
                try
                {
                    HttpPostedFile _file = Request.Files[0];
                    if (_file != null && _file.ContentLength > 0)
                    {
                        String _fileName = _file.FileName;
                        String _fileType = _file.ContentType;
                        Byte[] _fileContent;

                        byte[] buffer = new byte[16 * 1024];
                        using (MemoryStream ms = new MemoryStream())
                        {
                            int read;
                            while ((read = _file.InputStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                ms.Write(buffer, 0, read);
                            }
                            _fileContent = ms.ToArray();
                        }
                        if (_fileContent.Length > WebUI.Common.GetImageMaxSizeInBytes())
                            throw new ApplicationException(Resources.Messages.ErrorImageTooBig);

                        I.Modify(txtLastname.Text, txtFirstname.Text, txtEmail.Text, ddlLanguage.SelectedValue, _fileName, _fileType, _fileContent);

                    }
                    else
                        I.Modify(txtLastname.Text, txtFirstname.Text, txtEmail.Text, ddlLanguage.SelectedValue);

                    Session.Abandon();
                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Root, Request) + "Default.aspx");
                    
                }
                catch (Exception exception)
                {
                    String _error = Resources.Messages.SummaryError;
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

        #region Page Events

        void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        #endregion
    }

}