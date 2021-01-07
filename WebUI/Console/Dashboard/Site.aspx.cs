using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard
{
    public partial class Site : BasePage
    {
        private Library.Objects.Sites.SiteMine _Site;
        public String Location
        {
            set {
                ViewState["Location"] = value.Replace(',', '.');
            }
            get {
                return ViewState["Location"].ToString(); 
            } 
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            _Site = (Library.Objects.Sites.SiteMine)I.GetSite(Convert.ToInt64(Request.QueryString["Site"]));

            //If no sites then show guide
            if (_Site.MetersQuantity == 0)
            {
                ((Main)Page.Master).Guide.SetMessage(Resources.Messages.FirstTimeNeedHelp, Resources.Messages.FirstTimeGuideSite, 100, 100);
                ((Main)Page.Master).MenuNavigation.Blink(Console.Controls.ucMenuNavigation.MenuItem.Meters);
            }

            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTexts();
                SetMenu();

                LoadChartProtocols();
                LoadChartAggregatesOptions();

                LoadSite();
                LoadPerformance();
                
            }
            
        }

        #region Private Methods

        private void BindControls()
        {
            ddlChartProtocols.SelectedIndexChanged +=new EventHandler(ddlChartProtocols_SelectedIndexChanged);
            ddlChartAggregates.SelectedIndexChanged +=new EventHandler(ddlChartAggregates_SelectedIndexChanged);
        }
        private void SetMenu()
        {
            ((Main)Page.Master).MenuNavigation.Initialize(_Site.IdSite, _Site.Title, Console.Controls.ucMenuNavigation.MenuItem.None, WebUI.Common.GetPermissionFromContext(I,_Site));
        }
        private void LoadTexts()
        {
            lblSiteProperties.Text = Resources.Data.Properties;

            lblTitle.Text = Resources.Data.Title;
            lblLoadStatus.Text = Resources.Data.SiteLoadStatus;
            lblLiveStatus.Text = Resources.Data.SiteLiveStatus;
            lblType.Text = Resources.Data.SiteType;
            lblLocation.Text = Resources.Data.Location;
            lblStart.Text = Resources.Data.Start;
            lblWeeks.Text = Resources.Data.Weeks;
            lblNumber.Text = Resources.Data.Number;
            lblValue.Text = Resources.Data.Value;
            lblFloorSpace.Text = Resources.Data.FloorSpace;
            lblUnits.Text = Resources.Data.Units;
           
            lblChart.Text = Resources.Data.ChartsMonthlyEvolutions;
            lblChartAggregates.Text = Resources.Data.ChartsCompositions;

            lblPerformance.Text = Resources.Data.Performance;
            lblPerformanceHeaderElectricity.Text = Resources.Data.Electricity;
            lblPerformanceHeaderWater.Text = Resources.Data.Water;
            lblPerformanceHeaderTransport.Text = Resources.Data.Transport;
            lblPerformanceHeaderWaste.Text = Resources.Data.Waste;
            lblPerformanceHeaderFuels.Text = Resources.Data.Fuels;
            lblPerformanceHeaderTotalCO2.Text = Resources.Data.CO2Total;
        }
        private void LoadSite()
        { 
            Library.Objects.Auxiliaries.Geographic.Contact _contact = _Site.Contact;
            Location = _contact.Location.Position.Coordenates;

            lblTitleValue.Text = _Site.Title;
            lblLoadStatusValue.Text = (_Site is Library.Objects.Sites.SiteMineOpen ? Resources.Data.SiteOpened : Resources.Data.SiteClosed);
            lblLiveStatusValue.Text = (_Site.IsFinished ? Resources.Data.SiteFinished : Resources.Data.SiteLive);
            lblTypeValue.Text = _Site.Type.Name;
            lblLocationValue.Text = _contact.Location.Address;
            lblStartValue.Text = _Site.Start.ToShortDateString();
            lblWeeksValue.Text = _Site.Weeks.ToString();
            lblNumberValue.Text = _Site.Number;
            lblValueValue.Text = _Site.Currency.Symbol + " " + _Site.Value.ToString() + " [" + _Site.Currency.Name + "]";
            lblFloorSpaceValue.Text = _Site.FloorSpace.ToString();
            lblUnitsValue.Text = _Site.Units.ToString();
            
        }
        private void LoadPerformance()
        {
            Double _area = _Site.FloorSpace;
            Library.Objects.Auxiliaries.Units.Cost _cost = _Site.Cost;
            String _currency = _cost.Currency.Symbol + "100k";
            Double _costValue = _cost.Value / 100000;

            Library.Objects.Metrics.MetricPeriod _stats;
            String _symbol;
            Double _totalCO2;

            _stats = _Site.GetWaterStatistics();
            _symbol = _stats.Unit.Symbol;
            lblPerformanceWaterUnits.Text = WebUI.Common.RoundAndFormat(_stats.Sum).ToString();
            lblPerformanceWaterUnit.Text = _symbol;
            lblPerformanceWaterCO2.Text = WebUI.Common.RoundAndFormat(_stats.SumCO2).ToString();
            lblPerformanceWaterCO2Unit.Text = Resources.Data.CO2Unit;
            lblPerformanceWaterKPI.Text = WebUI.Common.RoundAndFormat((Double)(_area > 0 ? _stats.Sum / _area : 0)).ToString();
            lblPerformanceWaterKPIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _stats.Sum / _costValue : 0)).ToString();
            lblPerformanceWaterKPIMoneyUnit.Text = _symbol + "/" + _currency;
            lblPerformanceWaterKPIUnit.Text = _symbol + "/" + Resources.Data.KPIM2Unit;
            _totalCO2 = _stats.SumCO2;

            _stats = _Site.GetElectricityStatistics();
            _symbol = _stats.Unit.Symbol;
            lblPerformanceElectricityUnits.Text = WebUI.Common.RoundAndFormat(_stats.Sum).ToString();
            lblPerformanceElectricityUnit.Text = _symbol;
            lblPerformanceElectricityCO2.Text = WebUI.Common.RoundAndFormat(_stats.SumCO2).ToString();
            lblPerformanceElectricityCO2Unit.Text = Resources.Data.CO2Unit;
            lblPerformanceElectricityKPI.Text = WebUI.Common.RoundAndFormat((Double)(_area > 0 ? _stats.Sum / _area : 0)).ToString();
            lblPerformanceElectricityKPIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _stats.Sum / _costValue : 0)).ToString();
            lblPerformanceElectricityKPIMoneyUnit.Text = _symbol + "/" + _currency;
            lblPerformanceElectricityKPIUnit.Text = _symbol + "/" + Resources.Data.KPIM2Unit;
            _totalCO2 += _stats.SumCO2;

            _stats = _Site.GetTransportStatistics();
            _symbol = _stats.Unit.Symbol;
            lblPerformanceTransportUnits.Text = WebUI.Common.RoundAndFormat(_stats.Sum).ToString();
            lblPerformanceTransportUnit.Text = _symbol;
            lblPerformanceTransportCO2.Text = WebUI.Common.RoundAndFormat(_stats.SumCO2).ToString();
            lblPerformanceTransportCO2Unit.Text = Resources.Data.CO2Unit;
            lblPerformanceTransportKPI.Text = WebUI.Common.RoundAndFormat((Double)(_area > 0 ? _stats.Sum / _area : 0)).ToString();
            lblPerformanceTransportKPIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _stats.Sum / _costValue : 0)).ToString();
            lblPerformanceTransportKPIMoneyUnit.Text = _symbol + "/" + _currency;
            lblPerformanceTransportKPIUnit.Text = _symbol + "/" + Resources.Data.KPIM2Unit;
            _totalCO2 += _stats.SumCO2;

            _stats = _Site.GetFuelsStatistics();
            _symbol = _stats.Unit.Symbol;
            lblPerformanceFuelsUnits.Text = WebUI.Common.RoundAndFormat(_stats.Sum).ToString();
            lblPerformanceFuelsUnit.Text = _symbol;
            lblPerformanceFuelsCO2.Text = WebUI.Common.RoundAndFormat(_stats.SumCO2).ToString();
            lblPerformanceFuelsCO2Unit.Text = Resources.Data.CO2Unit;
            lblPerformanceFuelsKPI.Text = WebUI.Common.RoundAndFormat((Double)(_area > 0 ? _stats.Sum / _area : 0)).ToString();
            lblPerformanceFuelsKPIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _stats.Sum / _costValue : 0)).ToString();
            lblPerformanceFuelsKPIMoneyUnit.Text = _symbol + "/" + _currency;
            lblPerformanceFuelsKPIUnit.Text = _symbol + "/" + Resources.Data.KPIM2Unit;
            _totalCO2 += _stats.SumCO2;

            _stats = _Site.GetWasteStatistics();
            _symbol = _stats.Unit.Symbol;
            lblPerformanceWasteUnits.Text = WebUI.Common.RoundAndFormat(_stats.Sum).ToString();
            lblPerformanceWasteUnit.Text = _symbol;
            lblPerformanceWasteCO2.Text = WebUI.Common.RoundAndFormat(_stats.SumCO2).ToString();
            lblPerformanceWasteCO2Unit.Text = Resources.Data.CO2Unit;
            lblPerformanceWasteKPI.Text = WebUI.Common.RoundAndFormat((Double)(_area > 0 ? _stats.Sum / _area : 0)).ToString();
            lblPerformanceWasteKPIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _stats.Sum / _costValue : 0)).ToString();
            lblPerformanceWasteKPIMoneyUnit.Text = _symbol + "/" + _currency;
            lblPerformanceWasteKPIUnit.Text = _symbol + "/" + Resources.Data.KPIM2Unit;
            _totalCO2 += _stats.SumCO2;

            lblPerformanceTotalCO2.Text = WebUI.Common.RoundAndFormat(_totalCO2).ToString();
            lblPerformanceTotalCO2Unit.Text = Resources.Data.CO2Unit;

            lblPerformanceTotalCO2KPI.Text = WebUI.Common.RoundAndFormat((Double)(_area > 0 ? _totalCO2 / _area : 0)).ToString();
            lblPerformanceTotalCO2KPIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _totalCO2 / _costValue : 0)).ToString();
            lblPerformanceTotalCO2KPIMoneyUnit.Text = Resources.Data.CO2Unit + "/" + _currency;
            lblPerformanceTotalCO2KPIUnit.Text = Resources.Data.CO2Unit + "/" + Resources.Data.KPIM2Unit;
            
        }
        
        private void LoadChartProtocols()
        {
            foreach (WebUI.Common.ChartProtocols _item in Enum.GetValues(typeof(WebUI.Common.ChartProtocols)))
            {
                ListItem _protocol = new ListItem(GetGlobalResourceObject("Data", _item.ToString()).ToString(), _item.ToString());
                ddlChartProtocols.Items.Add(_protocol);
            }
            SetChart();
        }
        private void LoadChartAggregatesOptions()
        {
            ddlChartAggregates.Items.Add(new ListItem(Resources.Data.ChartCO2CompositionByProtocols, "0"));

            SetChartAggregates();
        }

        #endregion
        
        #region Charts

        private void SetChart()
        {
            radChart.Series.Clear();
            radChart.PlotArea.MarkedZones.Clear();

            //Targets
            Library.Objects.Sites.Targets _targets = _Site.Targets;

            WebUI.Common.ChartProtocols _protocol = (WebUI.Common.ChartProtocols)Enum.Parse(typeof(WebUI.Common.ChartProtocols), ddlChartProtocols.SelectedValue);
            switch (_protocol)
            {

                case CSI.WebUI.Common.ChartProtocols.Electricity:
                    {
                        LoadChart(Resources.Data.ChartElectricitySumMonthly, Resources.Data.Electricity, _Site.GetElectricityMonthly(), _targets.ElectricityConsumption, _targets.ElectricityUnit);
                        break;
                    }

                case CSI.WebUI.Common.ChartProtocols.Fuels:
                    {
                        LoadChart(Resources.Data.ChartFuelSumMonthly, Resources.Data.Fuels, _Site.GetFuelsMonthly(), _targets.FuelConsumption, _targets.FuelUnit);
                        break;
                    }

                case CSI.WebUI.Common.ChartProtocols.Transport:
                    {
                        LoadChart(Resources.Data.ChartTransportSumMonthly, Resources.Data.Transport, _Site.GetTransportMonthly(), _targets.TransportConsumption, _targets.TransportUnit);
                        break;
                    }

                case CSI.WebUI.Common.ChartProtocols.Waste:
                    {
                        LoadChart(Resources.Data.ChartWasteSumMonthly, Resources.Data.Waste, _Site.GetWasteMonthly(), _targets.WasteConsumption, _targets.WasteUnit);
                        break;
                    }

                case CSI.WebUI.Common.ChartProtocols.Water:
                    {
                        LoadChart(Resources.Data.ChartWaterSumMonthly, Resources.Data.Water, _Site.GetWaterMonthly(), _targets.WaterConsumption, _targets.WaterUnit);
                        break;
                    }

                case CSI.WebUI.Common.ChartProtocols.CO2:
                    {
                        LoadChart(Resources.Data.ChartCO2SumMonthly, Resources.Data.CO2, _Site.GetCO2Monthly(), _targets.TotalCO2, I.CO2DefaultUnit());
                        break;
                    }

                default:
                    break;
            }
        }
        private void SetChartAggregates()
        {
            radChartAggregates.Series.Clear();

            if (ddlChartAggregates.SelectedValue == "0")
                LoadChartAggregates(Resources.Data.ChartCO2CompositionByProtocols, Resources.Data.Protocols, _Site.GetCO2StatisticsByProtocols());
            
        }

        private void LoadChartAggregates(String title, String titleSerie, Library.Objects.Metrics.MetricComposite values)
        {
            radChartAggregates.ChartTitle.TextBlock.Text = "";
            
            //Create serie
            Telerik.Charting.ChartSeries _series = new Telerik.Charting.ChartSeries(titleSerie, Telerik.Charting.ChartSeriesType.Pie);
            WebUI.Common.ChartPieSerieStyle(radChartAggregates, _series);

            Telerik.Charting.ChartSeriesItemsCollection _points = new Telerik.Charting.ChartSeriesItemsCollection();

            Double _share;
            String _unitSymbol = values.Unit.Symbol;
            String _legend = String.Empty;
            foreach (Library.Objects.Metrics.MetricComponent _item in values.Components.Values)
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
            radChartAggregates.Series.Add(_series);

        }
        private void LoadChart(String title, String titleSerie, Dictionary<DateTime, Library.Objects.Metrics.MetricInstant> values, Double target, Library.Objects.Auxiliaries.Units.Unit targetUnit)
        {
            radChart.ChartTitle.TextBlock.Text = ""; 

            if (values.Count > 0)
            {
                //Create serie
                Telerik.Charting.ChartSeries _series = new Telerik.Charting.ChartSeries(titleSerie, Telerik.Charting.ChartSeriesType.Bar);
                WebUI.Common.ChartBarSerieStyle(_series);

                String[] _dates = new String[values.Count];
                Int32 x = 0;

                //Values, tooltips and units for targets
                Library.Objects.Auxiliaries.Units.Unit _co2Unit = I.CO2DefaultUnit();
                Library.Objects.Auxiliaries.Units.Unit _unit = null;
                if (values.Count > 0)
                    _unit = values.First().Value.Unit;
                Double _value, _max = 0, _target = targetUnit.ToUnit(target, _unit);
                
                Telerik.Charting.ChartSeriesItemsCollection _points = new Telerik.Charting.ChartSeriesItemsCollection();
                foreach (Library.Objects.Metrics.MetricInstant _item in values.Values)
                {
                    //Set value
                    _value = WebUI.Common.RoundAndFormat(_item.Sum);
                    if (_value > _max) _max = _value;
                    Telerik.Charting.ChartSeriesItem _point = new Telerik.Charting.ChartSeriesItem(_value);
                    WebUI.Common.ChartBarSerieItemStyle(_point,_target);
                    _point.ActiveRegion.Tooltip = _value.ToString() + " " + _item.Unit.Symbol + " [" + CSI.WebUI.Common.RoundAndFormat(_item.SumCO2).ToString() + " " + _co2Unit.Symbol + "]";
                    _points.Add(_point);

                    //Store date
                    _dates[x] = _item.Instant.Month.ToString() + '-' + _item.Instant.Year.ToString();
                    x += 1;
                }

                LoadXAxis(_dates);
                
                _series.AddItem(_points);
                radChart.Series.Add(_series);
                LoadTargetZones(_target, _max);
        
            }

        }
        private void LoadTargetZones(Double target, Double max)
        {
            if (target > 0)
            {
                Telerik.Charting.ChartMarkedZone _zone = new Telerik.Charting.ChartMarkedZone();
              
                _zone = new Telerik.Charting.ChartMarkedZone();
                _zone.ValueStartY = target;
                _zone.ValueEndY = max + max * 0.1;
                WebUI.Common.ChartZoneStyle(_zone);
                radChart.PlotArea.MarkedZones.Add(_zone);
            }
        }
        private void LoadXAxis(String[] dates)
        {
            int x;

            radChart.PlotArea.XAxis.AutoScale = false;
            radChart.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = 90;
            radChart.PlotArea.XAxis.AddRange(1, dates.Length, 1);
            for (x = 0; x < dates.Length; x++)
            {
                radChart.PlotArea.XAxis.Items[x].TextBlock.Text = dates[x];
            }
        }

        #endregion

        #region Events

        void ddlChartProtocols_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetChart();
        }
        void ddlChartAggregates_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetChartAggregates();
        }

        #endregion
    }
}