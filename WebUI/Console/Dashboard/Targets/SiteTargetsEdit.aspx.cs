using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard
{
    public partial class SiteTargetsEdit : BasePage
    {
        private Library.Objects.Sites.SiteMineOpen _Site;
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

        protected void Page_Init(object sender, EventArgs e)
        {
            _Site = (Library.Objects.Sites.SiteMineOpen)I.GetSite(Convert.ToInt64(Request.QueryString["Site"]));
            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //Permissions
            if (_Site.CurrentPermission() != Library.Security.Authority.PermissionTypes.SiteManager)
            {
                throw new ApplicationException(Resources.Messages.AccessDenied);
            }

            if (!IsPostBack)
            {
                SetMenu();
                LoadTexts();
                LoadSite();
                LoadUnits(); 
                LoadTargets();
                
            }
        }

        #region Private Methods

        private void BindControls()
        {
            btnEdit.Click += new EventHandler(btnEdit_Click);
        }        
        private void SetMenu()
        {
            ((Main)Page.Master).MenuNavigation.Initialize(_Site.IdSite, _Site.Title, Console.Controls.ucMenuNavigation.MenuItem.Target, WebUI.Common.GetPermissionFromContext(I,_Site));
        }
        private void LoadTexts()
        {
            lblSiteProperties.Text = Resources.Data.Properties;

            lblTitle.Text = Resources.Data.Title;
            lblType.Text = Resources.Data.SiteType;
            lblLoadStatus.Text = Resources.Data.SiteLoadStatus;
            lblLiveStatus.Text = Resources.Data.SiteLiveStatus;
            lblLocation.Text = Resources.Data.Location;
            lblStart.Text = Resources.Data.Start;
            lblWeeks.Text = Resources.Data.Weeks;
            lblNumber.Text = Resources.Data.Number;
            lblValue.Text = Resources.Data.Value;
            lblFloorSpace.Text = Resources.Data.FloorSpace;
            lblUnits.Text = Resources.Data.Units;

            lblHeaderElectricity.Text = Resources.Data.Electricity;
            lblHeaderFuel.Text = Resources.Data.Fuels;
            lblHeaderTransport.Text = Resources.Data.Transport;
            lblHeaderWaste.Text = Resources.Data.Waste;
            lblHeaderWater.Text = Resources.Data.Water;

            String _currencySymbol = _Site.Cost.Currency.Symbol;
            lblConsumption.Text = Resources.Data.ConsumptionMonthlyLimit;
            lblConsumptionByMoney.Text = Resources.Data.ConsumptionMonthlyLimitByMoney + " [unit/" + _currencySymbol + "]";
            lblConsumptionByMts.Text = Resources.Data.ConsumptionMonthlyLimitByMts + "[unit/m2]";
            lblCO2.Text = Resources.Data.CO2GenerationLimit + "[" + Resources.Data.CO2Unit + "]";
            lblCO2ByMoney.Text = Resources.Data.CO2GenerationLimitByMoney + " [" + Resources.Data.CO2Unit + "/" + _currencySymbol + "]"; ;
            lblCO2ByMts.Text = Resources.Data.CO2GenerationLimitByMts + "[" + Resources.Data.CO2Unit + "/" + "/m2]";
            lblConsumptionNewValues.Text = lblCO2NewValues.Text = Resources.Data.TargetsNewValues;


            rgvElectricityCO2.Text = cvElectricityConsumption.Text = rgvFuelCO2.Text = cvFuelConsumption.Text =
            rgvTransportCO2.Text = cvTransportConsumption.Text = rgvWasteCO2.Text = cvWasteConsumption.Text =
            rgvWaterCO2.Text = cvWaterConsumption.Text = rgvTotalCO2.Text = Resources.Messages.SummaryErrorCharacter;

            btnEdit.Text = Resources.Data.Save;


        }
        private void LoadSite()
        {
            Library.Objects.Auxiliaries.Geographic.Contact _contact = _Site.Contact;
            Location = _contact.Location.Position.Coordenates;

            lblTitleValue.Text = _Site.Title;
            lblLoadStatusValue.Text = (_Site is Library.Objects.Sites.SiteMineOpen ? Resources.Data.SiteOpened : Resources.Data.SiteClosed);
            lblLiveStatusValue.Text = (_Site.IsFinished ? Resources.Data.SiteFinished : Resources.Data.SiteLive);
            lblLocationValue.Text = _contact.Location.Address;
            lblStartValue.Text = _Site.Start.ToShortDateString();
            lblWeeksValue.Text = _Site.Weeks.ToString();
            lblNumberValue.Text = _Site.Number;
            lblValueValue.Text = _Site.Currency.Symbol + " " + _Site.Value.ToString() + " [" + _Site.Currency.Name + "]";
            lblFloorSpaceValue.Text = _Site.FloorSpace.ToString();
            lblUnitsValue.Text = _Site.Units.ToString();
        }
        private void LoadTargets()
        {
            Library.Objects.Sites.Targets _target = _Site.Targets;
            Library.Objects.Auxiliaries.Units.Cost _cost = _Site.Cost;
            Double _mts = _Site.FloorSpace;

            lblElectricityConsumption.Text = txtElectricityConsumption.Text = _target.ElectricityConsumption.ToString("N2");
            lblElectricityCO2.Text = txtElectricityCO2.Text = _target.ElectricityCO2.ToString("N2");
            lblElectricityConsumptionByMoney.Text = ((Double)(_cost.Value > 0 ? _target.ElectricityConsumption / _cost.Value : 0)).ToString("N2");
            lblElectricityConsumptionByMts.Text = ((Double)(_mts > 0 ? _target.ElectricityConsumption / _mts : 0)).ToString("N2");
            lblElectricityCO2ByMoney.Text = ((Double)(_cost.Value > 0 ? _target.ElectricityCO2 / _cost.Value : 0)).ToString("N2");
            lblElectricityCO2ByMts.Text = ((Double)(_mts > 0 ? _target.ElectricityCO2 / _mts : 0)).ToString("N2");
            ddlElectricityUnit.SelectedValue = _target.ElectricityUnit.IdUnit.ToString();

            lblFuelConsumption.Text = txtFuelConsumption.Text = _target.FuelConsumption.ToString("N2");
            lblFuelCO2.Text = txtFuelCO2.Text = _target.FuelCO2.ToString("N2");
            lblFuelConsumptionByMoney.Text = ((Double)(_cost.Value > 0 ? _target.FuelConsumption / _cost.Value : 0)).ToString("N2");
            lblFuelConsumptionByMts.Text = ((Double)(_mts > 0 ? _target.FuelConsumption / _mts : 0)).ToString("N2");
            lblFuelCO2ByMoney.Text = ((Double)(_cost.Value > 0 ? _target.FuelCO2 / _cost.Value : 0)).ToString("N2");
            lblFuelCO2ByMts.Text = ((Double)(_mts > 0 ? _target.FuelCO2 / _mts : 0)).ToString("N2");
            ddlFuelUnit.SelectedValue = _target.FuelUnit.IdUnit.ToString();

            lblTransportConsumption.Text = txtTransportConsumption.Text = _target.TransportConsumption.ToString("N2");
            lblTransportCO2.Text = txtTransportCO2.Text = _target.TransportCO2.ToString("N2");
            lblTransportConsumptionByMoney.Text = ((Double)(_cost.Value > 0 ? _target.TransportConsumption / _cost.Value : 0)).ToString("N2");
            lblTransportConsumptionByMts.Text = ((Double)(_mts > 0 ? _target.TransportConsumption / _mts : 0)).ToString("N2");
            lblTransportCO2ByMoney.Text = ((Double)(_cost.Value > 0 ? _target.TransportCO2 / _cost.Value : 0)).ToString("N2");
            lblTransportCO2ByMts.Text = ((Double)(_mts > 0 ? _target.TransportCO2 / _mts : 0)).ToString("N2");
            ddlTransportUnit.SelectedValue = _target.TransportUnit.IdUnit.ToString();

            lblWasteConsumption.Text = txtWasteConsumption.Text = _target.WasteConsumption.ToString("N2");
            lblWasteCO2.Text = txtWasteCO2.Text = _target.WasteCO2.ToString("N2");
            lblWasteConsumptionByMoney.Text = ((Double)(_cost.Value > 0 ? _target.WasteConsumption / _cost.Value : 0)).ToString("N2");
            lblWasteConsumptionByMts.Text = ((Double)(_mts > 0 ? _target.WasteConsumption / _mts : 0)).ToString("N2");
            lblWasteCO2ByMoney.Text = ((Double)(_cost.Value > 0 ? _target.WasteCO2 / _cost.Value : 0)).ToString("N2");
            lblWasteCO2ByMts.Text = ((Double)(_mts > 0 ? _target.WasteCO2 / _mts : 0)).ToString("N2");
            ddlWasteUnit.SelectedValue = _target.WasteUnit.IdUnit.ToString();

            lblWaterConsumption.Text = txtWaterConsumption.Text = _target.WaterConsumption.ToString("N2");
            lblWaterCO2.Text = txtWaterCO2.Text = _target.WaterCO2.ToString("N2");
            lblWaterConsumptionByMoney.Text = ((Double)(_cost.Value > 0 ? _target.WaterConsumption / _cost.Value : 0)).ToString("N2");
            lblWaterConsumptionByMts.Text = ((Double)(_mts > 0 ? _target.WaterConsumption / _mts : 0)).ToString("N2");
            lblWaterCO2ByMoney.Text = ((Double)(_cost.Value > 0 ? _target.WaterCO2 / _cost.Value : 0)).ToString("N2");
            lblWaterCO2ByMts.Text = ((Double)(_mts > 0 ? _target.WaterCO2 / _mts : 0)).ToString("N2");
            ddlWaterUnit.SelectedValue = _target.WaterUnit.IdUnit.ToString();

            lblTotalCO2.Text = txtTotalCO2.Text = _target.TotalCO2.ToString("N2");
            lblTotalCO2ByMoney.Text = ((Double)(_cost.Value > 0 ? _target.TotalCO2 / _cost.Value : 0)).ToString("N2");
            lblTotalCO2ByMts.Text = ((Double)(_mts > 0 ? _target.TotalCO2 / _mts : 0)).ToString("N2");
        }
        private void LoadUnits()
        {
            LoadElectricityUnits();
            LoadFuelUnits();
            LoadTransportUnits();
            LoadWasteUnits();
            LoadWaterUnits();
        }
        private void LoadElectricityUnits()
        {
            foreach (Library.Objects.Auxiliaries.Units.Unit _unit in I.GetElectricityUnits().Values)
                ddlElectricityUnit.Items.Add(new ListItem(_unit.Symbol, _unit.IdUnit.ToString()));
        }
        private void LoadFuelUnits()
        {
            foreach (Library.Objects.Auxiliaries.Units.Unit _unit in I.GetFuelUnits().Values)
                ddlFuelUnit.Items.Add(new ListItem(_unit.Symbol, _unit.IdUnit.ToString()));
        }
        private void LoadTransportUnits()
        {
            foreach (Library.Objects.Auxiliaries.Units.Unit _unit in I.GetTransportUnits().Values)
                ddlTransportUnit.Items.Add(new ListItem(_unit.Symbol, _unit.IdUnit.ToString()));
        }
        private void LoadWasteUnits()
        {
            foreach (Library.Objects.Auxiliaries.Units.Unit _unit in I.GetWasteUnits().Values)
                ddlWasteUnit.Items.Add(new ListItem(_unit.Symbol, _unit.IdUnit.ToString()));
        }
        private void LoadWaterUnits()
        {
            foreach (Library.Objects.Auxiliaries.Units.Unit _unit in I.GetWaterUnits().Values)
                ddlWaterUnit.Items.Add(new ListItem(_unit.Symbol, _unit.IdUnit.ToString()));
        }


        #endregion

        #region Page Events

        void btnEdit_Click(object sender, EventArgs e)
        {
            if(IsValid)
            {
                try
                {
                    ((UserOperatorMeManager)I).ModifyTargets(_Site, Convert.ToDouble(txtElectricityConsumption.Text), Convert.ToInt64(ddlElectricityUnit.SelectedValue), Convert.ToDouble(txtElectricityCO2.Text), Convert.ToDouble(txtFuelConsumption.Text), Convert.ToInt64(ddlFuelUnit.SelectedValue), Convert.ToDouble(txtFuelCO2.Text), Convert.ToDouble(txtTransportConsumption.Text), Convert.ToInt64(ddlTransportUnit.SelectedValue), Convert.ToDouble(txtTransportCO2.Text), Convert.ToDouble(txtWasteConsumption.Text), Convert.ToInt64(ddlWasteUnit.SelectedValue), Convert.ToDouble(txtWasteCO2.Text), Convert.ToDouble(txtWaterConsumption.Text), Convert.ToInt64(ddlWaterUnit.SelectedValue), Convert.ToDouble(txtWaterCO2.Text), Convert.ToDouble(txtTotalCO2.Text));

                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Targets, Request) + "SiteTargets.aspx?Site=" + _Site.IdSite.ToString(), false);
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
    }
}