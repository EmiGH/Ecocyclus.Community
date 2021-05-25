using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard.Meters
{
    public partial class MeterWaterLoadEdit : BasePage
    {
        private Library.Objects.Sites.Meters.WaterMeter _Meter;
        private Library.Objects.Sites.Meters.Series.WaterLoad _Load;

        protected void Page_Init(object sender, EventArgs e)
        {
            _Meter = I.GetWaterMeter(Convert.ToInt64(Request.QueryString["Meter"]));

            //Permissions
            Library.Security.Authority.PermissionTypes _permission = ((Library.Objects.Sites.SiteMine)_Meter.Site).CurrentPermission();
            if (_permission != Library.Security.Authority.PermissionTypes.SiteManager && _permission != Library.Security.Authority.PermissionTypes.SiteOperator)
            {
                throw new ApplicationException(Resources.Messages.AccessDenied);
            }
            
            _Load = _Meter.GetLoad(Convert.ToInt64(Request.QueryString["Load"]));

            BindControls();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMenu();
                LoadUnits();
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
            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)_Meter.Site;
            ((Main)Page.Master).MenuNavigation.Initialize(_site.IdSite, _site.Title, _Meter.IdMeter, _Meter.Identification, Console.Controls.ucMenuNavigation.MeterType.Water, Console.Controls.ucMenuNavigation.MenuItem.None, WebUI.Common.GetPermissionFromContext(I,_site));
         
        }
        private void LoadUnits()
        {
            Int64 _loadUnit = _Load.Unit.IdUnit;

            ddlLoadUnits.Items.Clear();
            foreach (Library.Objects.Auxiliaries.Units.Unit _unit in I.GetWaterUnits().Values)
            {
                ListItem _item = new ListItem(_unit.Symbol + " [" + _unit.Name + "]", _unit.IdUnit.ToString());
                if (_unit.IdUnit == _loadUnit) _item.Selected = true;

                ddlLoadUnits.Items.Add(_item);
            }
        }
        private void LoadTexts()
        {
            String _helpPostfix = Resources.Data.HelpPostfix;
            String _mandatoryField = Resources.Data.MandatoryField;

            lblSiteProperties.Text = Resources.Data.Properties;
            lblMandatoryFieldsExplanation.Text = Resources.Data.MandatoryFieldExplanation;

            lblIdentification.Text = Resources.Data.Identification;
            lblDescription.Text = Resources.Data.Description;
            lblIsPhysical.Text = Resources.Data.IsPhysical;
            if (_Meter is Library.Objects.Sites.Meters.WaterMeterPhysical)
            {
                divInitialReading.Style.Add("display", "block");
                lblInitialDate.Text = Resources.Data.InitialDate; 
                lblInitialReading.Text = Resources.Data.InitialReading;
                lblLoadValue.Text = Resources.Data.Reading + _mandatoryField + _helpPostfix;
            }
            else
                lblLoadValue.Text = Resources.Data.Value + _mandatoryField + _helpPostfix;

            lblEF.Text = Resources.Data.EF;
            lblUnit.Text = Resources.Data.DefaultUnit;
            lblFrequencyQuantity.Text = Resources.Data.Frequency;
            lblFrequencyUnits.Text = Resources.Data.TimeUnit;
            lblAlertBefore.Text = Resources.Data.AlertBefore;
            lblAlertAfter.Text = Resources.Data.AlertAfter;
            lblAlertOnStart.Text = Resources.Data.AlertOnStart;

            lblLoadEdit.Text = Resources.Data.Load;
            lblLoadFrom.Text = Resources.Data.From;
            lblLoadTo.Text = Resources.Data.To;
            lblLoadUnit.Text = Resources.Data.Unit + _mandatoryField + _helpPostfix;
            lblLoadLastOperator.Text = Resources.Data.Operator;

            btnSave.Text = Resources.Data.Save;

            rfvLoadValue.Text = cvLoadUnit.Text = Resources.Messages.SummaryErrorCharacter;
        }
        private void LoadData()
        {
            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)_Meter.Site;

            String _validRangeLabel = Resources.Data.ValidLoadRangeExplanation;
            _validRangeLabel = _validRangeLabel.Replace("[from]", _site.LoadTimeRange.Start.ToShortDateString());
            _validRangeLabel = _validRangeLabel.Replace("[to]", _site.LoadTimeRange.End.ToShortDateString());
            lblValidLoadRange.Text = _validRangeLabel; 
            
            lblIdentificationValue.Text = _Meter.Identification;
            lblDescriptionValue.Text = _Meter.LanguageOption.Description;
            if (_Meter is Library.Objects.Sites.Meters.WaterMeterPhysical)
                lblInitialReadingValue.Text = ((Library.Objects.Sites.Meters.WaterMeterPhysical)_Meter).InitialReading.ToString();
            
            lblIsPhysicalValue.Text = WebUI.Common.GetBooleanTranslation(_Meter is Library.Objects.Sites.Meters.WaterMeterPhysical);

            Library.Objects.Auxiliaries.EmissionFactors.EmissionFactor _ef = _Meter.EmissionFactor;
            lblEFValue.Text = _ef.Value.ToString() + " " + Resources.Data.CO2Unit + "/" + _ef.Unit.Symbol;
            
            lblUnitValue.Text = _Meter.DefaultUnit.Name;
            lblFrequencyQuantityValue.Text = _Meter.FrequencyQuantity.ToString();
            lblFrequencyUnitsValue.Text = Enum.GetName(typeof(Library.Objects.Auxiliaries.Units.TimeUnit.Units), _Meter.FrequencyUnit);
            lblAlertBeforeValue.Text = _Meter.AlertBeforeInDays.ToString();
            lblAlertAfterValue.Text = _Meter.AlertAfterInDays.ToString();
            lblAlertOnStartValue.Text = WebUI.Common.GetBooleanTranslation(_Meter.AlertOnStart);

            lblLoadFromValue.Text = _Load.From.ToShortDateString();
            lblLoadToValue.Text = _Load.To.ToShortDateString();
            lblLoadLastOperatorValue.Text = _Load.Operator.Fullname;
            txtLoadValue.Text = _Load.ValueInput.ToString();
            ddlLoadUnits.SelectedValue = _Load.Unit.IdUnit.ToString();

        }
        private void SaveData()
        {
            if (Page.IsValid)
            {
                try
                {
                    Library.Objects.Auxiliaries.Units.Unit _unit = I.GetUnit(Convert.ToInt64(ddlLoadUnits.SelectedValue));

                    I.ModifyWaterData(_Meter, _Load, Convert.ToDouble(txtLoadValue.Text), _unit);

                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterWaterLoads.aspx?Meter=" + _Meter.IdMeter.ToString(), false);
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