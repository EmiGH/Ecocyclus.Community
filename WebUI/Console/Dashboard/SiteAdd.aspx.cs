using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;
using System.Web.Services;

namespace CSI.WebUI.Console.Dashboard
{
    public partial class SiteAdd : BasePage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
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
                LoadTexts();
                LoadTypes();
                LoadCurrencies();
                LoadLanguages();
            }
        }

        #region Methods

        private void BindControls()
        {
            btnSave.Click += new EventHandler(btnSave_Click);
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
            lblStart.Text = Resources.Data.Start + _mandatoryField+ _helpPostfix;
            lblWeeks.Text = Resources.Data.Weeks + _mandatoryField+ _helpPostfix;
            lblTitle.Text = Resources.Data.Title + _mandatoryField+ _helpPostfix;
            lblNumber.Text = Resources.Data.Number + _mandatoryField + _helpPostfix;
            lblValue.Text = Resources.Data.Value + _mandatoryField+ _helpPostfix;
            lblFloorSpace.Text = Resources.Data.FloorSpace + _mandatoryField+ _helpPostfix;
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

            btnDescriptionTranslations.Text = Resources.Data.Translations;
            btnDescriptionTranslationsSave.Text = Resources.Data.Save;

            calStart.Format = CurrentCultureInfo.DateTimeFormat.ShortDatePattern;
            
            //Validators
            rfvTitle.Text = rfvValue.Text = rfvWeeks.Text = rfvFloorSpace.Text =  rfvNumber.Text =
            revUrl.Text = revEmail.Text = cuvLocation.Text = cvWeeks.Text = cvTypes.Text =
            cvStart.Text = cvValue.Text = cvFloorSpace.Text = cvUnits.Text = Resources.Messages.SummaryErrorCharacter;
            
            btnSave.Text = Resources.Data.Save;

            //Map
            lblMapLocator.Text = Resources.Data.Location + Resources.Data.MandatoryField + _helpPostfix;
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
            foreach (Library.Objects.Auxiliaries.Units.Currency _currency in I.GetCurrencies().Values)
            {
                ddlCurrencies.Items.Add(new ListItem(_currency.Symbol + " [" + _currency.Name + "]", _currency.IdCurrency.ToString()));
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
        private void SaveData()
        {
            if (Page.IsValid)
            {
                try
                {
                    Library.Objects.Sites.Site _site;

                    Library.Objects.Auxiliaries.Geographic.Country _country = I.GetCountry(hdnLocationCountry.Value);
                    if (_country == null)
                        throw new ApplicationException(Resources.Messages.ErrorCountryNotRecognized);
                    
                    Int64 _units = 0;
                    Int64.TryParse(txtUnits.Text, out _units);
                    
                    //Site Image
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

                        _site = ((UserOperatorMeManager)I).AddSite(Convert.ToInt64(ddlTypes.SelectedValue), Convert.ToDateTime(hdnStart.Value), 500, txtTitle.Text, txtNumber.Text, hdnLocationAddress.Value, new Library.Objects.Auxiliaries.Geographic.Position(hdnLocationPoint.Value), _country.IdCountry, Convert.ToDouble(txtValue.Text), Convert.ToInt64(ddlCurrencies.SelectedValue), Convert.ToDouble(txtFloorSpace.Text), _units, txtTelephone.Text, txtEmail.Text, txtUrl.Text, txtFacebook.Text, txtTwitter.Text, txtClient.Text, txtAgent.Text, txtContractor.Text, txtResponsible.Text, txtManager.Text, txtDescription.Text, WebUI.Common.GetTranslationStructure(hdnDescriptionTranslations.Value), chkIsPublic.Checked, _fileName, _fileType, _fileContent);

                    }
                    else
                        _site = ((UserOperatorMeManager)I).AddSite(Convert.ToInt64(ddlTypes.SelectedValue), Convert.ToDateTime(hdnStart.Value), 500, txtTitle.Text, txtNumber.Text, hdnLocationAddress.Value, new Library.Objects.Auxiliaries.Geographic.Position(hdnLocationPoint.Value), _country.IdCountry, Convert.ToDouble(txtValue.Text), Convert.ToInt64(ddlCurrencies.SelectedValue), Convert.ToDouble(txtFloorSpace.Text), _units, txtTelephone.Text, txtEmail.Text, txtUrl.Text, txtFacebook.Text, txtTwitter.Text, txtClient.Text, txtAgent.Text, txtContractor.Text, txtResponsible.Text, txtManager.Text, txtDescription.Text, WebUI.Common.GetTranslationStructure(hdnDescriptionTranslations.Value), chkIsPublic.Checked);

                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Dashboard, Request) + "Site.aspx?Site=" + _site.IdSite.ToString(), false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception exception)
                {
                    String _error = Resources.Messages.StandardError;
                    _error = _error.Replace("[error]", exception.Message);
                    _error = _error.Replace("[mail]", Resources.Data.HelpDeskMailAddress);

                    ((Main)Page.Master).ErrorHandler.SetMessage(Resources.Data.Error,_error);
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