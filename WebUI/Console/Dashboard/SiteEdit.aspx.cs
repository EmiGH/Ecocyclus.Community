using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard
{
    public partial class SiteEdit : BasePage
    {
        Library.Objects.Sites.SiteMineOpen _Site;
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
            _Site = (Library.Objects.Sites.SiteMineOpen)I.GetSite(Convert.ToInt64(Request.QueryString["Site"].ToString()));
            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            //Permissions
            if (!(I is UserOperatorMeManager))
            {
                throw new ApplicationException(Resources.Messages.AccessDenied);
            }

            if (!IsPostBack)
            {
                SetMenu();
                LoadTexts();
                LoadTypes();
                LoadCurrencies();
                LoadLanguages();
                LoadLanguageOptions();

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
            ((Main)Page.Master).MenuNavigation.Initialize(_Site.IdSite, _Site.Title, Console.Controls.ucMenuNavigation.MenuItem.None, WebUI.Common.GetPermissionFromContext(I,_Site));
        }
        private void LoadTexts()
        {
            String _helpPostfix = Resources.Data.HelpPostfix;
            String _mandatoryField = Resources.Data.MandatoryField;

            //Titles
            lblSiteProperties.Text = Resources.Data.Properties;
            lblMandatoryFieldsExplanation.Text = Resources.Data.MandatoryFieldExplanation;
            lblSiteClient.Text = Resources.Data.Client;
            lblMap.Text = Resources.Data.Map;

            //Labels
            lblEmailFormatMessage.Text = Resources.Data.EmailUniqueMessage;
            lblUrlFormatMessage.Text = Resources.Data.UrlFormat;

            lblChoosePicture.Text = Resources.Data.ChoosePicture;
            lblImageFormatMessage.Text = Resources.Data.ImageFormat.Replace("[size]", WebUI.Common.GetImageMaxSizeInBytes().ToString());

            lblType.Text = Resources.Data.SiteType + _mandatoryField + _helpPostfix; 
            lblStart.Text = Resources.Data.Start + Resources.Data.MandatoryField + _helpPostfix;
            lblWeeks.Text = Resources.Data.Weeks + Resources.Data.MandatoryField + _helpPostfix;
            lblTitle.Text = Resources.Data.Title + Resources.Data.MandatoryField + _helpPostfix;
            lblNumber.Text = Resources.Data.Number + _helpPostfix;
            lblPreviousLocation.Text = Resources.Data.LocationPrevious + Resources.Data.MandatoryField + _helpPostfix;
            lblValue.Text = Resources.Data.Value + Resources.Data.MandatoryField + _helpPostfix;
            lblFloorSpace.Text = Resources.Data.FloorSpace + Resources.Data.MandatoryField + _helpPostfix;
            lblUnits.Text = Resources.Data.Units + _helpPostfix;

            lblClient.Text = Resources.Data.Client + _helpPostfix;
            lblAgent.Text = Resources.Data.Agent + _helpPostfix;
            lblContractor.Text = Resources.Data.Contractor + _helpPostfix;
            lblResponsible.Text = Resources.Data.Responsible + _helpPostfix;
            lblManager.Text = Resources.Data.Manager + _helpPostfix;
            lblDescription.Text = Resources.Data.Description + " [" + Resources.Data.InDefaultLanguage + "]" + _helpPostfix;

            lblTelephone.Text = Resources.Data.Telephone + _helpPostfix;
            lblEmail.Text = Resources.Data.Email + _helpPostfix;
            lblUrl.Text = Resources.Data.Url + _helpPostfix;
            lblFacebook.Text = Resources.Data.Facebook + _helpPostfix;
            lblTwitter.Text = Resources.Data.Twitter + _helpPostfix;

            lblIsPublic.Text = Resources.Data.IsPublic + _helpPostfix;

            btnDescriptionTranslations.Text = Resources.Data.Translations + _helpPostfix;
            btnDescriptionTranslationsSave.Text = Resources.Data.Save;

            calStart.Format = CurrentCultureInfo.DateTimeFormat.ShortDatePattern;

            //Validators
            rfvTitle.Text = rfvValue.Text = rfvWeeks.Text = rfvFloorSpace.Text = cvStart.Text =
            revUrl.Text = revEmail.Text = cuvLocation.Text = cvUnits.Text = cvTypes.Text =
            cvWeeks.Text = cvValue.Text = cvFloorSpace.Text = Resources.Messages.SummaryErrorCharacter;

            btnSave.Text = Resources.Data.Save;

        }
        private void LoadData()
        {
            Library.Objects.Auxiliaries.Files.File _image = _Site.Image;
            CSI.Library.Objects.Auxiliaries.Geographic.Contact _contact = _Site.Contact;

            //Location
            CSI.Library.Objects.Auxiliaries.Geographic.Location _location = _contact.Location;
            Location = hdnLocationPoint.Value = _location.Position.Coordenates;
            Address = hdnLocationAddress.Value = _location.Address;
            hdnLocationCountry.Value = _Site.Country.Code;

            imgImage.ImageUrl = WebUI.Common.GetPath(WebUI.Common.eFolders.Common, Request) + "ImageViewer.aspx?IdFile=" + (_Site.Image != null ? _Site.Image.IdFile.ToString() : "-1");
            Pair _size;
            if (_Site.Image != null)
                _size = WebUI.Common.GetImageSize(_Site.Image.Stream, 150);
            else
                _size = WebUI.Common.GetDefaultImageSize(150);
            imgImage.Style.Add("height", _size.Second.ToString() + "px");
            imgImage.Style.Add("width", _size.First.ToString() + "px");

            ddlTypes.SelectedValue = _Site.Type.IdSiteType.ToString();
            txtStart.Text = _Site.Start.ToShortDateString();
            txtWeeks.Text = _Site.Weeks.ToString();
            txtTitle.Text = _Site.Title;
            txtNumber.Text = _Site.Number;
            
            txtValue.Text = _Site.Value.ToString();
            txtFloorSpace.Text = _Site.FloorSpace.ToString();
            txtUnits.Text = _Site.Units.ToString();

            txtClient.Text = _Site.Client;
            txtAgent.Text = _Site.Agent;
            txtContractor.Text = _Site.Contractor;
            txtResponsible.Text = _Site.Responsible;
            txtManager.Text = _Site.Manager;

            txtTelephone.Text = _contact.Telephone;
            txtEmail.Text = _contact.Email;
            txtUrl.Text = _contact.Website;
            txtFacebook.Text = _contact.Facebook;
            txtTwitter.Text = _contact.Twitter;
            
            txtDescription.Text = _Site.Description;
            
            chkIsPublic.Checked = _Site.IsPublic;
            
        }
        private void LoadTypes()
        {
            foreach (Library.Objects.Auxiliaries.Types.SiteType _type in I.GetSiteTypes().Values)
            {
                ddlTypes.Items.Add(new ListItem(_type.Name, _type.IdSiteType.ToString()));
            }
        }
        private void LoadCurrencies()
        {
            Int64 _idCurrency = _Site.Currency.IdCurrency;

            foreach (Library.Objects.Auxiliaries.Units.Currency _currency in I.GetCurrencies().Values)
            {
                ListItem _item = new ListItem(_currency.Symbol + " [" + _currency.Name + "]", _currency.IdCurrency.ToString());
                if (_currency.IdCurrency == _idCurrency)
                    _item.Selected = true;

                ddlCurrencies.Items.Add(_item);
            }
        }
        private void LoadLanguages()
        {
            foreach (Library.Objects.Auxiliaries.Globalization.Language _language in I.GetLanguagesAvailable().Values)
            {
                if(!_language.IsDefault)
                    ddlDescriptionTranslations.Items.Add(new ListItem(_language.Name, _language.IdLanguage));
            }
        }
        private void LoadLanguageOptions()
        {
            String _optionsString = "";
            foreach (Library.Objects.Sites.SiteLanguageOption _option in _Site.LanguageOptions.Values)
            {
                if (_optionsString != "")
                    _optionsString += "°";
                _optionsString += _option.Language.IdLanguage + '|' + _option.Description;
            }
            hdnDescriptionTranslations.Value = _optionsString;
        }
        private void SaveData()
        {
            if (Page.IsValid)
            {
                try
                {
                    Library.Objects.Auxiliaries.Geographic.Country _country = I.GetCountry(hdnLocationCountry.Value);
                    if (_country == null)
                        throw new ApplicationException(Resources.Messages.ErrorCountryNotRecognized);

                    Int64 _units = 0;
                    Int64.TryParse(txtUnits.Text, out _units);

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

                        ((UserOperatorMeManager)MyLibrary.CurrentUser).ModifySite(_Site, Convert.ToInt64(ddlTypes.SelectedValue), Convert.ToDateTime(hdnStart.Value), 500, txtTitle.Text, txtNumber.Text, hdnLocationAddress.Value, new Library.Objects.Auxiliaries.Geographic.Position(hdnLocationPoint.Value), _country.IdCountry, Convert.ToDouble(txtValue.Text), Convert.ToInt64(ddlCurrencies.SelectedValue), Convert.ToDouble(txtFloorSpace.Text), _units, txtTelephone.Text, txtEmail.Text, txtUrl.Text, txtFacebook.Text, txtTwitter.Text, txtClient.Text, txtAgent.Text, txtContractor.Text, txtResponsible.Text, txtManager.Text, txtDescription.Text, WebUI.Common.GetTranslationStructure(hdnDescriptionTranslations.Value), chkIsPublic.Checked, _fileName, _fileType, _fileContent);

                    }
                    else
                        ((UserOperatorMeManager)MyLibrary.CurrentUser).ModifySite(_Site, Convert.ToInt64(ddlTypes.SelectedValue), Convert.ToDateTime(hdnStart.Value), 500, txtTitle.Text, txtNumber.Text, hdnLocationAddress.Value, new Library.Objects.Auxiliaries.Geographic.Position(hdnLocationPoint.Value), _country.IdCountry, Convert.ToDouble(txtValue.Text), Convert.ToInt64(ddlCurrencies.SelectedValue), Convert.ToDouble(txtFloorSpace.Text), _units, txtTelephone.Text, txtEmail.Text, txtUrl.Text, txtFacebook.Text, txtTwitter.Text, txtClient.Text, txtAgent.Text, txtContractor.Text, txtResponsible.Text, txtManager.Text, txtDescription.Text, WebUI.Common.GetTranslationStructure(hdnDescriptionTranslations.Value), chkIsPublic.Checked);

                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Dashboard,Request) + "Site.aspx?Site=" + _Site.IdSite.ToString(), false);
                    Context.ApplicationInstance.CompleteRequest();
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