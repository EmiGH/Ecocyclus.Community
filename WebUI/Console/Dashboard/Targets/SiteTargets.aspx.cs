using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard
{
    public partial class SiteTargets : BasePage
    {
        private Library.Objects.Sites.SiteMine _Site;
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
            _Site = (Library.Objects.Sites.SiteMine)I.GetSite(Convert.ToInt64(Request.QueryString["Site"]));
            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMenu();
                LoadTexts();
                LoadSite();
                LoadTargets();
            }
        }

        #region Private Methods

        private void BindControls()
        {
        }
        private void SetMenu()
        {
            ((Main)Page.Master).MenuNavigation.Initialize(_Site.IdSite, _Site.Title, Console.Controls.ucMenuNavigation.MenuItem.Target, WebUI.Common.GetPermissionFromContext(I,_Site));
            
            if (_Site is Library.Objects.Sites.SiteMineOpen && (I is UserOperatorMeManager || _Site.CurrentPermission() == Library.Security.Authority.PermissionTypes.SiteManager))
            {
                lnkEdit.ToolTip = Resources.Data.TargetsModify;
                lnkEdit.PostBackUrl = "SiteTargetsEdit.aspx?Site=" + _Site.IdSite.ToString();
                lnkEdit.Visible = true;
            }
            else
            {
                lnkEdit.Visible = false;
            }
            
        }
        private void LoadTexts()
        {
            //Titles
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
            
            lblHeaderTarget.Text = Resources.Data.Targets;
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

            lblElectricityConsumption.Text = _target.ElectricityConsumption.ToString("N2") + " " + _target.ElectricityUnit.Symbol;
            lblElectricityCO2.Text = _target.ElectricityCO2.ToString("N2");
            lblElectricityConsumptionByMoney.Text = ((Double)(_cost.Value > 0 ? _target.ElectricityConsumption / _cost.Value : 0)).ToString("N2");
            lblElectricityConsumptionByMts.Text = ((Double)(_mts > 0 ? _target.ElectricityConsumption / _mts : 0)).ToString("N2");
            lblElectricityCO2ByMoney.Text = ((Double)(_cost.Value > 0 ? _target.ElectricityCO2 / _cost.Value : 0)).ToString("N2");
            lblElectricityCO2ByMts.Text = ((Double)(_mts > 0 ? _target.ElectricityCO2 / _mts : 0)).ToString("N2");

            lblFuelConsumption.Text = _target.FuelConsumption.ToString("N2") + " " + _target.FuelUnit.Symbol;
            lblFuelCO2.Text = _target.FuelCO2.ToString("N2");
            lblFuelConsumptionByMoney.Text = ((Double)(_cost.Value > 0 ? _target.FuelConsumption / _cost.Value : 0)).ToString("N2");
            lblFuelConsumptionByMts.Text = ((Double)(_mts > 0 ? _target.FuelConsumption / _mts : 0)).ToString("N2");
            lblFuelCO2ByMoney.Text = ((Double)(_cost.Value > 0 ? _target.FuelCO2 / _cost.Value : 0)).ToString("N2");
            lblFuelCO2ByMts.Text = ((Double)(_mts > 0 ? _target.FuelCO2 / _mts : 0)).ToString("N2");

            lblTransportConsumption.Text = _target.TransportConsumption.ToString("N2") + " " + _target.TransportUnit.Symbol;
            lblTransportCO2.Text = _target.TransportCO2.ToString("N2");
            lblTransportConsumptionByMoney.Text = ((Double)(_cost.Value > 0 ? _target.TransportConsumption / _cost.Value : 0)).ToString("N2");
            lblTransportConsumptionByMts.Text = ((Double)(_mts > 0 ? _target.TransportConsumption / _mts : 0)).ToString("N2");
            lblTransportCO2ByMoney.Text = ((Double)(_cost.Value > 0 ? _target.TransportCO2 / _cost.Value : 0)).ToString("N2");
            lblTransportCO2ByMts.Text = ((Double)(_mts > 0 ? _target.TransportCO2 / _mts : 0)).ToString("N2");

            lblWasteConsumption.Text = _target.WasteConsumption.ToString("N2") + " " + _target.WasteUnit.Symbol;
            lblWasteCO2.Text = _target.WasteCO2.ToString("N2");
            lblWasteConsumptionByMoney.Text = ((Double)(_cost.Value > 0 ? _target.WasteConsumption / _cost.Value : 0)).ToString("N2");
            lblWasteConsumptionByMts.Text = ((Double)(_mts > 0 ? _target.WasteConsumption / _mts : 0)).ToString("N2");
            lblWasteCO2ByMoney.Text = ((Double)(_cost.Value > 0 ? _target.WasteCO2 / _cost.Value : 0)).ToString("N2");
            lblWasteCO2ByMts.Text = ((Double)(_mts > 0 ? _target.WasteCO2 / _mts : 0)).ToString("N2");

            lblWaterConsumption.Text = _target.WaterConsumption.ToString("N2") + " " + _target.WaterUnit.Symbol;
            lblWaterCO2.Text = _target.WaterCO2.ToString("N2");
            lblWaterConsumptionByMoney.Text = ((Double)(_cost.Value > 0 ? _target.WaterConsumption / _cost.Value : 0)).ToString("N2");
            lblWaterConsumptionByMts.Text = ((Double)(_mts > 0 ? _target.WaterConsumption / _mts : 0)).ToString("N2");
            lblWaterCO2ByMoney.Text = ((Double)(_cost.Value > 0 ? _target.WaterCO2 / _cost.Value : 0)).ToString("N2");
            lblWaterCO2ByMts.Text = ((Double)(_mts > 0 ? _target.WaterCO2 / _mts : 0)).ToString("N2");

            lblTotalCO2.Text = _target.TotalCO2.ToString("N2");
            lblTotalCO2ByMoney.Text = ((Double)(_cost.Value > 0 ? _target.TotalCO2 / _cost.Value : 0)).ToString("N2");
            lblTotalCO2ByMts.Text = ((Double)(_mts > 0 ? _target.TotalCO2 / _mts : 0)).ToString("N2");
        }
        
        #endregion
    }
}