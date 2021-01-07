using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard.Meters
{
    public partial class Meters : BasePage
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
        public WebUI.Common.Protocols Protocol
        {
            set
            {
                ViewState["Protocol"] = value;
            }
            get
            {
                return (WebUI.Common.Protocols)ViewState["Protocol"];
            }
        }
        private Boolean _CanManage;
        
        protected void Page_Init(object sender, EventArgs e)
        {
            _Site = (Library.Objects.Sites.SiteMine)I.GetSite(Convert.ToInt64(Request.QueryString["Site"]));
            _CanManage = (I is UserOperatorMeManager) || (_Site.CurrentPermission() == Library.Security.Authority.PermissionTypes.SiteManager);

            //If no sites then show guide
            if (_Site.MetersQuantity == 0)
            {
                ((Main)Page.Master).Guide.SetMessage(Resources.Messages.FirstTimeNeedHelp, Resources.Messages.FirstTimeGuideSite, 100, 100);
                btnMeterAdd.CssClass = "blink";
            }

            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMenu();
                LoadTexts();
                LoadSite();
                
                Protocol = WebUI.Common.Protocols.Electricity;
                LoadMeters();
            }

        }

        #region Private Methods

        private void BindControls()
        {
            lnkMeterTypeElectricity.Click += new EventHandler(lnkMeterTypeElectricity_Click);
            lnkMeterTypeFuels.Click += new EventHandler(lnkMeterTypeFuels_Click);
            lnkMeterTypeTransport.Click += new EventHandler(lnkMeterTypeTransport_Click);
            lnkMeterTypeWaste.Click += new EventHandler(lnkMeterTypeWaste_Click);
            lnkMeterTypeWater.Click += new EventHandler(lnkMeterTypeWater_Click);

            rptMeters.ItemDataBound += new RepeaterItemEventHandler(rptMeters_ItemDataBound);
            rptMeters.ItemCommand += new RepeaterCommandEventHandler(rptMeters_ItemCommand);
        }

        
        private void SetMenu()
        {
            ((Main)Page.Master).MenuNavigation.Initialize(_Site.IdSite, _Site.Title, Console.Controls.ucMenuNavigation.MenuItem.Meters, WebUI.Common.GetPermissionFromContext(I,_Site));

            if (_CanManage && _Site is Library.Objects.Sites.SiteMineOpen)
            {
                btnMeterAdd.Visible = true;
            }
            else
            {
                btnMeterAdd.Visible = false;
            }
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

            lblMeters.Text = Resources.Data.Meters;

            lnkMeterTypeElectricity.Text = Resources.Data.Electricity;
            lnkMeterTypeFuels.Text = Resources.Data.Fuels;
            lnkMeterTypeTransport.Text = Resources.Data.Transport;
            lnkMeterTypeWaste.Text = Resources.Data.Waste;
            lnkMeterTypeWater.Text = Resources.Data.Water;
            
            lblMetersHeaderIdentification.Text = Resources.Data.Identification;
            lblMetersHeaderLastDate.Text = Resources.Data.LastDate;
            lblMetersHeaderSum.Text = Resources.Data.Sum;
            lblMetersHeaderSumCO2.Text = Resources.Data.CO2Total;
            btnMeterAdd.ToolTip = btnMeterAdd.Text = "+ " + Resources.Data.MetersAdd;
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
        private void LoadMeters()
        {
            switch (Protocol)
            {
                case CSI.WebUI.Common.Protocols.Electricity:
                    rptMeters.DataSource = _Site.ElectricityMeters.Values;
                    btnMeterAdd.PostBackUrl = WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterElectricityAdd.aspx?Site=" + _Site.IdSite;
                    break;
                case CSI.WebUI.Common.Protocols.Fuels:
                    rptMeters.DataSource = _Site.FuelMeters.Values;
                    btnMeterAdd.PostBackUrl = WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterFuelAdd.aspx?Site=" + _Site.IdSite;
                    break;
                case CSI.WebUI.Common.Protocols.Transport:
                    rptMeters.DataSource = _Site.TransportMeters.Values;
                    btnMeterAdd.PostBackUrl = WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterTransportAdd.aspx?Site=" + _Site.IdSite;
                    break;
                case CSI.WebUI.Common.Protocols.Water:
                    rptMeters.DataSource = _Site.WaterMeters.Values;
                    btnMeterAdd.PostBackUrl = WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterWaterAdd.aspx?Site=" + _Site.IdSite;
                    break;
                case CSI.WebUI.Common.Protocols.Waste:
                    rptMeters.DataSource = _Site.WasteMeters.Values;
                    btnMeterAdd.PostBackUrl = WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterWasteAdd.aspx?Site=" + _Site.IdSite;
                    break;
                default:
                    break;
            }

            rptMeters.DataBind();
        }

        #endregion

        #region Events

        void lnkMeterTypeWater_Click(object sender, EventArgs e)
        {
            lnkMeterTypeWater.CssClass = "active";
            lnkMeterTypeWaste.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            lnkMeterTypeTransport.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            lnkMeterTypeFuels.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            lnkMeterTypeElectricity.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            
            Protocol = WebUI.Common.Protocols.Water;
            LoadMeters();

        }
        void lnkMeterTypeWaste_Click(object sender, EventArgs e)
        {
            lnkMeterTypeWaste.CssClass = "active";
            lnkMeterTypeWater.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            lnkMeterTypeTransport.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            lnkMeterTypeFuels.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            lnkMeterTypeElectricity.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            
            Protocol = WebUI.Common.Protocols.Waste;
            LoadMeters();
         }
        void lnkMeterTypeTransport_Click(object sender, EventArgs e)
        {
            lnkMeterTypeTransport.CssClass = "active";
            lnkMeterTypeWater.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            lnkMeterTypeWaste.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            lnkMeterTypeFuels.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            lnkMeterTypeElectricity.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            
            Protocol = WebUI.Common.Protocols.Transport;
            LoadMeters();
        }
        void lnkMeterTypeFuels_Click(object sender, EventArgs e)
        {
            lnkMeterTypeFuels.CssClass = "active";
            lnkMeterTypeWater.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            lnkMeterTypeWaste.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            lnkMeterTypeTransport.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            lnkMeterTypeElectricity.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            
            Protocol = WebUI.Common.Protocols.Fuels;
            LoadMeters();
        }
        void lnkMeterTypeElectricity_Click(object sender, EventArgs e)
        {
            lnkMeterTypeElectricity.CssClass = "active";
            lnkMeterTypeWater.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            lnkMeterTypeWaste.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            lnkMeterTypeFuels.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            lnkMeterTypeTransport.CssClass = lnkMeterTypeWater.CssClass.Replace("active", "");
            
            Protocol = WebUI.Common.Protocols.Electricity;
            LoadMeters();
        }

        void rptMeters_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Sites.Meters.Meter _meter = (Library.Objects.Sites.Meters.Meter)e.Item.DataItem;

                String _idMeter = _meter.IdMeter.ToString();

                Button btn = ((Button)e.Item.FindControl("btnMeterView"));
                btn.CommandArgument = _idMeter;
                btn.ToolTip = Resources.Data.View;

                Button btnEdit = ((Button)e.Item.FindControl("btnMeterEdit"));
                Button btnDelete = ((Button)e.Item.FindControl("btnMeterDelete"));
                if (_CanManage)
                {
                    if (_meter.Site is CSI.Library.Objects.Sites.SiteMineOpen)
                    {
                        btn = ((Button)e.Item.FindControl("btnMeterEdit"));
                        btn.CommandArgument = _idMeter;
                        btn.ToolTip = Resources.Data.Edit;

                        btn = ((Button)e.Item.FindControl("btnMeterDelete"));
                        btn.CommandArgument = _idMeter;
                        btn.ToolTip = Resources.Data.Delete;

                        AddConfirmRequest((WebControl)btnDelete, Resources.Messages.ConfirmDeleteTitle, Resources.Messages.ConfirmDeleteMessage);
                    }
                    else
                    {
                        btnEdit.Visible = btnDelete.Visible = false;
                    }
                }
                else
                {
                    btnEdit.Visible = btnDelete.Visible = false;
                }

                Label _lbl = ((Label)e.Item.FindControl("lblMeterIdentification"));
                _lbl.Text = _meter.Identification;

                _lbl = ((Label)e.Item.FindControl("lblMeterLastDate"));
                DateTime? _lastDate = _meter.GetLastDate();
                if (_lastDate.HasValue)
                    _lbl.Text = _lastDate.Value.ToShortDateString();

                Library.Objects.Metrics.MetricPeriod _stats = _meter.GetStatistics();
                _lbl = ((Label)e.Item.FindControl("lblMeterTotal"));
                _lbl.Text = WebUI.Common.RoundAndFormat(_stats.Sum).ToString();

                _lbl = ((Label)e.Item.FindControl("lblMeterTotalUnit"));
                _lbl.Text = _stats.Unit.Symbol;

                _lbl = ((Label)e.Item.FindControl("lblMeterTotalCO2"));
                _lbl.Text = WebUI.Common.RoundAndFormat(_stats.SumCO2).ToString();

                _lbl = ((Label)e.Item.FindControl("lblMeterTotalCO2Unit"));
                _lbl.Text = Resources.Data.CO2Unit;

            }
        }
        void rptMeters_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            String _idSite = _Site.IdSite.ToString();

            switch (e.CommandName)
            {
                case "View":
                    switch (Protocol)
                    {
                        case CSI.WebUI.Common.Protocols.Electricity:
                            Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterElectricity.aspx?Meter=" + e.CommandArgument);
                            break;
                        case CSI.WebUI.Common.Protocols.Fuels:
                            Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterFuel.aspx?Meter=" + e.CommandArgument);
                            break;
                        case CSI.WebUI.Common.Protocols.Transport:
                            Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterTransport.aspx?Meter=" + e.CommandArgument);
                            break;
                        case CSI.WebUI.Common.Protocols.Water:
                            Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterWater.aspx?Meter=" + e.CommandArgument);
                            break;
                        case CSI.WebUI.Common.Protocols.Waste:
                            Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterWaste.aspx?Meter=" + e.CommandArgument);
                            break;
                        default:
                            break;
                    }
                    break;
                case "Edit":
                    switch (Protocol)
                    {
                        case CSI.WebUI.Common.Protocols.Electricity:
                            Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterElectricityEdit.aspx?Site=" + _idSite + "&Meter=" + e.CommandArgument);
                            break;
                        case CSI.WebUI.Common.Protocols.Fuels:
                            Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterFuelEdit.aspx?Site=" + _idSite + "&Meter=" + e.CommandArgument);
                            break;
                        case CSI.WebUI.Common.Protocols.Transport:
                            Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterTransportEdit.aspx?Site=" + _idSite + "&Meter=" + e.CommandArgument);
                            break;
                        case CSI.WebUI.Common.Protocols.Water:
                            Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterWaterEdit.aspx?Site=" + _idSite + "&Meter=" + e.CommandArgument);
                            break;
                        case CSI.WebUI.Common.Protocols.Waste:
                            Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterWasteEdit.aspx?Site=" + _idSite + "&Meter=" + e.CommandArgument);
                            break;
                        default:
                            break;
                    }
                    break;
                case "Delete":
                    switch (Protocol)
                    {
                        case CSI.WebUI.Common.Protocols.Electricity:
                            ((UserOperatorMeManager)I).RemoveMeterElectricity(I.GetElectricityMeter(Convert.ToInt64(e.CommandArgument)));
                            break;
                        case CSI.WebUI.Common.Protocols.Fuels:
                            ((UserOperatorMeManager)I).RemoveMeterFuels(I.GetFuelMeter(Convert.ToInt64(e.CommandArgument)));
                            break;
                        case CSI.WebUI.Common.Protocols.Transport:
                            ((UserOperatorMeManager)I).RemoveMeterTransport(I.GetTransportMeter(Convert.ToInt64(e.CommandArgument)));
                            break;
                        case CSI.WebUI.Common.Protocols.Water:
                            ((UserOperatorMeManager)I).RemoveMeterWater(I.GetWaterMeter(Convert.ToInt64(e.CommandArgument)));
                            break;
                        case CSI.WebUI.Common.Protocols.Waste:
                            ((UserOperatorMeManager)I).RemoveMeterWaste(I.GetWasteMeter(Convert.ToInt64(e.CommandArgument)));
                            break;
                        default:
                            break;
                    }
                    LoadMeters();
                    break;
                default:
                    break;
            }

        }
        

        #endregion
    }
}