using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSI.WebUI.Console.Controls
{
    public partial class ucMenuNavigation : System.Web.UI.UserControl
    {
        #region Control Properties
        
        private Int64 _IdSite=0;
        private String _SiteName="";
        private Int64 _IdMeter=0;
        private String _MeterName="";

        private String _BlinkClassName = "blink";

        private MeterType _MeterType=MeterType.None;
        public enum MeterType
        { Electricity, Fuel, Transport, Waste, Water, None }

        private MenuItem _Selected=MenuItem.None;
        public enum MenuItem
        { EFs, Meters, Meter, MeterSeries, MeterLoad, Payments, Permissions, Profile, Reports, Target, None }

        private WebUI.Common.PermissionType _Permission = WebUI.Common.PermissionType.NoAccess;
        
        #endregion

        public void Initialize(Int64 idSite, String siteName, Int64 idMeter, String meterName, MeterType meterType, MenuItem selected, WebUI.Common.PermissionType permission)
        {
            _IdSite = idSite;
            _SiteName = siteName;
            _IdMeter = idMeter;
            _MeterName = meterName;
            _MeterType = meterType;

            _Selected = selected;

            _Permission = permission;
        }
        public void Initialize(Int64 idSite, String siteName, MenuItem selected, WebUI.Common.PermissionType permission)
        {
            _IdSite = idSite;
            _SiteName = siteName;
            _Selected = selected;
            _Permission = permission;
        }
        public void Blink(MenuItem menu)
        {
            EvaluateBlink(menu);
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setVisibility();
                SetTexts();
                SetUrls();
                SetSelected();
            }
        }

        private void SetTexts()
        {
            lnkMenuNavigationSite.Text = lnkMenuNavigationSite.ToolTip = lnkMenuNavigationSite.ToolTip = _SiteName;
            lblMenuNavigationSiteEFs.Text = lblMenuNavigationSiteEFs.ToolTip = Resources.Data.EFs;
            lblMenuNavigationSiteMeter.Text = lblMenuNavigationSiteMeter.ToolTip = _MeterName;
            lblMenuNavigationSiteMeterLoad.Text = lblMenuNavigationSiteMeterLoad.ToolTip = Resources.Data.Load;
            lblMenuNavigationSiteMeters.Text = lblMenuNavigationSiteMeters.ToolTip = Resources.Data.Meters;
            lblMenuNavigationSiteMeterSerie.Text = lblMenuNavigationSiteMeterSerie.ToolTip = Resources.Data.Series;
            lblMenuNavigationSitePayments.Text = lblMenuNavigationSitePayments.ToolTip = Resources.Data.Payments;
            lblMenuNavigationSitePermissions.Text = lblMenuNavigationSitePermissions.ToolTip = Resources.Data.Permissions;
            lblMenuNavigationSiteProfile.Text = lblMenuNavigationSiteProfile.ToolTip = Resources.Data.Profile;
            lblMenuNavigationSiteReports.Text = lblMenuNavigationSiteReports.ToolTip = Resources.Data.Reports;
            lblMenuNavigationSiteTargets.Text = lblMenuNavigationSiteTargets.ToolTip = Resources.Data.Targets;
        }
        private void SetUrls()
        {
            lnkMenuNavigationSite.PostBackUrl = WebUI.Common.GetPath(WebUI.Common.eFolders.Dashboard,Request) + "Site.aspx?Site=" + _IdSite.ToString();
            lnkMenuNavigationSiteEFs.HRef = WebUI.Common.GetPath(WebUI.Common.eFolders.EmissionFactors, Request) + "SiteEmissionFactors.aspx?Site=" + _IdSite.ToString();
            lnkMenuNavigationSiteMeters.HRef = WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "Meters.aspx?Site=" + _IdSite.ToString();

            SetUrlForMeter();

            lnkMenuNavigationSitePayments.HRef = WebUI.Common.GetPath(WebUI.Common.eFolders.Payments, Request) + "SitePayments.aspx?Site=" + _IdSite.ToString();
            lnkMenuNavigationSitePermissions.HRef = WebUI.Common.GetPath(WebUI.Common.eFolders.Permissions, Request) + "SitePermissions.aspx?Site=" + _IdSite.ToString();
            lnkMenuNavigationSiteProfile.HRef = WebUI.Common.GetPath(WebUI.Common.eFolders.Dashboard, Request) + "SiteProfile.aspx?Site=" + _IdSite.ToString();
            lnkMenuNavigationSiteReports.HRef = WebUI.Common.GetPath(WebUI.Common.eFolders.Reports, Request) + "SiteReport.aspx?Site=" + _IdSite.ToString();
            lnkMenuNavigationSiteTargets.HRef = WebUI.Common.GetPath(WebUI.Common.eFolders.Targets, Request) + "SiteTargets.aspx?Site=" + _IdSite.ToString();
        }
        private void SetUrlForMeter()
        {
            String _pathToMeters = WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request);
            
            switch (_MeterType)
            {
                case MeterType.Electricity:
                    lnkMenuNavigationSiteMeter.HRef = _pathToMeters + "MeterElectricity.aspx?Meter=" + _IdMeter.ToString();
                    lnkMenuNavigationSiteMeterSerie.HRef = _pathToMeters + "MeterElectricityLoads.aspx?Meter=" + _IdMeter.ToString();
                    lnkMenuNavigationSiteMeterLoad.HRef = _pathToMeters + "MeterElectricityLoadAdd.aspx?Meter=" + _IdMeter.ToString();;
                    break;
                case MeterType.Fuel:
                    lnkMenuNavigationSiteMeter.HRef = _pathToMeters + "MeterFuel.aspx?Meter=" + _IdMeter.ToString();
                    lnkMenuNavigationSiteMeterSerie.HRef = _pathToMeters + "MeterFuelLoads.aspx?Meter=" + _IdMeter.ToString();
                    lnkMenuNavigationSiteMeterLoad.HRef = _pathToMeters + "MeterFuelLoadAdd.aspx?Meter=" + _IdMeter.ToString();;
                    break;
                case MeterType.Transport:
                    lnkMenuNavigationSiteMeter.HRef = _pathToMeters + "MeterTransport.aspx?Meter=" + _IdMeter.ToString();;
                    lnkMenuNavigationSiteMeterLoad.HRef = _pathToMeters + "MeterTransportLoadAdd.aspx?Meter=" + _IdMeter.ToString();;
                    lnkMenuNavigationSiteMeterSerie.HRef = _pathToMeters + "MeterTransportLoads.aspx?Meter=" + _IdMeter.ToString();
                    break;
                case MeterType.Waste:
                    lnkMenuNavigationSiteMeter.HRef = _pathToMeters + "MeterWaste.aspx?Meter=" + _IdMeter.ToString();
                    lnkMenuNavigationSiteMeterSerie.HRef = _pathToMeters + "MeterWasteLoads.aspx?Meter=" + _IdMeter.ToString();
                    lnkMenuNavigationSiteMeterLoad.HRef = _pathToMeters + "MeterWasteLoadAdd.aspx?Meter=" + _IdMeter.ToString();;
                    break;
                case MeterType.Water:
                    lnkMenuNavigationSiteMeter.HRef = _pathToMeters + "MeterWater.aspx?Meter=" + _IdMeter.ToString();
                    lnkMenuNavigationSiteMeterSerie.HRef = _pathToMeters + "MeterWaterLoads.aspx?Meter=" + _IdMeter.ToString();
                    lnkMenuNavigationSiteMeterLoad.HRef = _pathToMeters + "MeterWaterLoadAdd.aspx?Meter=" + _IdMeter.ToString();;
                    break;
                default:
                    break;
            }
        }
        private void setVisibility()
        {
            //Site
            if (_IdSite > 0 && _Permission != WebUI.Common.PermissionType.NoAccess)
            {
                lnkMenuNavigationSite.Visible = true;
                
                liMenuNavigationSiteEFs.Visible = true;
                liMenuNavigationSiteMeters.Visible = true;
                liMenuNavigationSiteProfile.Visible = true;
                liMenuNavigationSiteReports.Visible = true;
                liMenuNavigationSiteTargets.Visible = true;

                //Meter
                if (_IdMeter > 0)
                {
                    liMenuNavigationSiteMeter.Visible = true;
                    if (_Permission != WebUI.Common.PermissionType.SiteReader)
                    {
                        liMenuNavigationSiteMeterLoad.Visible = true;
                        liMenuNavigationSiteMeterSerie.Visible = true;
                    }
                    else
                    {
                        liMenuNavigationSiteMeterLoad.Visible = true;
                        liMenuNavigationSiteMeterSerie.Visible = true;
                    }
                }
                else
                {
                    //No Meter

                    liMenuNavigationSiteMeter.Visible = false;
                    liMenuNavigationSiteMeterLoad.Visible = false;
                    liMenuNavigationSiteMeterSerie.Visible = false;
                }

                //Company Manager Options
                if (_Permission == WebUI.Common.PermissionType.CompanyManager)
                {
                    liMenuNavigationSitePayments.Visible = true;
                    liMenuNavigationSitePermissions.Visible = true;
                }
                else
                {
                    liMenuNavigationSitePayments.Visible = false;
                    liMenuNavigationSitePermissions.Visible = false;
                }
            }
            else
            {
                //No Access
            
                lnkMenuNavigationSite.Visible = false;
                liMenuNavigationSiteEFs.Visible = false;
                liMenuNavigationSiteMeters.Visible = false;
                liMenuNavigationSiteMeter.Visible = false;
                liMenuNavigationSiteMeterLoad.Visible = false;
                liMenuNavigationSiteMeterSerie.Visible = false;
                liMenuNavigationSitePayments.Visible = false;
                liMenuNavigationSitePermissions.Visible = false;
                liMenuNavigationSiteProfile.Visible = false;
                liMenuNavigationSiteReports.Visible = false;
                liMenuNavigationSiteTargets.Visible = false;
            }
        }
        private void SetSelected()
        {
            switch (_Selected)
            {
                case MenuItem.EFs:
                    liMenuNavigationSiteEFs.Attributes.Add("class", "selected");
                    break;
                case MenuItem.Meter:
                    liMenuNavigationSiteMeter.Attributes.Add("class", "selected");
                    break;
                case MenuItem.Meters:
                    liMenuNavigationSiteMeters.Attributes.Add("class", "selected");
                    break;
                case MenuItem.MeterSeries:
                    liMenuNavigationSiteMeterSerie.Attributes.Add("class", "selected");
                    break;
                case MenuItem.MeterLoad:
                    liMenuNavigationSiteMeterLoad.Attributes.Add("class", "selected");
                    break;
                case MenuItem.Payments:
                    liMenuNavigationSitePayments.Attributes.Add("class", "selected");
                    break;
                case MenuItem.Permissions:
                    liMenuNavigationSitePermissions.Attributes.Add("class", "selected");
                    break;
                case MenuItem.Profile:
                    liMenuNavigationSiteProfile.Attributes.Add("class", "selected");
                    break;
                case MenuItem.Reports:
                    liMenuNavigationSiteReports.Attributes.Add("class", "selected");
                    break;
                case MenuItem.Target:
                    liMenuNavigationSiteTargets.Attributes.Add("class", "selected");
                    break;
                case MenuItem.None:
                    break;
                default:
                    break;
            }
        }
        private void EvaluateBlink(MenuItem menu)
        {
            switch (menu)
            {
                case MenuItem.EFs:
                    liMenuNavigationSiteEFs.Attributes.Add("class", _BlinkClassName);
                    break;
                case MenuItem.Meters:
                    liMenuNavigationSiteMeters.Attributes.Add("class", _BlinkClassName);
                    break;
                case MenuItem.Meter:
                    liMenuNavigationSiteMeter.Attributes.Add("class", _BlinkClassName);
                    break;
                case MenuItem.MeterSeries:
                    liMenuNavigationSiteMeterSerie.Attributes.Add("class", _BlinkClassName);
                    break;
                case MenuItem.MeterLoad:
                    liMenuNavigationSiteMeterLoad.Attributes.Add("class", _BlinkClassName);
                    break;
                case MenuItem.Payments:
                    liMenuNavigationSitePayments.Attributes.Add("class", _BlinkClassName);
                    break;
                case MenuItem.Permissions:
                    liMenuNavigationSitePermissions.Attributes.Add("class", _BlinkClassName);
                    break;
                case MenuItem.Profile:
                    liMenuNavigationSiteProfile.Attributes.Add("class", _BlinkClassName);
                    break;
                case MenuItem.Reports:
                    liMenuNavigationSiteReports.Attributes.Add("class", _BlinkClassName);
                    break;
                case MenuItem.Target:
                    liMenuNavigationSiteTargets.Attributes.Add("class", _BlinkClassName);
                    break;
                case MenuItem.None:
                    break;
                default:
                    break;
            }
        }
    
    }
}