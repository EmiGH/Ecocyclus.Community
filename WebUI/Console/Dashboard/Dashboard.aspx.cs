using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSI.WebUI.Console.Dashboard
{
    public partial class Dashboard : BasePage
    {
        private Library.Objects.Companies.CompanyMine _Company;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!(I is Library.Objects.Users.UserOperatorMeManager))
                throw new ApplicationException(Resources.Messages.AccessDenied);

            _Company = (Library.Objects.Companies.CompanyMine)I.Company;

            //If no sites then show guide
            if (I.GetSites().Count == 0)
            {
                ((Main)Page.Master).Guide.SetMessage(Resources.Messages.FirstTimeTitle.Replace("[user]", I.Firstname), Resources.Messages.FirstTimeGuideApp, 100, 100);
                ((Main)Page.Master).MenuGlobal.Blink(Console.Controls.ucMenuGlobal.MenuItem.AddSite);
            }

            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadTexts();
            
                LoadChartProtocols();
                LoadChartAggregatesOptions();

                LoadPerformance();
            }
        }

        #region Private Methods

        private void BindControls()
        {
            ddlChartProtocols.SelectedIndexChanged += new EventHandler(ddlChartProtocols_SelectedIndexChanged);
            ddlChartAggregates.SelectedIndexChanged += new EventHandler(ddlChartAggregates_SelectedIndexChanged);
        }
        private void LoadTexts()
        {
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
        private void LoadPerformance()
        {
            Double _area = _Company.GetTotalArea();
            Library.Objects.Auxiliaries.Units.Cost _cost = _Company.GetTotalCost();
            String _currency = _cost.Currency.Symbol + "100k";
            Double _costValue = _cost.Value / 100000;

            Library.Objects.Metrics.MetricPeriod _stats;
            String _symbol;
            Double _totalCO2;

            _stats = _Company.GetWaterStatistics();
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

            _stats = _Company.GetElectricityStatistics();
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

            _stats = _Company.GetTransportStatistics();
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

            _stats = _Company.GetFuelsStatistics();
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

            _stats = _Company.GetWasteStatistics();
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
            ddlChartAggregates.Items.Add(new ListItem(Resources.Data.ChartCO2CompositionBySites, "1"));

            SetChartAggregates();
        }
        
        #endregion

        #region Charts

        private void SetChart()
        {
            radChart.Series.Clear();
                        
            WebUI.Common.ChartProtocols _protocol = (WebUI.Common.ChartProtocols)Enum.Parse(typeof(WebUI.Common.ChartProtocols), ddlChartProtocols.SelectedValue);
            switch (_protocol)
            {

                case CSI.WebUI.Common.ChartProtocols.Electricity:
                    {
                        LoadChart(Resources.Data.ChartElectricitySumMonthly, Resources.Data.Electricity, _Company.GetElectricityMonthly());
                        break;
                    }

                case CSI.WebUI.Common.ChartProtocols.Fuels:
                    {
                        LoadChart(Resources.Data.ChartFuelSumMonthly, Resources.Data.Fuels, _Company.GetFuelsMonthly());
                        break;
                    }

                case CSI.WebUI.Common.ChartProtocols.Transport:
                    {
                        LoadChart(Resources.Data.ChartTransportSumMonthly, Resources.Data.Transport, _Company.GetTransportMonthly());
                        break;
                    }

                case CSI.WebUI.Common.ChartProtocols.Waste:
                    {
                        LoadChart(Resources.Data.ChartWasteSumMonthly, Resources.Data.Waste, _Company.GetWasteMonthly());
                        break;
                    }

                case CSI.WebUI.Common.ChartProtocols.Water:
                    {
                        LoadChart(Resources.Data.ChartWaterSumMonthly, Resources.Data.Water, _Company.GetWaterMonthly());
                        break;
                    }

                case CSI.WebUI.Common.ChartProtocols.CO2:
                    {
                        LoadChart(Resources.Data.ChartCO2SumMonthly, Resources.Data.CO2, _Company.GetCO2Monthly());
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
                LoadChartAggregates(Resources.Data.ChartCO2CompositionByProtocols, Resources.Data.Protocols, _Company.GetCO2StatisticsByProtocols());
            else
                LoadChartAggregates(Resources.Data.ChartCO2CompositionBySites, Resources.Data.Sites, _Company.GetCO2StatisticsBySites());
        }

        private void LoadChartAggregates(String title, String titleSerie, Library.Objects.Metrics.MetricComposite values)
        {
            radChartAggregates.ChartTitle.TextBlock.Text = "";//title;
            radChartAggregates.Chart.Legend.Visible = false;

            //Create serie
            Telerik.Charting.ChartSeries _series = new Telerik.Charting.ChartSeries(titleSerie, Telerik.Charting.ChartSeriesType.Pie);
            WebUI.Common.ChartPieSerieStyle(radChartAggregates, _series);
            Telerik.Charting.ChartSeriesItemsCollection _points = new Telerik.Charting.ChartSeriesItemsCollection();

            Double _value;
            String _unitSymbol = values.Unit.Symbol;
            String _legend = String.Empty;
            foreach (Library.Objects.Metrics.MetricComponent _item in values.Components.Values)
            {
                //Set value
                _value = WebUI.Common.RoundAndFormat(_item.Share);
                _legend = _item.Name + ": " + _value.ToString() + "% [" + CSI.WebUI.Common.RoundAndFormat(_item.Sum).ToString() + " " + _unitSymbol + "]";
                Telerik.Charting.ChartSeriesItem _point = new Telerik.Charting.ChartSeriesItem(_value, _legend);
                WebUI.Common.ChartPieSerieItemStyle(_point);                
                _point.ActiveRegion.Tooltip = _legend;
                _point.YValue = _value;

                _points.Add(_point);
            }

            _series.AddItem(_points);
            radChartAggregates.Series.Add(_series);

        }
        private void LoadChart(String title, String titleSerie, Dictionary<DateTime, Library.Objects.Metrics.MetricInstant> values)
        {
            radChart.ChartTitle.TextBlock.Text = "";//title;

            if (values.Count > 0)
            {
                //Create serie
                Telerik.Charting.ChartSeries _series = new Telerik.Charting.ChartSeries(titleSerie, Telerik.Charting.ChartSeriesType.Bar);
                WebUI.Common.ChartBarSerieStyle(_series);
               
                String[] _dates = new String[values.Count];
                Int32 x = 0;

                Library.Objects.Auxiliaries.Units.Unit _co2Unit = I.CO2DefaultUnit();

                Telerik.Charting.ChartSeriesItemsCollection _points = new Telerik.Charting.ChartSeriesItemsCollection();
                foreach (Library.Objects.Metrics.MetricInstant _item in values.Values)
                {
                    //Set value
                    Double _value = WebUI.Common.RoundAndFormat(_item.Sum);
                    Telerik.Charting.ChartSeriesItem _point = new Telerik.Charting.ChartSeriesItem(_value);
                    WebUI.Common.ChartBarSerieItemStyle(_point, 0);
                    _point.ActiveRegion.Tooltip = _value.ToString() + " " + _item.Unit.Symbol + " [" + CSI.WebUI.Common.RoundAndFormat(_item.SumCO2).ToString() + " " + _co2Unit.Symbol + "]";
                    _points.Add(_point);

                    //Store date
                    _dates[x] = _item.Instant.Month.ToString() + '-' + _item.Instant.Year.ToString();
                    x += 1;
                }

                LoadXAxis(_dates);
                _series.AddItem(_points);
                radChart.Series.Add(_series);
            }

        }
        private void LoadXAxis(String[] dates)
        {
            int x;

            radChart.PlotArea.XAxis.AutoScale = false;
            radChart.PlotArea.XAxis.AddRange(1, dates.Length, 1);
            radChart.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = 90;
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
        void radChartAggregates_ItemDataBound(object sender, Telerik.Charting.ChartItemDataBoundEventArgs e)
        {
            e.SeriesItem.Name = e.SeriesItem.Label.TextBlock.Text;
        }

        #endregion
    }
}