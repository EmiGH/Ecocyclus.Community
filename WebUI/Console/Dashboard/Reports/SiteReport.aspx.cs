using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;
using System.IO;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard.Reports
{
    public partial class SiteReport : BasePage
    {
        #region Properties 

        private Library.Objects.Sites.SiteMine _Site;
        private Library.Objects.Sites.Targets _Targets;

        //Evolution Serie
        private DataTable _EvolutionTable;
        
        //Evolution Totals
        Double _ElectricityTotal = 0, _FuelTotal = 0, _TransportTotal = 0, _WasteTotal = 0, _WaterTotal = 0;
        Double _ElectricityTotalKPIMts = 0, _FuelTotalKPIMts = 0, _TransportTotalKPIMts = 0, _WasteTotalKPIMts = 0, _WaterTotalKPIMts = 0;
        Double _ElectricityTotalKPIMoney = 0, _FuelTotalKPIMoney = 0, _TransportTotalKPIMoney = 0, _WasteTotalKPIMoney = 0, _WaterTotalKPIMoney = 0;

        Double _ElectricityTotalCO2 = 0, _FuelTotalCO2 = 0, _TransportTotalCO2 = 0, _WasteTotalCO2 = 0, _WaterTotalCO2 = 0, _CO2Total = 0;
        Double _ElectricityTotalCO2KPIMts = 0, _FuelTotalCO2KPIMts = 0, _TransportTotalCO2KPIMts = 0, _WasteTotalCO2KPIMts = 0, _WaterTotalCO2KPIMts = 0, _CO2TotalKPIMts;
        Double _ElectricityTotalCO2KPIMoney = 0, _FuelTotalCO2KPIMoney = 0, _TransportTotalCO2KPIMoney = 0, _WasteTotalCO2KPIMoney = 0, _WaterTotalCO2KPIMoney = 0, _CO2TotalKPIMoney;
        
        //Max Values for Target Zones
        Double _MaxElectricity = 0, _MaxFuels = 0, _MaxTransport = 0, _MaxWaste = 0, _MaxWater = 0, _MaxCO2 = 0;

        //Units
        Library.Objects.Auxiliaries.Units.Unit _ElectricityUnit, _FuelUnit, _TransportUnit, _WasteUnit, _WaterUnit, _CO2Unit;

        //Months
        String[] _Months;

        //Location Coords
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

        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            _Site = (Library.Objects.Sites.SiteMine)I.GetSite(Convert.ToInt64(Request.QueryString["Site"]));
            _Targets = _Site.Targets;

            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                SetMenu();
                LoadTexts();
                LoadSite();
                LoadMeters();
                LoadTargets();
            }
            SetChartsEvolution();
            SetEvolutionTable();
            LoadEvolutionTable();
            LoadPies();
        }

        #region Private Methods

        private void BindControls()
        {
            hplPrint.Attributes.Add("onclick", "window.open('" + WebUI.Common.GetPath(WebUI.Common.eFolders.Reports, Request) + "SiteReportPrintable.aspx?Site=" + _Site.IdSite.ToString() + "', '_blank');return false;");
            btnFilter.Click += new EventHandler(btnFilter_Click);

            rptCO2MonthlyEvolution.ItemDataBound += new RepeaterItemEventHandler(rptCO2MonthlyEvolution_ItemDataBound);
            rptElectricityMonthlyEvolution.ItemDataBound += new RepeaterItemEventHandler(rptElectricityMonthlyEvolution_ItemDataBound);
            rptFuelsMonthlyEvolution.ItemDataBound += new RepeaterItemEventHandler(rptFuelsMonthlyEvolution_ItemDataBound);
            rptTransportMonthlyEvolution.ItemDataBound += new RepeaterItemEventHandler(rptTransportMonthlyEvolution_ItemDataBound);
            rptWasteMonthlyEvolution.ItemDataBound += new RepeaterItemEventHandler(rptWasteMonthlyEvolution_ItemDataBound);
            rptWaterMonthlyEvolution.ItemDataBound += new RepeaterItemEventHandler(rptWaterMonthlyEvolution_ItemDataBound);

            rptElectricityMeters.ItemDataBound += new RepeaterItemEventHandler(rptElectricityMeters_ItemDataBound);
            rptFuelsMeters.ItemDataBound += new RepeaterItemEventHandler(rptFuelsMeters_ItemDataBound);
            rptTransportMeters.ItemDataBound += new RepeaterItemEventHandler(rptTransportMeters_ItemDataBound);
            rptWasteMeters.ItemDataBound += new RepeaterItemEventHandler(rptWasteMeters_ItemDataBound);
            rptWaterMeters.ItemDataBound += new RepeaterItemEventHandler(rptWaterMeters_ItemDataBound);

        }
        private void SetMenu()
        {
            ((Main)Page.Master).MenuNavigation.Initialize(_Site.IdSite, _Site.Title, Console.Controls.ucMenuNavigation.MenuItem.Reports, WebUI.Common.GetPermissionFromContext(I,_Site));
        }
        private void LoadTexts()
        {
            //Site Property
            lblSiteProperties.Text = Resources.Data.Properties;

            lblSiteClient.Text = Resources.Data.Client;

            lblTitle.Text = Resources.Data.Title;
            lblLocation.Text = Resources.Data.Location;
            lblStart.Text = Resources.Data.Start;
            lblWeeks.Text = Resources.Data.Weeks;
            lblNumber.Text = Resources.Data.Number;
            lblValue.Text = Resources.Data.Value;
            lblFloorSpace.Text = Resources.Data.FloorSpace;
            lblUnits.Text = Resources.Data.Units;
            lblClient.Text = Resources.Data.Client;
            lblAgent.Text = Resources.Data.Agent;
            lblContractor.Text = Resources.Data.Contractor;
            lblResponsible.Text = Resources.Data.Responsible;
            lblManager.Text = Resources.Data.Manager;
            lblTelephone.Text = Resources.Data.Telephone;
            lblEmail.Text = Resources.Data.Email;
            lblUrl.Text = Resources.Data.Url;
            lblFacebook.Text = Resources.Data.Facebook;
            lblTwitter.Text = Resources.Data.Twitter;
            lblDescription.Text = Resources.Data.Description;


            //Filter Data
            txtFrom.Text = hdnFrom.Value = _Site.Start.ToShortDateString();
            txtTo.Text = hdnTo.Value = DateTime.Now.ToShortDateString();
            calFrom.SelectedDate = _Site.Start;
            calTo.SelectedDate = DateTime.Now;

            rvFilter.Text = Resources.Messages.SummaryErrorCharacter;

            //Map Data
            lblMap.Text = Resources.Data.Map;

            //Printable
            hplPrint.ToolTip = Resources.Data.PrintableVersion;

            //Filter
            lblFrom.Text = Resources.Data.From;
            lblTo.Text = Resources.Data.To;
            btnFilter.Text = Resources.Data.Filter;

            //Performance
            lblCO2Data.Text = Resources.Data.Performance;
            lblElectricityData.Text = Resources.Data.Electricity;
            lblFuelsData.Text = Resources.Data.Fuels;
            lblTransportData.Text = Resources.Data.Transport;
            lblWasteData.Text = Resources.Data.Waste;
            lblWaterData.Text = Resources.Data.Water;
            
            //Evolution Sections
            lblElectricityMonthlyEvolutionHeader.Text = lblFuelsMonthlyEvolutionHeader.Text =
            lblTransportMonthlyEvolutionHeader.Text = lblWasteMonthlyEvolutionHeader.Text =
            lblWaterMonthlyEvolutionHeader.Text = Resources.Data.MonthlyEvolutions;

            lblCO2MonthlyEvolutionHeaderElectricity.Text = Resources.Data.Electricity;
            lblCO2MonthlyEvolutionHeaderFuels.Text = Resources.Data.Fuels;
            lblCO2MonthlyEvolutionHeaderTransport.Text = Resources.Data.Transport;
            lblCO2MonthlyEvolutionHeaderWaste.Text  = Resources.Data.Waste;
            lblCO2MonthlyEvolutionHeaderWater.Text = Resources.Data.Water;
            lblCO2MonthlyEvolutionHeaderCO2.Text = Resources.Data.CO2;
                       
            //Meters
            lblElectricityMeters.Text = lblFuelsMeters.Text = lblTransportMeters.Text =
            lblWasteMeters.Text = lblWaterMeters.Text = Resources.Data.Meters;
            lblElectricityMetersHeaderIdentification.Text = lblFuelsMetersHeaderIdentification.Text =
            lblTransportMetersHeaderIdentification.Text = lblWasteMetersHeaderIdentification.Text =
            lblWaterMetersHeaderIdentification.Text = Resources.Data.Identification;
            lblElectricityMetersHeaderLastDate.Text = lblFuelsMetersHeaderLastDate.Text =
            lblTransportMetersHeaderLastDate.Text = lblWasteMetersHeaderLastDate.Text =
            lblWaterMetersHeaderLastDate.Text = Resources.Data.LastDate;
            lblElectricityMetersHeaderSum.Text = lblFuelsMetersHeaderSum.Text =
            lblTransportMetersHeaderSum.Text = lblWasteMetersHeaderSum.Text =
            lblWaterMetersHeaderSum.Text = Resources.Data.Total;
            lblElectricityMetersHeaderSumCO2.Text = lblFuelsMetersHeaderSumCO2.Text =
            lblTransportMetersHeaderSumCO2.Text = lblWasteMetersHeaderSumCO2.Text =
            lblWaterMetersHeaderSumCO2.Text = Resources.Data.CO2;

            // Targets Data
            String _currencySymbol = "100k " + _Site.Cost.Currency.Symbol;
            lblElectricityTargetsHeaderConsumption.Text = lblFuelsTargetsHeaderConsumption.Text =
            lblTransportTargetsHeaderConsumption.Text = lblWasteTargetsHeaderConsumption.Text =
            lblWaterTargetsHeaderConsumption.Text = Resources.Data.ConsumptionMonthlyLimit;

            lblElectricityTargetHeaderConsumptionPKIMoney.Text = lblFuelsTargetHeaderConsumptionPKIMoney.Text =
            lblTransportTargetHeaderConsumptionPKIMoney.Text = lblWasteTargetHeaderConsumptionPKIMoney.Text =
            lblWaterTargetHeaderConsumptionPKIMoney.Text = Resources.Data.ConsumptionMonthlyLimitByMoney + " [unit/" + _currencySymbol + "]";

            lblElectricityTargetHeaderConsumptionPKIMts.Text = lblFuelsTargetHeaderConsumptionPKIMts.Text =
            lblTransportTargetHeaderConsumptionPKIMts.Text = lblWasteTargetHeaderConsumptionPKIMts.Text =
            lblWaterTargetHeaderConsumptionPKIMts.Text = Resources.Data.ConsumptionMonthlyLimitByMts + " [unit/m2]";
            
            lblElectricityTargetHeaderCO2.Text = lblFuelsTargetHeaderCO2.Text =
            lblTransportTargetHeaderCO2.Text = lblWasteTargetHeaderCO2.Text =
            lblWaterTargetHeaderCO2.Text = lblCO2TargetsHeader.Text = Resources.Data.CO2GenerationLimit + "[" + Resources.Data.CO2Unit + "]";

            lblElectricityTargetHeaderCO2PKIMoney.Text = lblFuelsTargetHeaderCO2PKIMoney.Text =
            lblTransportTargetHeaderCO2PKIMoney.Text = lblWasteTargetHeaderCO2PKIMoney.Text =
            lblWaterTargetHeaderCO2PKIMoney.Text = lblCO2TargetsHeaderPKIMoney.Text = Resources.Data.CO2GenerationLimitByMoney + " [" + Resources.Data.CO2Unit + "/" + _currencySymbol + "]"; ;

            lblElectricityTargetHeaderCO2PKIMts.Text = lblFuelsTargetHeaderCO2PKIMts.Text =
            lblTransportTargetHeaderCO2PKIMts.Text = lblWasteTargetHeaderCO2PKIMts.Text =
            lblWaterTargetHeaderCO2PKIMts.Text = lblCO2TargetsHeaderPKIMts.Text = Resources.Data.CO2GenerationLimitByMts + "[" + Resources.Data.CO2Unit + "/m2]";
        }
        private void LoadSite()
        {
            Library.Objects.Auxiliaries.Geographic.Contact _contact = _Site.Contact;
            Location = _contact.Location.Position.Coordenates;
            
            imgImage.Src = WebUI.Common.GetPath(WebUI.Common.eFolders.Common, Request) + "ImageViewer.aspx?IdFile=" + (_Site.Image != null ? _Site.Image.IdFile.ToString() : "-1");
            Pair _size;
            if (_Site.Image != null)
                _size = WebUI.Common.GetImageSize(_Site.Image.Stream, 150);
            else
                _size = WebUI.Common.GetDefaultImageSize(150);
            imgImage.Style.Add("height", _size.Second.ToString() + "px");
            imgImage.Style.Add("width", _size.First.ToString() + "px");
            
            lblTitleValue.Text = _Site.Title;
            lblLocationValue.Text = _contact.Location.Address;
            lblStartValue.Text = _Site.Start.ToShortDateString();
            lblWeeksValue.Text = _Site.Weeks.ToString();
            lblNumberValue.Text = _Site.Number;
            lblValueValue.Text = _Site.Currency.Symbol + " " + _Site.Value.ToString() + " [" + _Site.Currency.Name + "]";
            lblFloorSpaceValue.Text = _Site.FloorSpace.ToString();
            lblUnitsValue.Text = _Site.Units.ToString();
            lblClientValue.Text = _Site.Client;
            lblAgentValue.Text = _Site.Agent;
            lblContractorValue.Text = _Site.Contractor;
            lblResponsibleValue.Text = _Site.Responsible;
            lblManagerValue.Text = _Site.Manager;
            lblTelephoneValue.Text = _contact.Telephone;
            lblEmailValue.Text = _contact.Email;
            lblUrlValue.Text = _contact.Website;
            lblFacebookValue.Text = _contact.Facebook;
            lblTwitterValue.Text = _contact.Twitter;
        }

        #region Evolution

        private void SetEvolutionTable()
        {
            _EvolutionTable = new DataTable("Evolution");
            _EvolutionTable.Columns.Add("Month", typeof(String));

            _EvolutionTable.Columns.Add("ElectricityUnit", typeof(String));
            _EvolutionTable.Columns.Add("ElectricityUnits", typeof(String));
            _EvolutionTable.Columns.Add("ElectricityKPIMts", typeof(String));
            _EvolutionTable.Columns.Add("ElectricityKPIMtsUnits", typeof(String));
            _EvolutionTable.Columns.Add("ElectricityKPIMoney", typeof(String));
            _EvolutionTable.Columns.Add("ElectricityKPIMoneyUnits", typeof(String));
            _EvolutionTable.Columns.Add("ElectricityCO2", typeof(String));
            _EvolutionTable.Columns.Add("ElectricityCO2Units", typeof(String));
            _EvolutionTable.Columns.Add("ElectricityCO2KPIMts", typeof(String));
            _EvolutionTable.Columns.Add("ElectricityCO2KPIMtsUnits", typeof(String));
            _EvolutionTable.Columns.Add("ElectricityCO2KPIMoney", typeof(String));
            _EvolutionTable.Columns.Add("ElectricityCO2KPIMoneyUnits", typeof(String));

            _EvolutionTable.Columns.Add("FuelsUnit", typeof(String));
            _EvolutionTable.Columns.Add("FuelsUnits", typeof(String));
            _EvolutionTable.Columns.Add("FuelsKPIMts", typeof(String));
            _EvolutionTable.Columns.Add("FuelsKPIMtsUnits", typeof(String));
            _EvolutionTable.Columns.Add("FuelsKPIMoney", typeof(String));
            _EvolutionTable.Columns.Add("FuelsKPIMoneyUnits", typeof(String));
            _EvolutionTable.Columns.Add("FuelsCO2", typeof(String));
            _EvolutionTable.Columns.Add("FuelsCO2Units", typeof(String));
            _EvolutionTable.Columns.Add("FuelsCO2KPIMts", typeof(String));
            _EvolutionTable.Columns.Add("FuelsCO2KPIMtsUnits", typeof(String));
            _EvolutionTable.Columns.Add("FuelsCO2KPIMoney", typeof(String));
            _EvolutionTable.Columns.Add("FuelsCO2KPIMoneyUnits", typeof(String));

            _EvolutionTable.Columns.Add("TransportUnit", typeof(String));
            _EvolutionTable.Columns.Add("TransportUnits", typeof(String));
            _EvolutionTable.Columns.Add("TransportKPIMts", typeof(String));
            _EvolutionTable.Columns.Add("TransportKPIMtsUnits", typeof(String));
            _EvolutionTable.Columns.Add("TransportKPIMoney", typeof(String));
            _EvolutionTable.Columns.Add("TransportKPIMoneyUnits", typeof(String));
            _EvolutionTable.Columns.Add("TransportCO2", typeof(String));
            _EvolutionTable.Columns.Add("TransportCO2Units", typeof(String));
            _EvolutionTable.Columns.Add("TransportCO2KPIMts", typeof(String));
            _EvolutionTable.Columns.Add("TransportCO2KPIMtsUnits", typeof(String));
            _EvolutionTable.Columns.Add("TransportCO2KPIMoney", typeof(String));
            _EvolutionTable.Columns.Add("TransportCO2KPIMoneyUnits", typeof(String));

            _EvolutionTable.Columns.Add("WasteUnit", typeof(String));
            _EvolutionTable.Columns.Add("WasteUnits", typeof(String));
            _EvolutionTable.Columns.Add("WasteKPIMts", typeof(String));
            _EvolutionTable.Columns.Add("WasteKPIMtsUnits", typeof(String));
            _EvolutionTable.Columns.Add("WasteKPIMoney", typeof(String));
            _EvolutionTable.Columns.Add("WasteKPIMoneyUnits", typeof(String));
            _EvolutionTable.Columns.Add("WasteCO2", typeof(String));
            _EvolutionTable.Columns.Add("WasteCO2Units", typeof(String));
            _EvolutionTable.Columns.Add("WasteCO2KPIMts", typeof(String));
            _EvolutionTable.Columns.Add("WasteCO2KPIMtsUnits", typeof(String));
            _EvolutionTable.Columns.Add("WasteCO2KPIMoney", typeof(String));
            _EvolutionTable.Columns.Add("WasteCO2KPIMoneyUnits", typeof(String));

            _EvolutionTable.Columns.Add("WaterUnit", typeof(String));
            _EvolutionTable.Columns.Add("WaterUnits", typeof(String));
            _EvolutionTable.Columns.Add("WaterKPIMts", typeof(String));
            _EvolutionTable.Columns.Add("WaterKPIMtsUnits", typeof(String));
            _EvolutionTable.Columns.Add("WaterKPIMoney", typeof(String));
            _EvolutionTable.Columns.Add("WaterKPIMoneyUnits", typeof(String));
            _EvolutionTable.Columns.Add("WaterCO2", typeof(String));
            _EvolutionTable.Columns.Add("WaterCO2Units", typeof(String));
            _EvolutionTable.Columns.Add("WaterCO2KPIMts", typeof(String));
            _EvolutionTable.Columns.Add("WaterCO2KPIMtsUnits", typeof(String));
            _EvolutionTable.Columns.Add("WaterCO2KPIMoney", typeof(String));
            _EvolutionTable.Columns.Add("WaterCO2KPIMoneyUnits", typeof(String));

            _EvolutionTable.Columns.Add("TotalCO2", typeof(String));
            _EvolutionTable.Columns.Add("TotalCO2Units", typeof(String));
            _EvolutionTable.Columns.Add("TotalCO2KPIMts", typeof(String));
            _EvolutionTable.Columns.Add("TotalCO2KPIMtsUnits", typeof(String));
            _EvolutionTable.Columns.Add("TotalCO2KPIMoney", typeof(String));
            _EvolutionTable.Columns.Add("TotalCO2KPIMoneyUnits", typeof(String));
        }
        private void LoadEvolutionTable()
        {
            DateTime _start, _end;
            DateTime.TryParse(hdnFrom.Value, out _start);
            DateTime.TryParse(hdnTo.Value, out _end);

            if (_start == DateTime.MinValue)
                _start = _Site.Start;
            if(_end == DateTime.MinValue)
                _end = DateTime.Now;

            Library.Objects.Metrics.Evolution _evolution = _Site.GetCO2Evolution(_start, _end, 1, Library.Objects.Auxiliaries.Units.TimeUnit.Units.Month);

            String _kpiMtsUnit = Resources.Data.KPIM2Unit;
            Double _mts = _Site.FloorSpace;
            Library.Objects.Auxiliaries.Units.Cost _cost = _Site.Cost;
            String _currency = _cost.Currency.Symbol + "100k";
            Double _costValue = _cost.Value / 100000;
            
            //Units
            _CO2Unit = _evolution.CO2Unit;
            _ElectricityUnit = _evolution.ElectricityUnit;
            _FuelUnit = _evolution.FuelUnit;
            _TransportUnit = _evolution.TransportUnit;
            _WasteUnit = _evolution.WasteUnit;
            _WaterUnit = _evolution.WaterUnit;

            //CO2 Units labels
            lblCO2MonthlyEvolutionElectricityCO2Units.Text = lblCO2MonthlyEvolutionFuelsCO2Units.Text =
            lblCO2MonthlyEvolutionTransportCO2Units.Text = lblCO2MonthlyEvolutionWasteCO2Units.Text =
            lblCO2MonthlyEvolutionWaterCO2Units.Text = lblCO2MonthlyEvolutionTotalCO2Units.Text =
            lblElectricityMonthlyEvolutionCO2Units.Text = lblFuelsMonthlyEvolutionCO2Units.Text =
            lblTransportMonthlyEvolutionCO2Units.Text = lblWasteMonthlyEvolutionCO2Units.Text =
            lblWaterMonthlyEvolutionCO2Units.Text = _CO2Unit.Symbol;

            //CO2 KPIs labels
            lblCO2MonthlyEvolutionElectricityCO2KPIMtsUnits.Text = lblCO2MonthlyEvolutionFuelsCO2KPIMtsUnits.Text =
            lblCO2MonthlyEvolutionTransportCO2KPIMtsUnits.Text = lblCO2MonthlyEvolutionWasteCO2KPIMtsUnits.Text =
            lblCO2MonthlyEvolutionWaterCO2KPIMtsUnits.Text = lblCO2MonthlyEvolutionTotalCO2KPIMtsUnits.Text =
            lblElectricityMonthlyEvolutionCO2KPIMtsUnits.Text = lblFuelsMonthlyEvolutionCO2KPIMtsUnits.Text =
            lblTransportMonthlyEvolutionCO2KPIMtsUnits.Text = lblWasteMonthlyEvolutionCO2KPIMtsUnits.Text =
            lblWaterMonthlyEvolutionCO2KPIMtsUnits.Text = "/" + _kpiMtsUnit; //_co2Unit + "/" + _kpiMtsUnit;

            lblCO2MonthlyEvolutionElectricityCO2KPIMoneyUnits.Text = lblCO2MonthlyEvolutionFuelsCO2KPIMoneyUnits.Text =
            lblCO2MonthlyEvolutionTransportCO2KPIMoneyUnits.Text = lblCO2MonthlyEvolutionWasteCO2KPIMoneyUnits.Text =
            lblCO2MonthlyEvolutionWaterCO2KPIMoneyUnits.Text = lblCO2MonthlyEvolutionTotalCO2KPIMoneyUnits.Text =
            lblElectricityMonthlyEvolutionCO2KPIMoneyUnits.Text = lblFuelsMonthlyEvolutionCO2KPIMoneyUnits.Text =
            lblTransportMonthlyEvolutionCO2KPIMoneyUnits.Text = lblWasteMonthlyEvolutionCO2KPIMoneyUnits.Text =
            lblWaterMonthlyEvolutionCO2KPIMoneyUnits.Text = "/" + _currency;  //_co2Unit + "/" + _currency;

            //Protocols Units labels
            String _electricityUnit, _fuelUnit, _transportUnit, _wasteUnit, _waterUnit;
            lblElectricityMonthlyEvolutionUnits.Text = _electricityUnit = _ElectricityUnit.Symbol;
            lblFuelsMonthlyEvolutionUnits.Text = _fuelUnit = _FuelUnit.Symbol;
            lblTransportMonthlyEvolutionUnits.Text = _transportUnit = _TransportUnit.Symbol;
            lblWasteMonthlyEvolutionUnits.Text = _wasteUnit = _WasteUnit.Symbol;
            lblWaterMonthlyEvolutionUnits.Text = _waterUnit = _WaterUnit.Symbol;
            
            //Protocols KPIs labels
            lblElectricityMonthlyEvolutionKPIMtsUnits.Text = _electricityUnit + "/" + _kpiMtsUnit;
            lblFuelsMonthlyEvolutionKPIMtsUnits.Text = _fuelUnit + "/" + _kpiMtsUnit;
            lblTransportMonthlyEvolutionKPIMtsUnits.Text = _transportUnit + "/" + _kpiMtsUnit;
            lblWasteMonthlyEvolutionKPIMtsUnits.Text = _wasteUnit + "/" + _kpiMtsUnit;
            lblWaterMonthlyEvolutionKPIMtsUnits.Text = _waterUnit + "/" + _kpiMtsUnit;
                        
            lblElectricityMonthlyEvolutionKPIMoneyUnits.Text = _electricityUnit + "/" + _currency;
            lblFuelsMonthlyEvolutionKPIMoneyUnits.Text = _fuelUnit + "/" + _currency;
            lblTransportMonthlyEvolutionKPIMoneyUnits.Text = _transportUnit + "/" + _currency;
            lblWasteMonthlyEvolutionKPIMoneyUnits.Text = _wasteUnit + "/" + _currency;
            lblWaterMonthlyEvolutionKPIMoneyUnits.Text = _waterUnit + "/" + _currency;
            
            
            //X-Axis Set-up
            _Months = new String[_evolution.Serie.Count];

            //Load Serie int Datatable
            int _count=0;
            foreach (var _item in _evolution.Serie.OrderBy(key => key.Key))
            {
                _count++;

                DataRow _dr = _EvolutionTable.NewRow();
                DateTime _month = (DateTime)_item.Key;

                _dr["Month"] = _month.Month + "-" + _month.Year;
                _Months[_count-1] = _dr["Month"].ToString();

                Library.Objects.Metrics.EvolutionPoint _point = (Library.Objects.Metrics.EvolutionPoint)_item.Value;

                //Electricity
                Double _sum = _point.Electricity.Sum;
                Double _sumCO2 = _point.Electricity.SumCO2;

                _dr["ElectricityUnit"] = WebUI.Common.RoundAndFormat(_sum).ToString();
                _dr["ElectricityKPIMts"] = WebUI.Common.RoundAndFormat(BuildKPI(_sum, _mts)).ToString();
                _dr["ElectricityKPIMoney"] = WebUI.Common.RoundAndFormat(BuildKPI(_sum, _costValue)).ToString();
                _dr["ElectricityCO2"] = WebUI.Common.RoundAndFormat(_sumCO2).ToString();
                _dr["ElectricityCO2KPIMts"] = WebUI.Common.RoundAndFormat(BuildKPI(_sumCO2, _mts)).ToString();
                _dr["ElectricityCO2KPIMoney"] = WebUI.Common.RoundAndFormat(BuildKPI(_sumCO2, _costValue)).ToString();

                _ElectricityTotal += _sum;
                _ElectricityTotalKPIMts += _sum/_mts;
                _ElectricityTotalKPIMoney += _sum/_costValue;
                _ElectricityTotalCO2 += _sumCO2;
                _ElectricityTotalCO2KPIMts += _sumCO2/_mts;
                _ElectricityTotalCO2KPIMoney += _sumCO2/_costValue;

                _MaxElectricity = _MaxElectricity < _sum ? _sum : _MaxElectricity;
                LoadChartEvolutionElectricityPoint(WebUI.Common.RoundAndFormat(_sum), _electricityUnit);

                //Fuels
                _sum = _point.Fuel.Sum;
                _sumCO2 = _point.Fuel.SumCO2;

                _dr["FuelsUnit"] = WebUI.Common.RoundAndFormat(_sum).ToString();
                _dr["FuelsKPIMts"] = WebUI.Common.RoundAndFormat(BuildKPI(_sum, _mts)).ToString();
                _dr["FuelsKPIMoney"] = WebUI.Common.RoundAndFormat(BuildKPI(_sum, _costValue)).ToString();
                _dr["FuelsCO2"] = WebUI.Common.RoundAndFormat(_sumCO2).ToString();
                _dr["FuelsCO2KPIMts"] = WebUI.Common.RoundAndFormat(BuildKPI(_sumCO2, _mts)).ToString();
                _dr["FuelsCO2KPIMoney"] = WebUI.Common.RoundAndFormat(BuildKPI(_sumCO2, _costValue)).ToString();
                
                _FuelTotal += _sum;
                _FuelTotalKPIMts += _sum / _mts;
                _FuelTotalKPIMoney += _sum / _costValue;
                _FuelTotalCO2 += _sumCO2;
                _FuelTotalCO2KPIMts += _sumCO2 / _mts;
                _FuelTotalCO2KPIMoney += _sumCO2 / _costValue;

                _MaxFuels = _MaxFuels < _sum ? _sum : _MaxFuels;
                LoadChartEvolutionFuelPoint(WebUI.Common.RoundAndFormat(_sum), _fuelUnit);

                //Transport
                _sum = _point.Transport.Sum;
                _sumCO2 = _point.Transport.SumCO2;

                _dr["TransportUnit"] = WebUI.Common.RoundAndFormat(_sum).ToString();
                _dr["TransportKPIMts"] = WebUI.Common.RoundAndFormat(BuildKPI(_sum, _mts)).ToString();
                _dr["TransportKPIMoney"] = WebUI.Common.RoundAndFormat(BuildKPI(_sum, _costValue)).ToString();
                _dr["TransportCO2"] = WebUI.Common.RoundAndFormat(_sumCO2).ToString();
                _dr["TransportCO2KPIMts"] = WebUI.Common.RoundAndFormat(BuildKPI(_sumCO2, _mts)).ToString();
                _dr["TransportCO2KPIMoney"] = WebUI.Common.RoundAndFormat(BuildKPI(_sumCO2, _costValue)).ToString();
                
                _TransportTotal += _sum;
                _TransportTotalKPIMts += _sum / _mts;
                _TransportTotalKPIMoney += _sum / _costValue;
                _TransportTotalCO2 += _sumCO2;
                _TransportTotalCO2KPIMts += _sumCO2 / _mts;
                _TransportTotalCO2KPIMoney += _sumCO2 / _costValue;

                _MaxTransport = _MaxTransport < _sum ? _sum : _MaxTransport;
                LoadChartEvolutionTransportPoint(WebUI.Common.RoundAndFormat(_sum), _transportUnit);

                //Waste
                _sum = _point.Waste.Sum;
                _sumCO2 = _point.Waste.SumCO2;

                _dr["WasteUnit"] = WebUI.Common.RoundAndFormat(_sum).ToString();
                _dr["WasteKPIMts"] = WebUI.Common.RoundAndFormat(BuildKPI(_sum, _mts)).ToString();
                _dr["WasteKPIMoney"] = WebUI.Common.RoundAndFormat(BuildKPI(_sum, _costValue)).ToString();
                _dr["WasteCO2"] = WebUI.Common.RoundAndFormat(_sumCO2).ToString();
                _dr["WasteCO2KPIMts"] = WebUI.Common.RoundAndFormat(BuildKPI(_sumCO2, _mts)).ToString();
                _dr["WasteCO2KPIMoney"] = WebUI.Common.RoundAndFormat(BuildKPI(_sumCO2, _costValue)).ToString();

                _WasteTotal += _sum;
                _WasteTotalKPIMts += _sum / _mts;
                _WasteTotalKPIMoney += _sum / _costValue;
                _WasteTotalCO2 += _sumCO2;
                _WasteTotalCO2KPIMts += _sumCO2 / _mts;
                _WasteTotalCO2KPIMoney += _sumCO2 / _costValue;

                _MaxWaste = _MaxWaste < _sum ? _sum : _MaxWaste;
                LoadChartEvolutionWastePoint(WebUI.Common.RoundAndFormat(_sum), _wasteUnit);

                //Water
                _sum = _point.Water.Sum;
                _sumCO2 = _point.Water.SumCO2;

                _dr["WaterUnit"] = WebUI.Common.RoundAndFormat(_sum).ToString();
                _dr["WaterKPIMts"] = WebUI.Common.RoundAndFormat(BuildKPI(_sum, _mts)).ToString();
                _dr["WaterKPIMoney"] = WebUI.Common.RoundAndFormat(BuildKPI(_sum, _costValue)).ToString();
                _dr["WaterCO2"] = WebUI.Common.RoundAndFormat(_sumCO2).ToString();
                _dr["WaterCO2KPIMts"] = WebUI.Common.RoundAndFormat(BuildKPI(_sumCO2, _mts)).ToString();
                _dr["WaterCO2KPIMoney"] = WebUI.Common.RoundAndFormat(BuildKPI(_sumCO2, _costValue)).ToString();

                _WaterTotal += _sum;
                _WaterTotalKPIMts += _sum / _mts;
                _WaterTotalKPIMoney += _sum / _costValue;
                _WaterTotalCO2 += _sumCO2;
                _WaterTotalCO2KPIMts += _sumCO2 / _mts;
                _WaterTotalCO2KPIMoney += _sumCO2 / _costValue;

                _MaxWater = _MaxWater < _sum ? _sum : _MaxWater;
                LoadChartEvolutionWaterPoint(WebUI.Common.RoundAndFormat(_sum), _waterUnit);

                //CO2
                _sumCO2 = _point.TotalCO2;
                _dr["TotalCO2"] = WebUI.Common.RoundAndFormat(_sumCO2).ToString();
                _dr["TotalCO2KPIMts"] = WebUI.Common.RoundAndFormat(BuildKPI(_sumCO2, _mts)).ToString();
                _dr["TotalCO2KPIMoney"] = WebUI.Common.RoundAndFormat(BuildKPI(_sumCO2, _costValue)).ToString();

                _CO2Total += _sumCO2;
                _CO2TotalKPIMts += _sumCO2 / _mts;
                _CO2TotalKPIMoney += _sumCO2 / _costValue;

                _MaxCO2 = _MaxCO2 < _sumCO2 ? _sumCO2 : _MaxCO2;
                LoadChartCO2EvolutionPoint(WebUI.Common.RoundAndFormat(_sumCO2), _CO2Unit.Symbol);

                //Add row to table
                _EvolutionTable.Rows.Add(_dr);
            }

            //x-axis
            LoadXAxis();

            LoadTargetZones();

            //Calculate Averages
            if(_count>0) 
            {
                _ElectricityTotalKPIMts /= _count;
                _FuelTotalKPIMts /= _count;
                _TransportTotalKPIMts /= _count;
                _WasteTotalKPIMts /= _count;
                _WaterTotalKPIMts /= _count;
                _ElectricityTotalKPIMoney /= _count;
                _FuelTotalKPIMoney /= _count;
                _TransportTotalKPIMoney /= _count;
                _WasteTotalKPIMoney /= _count;
                _WaterTotalKPIMoney /= _count;

                _ElectricityTotalCO2KPIMts /= _count;
                _FuelTotalCO2KPIMts /= _count;
                _TransportTotalCO2KPIMts /= _count;
                _WasteTotalCO2KPIMts /= _count;
                _WaterTotalCO2KPIMts /= _count;
                _CO2TotalKPIMts /= _count;
                _ElectricityTotalCO2KPIMoney /= _count;
                _FuelTotalCO2KPIMoney /= _count;
                _TransportTotalCO2KPIMoney /= _count;
                _WasteTotalCO2KPIMoney /= _count;
                _WaterTotalCO2KPIMoney /= _count;
                _CO2TotalKPIMoney /= _count; 
            }

            //Repeaters Bind
            rptCO2MonthlyEvolution.DataSource = _EvolutionTable;
            rptElectricityMonthlyEvolution.DataSource = _EvolutionTable;
            rptFuelsMonthlyEvolution.DataSource = _EvolutionTable;
            rptTransportMonthlyEvolution.DataSource = _EvolutionTable;
            rptWasteMonthlyEvolution.DataSource = _EvolutionTable;
            rptWaterMonthlyEvolution.DataSource = _EvolutionTable;
            
            rptCO2MonthlyEvolution.DataBind();
            rptElectricityMonthlyEvolution.DataBind();
            rptFuelsMonthlyEvolution.DataBind();
            rptTransportMonthlyEvolution.DataBind();
            rptWasteMonthlyEvolution.DataBind();
            rptWaterMonthlyEvolution.DataBind();

            //Table Totals
            LoadEvolutionFooters();

        }
        private void LoadEvolutionFooters()
        {
            //CO2 Evolution Table
            lblCO2MonthlyEvolutionFooterElectricityCO2.Text = WebUI.Common.RoundAndFormat(_ElectricityTotalCO2).ToString();
            lblCO2MonthlyEvolutionFooterElectricityCO2KPIMts.Text = WebUI.Common.RoundAndFormat(_ElectricityTotalCO2KPIMts).ToString();
            lblCO2MonthlyEvolutionFooterElectricityCO2KPIMoney.Text = WebUI.Common.RoundAndFormat(_ElectricityTotalCO2KPIMoney).ToString();

            lblCO2MonthlyEvolutionFooterFuelsCO2.Text = WebUI.Common.RoundAndFormat(_FuelTotalCO2).ToString();
            lblCO2MonthlyEvolutionFooterFuelsCO2KPIMts.Text = WebUI.Common.RoundAndFormat(_FuelTotalCO2KPIMts).ToString();
            lblCO2MonthlyEvolutionFooterFuelsCO2KPIMoney.Text = WebUI.Common.RoundAndFormat(_FuelTotalCO2KPIMoney).ToString();

            lblCO2MonthlyEvolutionFooterTransportCO2.Text = WebUI.Common.RoundAndFormat(_TransportTotalCO2).ToString();
            lblCO2MonthlyEvolutionFooterTransportCO2KPIMts.Text = WebUI.Common.RoundAndFormat(_TransportTotalCO2KPIMts).ToString();
            lblCO2MonthlyEvolutionFooterTransportCO2KPIMoney.Text = WebUI.Common.RoundAndFormat(_TransportTotalCO2KPIMoney).ToString();

            lblCO2MonthlyEvolutionFooterWasteCO2.Text = WebUI.Common.RoundAndFormat(_WasteTotalCO2).ToString();
            lblCO2MonthlyEvolutionFooterWasteCO2KPIMts.Text = WebUI.Common.RoundAndFormat(_WasteTotalCO2KPIMts).ToString();
            lblCO2MonthlyEvolutionFooterWasteCO2KPIMoney.Text = WebUI.Common.RoundAndFormat(_WasteTotalCO2KPIMoney).ToString();

            lblCO2MonthlyEvolutionFooterWaterCO2.Text = WebUI.Common.RoundAndFormat(_WaterTotalCO2).ToString();
            lblCO2MonthlyEvolutionFooterWaterCO2KPIMts.Text = WebUI.Common.RoundAndFormat(_WaterTotalCO2KPIMts).ToString();
            lblCO2MonthlyEvolutionFooterWaterCO2KPIMoney.Text = WebUI.Common.RoundAndFormat(_WaterTotalCO2KPIMoney).ToString();

            lblCO2MonthlyEvolutionFooterTotalCO2.Text = WebUI.Common.RoundAndFormat(_CO2Total).ToString();
            lblCO2MonthlyEvolutionFooterTotalCO2KPIMts.Text = WebUI.Common.RoundAndFormat(_CO2TotalKPIMts).ToString();
            lblCO2MonthlyEvolutionFooterTotalCO2KPIMoney.Text = WebUI.Common.RoundAndFormat(_CO2TotalKPIMoney).ToString();

            //Electricity
            lblElectricityMonthlyEvolutionFooterUnit.Text = WebUI.Common.RoundAndFormat(_ElectricityTotal).ToString();
            lblElectricityMonthlyEvolutionFooterKPIMts.Text = WebUI.Common.RoundAndFormat(_ElectricityTotalKPIMts).ToString();
            lblElectricityMonthlyEvolutionFooterKPIMoney.Text = WebUI.Common.RoundAndFormat(_ElectricityTotalKPIMoney).ToString();

            lblElectricityMonthlyEvolutionFooterCO2.Text = WebUI.Common.RoundAndFormat(_ElectricityTotalCO2).ToString();
            lblElectricityMonthlyEvolutionFooterCO2KPIMts.Text = WebUI.Common.RoundAndFormat(_ElectricityTotalCO2KPIMts).ToString();
            lblElectricityMonthlyEvolutionFooterCO2KPIMoney.Text = WebUI.Common.RoundAndFormat(_ElectricityTotalCO2KPIMoney).ToString();

            //Fuels
            lblFuelsMonthlyEvolutionFooterUnit.Text = WebUI.Common.RoundAndFormat(_FuelTotal).ToString();
            lblFuelsMonthlyEvolutionFooterKPIMts.Text = WebUI.Common.RoundAndFormat(_FuelTotalKPIMts).ToString();
            lblFuelsMonthlyEvolutionFooterKPIMoney.Text = WebUI.Common.RoundAndFormat(_FuelTotalKPIMoney).ToString();

            lblFuelsMonthlyEvolutionFooterCO2.Text = WebUI.Common.RoundAndFormat(_FuelTotalCO2).ToString();
            lblFuelsMonthlyEvolutionFooterCO2KPIMts.Text = WebUI.Common.RoundAndFormat(_FuelTotalCO2KPIMts).ToString();
            lblFuelsMonthlyEvolutionFooterCO2KPIMoney.Text = WebUI.Common.RoundAndFormat(_FuelTotalCO2KPIMoney).ToString();

            //Transport
            lblTransportMonthlyEvolutionFooterUnit.Text = WebUI.Common.RoundAndFormat(_TransportTotal).ToString();
            lblTransportMonthlyEvolutionFooterKPIMts.Text = WebUI.Common.RoundAndFormat(_TransportTotalKPIMts).ToString();
            lblTransportMonthlyEvolutionFooterKPIMoney.Text = WebUI.Common.RoundAndFormat(_TransportTotalKPIMoney).ToString();

            lblTransportMonthlyEvolutionFooterCO2.Text = WebUI.Common.RoundAndFormat(_TransportTotalCO2).ToString();
            lblTransportMonthlyEvolutionFooterCO2KPIMts.Text = WebUI.Common.RoundAndFormat(_TransportTotalCO2KPIMts).ToString();
            lblTransportMonthlyEvolutionFooterCO2KPIMoney.Text = WebUI.Common.RoundAndFormat(_TransportTotalCO2KPIMoney).ToString();

            //Waste
            lblWasteMonthlyEvolutionFooterUnit.Text = WebUI.Common.RoundAndFormat(_WasteTotal).ToString();
            lblWasteMonthlyEvolutionFooterKPIMts.Text = WebUI.Common.RoundAndFormat(_WasteTotalKPIMts).ToString();
            lblWasteMonthlyEvolutionFooterKPIMoney.Text = WebUI.Common.RoundAndFormat(_WasteTotalKPIMoney).ToString();

            lblWasteMonthlyEvolutionFooterCO2.Text = WebUI.Common.RoundAndFormat(_WasteTotalCO2).ToString();
            lblWasteMonthlyEvolutionFooterCO2KPIMts.Text = WebUI.Common.RoundAndFormat(_WasteTotalCO2KPIMts).ToString();
            lblWasteMonthlyEvolutionFooterCO2KPIMoney.Text = WebUI.Common.RoundAndFormat(_WasteTotalCO2KPIMoney).ToString();

            //Water
            lblWaterMonthlyEvolutionFooterUnit.Text = WebUI.Common.RoundAndFormat(_WaterTotal).ToString();
            lblWaterMonthlyEvolutionFooterKPIMts.Text = WebUI.Common.RoundAndFormat(_WaterTotalKPIMts).ToString();
            lblWaterMonthlyEvolutionFooterKPIMoney.Text = WebUI.Common.RoundAndFormat(_WaterTotalKPIMoney).ToString();

            lblWaterMonthlyEvolutionFooterCO2.Text = WebUI.Common.RoundAndFormat(_WaterTotalCO2).ToString();
            lblWaterMonthlyEvolutionFooterCO2KPIMts.Text = WebUI.Common.RoundAndFormat(_WaterTotalCO2KPIMts).ToString();
            lblWaterMonthlyEvolutionFooterCO2KPIMoney.Text = WebUI.Common.RoundAndFormat(_WaterTotalCO2KPIMoney).ToString();
        }
        private void LoadXAxis()
        {
            int x;

            radChartCO2Evolution.PlotArea.XAxis.AutoScale = false;
            radChartCO2Evolution.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = 90;
            radChartCO2Evolution.PlotArea.XAxis.AddRange(1, _Months.Length, 1);
            radChartElectricityEvolution.PlotArea.XAxis.AutoScale = false;
            radChartElectricityEvolution.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = 90;
            radChartElectricityEvolution.PlotArea.XAxis.AddRange(1, _Months.Length, 1);
            radChartFuelsEvolution.PlotArea.XAxis.AutoScale = false;
            radChartFuelsEvolution.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = 90;
            radChartFuelsEvolution.PlotArea.XAxis.AddRange(1, _Months.Length, 1);
            radChartTransportEvolution.PlotArea.XAxis.AutoScale = false;
            radChartTransportEvolution.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = 90;
            radChartTransportEvolution.PlotArea.XAxis.AddRange(1, _Months.Length, 1);
            radChartWasteEvolution.PlotArea.XAxis.AutoScale = false;
            radChartWasteEvolution.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = 90;
            radChartWasteEvolution.PlotArea.XAxis.AddRange(1, _Months.Length, 1);
            radChartWaterEvolution.PlotArea.XAxis.AutoScale = false;
            radChartWaterEvolution.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = 90;
            radChartWaterEvolution.PlotArea.XAxis.AddRange(1, _Months.Length, 1);

            for (x = 0; x < _Months.Length; x++)
            {
                radChartCO2Evolution.PlotArea.XAxis.Items[x].TextBlock.Text = _Months[x];
                radChartCO2Evolution.PlotArea.XAxis.Items[x].TextBlock.Text = _Months[x];
                radChartElectricityEvolution.PlotArea.XAxis.Items[x].TextBlock.Text = _Months[x];
                radChartFuelsEvolution.PlotArea.XAxis.Items[x].TextBlock.Text = _Months[x];
                radChartTransportEvolution.PlotArea.XAxis.Items[x].TextBlock.Text = _Months[x];
                radChartWasteEvolution.PlotArea.XAxis.Items[x].TextBlock.Text = _Months[x];
                radChartWaterEvolution.PlotArea.XAxis.Items[x].TextBlock.Text = _Months[x];
            }
        }
        private Double BuildKPI(Double valueNumerator, Double valueDenominator)
        {
            return (valueDenominator > 0 ? valueNumerator / valueDenominator : 0);
        }

        #endregion

        #region Charts

        private void LoadPies()
        {
            LoadPieCO2ByProtocol();
            LoadPieFuelsTypes();
            LoadPieTransportTypes();
            LoadPieWasteTypes();
        }
        private void LoadPieCO2ByProtocol()
        {
            radChartCO2ByProtocol.Series.Clear();

            Library.Objects.Metrics.MetricComposite _values = _Site.GetCO2StatisticsByProtocols();
            radChartCO2ByProtocol.ChartTitle.TextBlock.Text = "";

            //Create serie
            Telerik.Charting.ChartSeries _series = new Telerik.Charting.ChartSeries("", Telerik.Charting.ChartSeriesType.Pie);
            WebUI.Common.ChartPieSerieStyle(radChartCO2ByProtocol, _series);
            Telerik.Charting.ChartSeriesItemsCollection _points = new Telerik.Charting.ChartSeriesItemsCollection();

            Double _share;
            String _unitSymbol = _values.Unit.Symbol; String _legend = String.Empty;
            foreach (Library.Objects.Metrics.MetricComponent _item in _values.Components.Values)
            {
                //Set value
                _share = WebUI.Common.RoundAndFormat(_item.Share);
                _legend = _item.Name + ": " + _share.ToString() + "% [" + CSI.WebUI.Common.RoundAndFormat(_item.Sum).ToString() + " " + _unitSymbol + "]";
                Telerik.Charting.ChartSeriesItem _point = new Telerik.Charting.ChartSeriesItem(_share, _legend);
                WebUI.Common.ChartPieSerieItemStyle(_point);
                _point.ActiveRegion.Tooltip = _legend;
                _points.Add(_point);
            }
            _series.AddItem(_points);
            _series.Appearance.LegendDisplayMode = Telerik.Charting.ChartSeriesLegendDisplayMode.ItemLabels;
            radChartCO2ByProtocol.Series.Add(_series);
        }
        private void LoadPieFuelsTypes()
        {
            RadChartFuelsByTypes.Series.Clear();
            Library.Objects.Metrics.MetricComposite _values = _Site.GetFuelsStatisticsByTypes();

            RadChartFuelsByTypes.ChartTitle.TextBlock.Text = "";

            //Create serie
            Telerik.Charting.ChartSeries _series = new Telerik.Charting.ChartSeries("", Telerik.Charting.ChartSeriesType.Pie);
            WebUI.Common.ChartPieSerieStyle(RadChartFuelsByTypes, _series);
            Telerik.Charting.ChartSeriesItemsCollection _points = new Telerik.Charting.ChartSeriesItemsCollection();

            Double _share;
            String _unitSymbol = _values.Unit.Symbol; String _legend = String.Empty;
            foreach (Library.Objects.Metrics.MetricComponent _item in _values.Components.Values)
            {
                //Set value
                _share = WebUI.Common.RoundAndFormat(_item.Share);
                _legend = _item.Name + ": " + _share.ToString() + "% [" + CSI.WebUI.Common.RoundAndFormat(_item.Sum).ToString() + " " + _unitSymbol + "]";
                Telerik.Charting.ChartSeriesItem _point = new Telerik.Charting.ChartSeriesItem(_share, _legend);
                WebUI.Common.ChartPieSerieItemStyle(_point);
                _point.ActiveRegion.Tooltip = _legend;
                _points.Add(_point);
            }
            _series.AddItem(_points);
            _series.Appearance.LegendDisplayMode = Telerik.Charting.ChartSeriesLegendDisplayMode.ItemLabels;
            RadChartFuelsByTypes.Series.Add(_series);
        }
        private void LoadPieTransportTypes()
        {
            RadChartTransportByTypes.Series.Clear();
            Library.Objects.Metrics.MetricComposite _values = _Site.GetTransportStatisticsByTypes();

            RadChartTransportByTypes.ChartTitle.TextBlock.Text = "";

            //Create serie
            Telerik.Charting.ChartSeries _series = new Telerik.Charting.ChartSeries("", Telerik.Charting.ChartSeriesType.Pie);
            WebUI.Common.ChartPieSerieStyle(RadChartTransportByTypes, _series);
            Telerik.Charting.ChartSeriesItemsCollection _points = new Telerik.Charting.ChartSeriesItemsCollection();

            Double _share;
            String _unitSymbol = _values.Unit.Symbol; String _legend = String.Empty;
            foreach (Library.Objects.Metrics.MetricComponent _item in _values.Components.Values)
            {
                //Set value
                _share = WebUI.Common.RoundAndFormat(_item.Share);
                _legend = _item.Name + ": " + _share.ToString() + "% [" + CSI.WebUI.Common.RoundAndFormat(_item.Sum).ToString() + " " + _unitSymbol + "]";
                Telerik.Charting.ChartSeriesItem _point = new Telerik.Charting.ChartSeriesItem(_share, _legend);
                WebUI.Common.ChartPieSerieItemStyle(_point);
                _point.ActiveRegion.Tooltip = _legend;
                _points.Add(_point);
            }
            _series.AddItem(_points);
            _series.Appearance.LegendDisplayMode = Telerik.Charting.ChartSeriesLegendDisplayMode.ItemLabels;
            RadChartTransportByTypes.Series.Add(_series);
        }
        private void LoadPieWasteTypes()
        {
            RadChartWasteByTypes.Series.Clear();
            Library.Objects.Metrics.MetricComposite _values = _Site.GetWasteStatisticsByTypes();

            RadChartWasteByTypes.ChartTitle.TextBlock.Text = "";

            //Create serie
            Telerik.Charting.ChartSeries _series = new Telerik.Charting.ChartSeries("", Telerik.Charting.ChartSeriesType.Pie);
            WebUI.Common.ChartPieSerieStyle(RadChartWasteByTypes, _series);
            Telerik.Charting.ChartSeriesItemsCollection _points = new Telerik.Charting.ChartSeriesItemsCollection();

            Double _share;
            String _unitSymbol = _values.Unit.Symbol; String _legend = String.Empty;
            foreach (Library.Objects.Metrics.MetricComponent _item in _values.Components.Values)
            {
                //Set value
                _share = WebUI.Common.RoundAndFormat(_item.Share);
                _legend = _item.Name + ": " + _share.ToString() + "% [" + CSI.WebUI.Common.RoundAndFormat(_item.Sum).ToString() + " " + _unitSymbol + "]";
                Telerik.Charting.ChartSeriesItem _point = new Telerik.Charting.ChartSeriesItem(_share, _legend);
                WebUI.Common.ChartPieSerieItemStyle(_point);
                _point.ActiveRegion.Tooltip = _legend;
                _points.Add(_point);
            }
            _series.AddItem(_points);
            _series.Appearance.LegendDisplayMode = Telerik.Charting.ChartSeriesLegendDisplayMode.ItemLabels;
            RadChartWasteByTypes.Series.Add(_series);
        }

        private void SetChartsEvolution()
        {
            SetChartCO2Evolution();
            SetChartElectricityEvolution();
            SetChartFuelEvolution();
            SetChartTransportEvolution();
            SetChartWasteEvolution();
            SetChartWaterEvolution();
        }
        private void SetChartCO2Evolution()
        {
            radChartCO2Evolution.ChartTitle.TextBlock.Text = Resources.Data.CO2Generation;
            Telerik.Charting.ChartSeries _series = new Telerik.Charting.ChartSeries(Resources.Data.CO2Generation, Telerik.Charting.ChartSeriesType.Bar);
            
            WebUI.Common.ChartBarSerieStyle(_series);
            radChartCO2Evolution.Series.Add(_series);
        }
        private void SetChartElectricityEvolution()
        {
            radChartElectricityEvolution.ChartTitle.TextBlock.Text = Resources.Data.ChartElectricitySumMonthly;
            Telerik.Charting.ChartSeries _series = new Telerik.Charting.ChartSeries(Resources.Data.ChartElectricitySumMonthly, Telerik.Charting.ChartSeriesType.Bar);

            WebUI.Common.ChartBarSerieStyle(_series);
            radChartElectricityEvolution.Series.Add(_series);
                       
        }
        private void SetChartFuelEvolution()
        {
            radChartFuelsEvolution.ChartTitle.TextBlock.Text = Resources.Data.ChartFuelSumMonthly;
            Telerik.Charting.ChartSeries _series = new Telerik.Charting.ChartSeries(Resources.Data.ChartFuelSumMonthly, Telerik.Charting.ChartSeriesType.Bar);

            WebUI.Common.ChartBarSerieStyle(_series);
            radChartFuelsEvolution.Series.Add(_series);
        }
        private void SetChartTransportEvolution()
        {
            radChartTransportEvolution.ChartTitle.TextBlock.Text = Resources.Data.ChartTransportSumMonthly;
            Telerik.Charting.ChartSeries _series = new Telerik.Charting.ChartSeries(Resources.Data.ChartTransportSumMonthly, Telerik.Charting.ChartSeriesType.Bar);

            WebUI.Common.ChartBarSerieStyle(_series);
            radChartTransportEvolution.Series.Add(_series);

        }
        private void SetChartWasteEvolution()
        {
            radChartWasteEvolution.ChartTitle.TextBlock.Text = Resources.Data.ChartWasteSumMonthly;
            Telerik.Charting.ChartSeries _series = new Telerik.Charting.ChartSeries(Resources.Data.ChartWasteSumMonthly, Telerik.Charting.ChartSeriesType.Bar);

            WebUI.Common.ChartBarSerieStyle(_series);
            radChartWasteEvolution.Series.Add(_series);
        }
        private void SetChartWaterEvolution()
        {
            radChartWaterEvolution.ChartTitle.TextBlock.Text = Resources.Data.ChartWaterSumMonthly;
            Telerik.Charting.ChartSeries _series = new Telerik.Charting.ChartSeries(Resources.Data.ChartWaterSumMonthly, Telerik.Charting.ChartSeriesType.Bar);

            WebUI.Common.ChartBarSerieStyle(_series);
            radChartWaterEvolution.Series.Add(_series);
        }

        private void LoadChartCO2EvolutionPoint(Double value, String unit)
        {
            Double _value = WebUI.Common.RoundAndFormat(value);
            String _tooltip = value.ToString() + " " + unit;
            
            Telerik.Charting.ChartSeriesItem _point = new Telerik.Charting.ChartSeriesItem(_value);
            WebUI.Common.ChartBarSerieItemStyle(_point, _Targets.TotalCO2);
            _point.ActiveRegion.Tooltip = _tooltip;
            radChartCO2Evolution.Series[0].Items.Add(_point);
        }
        private void LoadChartEvolutionElectricityPoint(Double value, String unit)
        {
            Double _value = WebUI.Common.RoundAndFormat(value);
            String _tooltip = value.ToString() + " " + unit;

            Telerik.Charting.ChartSeriesItem _point = new Telerik.Charting.ChartSeriesItem(_value);
            WebUI.Common.ChartBarSerieItemStyle(_point, _Targets.WaterUnit.ToUnit(_Targets.ElectricityConsumption, _ElectricityUnit));
            _point.ActiveRegion.Tooltip = _tooltip;
            radChartElectricityEvolution.Series[0].Items.Add(_point);
        }
        private void LoadChartEvolutionFuelPoint(Double value, String unit)
        {
            Double _value = WebUI.Common.RoundAndFormat(value);
            String _tooltip = value.ToString() + " " + unit;

            Telerik.Charting.ChartSeriesItem _point = new Telerik.Charting.ChartSeriesItem(_value);
            WebUI.Common.ChartBarSerieItemStyle(_point, _Targets.WaterUnit.ToUnit(_Targets.FuelConsumption, _FuelUnit));
            _point.ActiveRegion.Tooltip = _tooltip;
            radChartFuelsEvolution.Series[0].Items.Add(_point);
        }
        private void LoadChartEvolutionTransportPoint(Double value, String unit)
        {
            Double _value = WebUI.Common.RoundAndFormat(value);
            String _tooltip = value.ToString() + " " + unit;

            Telerik.Charting.ChartSeriesItem _point = new Telerik.Charting.ChartSeriesItem(_value);
            WebUI.Common.ChartBarSerieItemStyle(_point, _Targets.WaterUnit.ToUnit(_Targets.TransportConsumption, _TransportUnit));
            _point.ActiveRegion.Tooltip = _tooltip;
            radChartTransportEvolution.Series[0].Items.Add(_point);
        }
        private void LoadChartEvolutionWastePoint(Double value, String unit)
        {
            Double _value = WebUI.Common.RoundAndFormat(value);
            String _tooltip = value.ToString() + " " + unit;

            Telerik.Charting.ChartSeriesItem _point = new Telerik.Charting.ChartSeriesItem(_value);
            WebUI.Common.ChartBarSerieItemStyle(_point, _Targets.WaterUnit.ToUnit(_Targets.WasteConsumption, _WasteUnit));
            _point.ActiveRegion.Tooltip = _tooltip;
            radChartWasteEvolution.Series[0].Items.Add(_point);
        }
        private void LoadChartEvolutionWaterPoint(Double value, String unit)
        {
            Double _value = WebUI.Common.RoundAndFormat(value);
            String _tooltip = value.ToString() + " " + unit;

            Telerik.Charting.ChartSeriesItem _point = new Telerik.Charting.ChartSeriesItem(_value);
            WebUI.Common.ChartBarSerieItemStyle(_point, _Targets.WaterUnit.ToUnit(_Targets.WaterConsumption, _WaterUnit));
            _point.ActiveRegion.Tooltip = _tooltip;
            radChartWaterEvolution.Series[0].Items.Add(_point);
        }
                
        #endregion

        #region Meters

        private void LoadMeters()
        {
            rptElectricityMeters.DataSource = _Site.ElectricityMeters.Values;
            rptElectricityMeters.DataBind();

            rptFuelsMeters.DataSource = _Site.FuelMeters.Values;
            rptFuelsMeters.DataBind();

            rptTransportMeters.DataSource = _Site.TransportMeters.Values;
            rptTransportMeters.DataBind();

            rptWasteMeters.DataSource = _Site.WasteMeters.Values;
            rptWasteMeters.DataBind();

            rptWaterMeters.DataSource = _Site.WaterMeters.Values;
            rptWaterMeters.DataBind();
        }

        #endregion
        
        #region Targets
        
        private void LoadTargets()
        {
            Library.Objects.Auxiliaries.Units.Cost _cost = _Site.Cost;
            Double _costValue = _cost.Value / 100000;
            Double _mts = _Site.FloorSpace;

            lblElectricityTargetsConsumption.Text = _Targets.ElectricityConsumption.ToString() + " " + _Targets.ElectricityUnit.Symbol;
            lblElectricityTargetsCO2.Text =  _Targets.ElectricityCO2.ToString();
            lblElectricityTargetsConsumptionPKIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _Targets.ElectricityConsumption / _costValue : 0)).ToString();
            lblElectricityTargetsConsumptionPKIMts.Text = WebUI.Common.RoundAndFormat((Double)(_mts > 0 ? _Targets.ElectricityConsumption / _mts : 0)).ToString();
            lblElectricityTargetsCO2PKIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _Targets.ElectricityCO2 / _costValue : 0)).ToString();
            lblElectricityTargetsCO2PKIMts.Text = WebUI.Common.RoundAndFormat((Double)(_mts > 0 ? _Targets.ElectricityCO2 / _mts : 0)).ToString();

            lblFuelsTargetsConsumption.Text = _Targets.FuelConsumption.ToString() + " " + _Targets.FuelUnit.Symbol;
            lblFuelsTargetsCO2.Text = _Targets.FuelCO2.ToString();
            lblFuelsTargetsConsumptionPKIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _Targets.FuelConsumption / _costValue : 0)).ToString();
            lblFuelsTargetsConsumptionPKIMts.Text = WebUI.Common.RoundAndFormat((Double)(_mts > 0 ? _Targets.FuelConsumption / _mts : 0)).ToString();
            lblFuelsTargetsCO2PKIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _Targets.FuelCO2 / _costValue : 0)).ToString();
            lblFuelsTargetsCO2PKIMts.Text = WebUI.Common.RoundAndFormat((Double)(_mts > 0 ? _Targets.FuelCO2 / _mts : 0)).ToString();

            lblTransportTargetsConsumption.Text = _Targets.TransportConsumption.ToString() + " " + _Targets.TransportUnit.Symbol;
            lblTransportTargetsCO2.Text = _Targets.TransportCO2.ToString();
            lblTransportTargetsConsumptionPKIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _Targets.TransportConsumption / _costValue : 0)).ToString();
            lblTransportTargetsConsumptionPKIMts.Text = WebUI.Common.RoundAndFormat((Double)(_mts > 0 ? _Targets.TransportConsumption / _mts : 0)).ToString();
            lblTransportTargetsCO2PKIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _Targets.TransportCO2 / _costValue : 0)).ToString();
            lblTransportTargetsCO2PKIMts.Text = WebUI.Common.RoundAndFormat((Double)(_mts > 0 ? _Targets.TransportCO2 / _mts : 0)).ToString();

            lblWasteTargetsConsumption.Text = _Targets.WasteConsumption.ToString() + " " + _Targets.WasteUnit.Symbol;
            lblWasteTargetsCO2.Text = _Targets.WasteCO2.ToString();
            lblWasteTargetsConsumptionPKIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _Targets.WasteConsumption / _costValue : 0)).ToString();
            lblWasteTargetsConsumptionPKIMts.Text = WebUI.Common.RoundAndFormat((Double)(_mts > 0 ? _Targets.WasteConsumption / _mts : 0)).ToString();
            lblWasteTargetsCO2PKIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _Targets.WasteCO2 / _costValue : 0)).ToString();
            lblWasteTargetsCO2PKIMts.Text = WebUI.Common.RoundAndFormat((Double)(_mts > 0 ? _Targets.WasteCO2 / _mts : 0)).ToString();

            lblWaterTargetsConsumption.Text = _Targets.WaterConsumption.ToString() + " " + _Targets.WaterUnit.Symbol;
            lblWaterTargetsCO2.Text = _Targets.WaterCO2.ToString();
            lblWaterTargetsConsumptionPKIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _Targets.WaterConsumption / _costValue : 0)).ToString();
            lblWaterTargetsConsumptionPKIMts.Text = WebUI.Common.RoundAndFormat((Double)(_mts > 0 ? _Targets.WaterConsumption / _mts : 0)).ToString();
            lblWaterTargetsCO2PKIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _Targets.WaterCO2 / _costValue : 0)).ToString();
            lblWaterTargetsCO2PKIMts.Text = WebUI.Common.RoundAndFormat((Double)(_mts > 0 ? _Targets.WaterCO2 / _mts : 0)).ToString();

            lblCO2Targets.Text = _Targets.TotalCO2.ToString();
            lblCO2TargetsPKIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _Targets.TotalCO2 / _costValue : 0)).ToString();
            lblCO2TargetsPKIMts.Text = WebUI.Common.RoundAndFormat((Double)(_mts > 0 ? _Targets.TotalCO2 / _mts : 0)).ToString();
        }
        private void LoadTargetZones()
        {
            Telerik.Charting.ChartMarkedZone _zone;
            Double _y;

            //Electricity
            if (_Targets.ElectricityConsumption > 0 && _MaxElectricity>0)
            {
                _y = _Targets.ElectricityUnit.ToUnit(_Targets.ElectricityConsumption, _ElectricityUnit);
                _zone = new Telerik.Charting.ChartMarkedZone();

                _zone = new Telerik.Charting.ChartMarkedZone();
                _zone.ValueStartY = _y;
                _zone.ValueEndY = _MaxElectricity + _MaxElectricity * 0.1;
                WebUI.Common.ChartZoneStyle(_zone);
                radChartElectricityEvolution.PlotArea.MarkedZones.Add(_zone);
            }


            //Fuels
            if (_Targets.FuelConsumption > 0 && _MaxFuels > 0)
            {
                _y = _Targets.FuelUnit.ToUnit(_Targets.FuelConsumption, _FuelUnit); 
                _zone = new Telerik.Charting.ChartMarkedZone();

                _zone = new Telerik.Charting.ChartMarkedZone();
                _zone.ValueStartY = _y;
                _zone.ValueEndY = _MaxFuels + _MaxFuels * 0.1;
                WebUI.Common.ChartZoneStyle(_zone);
                radChartFuelsEvolution.PlotArea.MarkedZones.Add(_zone);
            }


            //Transport
            if (_Targets.TransportConsumption > 0 && _MaxTransport > 0)
            {
                _y = _Targets.TransportUnit.ToUnit(_Targets.TransportConsumption, _TransportUnit);
                _zone = new Telerik.Charting.ChartMarkedZone();

                _zone = new Telerik.Charting.ChartMarkedZone();
                _zone.ValueStartY = _y;
                _zone.ValueEndY = _MaxTransport + _MaxTransport * 0.1;
                WebUI.Common.ChartZoneStyle(_zone);
                radChartTransportEvolution.PlotArea.MarkedZones.Add(_zone);
            }

            //Waste
            if (_Targets.WasteConsumption > 0 && _MaxWaste > 0)
            {
                _y = _Targets.WasteUnit.ToUnit(_Targets.WasteConsumption, _WasteUnit);
                _zone = new Telerik.Charting.ChartMarkedZone();

                _zone = new Telerik.Charting.ChartMarkedZone();
                _zone.ValueStartY = _y;
                _zone.ValueEndY = _MaxWaste + _MaxWaste * 0.1;
                WebUI.Common.ChartZoneStyle(_zone);
                radChartWasteEvolution.PlotArea.MarkedZones.Add(_zone);
            }

            //Water
            if (_Targets.WaterConsumption > 0 && _MaxWater > 0)
            {
                _y = _Targets.WaterUnit.ToUnit(_Targets.WaterConsumption, _WaterUnit);
                _zone = new Telerik.Charting.ChartMarkedZone();

                _zone = new Telerik.Charting.ChartMarkedZone();
                _zone.ValueStartY = _y;
                _zone.ValueEndY = _MaxWater + _MaxWater * 0.1;
                WebUI.Common.ChartZoneStyle(_zone);
                radChartWaterEvolution.PlotArea.MarkedZones.Add(_zone);
            }
        }

        #endregion


        #endregion

        #region Events

        void rptCO2MonthlyEvolution_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView _dr = (DataRowView)e.Item.DataItem;

                Label _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionMonth");
                _lbl.Text = _dr["Month"].ToString();

                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionElectricityCO2");
                _lbl.Text = _dr["ElectricityCO2"].ToString();
                if ((Convert.ToDouble(_lbl.Text) > _Targets.TotalCO2) && _Targets.TotalCO2 > 0)
                    _lbl.Style.Add("Color", "Red");
                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionElectricityCO2KPIMts");
                _lbl.Text = _dr["ElectricityCO2KPIMts"].ToString();
                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionElectricityCO2KPIMoney");
                _lbl.Text = _dr["ElectricityCO2KPIMoney"].ToString();

                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionFuelsCO2");
                _lbl.Text = _dr["FuelsCO2"].ToString();
                if ((Convert.ToDouble(_lbl.Text) > _Targets.FuelCO2))
                    _lbl.Style.Add("Color", "Red");
                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionFuelsCO2KPIMts");
                _lbl.Text = _dr["FuelsCO2KPIMts"].ToString();
                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionFuelsCO2KPIMoney");
                _lbl.Text = _dr["FuelsCO2KPIMoney"].ToString();

                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionTransportCO2");
                _lbl.Text = _dr["TransportCO2"].ToString();
                if ((Convert.ToDouble(_lbl.Text) > _Targets.TransportCO2))
                    _lbl.Style.Add("Color", "Red");
                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionTransportCO2KPIMts");
                _lbl.Text = _dr["TransportCO2KPIMts"].ToString();
                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionTransportCO2KPIMoney");
                _lbl.Text = _dr["TransportCO2KPIMoney"].ToString();

                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionWasteCO2");
                _lbl.Text = _dr["WasteCO2"].ToString();
                if ((Convert.ToDouble(_lbl.Text) > _Targets.WaterCO2))
                    _lbl.Style.Add("Color", "Red");
                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionWasteCO2KPIMts");
                _lbl.Text = _dr["WasteCO2KPIMts"].ToString();
                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionWasteCO2KPIMoney");
                _lbl.Text = _dr["WasteCO2KPIMoney"].ToString();

                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionWaterCO2");
                _lbl.Text = _dr["WaterCO2"].ToString();
                if ((Convert.ToDouble(_lbl.Text) > _Targets.WaterCO2))
                    _lbl.Style.Add("Color", "Red");
                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionWaterCO2KPIMts");
                _lbl.Text = _dr["WaterCO2KPIMts"].ToString();
                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionWaterCO2KPIMoney");
                _lbl.Text = _dr["WaterCO2KPIMoney"].ToString();

                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionTotalCO2");
                _lbl.Text = _dr["TotalCO2"].ToString();
                if ((Convert.ToDouble(_lbl.Text) > _Targets.TotalCO2))
                    _lbl.Style.Add("Color", "Red");
                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionTotalCO2KPIMts");
                _lbl.Text = _dr["TotalCO2KPIMts"].ToString();
                _lbl = (Label)e.Item.FindControl("lblCO2MonthlyEvolutionTotalCO2KPIMoney");
                _lbl.Text = _dr["TotalCO2KPIMoney"].ToString();
            }
        }
        void rptWaterMonthlyEvolution_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView _dr = (DataRowView)e.Item.DataItem;

                Label _lbl = (Label)e.Item.FindControl("lblWaterMonthlyEvolutionMonth");
                _lbl.Text = _dr["Month"].ToString();

                _lbl = (Label)e.Item.FindControl("lblWaterMonthlyEvolutionUnit");
                _lbl.Text = _dr["WaterUnit"].ToString();
                if (Convert.ToDouble(_lbl.Text) > _Targets.WaterUnit.ToUnit(_Targets.WaterConsumption, _WaterUnit) && _Targets.WaterConsumption>0)
                    _lbl.Style.Add("Color", "Red");
                _lbl = (Label)e.Item.FindControl("lblWaterMonthlyEvolutionKPIMts");
                _lbl.Text = _dr["WaterKPIMts"].ToString();
                _lbl = (Label)e.Item.FindControl("lblWaterMonthlyEvolutionKPIMoney");
                _lbl.Text = _dr["WaterKPIMoney"].ToString();
                _lbl = (Label)e.Item.FindControl("lblWaterMonthlyEvolutionCO2");
                _lbl.Text = _dr["WaterCO2"].ToString();
                _lbl = (Label)e.Item.FindControl("lblWaterMonthlyEvolutionCO2KPIMts");
                _lbl.Text = _dr["WaterCO2KPIMts"].ToString();
                _lbl = (Label)e.Item.FindControl("lblWaterMonthlyEvolutionCO2KPIMoney");
                _lbl.Text = _dr["WaterCO2KPIMoney"].ToString();

            }
        }
        void rptWasteMonthlyEvolution_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView _dr = (DataRowView)e.Item.DataItem;

                Label _lbl = (Label)e.Item.FindControl("lblWasteMonthlyEvolutionMonth");
                _lbl.Text = _dr["Month"].ToString();
                
                _lbl = (Label)e.Item.FindControl("lblWasteMonthlyEvolutionUnit");
                _lbl.Text = _dr["WasteUnit"].ToString();
                if (Convert.ToDouble(_lbl.Text) > _Targets.WasteUnit.ToUnit(_Targets.WasteConsumption, _WasteUnit) && _Targets.WasteConsumption > 0)
                    _lbl.Style.Add("Color", "Red");
                _lbl = (Label)e.Item.FindControl("lblWasteMonthlyEvolutionKPIMts");
                _lbl.Text = _dr["WasteKPIMts"].ToString();
                _lbl = (Label)e.Item.FindControl("lblWasteMonthlyEvolutionKPIMoney");
                _lbl.Text = _dr["WasteKPIMoney"].ToString();
                _lbl = (Label)e.Item.FindControl("lblWasteMonthlyEvolutionCO2");
                _lbl.Text = _dr["WasteCO2"].ToString();
                _lbl = (Label)e.Item.FindControl("lblWasteMonthlyEvolutionCO2KPIMts");
                _lbl.Text = _dr["WasteCO2KPIMts"].ToString();
                _lbl = (Label)e.Item.FindControl("lblWasteMonthlyEvolutionCO2KPIMoney");
                _lbl.Text = _dr["WasteCO2KPIMoney"].ToString();

            }
        }
        void rptTransportMonthlyEvolution_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView _dr = (DataRowView)e.Item.DataItem;

                Label _lbl = (Label)e.Item.FindControl("lblTransportMonthlyEvolutionMonth");
                _lbl.Text = _dr["Month"].ToString();
                
                _lbl = (Label)e.Item.FindControl("lblTransportMonthlyEvolutionUnit");
                _lbl.Text = _dr["TransportUnit"].ToString();
                if (Convert.ToDouble(_lbl.Text) > _Targets.TransportUnit.ToUnit(_Targets.TransportConsumption, _TransportUnit) && _Targets.TransportConsumption > 0)
                    _lbl.Style.Add("Color", "Red");
                _lbl = (Label)e.Item.FindControl("lblTransportMonthlyEvolutionKPIMts");
                _lbl.Text = _dr["TransportKPIMts"].ToString();
                _lbl = (Label)e.Item.FindControl("lblTransportMonthlyEvolutionKPIMoney");
                _lbl.Text = _dr["TransportKPIMoney"].ToString();
                _lbl = (Label)e.Item.FindControl("lblTransportMonthlyEvolutionCO2");
                _lbl.Text = _dr["TransportCO2"].ToString();
                _lbl = (Label)e.Item.FindControl("lblTransportMonthlyEvolutionCO2KPIMts");
                _lbl.Text = _dr["TransportCO2KPIMts"].ToString();
                _lbl = (Label)e.Item.FindControl("lblTransportMonthlyEvolutionCO2KPIMoney");
                _lbl.Text = _dr["TransportCO2KPIMoney"].ToString();
                
            }
        }
        void rptFuelsMonthlyEvolution_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView _dr = (DataRowView)e.Item.DataItem;

                Label _lbl = (Label)e.Item.FindControl("lblFuelsMonthlyEvolutionMonth");
                _lbl.Text = _dr["Month"].ToString();
                
                _lbl = (Label)e.Item.FindControl("lblFuelsMonthlyEvolutionUnit");
                _lbl.Text = _dr["FuelsUnit"].ToString();
                if (Convert.ToDouble(_lbl.Text) > _Targets.FuelUnit.ToUnit(_Targets.FuelConsumption, _FuelUnit) && _Targets.FuelConsumption > 0)
                    _lbl.Style.Add("Color", "Red");
                _lbl = (Label)e.Item.FindControl("lblFuelsMonthlyEvolutionKPIMts");
                _lbl.Text = _dr["FuelsKPIMts"].ToString();
                _lbl = (Label)e.Item.FindControl("lblFuelsMonthlyEvolutionKPIMoney");
                _lbl.Text = _dr["FuelsKPIMoney"].ToString();
                _lbl = (Label)e.Item.FindControl("lblFuelsMonthlyEvolutionCO2");
                _lbl.Text = _dr["FuelsCO2"].ToString();
                _lbl = (Label)e.Item.FindControl("lblFuelsMonthlyEvolutionCO2KPIMts");
                _lbl.Text = _dr["FuelsCO2KPIMts"].ToString();
                _lbl = (Label)e.Item.FindControl("lblFuelsMonthlyEvolutionCO2KPIMoney");
                _lbl.Text = _dr["FuelsCO2KPIMoney"].ToString();

            }
        }
        void rptElectricityMonthlyEvolution_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView _dr = (DataRowView)e.Item.DataItem;

                Label _lbl = (Label)e.Item.FindControl("lblElectricityMonthlyEvolutionMonth");
                _lbl.Text = _dr["Month"].ToString();

                _lbl = (Label)e.Item.FindControl("lblElectricityMonthlyEvolutionUnit");
                _lbl.Text = _dr["ElectricityUnit"].ToString();
                if (Convert.ToDouble(_lbl.Text) > _Targets.ElectricityUnit.ToUnit(_Targets.ElectricityConsumption, _ElectricityUnit) && _Targets.ElectricityConsumption > 0)
                    _lbl.Style.Add("Color", "Red");
                _lbl = (Label)e.Item.FindControl("lblElectricityMonthlyEvolutionKPIMts");
                _lbl.Text = _dr["ElectricityKPIMts"].ToString();
                _lbl = (Label)e.Item.FindControl("lblElectricityMonthlyEvolutionKPIMoney");
                _lbl.Text = _dr["ElectricityKPIMoney"].ToString();
                _lbl = (Label)e.Item.FindControl("lblElectricityMonthlyEvolutionCO2");
                _lbl.Text = _dr["ElectricityCO2"].ToString();
                _lbl = (Label)e.Item.FindControl("lblElectricityMonthlyEvolutionCO2KPIMts");
                _lbl.Text = _dr["ElectricityCO2KPIMts"].ToString();
                _lbl = (Label)e.Item.FindControl("lblElectricityMonthlyEvolutionCO2KPIMoney");
                _lbl.Text = _dr["ElectricityCO2KPIMoney"].ToString();

            }
        }
        
        void rptElectricityMeters_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Sites.Meters.Meter _meter = (Library.Objects.Sites.Meters.Meter)e.Item.DataItem;
                
                Label _lbl = ((Label)e.Item.FindControl("lblElectricityMeterIdentification"));
                _lbl.Text = _meter.Identification;

                _lbl = ((Label)e.Item.FindControl("lblElectricityMeterLastDate"));
                DateTime? _lastDate = _meter.GetLastDate();
                if (_lastDate.HasValue)
                    _lbl.Text = _lastDate.Value.ToShortDateString();

                Library.Objects.Metrics.MetricPeriod _stats = _meter.GetStatistics();
                _lbl = ((Label)e.Item.FindControl("lblElectricityMeterTotal"));
                _lbl.Text = WebUI.Common.RoundAndFormat(_stats.Sum).ToString();

                _lbl = ((Label)e.Item.FindControl("lblElectricityMeterTotalCO2"));
                _lbl.Text = WebUI.Common.RoundAndFormat(_stats.SumCO2).ToString();
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                if (((Repeater)sender).Items.Count < 1)
                {
                    // Show the empty label.
                    Label lblNoItems = (Label)e.Item.FindControl("lblNoItems");
                    lblNoItems.Text = Resources.Data.NoItems;
                    lblNoItems.Style.Add("display", "block");
                }
            }
        }
        void rptFuelsMeters_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Sites.Meters.Meter _meter = (Library.Objects.Sites.Meters.Meter)e.Item.DataItem;

                Label _lbl = ((Label)e.Item.FindControl("lblFuelsMeterIdentification"));
                _lbl.Text = _meter.Identification;

                _lbl = ((Label)e.Item.FindControl("lblFuelsMeterLastDate"));
                DateTime? _lastDate = _meter.GetLastDate();
                if (_lastDate.HasValue)
                    _lbl.Text = _lastDate.Value.ToShortDateString();

                Library.Objects.Metrics.MetricPeriod _stats = _meter.GetStatistics();
                _lbl = ((Label)e.Item.FindControl("lblFuelsMeterTotal"));
                _lbl.Text = WebUI.Common.RoundAndFormat(_stats.Sum).ToString();

                _lbl = ((Label)e.Item.FindControl("lblFuelsMeterTotalCO2"));
                _lbl.Text = WebUI.Common.RoundAndFormat(_stats.SumCO2).ToString();
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                if (((Repeater)sender).Items.Count < 1)
                {
                    // Show the empty label.
                    Label lblNoItems = (Label)e.Item.FindControl("lblNoItems");
                    lblNoItems.Text = Resources.Data.NoItems;
                    lblNoItems.Style.Add("display", "block");
                }
            }
        }
        void rptTransportMeters_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Sites.Meters.Meter _meter = (Library.Objects.Sites.Meters.Meter)e.Item.DataItem;

                Label _lbl = ((Label)e.Item.FindControl("lblTransportMeterIdentification"));
                _lbl.Text = _meter.Identification;

                _lbl = ((Label)e.Item.FindControl("lblTransportMeterLastDate"));
                DateTime? _lastDate = _meter.GetLastDate();
                if (_lastDate.HasValue)
                    _lbl.Text = _lastDate.Value.ToShortDateString();

                Library.Objects.Metrics.MetricPeriod _stats = _meter.GetStatistics();
                _lbl = ((Label)e.Item.FindControl("lblTransportMeterTotal"));
                _lbl.Text = WebUI.Common.RoundAndFormat(_stats.Sum).ToString();

                _lbl = ((Label)e.Item.FindControl("lblTransportMeterTotalCO2"));
                _lbl.Text = WebUI.Common.RoundAndFormat(_stats.SumCO2).ToString();
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                if (((Repeater)sender).Items.Count < 1)
                {
                    // Show the empty label.
                    Label lblNoItems = (Label)e.Item.FindControl("lblNoItems");
                    lblNoItems.Text = Resources.Data.NoItems;
                    lblNoItems.Style.Add("display", "block");
                }
            }
        }
        void rptWasteMeters_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Sites.Meters.Meter _meter = (Library.Objects.Sites.Meters.Meter)e.Item.DataItem;

                Label _lbl = ((Label)e.Item.FindControl("lblWasteMeterIdentification"));
                _lbl.Text = _meter.Identification;

                _lbl = ((Label)e.Item.FindControl("lblWasteMeterLastDate"));
                DateTime? _lastDate = _meter.GetLastDate();
                if (_lastDate.HasValue)
                    _lbl.Text = _lastDate.Value.ToShortDateString();

                Library.Objects.Metrics.MetricPeriod _stats = _meter.GetStatistics();
                _lbl = ((Label)e.Item.FindControl("lblWasteMeterTotal"));
                _lbl.Text = WebUI.Common.RoundAndFormat(_stats.Sum).ToString();

                _lbl = ((Label)e.Item.FindControl("lblWasteMeterTotalCO2"));
                _lbl.Text = WebUI.Common.RoundAndFormat(_stats.SumCO2).ToString();
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                if (((Repeater)sender).Items.Count < 1)
                {
                    // Show the empty label.
                    Label lblNoItems = (Label)e.Item.FindControl("lblNoItems");
                    lblNoItems.Text = Resources.Data.NoItems;
                    lblNoItems.Style.Add("display", "block");
                }
            }
        }
        void rptWaterMeters_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Sites.Meters.Meter _meter = (Library.Objects.Sites.Meters.Meter)e.Item.DataItem;

                Label _lbl = ((Label)e.Item.FindControl("lblWaterMeterIdentification"));
                _lbl.Text = _meter.Identification;

                _lbl = ((Label)e.Item.FindControl("lblWaterMeterLastDate"));
                DateTime? _lastDate = _meter.GetLastDate();
                if (_lastDate.HasValue)
                    _lbl.Text = _lastDate.Value.ToShortDateString();

                Library.Objects.Metrics.MetricPeriod _stats = _meter.GetStatistics();
                _lbl = ((Label)e.Item.FindControl("lblWaterMeterTotal"));
                _lbl.Text = WebUI.Common.RoundAndFormat(_stats.Sum).ToString();

                _lbl = ((Label)e.Item.FindControl("lblWaterMeterTotalCO2"));
                _lbl.Text = WebUI.Common.RoundAndFormat(_stats.SumCO2).ToString();
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                if (((Repeater)sender).Items.Count < 1)
                {
                    // Show the empty label.
                    Label lblNoItems = (Label)e.Item.FindControl("lblNoItems");
                    lblNoItems.Text = Resources.Data.NoItems;
                    lblNoItems.Style.Add("display", "block");
                }
            }
        }

        void rptEFWaste_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor _ef = (Library.Objects.Auxiliaries.EmissionFactors.WasteTypeEmissionFactor)e.Item.DataItem;

                Label lbl = (Label)e.Item.FindControl("lblEFWasteValue");
                lbl.Text = _ef.Value.ToString("N6");

                lbl = (Label)e.Item.FindControl("lblEFWasteTypeName");
                lbl.Text = _ef.WasteType.Name;

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
                lbl.Text = _ef.Value.ToString("N6");

                lbl = (Label)e.Item.FindControl("lblEFTransportTypeName");
                lbl.Text = _ef.TransportType.Name;

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
                lbl.Text = _ef.Value.ToString("N6");

                lbl = (Label)e.Item.FindControl("lblEFFuelsTypeName");
                lbl.Text = _ef.FuelType.Name;

                lbl = (Label)e.Item.FindControl("lblEFFuelsDescription");
                lbl.Text = _ef.LanguageOption.Description;
            }
        }

        void btnFilter_Click(object sender, EventArgs e)
        {
            
        }
        
        #endregion
    }
}