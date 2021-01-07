using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard.Meters
{
    public partial class MeterElectricityEdit : BasePage
    {
        private Library.Objects.Sites.Meters.ElectricityMeter _Meter;
        private Library.Objects.Sites.SiteMineOpen _Site;

        protected void Page_Init(object sender, EventArgs e)
        {
            _Meter = I.GetElectricityMeter(Convert.ToInt64(Request.QueryString["Meter"].ToString()));
            _Site = (Library.Objects.Sites.SiteMineOpen)_Meter.Site;
            
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
                LoadMeter();
                LoadLanguages();
                LoadUnits();
                LoadFrequencyUnits();
                LoadLanguageOptions();
                LoadEFCountries();
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

            lblSiteProperties.Text = Resources.Data.Properties;
            lblMandatoryFieldsExplanation.Text = Resources.Data.MandatoryFieldExplanation;

            lblIdentification.Text = Resources.Data.Identification + _mandatoryField + _helpPostfix;
            lblDescription.Text = Resources.Data.Description + " [" + Resources.Data.InDefaultLanguage + "]" + _helpPostfix;
            lblInitialReading.Text = Resources.Data.InitialReading + _mandatoryField + _helpPostfix;
            lblInitialDate.Text = Resources.Data.InitialDate + _mandatoryField + _helpPostfix;
            lblEF.Text = Resources.Data.EF + _mandatoryField + _helpPostfix;
            lblEFDescription.Text = Resources.Data.EFDescription + " [" + Resources.Data.InDefaultLanguage + "]" + _helpPostfix;
            lblUnits.Text = Resources.Data.DefaultUnit + _mandatoryField + _helpPostfix;
            lblFrequencyQuantity.Text = Resources.Data.Frequency + _mandatoryField + _helpPostfix;
            lblFrequencyUnits.Text = Resources.Data.TimeUnit + _mandatoryField + _helpPostfix;
            lblAlertBefore.Text = Resources.Data.AlertBefore + _mandatoryField + _helpPostfix;
            lblAlertAfter.Text = Resources.Data.AlertAfter + _mandatoryField + _helpPostfix;
            lblAlertOnStart.Text = Resources.Data.AlertOnStart + _mandatoryField + _helpPostfix;

            btnEFSelector.AlternateText = Resources.Data.Select;
            btnDescriptionTranslations.Text = Resources.Data.Translations;
            btnDescriptionTranslationsSave.Text = Resources.Data.Save;
            btnEFDescriptionTranslations.AlternateText = Resources.Data.Translations;
            btnEFDescriptionTranslationsSave.Text = Resources.Data.Save;

            btnSave.Text = Resources.Data.Save;

            rfvIdentification.Text = rfvAlertAfter.Text = rfvAlertBefore.Text = rfvFrequencyQuantity.Text =
            rfvFrequencyUnits.Text = rfvIdentification.Text = cvEF.Text =
            cvFrequencyQuantity.Text = cvFrequencyUnits.Text = cvUnits.Text = rvAlertAfter.Text =
            rvAlertBefore.Text = cvInitialDate.Text = cvInitialReading.Text = Resources.Messages.SummaryErrorCharacter;
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
            String _idUnit = _Meter.DefaultUnit.IdUnit.ToString();
            ddlUnits.Items.Clear();

            foreach (Library.Objects.Auxiliaries.Units.Unit _unit in I.GetElectricityUnits().Values)
            {
                ListItem _item = new ListItem(_unit.Symbol + " [" + _unit.Name + "]", _unit.IdUnit.ToString());
                ddlUnits.Items.Add(_item);
                if (_idUnit == _item.Value)
                    _item.Selected = true;

            }
        }
        private void LoadFrequencyUnits()
        {
            String _idTimeUnit = Enum.GetName(typeof(Library.Objects.Auxiliaries.Units.TimeUnit.Units), _Meter.FrequencyUnit);
            ddlFrequencyUnits.Items.Clear();

            foreach (Library.Objects.Auxiliaries.Units.TimeUnit.Units _unit in Enum.GetValues(typeof(Library.Objects.Auxiliaries.Units.TimeUnit.Units)))
            {
                ListItem _item = new ListItem((String)GetGlobalResourceObject("Data", "TimeUnit" + _unit.ToString()), _unit.ToString());
                
                ddlFrequencyUnits.Items.Add(_item);
                if (_idTimeUnit == _item.Value)
                    _item.Selected = true;
            }
        }
        private void LoadLanguageOptions()
        {
            String _optionsString = "";
            String _optionsEFString = "";

            foreach (Library.Objects.Sites.Meters.ElectricityMeterLanguageOption _option in _Meter.LanguageOptions.Values)
            {
                //Description
                if (_optionsString != "")
                    _optionsString += "°";
                
                if(_option.Description.Trim()!="")
                    _optionsString += _option.Language.IdLanguage + '|' + _option.Description;
            }
            foreach (Library.Objects.Auxiliaries.EmissionFactors.EmissionFactorLanguageOption _option in _Meter.EmissionFactor.LanguageOptions.Values)
            {
                //EF Description
                if (_optionsEFString != "")
                    _optionsEFString += "°";

                if (_option.Description.Trim() != "")
                    _optionsEFString += _option.IdLanguage + '|' + _option.Description;
            }
            hdnDescriptionTranslations.Value = _optionsString;
            hdnEFDescriptionTranslations.Value = _optionsEFString;
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
        private void LoadMeter()
        {
            String _idDefaultLanguage = MyLibrary.DefaultLanguage.IdLanguage;

            txtIdentification.Text = _Meter.Identification;
            txtDescription.Text = _Meter.LanguageOptions[_idDefaultLanguage].Description;

            //txtEFDescription.Text = _Meter.EmissionFactor.LanguageOptions[_idDefaultLanguage].Description;

            
            if (_Meter is Library.Objects.Sites.Meters.ElectricityMeterPhysical)
            {
                Library.Objects.Sites.Meters.ElectricityMeterPhysical _castedMeter = (Library.Objects.Sites.Meters.ElectricityMeterPhysical)_Meter;
                if (_castedMeter.InitialReading != -1)
                {
                    calInitialDate.SelectedDate = _castedMeter.InitialDate;
                    txtInitialDate.Text = hdnInitialDate.Value = _castedMeter.InitialDate.ToShortDateString();
                    txtInitialReading.Text = _castedMeter.InitialReading.ToString();
                }
                divInitialReading.Style.Add("display", "block");
            }

            //hdnIdEF.Value = _Meter.EmissionFactor.IdEmissionFactor.ToString();
            //txtEF.Text = _Meter.EmissionFactor.Value.ToString();
            ddlUnits.SelectedValue = _Meter.DefaultUnit.IdUnit.ToString();
            if (_Meter.HasValue())
                ddlFrequencyUnits.Enabled = false;
            txtFrequencyQuantity.Text = _Meter.FrequencyQuantity.ToString();
            ddlFrequencyUnits.SelectedValue = _Meter.FrequencyUnit.ToString();
            txtAlertBefore.Text = _Meter.AlertBeforeInDays.ToString();
            txtAlertAfter.Text = _Meter.AlertAfterInDays.ToString();
            chkAlertOnStart.Checked = _Meter.AlertOnStart;

        }
        private void SaveData()
        {
            if (Page.IsValid)
            {
                try
                {
                    Library.Objects.Sites.Meters.Series.ElectricityDataEmissionFactor _EF = null;

                    //Emission Factor
                    Int64 _idEF = Convert.ToInt64(hdnIdEF.Value);
                    if (_idEF > 0)
                        _EF = new Library.Objects.Sites.Meters.Series.ElectricityDataEmissionFactor(_idEF, I);
                    else
                        if (txtEF.Text != "")
                            _EF = new Library.Objects.Sites.Meters.Series.ElectricityDataEmissionFactor(_Site.Country, Convert.ToDouble(txtEF.Text), txtEFDescription.Text, WebUI.Common.GetTranslationStructure(hdnEFDescriptionTranslations.Value), I);

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

                    if (_Meter is Library.Objects.Sites.Meters.ElectricityMeterPhysical)
                    {
                        if (_EF == null)
                            I.ModifyMeterElectricity((Library.Objects.Sites.Meters.ElectricityMeterPhysical)_Meter, txtIdentification.Text, txtDescription.Text, WebUI.Common.GetTranslationStructure(hdnDescriptionTranslations.Value), _initialDate, _initialReading, _unit, Convert.ToInt16(txtFrequencyQuantity.Text), _timeUnit, Convert.ToInt16(txtAlertBefore.Text), Convert.ToInt16(txtAlertAfter.Text), chkAlertOnStart.Checked);
                        else
                            I.ModifyMeterElectricity((Library.Objects.Sites.Meters.ElectricityMeterPhysical)_Meter, txtIdentification.Text, txtDescription.Text, WebUI.Common.GetTranslationStructure(hdnDescriptionTranslations.Value), _EF, _initialDate, _initialReading, _unit, Convert.ToInt16(txtFrequencyQuantity.Text), _timeUnit, Convert.ToInt16(txtAlertBefore.Text), Convert.ToInt16(txtAlertAfter.Text), chkAlertOnStart.Checked);
                    }
                    else
                    {
                        if (_EF == null)
                            I.ModifyMeterElectricity(_Meter, txtIdentification.Text, txtDescription.Text, WebUI.Common.GetTranslationStructure(hdnDescriptionTranslations.Value), _unit, Convert.ToInt16(txtFrequencyQuantity.Text), _timeUnit, Convert.ToInt16(txtAlertBefore.Text), Convert.ToInt16(txtAlertAfter.Text), chkAlertOnStart.Checked);
                        else
                            I.ModifyMeterElectricity(_Meter, txtIdentification.Text, txtDescription.Text, WebUI.Common.GetTranslationStructure(hdnDescriptionTranslations.Value), _EF, _unit, Convert.ToInt16(txtFrequencyQuantity.Text), _timeUnit, Convert.ToInt16(txtAlertBefore.Text), Convert.ToInt16(txtAlertAfter.Text), chkAlertOnStart.Checked);
                    }

                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterElectricity.aspx?Meter=" + _Meter.IdMeter.ToString());
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