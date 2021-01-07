using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using CSI.Library.Objects.Companies;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Directory
{
    public partial class CompanyEdit : BasePage
    {
        public String Location
        {
            set
            {
                ViewState["Location"] = value.Replace(',', '.');
            }
            get
            {
                return ViewState["Location"].ToString();
            }
        }
        public String Address
        {
            set
            {
                ViewState["Address"] = value;
            }
            get
            {
                return ViewState["Address"].ToString();
            }
        }
        
        protected void Page_Init(object sender, EventArgs e)
        {
            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Permissions
            if (!(I is UserOperatorMeManager))
                throw new ApplicationException(Resources.Messages.AccessDenied);

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

            //Titles
            lblSiteProperties.Text = Resources.Data.Properties;
            lblMandatoryFieldsExplanation.Text = Resources.Data.MandatoryFieldExplanation;

            lblLocation.Text = Resources.Data.Location + _helpPostfix;
            
            //Labels
            lblChoosePicture.Text = Resources.Data.ChoosePicture;

            lblImageFormatMessage.Text = Resources.Data.ImageFormat.Replace("[size]", WebUI.Common.GetImageMaxSizeInBytes().ToString());
            lblUrlFormatMessage.Text = Resources.Data.UrlFormat;
            lblEmailFormatMessage.Text = Resources.Data.EmailUniqueMessage;

            lblName.Text = Resources.Data.CompanyName + _mandatoryField + _helpPostfix;
            lblEmail.Text = Resources.Data.Email + _helpPostfix;
            lblUrl.Text = Resources.Data.Url + _helpPostfix;
            lblFacebook.Text = Resources.Data.Facebook + _helpPostfix;
            lblTwitter.Text = Resources.Data.Twitter + _helpPostfix;
            lblTelephone.Text = Resources.Data.Telephone + _helpPostfix;
            lblAddress.Text = Resources.Data.Address + _helpPostfix;

            rfvName.Text = revUrl.Text = revEmail.Text = revLogo.Text = Resources.Messages.SummaryErrorCharacter;

            btnSave.Text = Resources.Data.Save;

            //Map
            lblMapLocator.Text = Resources.Data.Locator;
         }
        private void LoadData()
        {
            CompanyMine _company = (CompanyMine)MyLibrary.CurrentUser.Company;
            CSI.Library.Objects.Auxiliaries.Geographic.Contact _contact = _company.Contact;
            
            //Location and Address
            if (_contact.Location != null)
            {
                CSI.Library.Objects.Auxiliaries.Geographic.Location _location = _contact.Location;
                CSI.Library.Objects.Auxiliaries.Geographic.Position _position = _location.Position;

                Location = hdnMapLocationPoint.Value = _location.Position.Coordenates;
                Address  = lblMapLocationSelected.Text = hdnMapLocationAddress.Value = _location.Address;
            }
            else
            {
                Location = "";
                Address = "";
            }

            //Logo
            imgLogo.ImageUrl = WebUI.Common.GetPath(WebUI.Common.eFolders.Common, Request) + "ImageViewer.aspx?IdFile=" + (_company.Logo != null ? _company.Logo.IdFile.ToString() : "-1");
            Pair _size;
            if (_company.Logo != null)
                _size = WebUI.Common.GetImageSize(_company.Logo.Stream, 150);
            else
                _size = WebUI.Common.GetDefaultImageSize(150);
            imgLogo.Style.Add("height", _size.Second.ToString() + "px");
            imgLogo.Style.Add("width", _size.First.ToString() + "px");

            txtName.Text = _company.Name;

            txtUrl.Text = _contact.Website;
            txtEmail.Text = _contact.Email;
            txtFacebook.Text = _contact.Facebook;
            txtTwitter.Text = _contact.Twitter;
            txtTelephone.Text = _contact.Telephone;
            
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

                        if(hdnMapLocationAddress.Value == "")
                            ((UserOperatorMeManager)MyLibrary.CurrentUser).ModifyCompany(txtName.Text, txtTelephone.Text, txtEmail.Text, txtUrl.Text, txtFacebook.Text, txtTwitter.Text, _fileName, _fileType, _fileContent);
                        else
                            ((UserOperatorMeManager)MyLibrary.CurrentUser).ModifyCompany(txtName.Text, hdnMapLocationAddress.Value, new Library.Objects.Auxiliaries.Geographic.Position(hdnMapLocationPoint.Value), txtTelephone.Text, txtEmail.Text, txtUrl.Text, txtFacebook.Text, txtTwitter.Text, _fileName, _fileType, _fileContent);
                    }
                    else
                        if (hdnMapLocationAddress.Value == "")
                            ((UserOperatorMeManager)MyLibrary.CurrentUser).ModifyCompany(txtName.Text, txtTelephone.Text, txtEmail.Text, txtUrl.Text, txtFacebook.Text, txtTwitter.Text);
                        else
                            ((UserOperatorMeManager)MyLibrary.CurrentUser).ModifyCompany(txtName.Text, hdnMapLocationAddress.Value, new Library.Objects.Auxiliaries.Geographic.Position(hdnMapLocationPoint.Value), txtTelephone.Text, txtEmail.Text, txtUrl.Text, txtFacebook.Text, txtTwitter.Text);

                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Directory, Request) + "Company.aspx");
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

        #region Page Events

        void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        #endregion

    }
}