using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard.EmissionFactors
{
    public partial class SiteEmissionFactors : BasePage
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
        private String _Co2Unit;

        protected void Page_Init(object sender, EventArgs e)
        {
            _Site = (Library.Objects.Sites.SiteMine)I.GetSite(Convert.ToInt64(Request.QueryString["Site"]));
            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _Co2Unit = I.CO2DefaultUnit().Symbol;
                SetMenu();
                LoadTexts();
                LoadSite();
                LoadEFs();
            }
        }

        #region Private Methods

        private void BindControls()
        {
            rptEFFuels.ItemDataBound += new RepeaterItemEventHandler(rptEFFuels_ItemDataBound);
            rptEFTransport.ItemDataBound += new RepeaterItemEventHandler(rptEFTransport_ItemDataBound);
            rptEFWaste.ItemDataBound += new RepeaterItemEventHandler(rptEFWaste_ItemDataBound);
        }

        private void SetMenu()
        {
            ((Main)Page.Master).MenuNavigation.Initialize(_Site.IdSite, _Site.Title, Console.Controls.ucMenuNavigation.MenuItem.EFs, WebUI.Common.GetPermissionFromContext(I,_Site));
        }
        private void LoadTexts()
        {
            lblSiteProperties.Text = Resources.Data.Properties;

            lblTitle.Text = Resources.Data.Title;
            lblLocation.Text = Resources.Data.Location;
            lblStart.Text = Resources.Data.Start;
            lblWeeks.Text = Resources.Data.Weeks;
            lblNumber.Text = Resources.Data.Number;
            lblValue.Text = Resources.Data.Value;
            lblFloorSpace.Text = Resources.Data.FloorSpace;
            lblUnits.Text = Resources.Data.Units;
            
            lblEFElectricity.Text = Resources.Data.Electricity;
            lblEFFuels.Text = Resources.Data.Fuels;
            lblEFTransport.Text = Resources.Data.Transport;
            lblEFWaste.Text = Resources.Data.Waste;
            lblEFWater.Text = Resources.Data.Water;

            lblEFFuelsHeaderName.Text = lblEFTransportHeaderName.Text = lblEFWasteHeaderName.Text = Resources.Data.Name;
            lblEFElectricityHeaderValue.Text = lblEFWaterHeaderValue.Text = lblEFFuelsHeaderValue.Text = lblEFTransportHeaderValue.Text = lblEFWasteHeaderValue.Text = Resources.Data.Value;
            lblEFElectricityHeaderUnit.Text = lblEFWaterHeaderUnit.Text = lblEFFuelsHeaderUnit.Text = lblEFTransportHeaderUnit.Text = lblEFWasteHeaderUnit.Text = Resources.Data.Unit;
            lblEFElectricityHeaderDescription.Text = lblEFWaterHeaderDescription.Text = lblEFFuelsHeaderDescription.Text = lblEFTransportHeaderDescription.Text = lblEFWasteHeaderDescription.Text = Resources.Data.Description;

        }
        private void LoadSite()
        {
            Library.Objects.Auxiliaries.Geographic.Contact _contact = _Site.Contact;
            Location = _contact.Location.Position.Coordenates;
            
            lblTitleValue.Text = _Site.Title;
            lblLocationValue.Text = _contact.Location.Address;
            lblStartValue.Text = _Site.Start.ToShortDateString();
            lblWeeksValue.Text = _Site.Weeks.ToString();
            lblNumberValue.Text = _Site.Number;
            lblValueValue.Text = _Site.Currency.Symbol + " " + _Site.Value.ToString() + " [" + _Site.Currency.Name + "]";
            lblFloorSpaceValue.Text = _Site.FloorSpace.ToString();
            lblUnitsValue.Text = _Site.Units.ToString();
            
        }

        private void LoadEFs()
        {        
            LoadEFElectricity();
            LoadEFFuels();
            LoadEFTransport();
            LoadEFWaste();
            LoadEFWater();

        }
        private void LoadEFElectricity()
        {
            Library.Objects.Auxiliaries.EmissionFactors.ElectricityEmissionFactor _ef = _Site.GetEmissionFactorForElectricity();
            if (_ef != null)
            {
                lblEFElectricityValue.Text = _ef.Value.ToString();
                lblEFElectricityUnit.Text = _Co2Unit + "/" +  _ef.Unit.Symbol;
                lblEFElectricityDescription.Text = _ef.LanguageOption.Description;
            }
        }
        private void LoadEFFuels()
        {
            rptEFFuels.DataSource = _Site.GetEmissionFactorsForFuels().Values;
            rptEFFuels.DataBind();
        }
        private void LoadEFTransport()
        {
            rptEFTransport.DataSource = _Site.GetEmissionFactorsForTransport().Values;
            rptEFTransport.DataBind();
        }
        private void LoadEFWaste()
        {
            rptEFWaste.DataSource = _Site.GetEmissionFactorsForWaste().Values;
            rptEFWaste.DataBind();
        }
        private void LoadEFWater()
        {
            Library.Objects.Auxiliaries.EmissionFactors.WaterEmissionFactor _ef = _Site.GetEmissionFactorForWater();
            if (_ef != null)
            {
                lblEFWaterValue.Text = _ef.Value.ToString();
                lblEFWaterUnit.Text = _Co2Unit + "/" + _ef.Unit.Symbol;
                lblEFWaterDescription.Text = _ef.LanguageOption.Description;
            }
        }

        #endregion

        #region Events

        void rptEFWaste_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor _ef = (Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor)e.Item.DataItem;

                Label lbl = (Label)e.Item.FindControl("lblEFWasteValue");
                lbl.Text = _ef.Value.ToString();

                lbl = (Label)e.Item.FindControl("lblEFWasteTypeName");
                lbl.Text = _ef.WasteType.Name;

                lbl = (Label)e.Item.FindControl("lblEFWasteUnit");
                lbl.Text = _Co2Unit + "/" + _ef.Unit.Symbol;

                lbl = (Label)e.Item.FindControl("lblEFWasteDescription");
                lbl.Text = _ef.LanguageOption.Description;
            }
        }
        void rptEFTransport_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor _ef = (Library.Objects.Auxiliaries.EmissionFactors.TransportTypeEmissionFactor)e.Item.DataItem;

                Label lbl = (Label)e.Item.FindControl("lblEFTransportValue");
                lbl.Text = _ef.Value.ToString();

                lbl = (Label)e.Item.FindControl("lblEFTransportTypeName");
                lbl.Text = _ef.TransportType.Name;

                lbl = (Label)e.Item.FindControl("lblEFTransportUnit");
                lbl.Text = _Co2Unit + "/" + _ef.Unit.Symbol;

                lbl = (Label)e.Item.FindControl("lblEFTransportDescription");
                lbl.Text = _ef.LanguageOption.Description;
            }
        }
        void rptEFFuels_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor _ef = (Library.Objects.Auxiliaries.EmissionFactors.FuelTypeEmissionFactor)e.Item.DataItem;
                
                Label lbl = (Label)e.Item.FindControl("lblEFFuelsValue");
                lbl.Text = _ef.Value.ToString();

                lbl = (Label)e.Item.FindControl("lblEFFuelsTypeName");
                lbl.Text = _ef.FuelType.Name;

                lbl = (Label)e.Item.FindControl("lblEFFuelsUnit");
                lbl.Text = _Co2Unit + "/" + _ef.Unit.Symbol;

                lbl = (Label)e.Item.FindControl("lblEFFuelsDescription");
                lbl.Text = _ef.LanguageOption.Description;
            }
        }

        #endregion
    }
}