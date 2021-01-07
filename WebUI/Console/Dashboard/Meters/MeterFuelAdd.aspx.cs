using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard.Meters
{
    public partial class FuelMeterAdd : BasePage
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
                LoadLanguages();
                LoadTexts();
                LoadUnits();
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
        private void LoadLanguages()
        {
            foreach (Library.Objects.Auxiliaries.Globalization.Language _language in I.GetLanguagesAvailable().Values)
            {
                if(!_language.IsDefault)
                    ddlDescriptionTranslations.Items.Add(new ListItem(_language.Name, _language.IdLanguage));
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
            lblUnits.Text = Resources.Data.DefaultUnit + _mandatoryField + _helpPostfix;

            btnDescriptionTranslations.Text = Resources.Data.Translations;
            btnDescriptionTranslationsSave.Text = Resources.Data.Save;
            btnSave.Text = Resources.Data.Save;

            rfvIdentification.Text = Resources.Messages.SummaryErrorCharacter;
        }
        private void LoadUnits()
        {
            ddlUnits.Items.Clear();
            foreach (Library.Objects.Auxiliaries.Units.Unit _unit in I.GetFuelUnits().Values)
            {
                ddlUnits.Items.Add(new ListItem(_unit.Symbol + " [" + _unit.Name + "]", _unit.IdUnit.ToString()));
            }
        }
        private void SaveData()
        {
            if (Page.IsValid)
            {
                try
                {
                    //Units
                    Library.Objects.Auxiliaries.Units.Unit _unit = I.GetUnit(Convert.ToInt16(ddlUnits.SelectedValue));
                   
                    Library.Objects.Sites.Meters.FuelMeter _meter = I.AddMeterFuel(_Site, txtIdentification.Text, txtDescription.Text, WebUI.Common.GetTranslationStructure(hdnDescriptionTranslations.Value), _unit);

                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterFuel.aspx?Meter=" + _meter.IdMeter.ToString());
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