using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard.Meters
{
    public partial class MeterElectricityAdd : BasePage
    {
        private Library.Objects.Sites.SiteMineOpen _Site;

        protected void Page_Init(object sender, EventArgs e)
        {

            _Site = (Library.Objects.Sites.SiteMineOpen)I.GetSite(Convert.ToInt64(Request.QueryString["Site"]));

            //Permissions
            if (_Site.CurrentPermission() != Library.Security.Authority.PermissionTypes.SiteManager)
            {
                throw new ApplicationException(Resources.Messages.AccessDenied);
            }
            
            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMenu();
                LoadTexts();
                LoadLanguages();
                LoadFrequencyUnits();
                LoadUnits();
                LoadEFCountries();
            }
        }

        #region Methods

        private void BindControls()
        {
            ddlEFSelectorCountries.SelectedIndexChanged += new EventHandler(ddlEFSelectorCountries_SelectedIndexChanged);
            chkIsPhysical.Attributes.Add("onclick", "toggleInitialReading();");
            lstEFSelector.Attributes.Add("onclick", "selectEF();return false;");
            btnSave.Click += new EventHandler(btnSave_Click);
        }
        private void SetMenu()
        {
            ((Main)Page.Master).MenuNavigation.Initialize(_Site.IdSite, _Site.Title, Console.Controls.ucMenuNavigation.MenuItem.None, WebUI.Common.GetPermissionFromContext(I,_Site));
           
        }
        private void LoadLanguages()
        {
            ddlDescriptionTranslations.Items.Clear();
            ddlEFDescriptionTranslations.Items.Clear();

            foreach (Library.Objects.Auxiliaries.Globalization.Language _language in I.GetLanguagesAvailable().Values)
            {
                if (!_language.IsDefault)
                {
                    ddlDescriptionTranslations.Items.Add(new ListItem(_language.Name, _language.IdLanguage));
                    ddlEFDescriptionTranslations.Items.Add(new ListItem(_language.Name, _language.IdLanguage));
                }
            }
        }
        private void LoadUnits()
        {
            ddlUnits.Items.Clear();
            foreach (Library.Objects.Auxiliaries.Units.Unit _unit in I.GetElectricityUnits().Values)
            {
                ddlUnits.Items.Add(new ListItem(_unit.Symbol + " [" + _unit.Name + "]", _unit.IdUnit.ToString()));
            }
        }
        private void LoadFrequencyUnits()
        {
            ddlFrequencyUnits.Items.Clear();
            foreach (Library.Objects.Auxiliaries.Units.TimeUnit.Units _unit in Enum.GetValues(typeof(Library.Objects.Auxiliaries.Units.TimeUnit.Units)))
            {
                ddlFrequencyUnits.Items.Add(new ListItem((String)GetGlobalResourceObject("Data", "TimeUnit"+_unit.ToString()), _unit.ToString()));
            }
        }
        private void LoadTexts()
        {
            String _helpPostfix = Resources.Data.HelpPostfix;
            String _mandatoryField = Resources.Data.MandatoryField;

            lblSiteProperties.Text = Resources.Data.Properties;
            lblMandatoryFieldsExplanation.Text = Resources.Data.MandatoryFieldExplanation;

            lblIdentification.Text = Resources.Data.Identification + _mandatoryField + _helpPostfix;
            lblDescription.Text = Resources.Data.Description + " [" + Resources.Data.InDefaultLanguage + "]" + _helpPostfix;
            lblIsPhysical.Text = Resources.Data.IsPhysical + _helpPostfix;
            lblInitialReading.Text = Resources.Data.InitialReading + _helpPostfix;
            lblInitialDate.Text = Resources.Data.InitialDate + _helpPostfix;

            lblEF.Text = Resources.Data.EF + _helpPostfix;
            lblEFDescription.Text = Resources.Data.EFDescription + " [" + Resources.Data.InDefaultLanguage + "]" + _helpPostfix;
            lblUnits.Text = Resources.Data.DefaultUnit + _mandatoryField + _helpPostfix;
            lblFrequencyQuantity.Text = Resources.Data.Frequency + _mandatoryField + _helpPostfix;
            lblFrequencyUnits.Text = Resources.Data.TimeUnit + _mandatoryField + _helpPostfix;
            lblAlertBefore.Text = Resources.Data.AlertBefore + _mandatoryField + _helpPostfix;
            lblAlertAfter.Text = Resources.Data.AlertAfter + _mandatoryField + _helpPostfix;
            lblAlertOnStart.Text = Resources.Data.AlertOnStart + _mandatoryField + _helpPostfix;
            lblInitialDate.Text = Resources.Data.InitialDate + _mandatoryField + _helpPostfix;

            btnEFSelector.AlternateText = Resources.Data.Select;
            btnDescriptionTranslations.Text = Resources.Data.Translations;
            btnDescriptionTranslationsSave.Text = Resources.Data.Save;
            btnEFDescriptionTranslations.AlternateText = Resources.Data.Translations;
            btnEFDescriptionTranslationsSave.Text = Resources.Data.Save;

            btnSave.Text = Resources.Data.Save;

            rfvIdentification.Text = rfvAlertAfter.Text = rfvAlertBefore.Text = rfvFrequencyQuantity.Text =
            rfvFrequencyUnits.Text = rfvIdentification.Text =  cvEF.Text =
            cvFrequencyQuantity.Text = cvFrequencyUnits.Text = cvUnits.Text = rvAlertAfter.Text =
            rvAlertBefore.Text = cvInitialDate.Text = cvInitialReading.Text  = Resources.Messages.SummaryErrorCharacter;
        }
        private void LoadEFCountries()
        {
            Int64 _idCountry = _Site.Country.IdCountry;
            ddlEFSelectorCountries.Items.Clear();
            foreach (Library.Objects.Auxiliaries.Geographic.Country _country in Library.Objects.Auxiliaries.EmissionFactors.EmissionFactor.GetCountriesForElectricity(I).Values)
            {
                    ListItem _item = new ListItem(_country.LanguageOption.Name, _country.IdCountry.ToString());
                    if (_idCountry == _country.IdCountry)
                        _item.Selected = true;
                    ddlEFSelectorCountries.Items.Add(_item);
            }
            LoadEFs();
        }
        private void LoadEFs()
        {
            if (ddlEFSelectorCountries.SelectedValue != "")
            {
                String _idLanguage = MyLibrary.CurrentLanguage.IdLanguage;
                Library.Objects.Auxiliaries.Geographic.Country _country = I.GetCountry(Convert.ToInt64(ddlEFSelectorCountries.SelectedValue));

                lstEFSelector.Items.Clear();
                foreach (Library.Objects.Auxiliaries.EmissionFactors.EmissionFactor _ef in _country.GetEmissionFactorsForElectricity().Values)
                {
                    ListItem _item = new ListItem(_ef.LanguageOption.Description, _ef.Value.ToString());
                    lstEFSelector.Items.Add(_item);
                }
            }
        }

        private void SaveData()
        {
            if (Page.IsValid)
            {
                try
                {
                    Library.Objects.Sites.Meters.ElectricityMeter _meter;

                    //Emission Factor
                    Library.Objects.Sites.Meters.Series.ElectricityDataEmissionFactor _EF = null;

                    Int64 _idEF = Convert.ToInt64(hdnIdEF.Value);
                    if(_idEF>0)
                        _EF = new Library.Objects.Sites.Meters.Series.ElectricityDataEmissionFactor(_idEF, I);
                    else
                        if(txtEF.Text !="")
                            _EF = new Library.Objects.Sites.Meters.Series.ElectricityDataEmissionFactor(_Site.Country, Convert.ToDouble(txtEF.Text), txtEFDescription.Text, WebUI.Common.GetTranslationStructure(hdnEFDescriptionTranslations.Value), I);
                    
                    if(_EF == null)
                        throw new ApplicationException(Resources.Messages.ErrorNoEFforCountry);

                    //Units
                    Library.Objects.Auxiliaries.Units.Unit _unit = I.GetUnit(Convert.ToInt16(ddlUnits.SelectedValue));
                    Library.Objects.Auxiliaries.Units.TimeUnit.Units _timeUnit = (Library.Objects.Auxiliaries.Units.TimeUnit.Units)Enum.Parse(typeof(Library.Objects.Auxiliaries.Units.TimeUnit.Units), ddlFrequencyUnits.Text);

                    //Initial Date
                    DateTime _initialDate;
                    if (!DateTime.TryParse(hdnInitialDate.Value, out _initialDate))
                        _initialDate = DateTime.MinValue;

                    //Initial Reading
                    Double _initialReading;
                    if (!Double.TryParse(txtInitialReading.Text, out _initialReading))
                        _initialReading = 0;

                    if(_EF != null)
                        _meter =  I.AddMeterElectricity(_Site, txtIdentification.Text, txtDescription.Text, WebUI.Common.GetTranslationStructure(hdnDescriptionTranslations.Value), _EF, chkIsPhysical.Checked, _initialDate, _initialReading, _unit, Convert.ToInt16(txtFrequencyQuantity.Text), _timeUnit, Convert.ToInt16(txtAlertBefore.Text), Convert.ToInt16(txtAlertAfter.Text), chkAlertOnStart.Checked);
                    else
                        _meter = I.AddMeterElectricity(_Site, txtIdentification.Text, txtDescription.Text, WebUI.Common.GetTranslationStructure(hdnDescriptionTranslations.Value), chkIsPhysical.Checked, _initialDate, _initialReading, _unit, Convert.ToInt16(txtFrequencyQuantity.Text), _timeUnit, Convert.ToInt16(txtAlertBefore.Text), Convert.ToInt16(txtAlertAfter.Text), chkAlertOnStart.Checked);

                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterElectricity.aspx?Meter=" + _meter.IdMeter.ToString());
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

        void ddlEFSelectorCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEFs();
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        #endregion
    }
}