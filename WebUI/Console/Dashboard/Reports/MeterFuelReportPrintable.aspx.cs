using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSI.WebUI.Console.Dashboard.Reports
{
    public partial class MeterFuelReportPrintable : BasePage
    {
        private Library.Objects.Sites.Meters.FuelMeter _Meter;
        Library.Security.Authority.PermissionTypes _Permission;

        protected void Page_Init(object sender, EventArgs e)
        {
            _Meter = I.GetFuelMeter(Convert.ToInt64(Request.QueryString["Meter"].ToString()));
            _Permission = ((Library.Objects.Sites.SiteMine)_Meter.Site).CurrentPermission();
            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTexts();
                LoadMeter();

                SetCharts();
                SetChartAggregates();

                LoadLoadSeries();
                LoadPerformance();
            }
        }

        private void BindControls()
        {
            rptLoadSeries.ItemDataBound += new RepeaterItemEventHandler(rptLoadSeries_ItemDataBound);
        }
        private void LoadTexts()
        {
            lblSiteProperties.Text = Resources.Data.Meter + " " + Resources.Data.Properties;

            lblPerformance.Text = Resources.Data.Performance;
            lblPerformanceHeaderFuels.Text = Resources.Data.Fuels;

            lblSeries.Text = Resources.Data.Series;

            lblChartSeriesConsumption.Text = Resources.Data.ChartsMonthlyEvolutions + " - " + Resources.Data.Consumption;
            lblChartSeriesCO2.Text = Resources.Data.ChartsMonthlyEvolutions + " - " + Resources.Data.CO2Generation;
            lblChartAggregatesConsumption.Text = Resources.Data.ChartsCompositions + " - " + Resources.Data.Consumption;
            lblChartAggregatesCO2.Text = Resources.Data.ChartsCompositions + " - " + Resources.Data.CO2Generation;

            lblIdentification.Text = Resources.Data.Identification;
            lblDescription.Text = Resources.Data.Description;

            lblHeaderDate.Text = Resources.Data.Date;
            lblHeaderFuel.Text = Resources.Data.FuelType;
            lblHeaderUnit.Text = Resources.Data.Unit;
            lblHeaderTotal.Text = Resources.Data.Total;
            lblHeaderTotalCO2.Text = Resources.Data.CO2Total;
            lblHeaderOperator.Text = Resources.Data.Operator;

        }
        private void LoadMeter()
        {
            String _idDefaultLanguage = MyLibrary.DefaultLanguage.IdLanguage;

            lblIdentificationValue.Text = _Meter.Identification;
            lblDescriptionValue.Text = _Meter.LanguageOption.Description;

        }
        private void LoadLoadSeries()
        {
            rptLoadSeries.DataSource = _Meter.GetSeries().Values;
            rptLoadSeries.DataBind();
        }
        private void LoadPerformance()
        {
            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)_Meter.Site;

            Double _area = _site.FloorSpace;
            Library.Objects.Auxiliaries.Units.Cost _cost = _site.Cost;
            String _currency = _cost.Currency.Symbol + "100k";
            Double _costValue = _cost.Value / 100000;

            Library.Objects.Metrics.MetricPeriod _stats;

            _stats = _Meter.GetStatistics();
            String _symbol = _stats.Unit.Symbol;
            lblPerformanceFuelsUnits.Text = WebUI.Common.RoundAndFormat(_stats.Sum).ToString();
            lblPerformanceFuelsUnit.Text = _stats.Unit.Symbol;
            lblPerformanceFuelsCO2.Text = WebUI.Common.RoundAndFormat(_stats.SumCO2).ToString();
            lblPerformanceFuelsCO2Unit.Text = Resources.Data.CO2Unit;
            lblPerformanceFuelsKPI.Text = WebUI.Common.RoundAndFormat((Double)(_area > 0 ? _stats.Sum / _area : 0)).ToString();
            lblPerformanceFuelsKPIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _stats.Sum / _costValue : 0)).ToString();
            lblPerformanceFuelsKPIMoneyUnit.Text = _symbol + "/" + _currency;
            lblPerformanceFuelsKPIUnit.Text = _symbol + "/" + Resources.Data.KPIM2Unit;
        }

        #region Charts

        private void SetCharts()
        {
            //Targets
            Library.Objects.Sites.Targets _target = ((Library.Objects.Sites.SiteMine)_Meter.Site).Targets;

            radChartSeriesConsumption.ChartTitle.TextBlock.Text = "";
            radChartSeriesCO2.ChartTitle.TextBlock.Text = "";
            
            LoadChart(Resources.Data.ChartFuelSumMonthly, Resources.Data.Fuels, _Meter.GetSerieMonthly(), false, _target.FuelConsumption, _target.FuelUnit);
            LoadChart(Resources.Data.ChartFuelSumMonthly, Resources.Data.CO2, _Meter.GetSerieMonthly(), true, _target.FuelCO2, I.CO2DefaultUnit());
            
        }
        private void SetChartAggregates()
        {
            LoadChartAggregates(Resources.Data.ChartCompositionByTypes, Resources.Data.Types, _Meter.GetStatisticsByTypes(), false);
            LoadChartAggregates(Resources.Data.ChartCO2CompositionByTypes, Resources.Data.Types, _Meter.GetStatisticsCO2ByTypes(), true);

        }
        
        private void LoadChart(String title, String titleSerie, Dictionary<DateTime, Library.Objects.Metrics.MetricInstant> values, Boolean co2, Double target, Library.Objects.Auxiliaries.Units.Unit targetUnit)
        {            
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
                String _tooltip;

                Telerik.Charting.ChartSeriesItemsCollection _points = new Telerik.Charting.ChartSeriesItemsCollection();
                foreach (Library.Objects.Metrics.MetricInstant _item in values.Values)
                {
                    if (_unit == null)
                        _unit = _item.Unit;

                    if (co2)
                    {
                        _value = CSI.WebUI.Common.RoundAndFormat(_item.SumCO2);
                        _tooltip = _value.ToString() + " " + _unit.Symbol;
                    }
                    else
                    {
                        _value = CSI.WebUI.Common.RoundAndFormat(_item.Sum);
                        _tooltip = _value.ToString() + " " + _unit.Symbol + " [" + CSI.WebUI.Common.RoundAndFormat(_item.SumCO2).ToString() + " " + _co2Unit.Symbol + "]"; 
                    }

                    if (_value > _max) _max = _value;
                    Telerik.Charting.ChartSeriesItem _point = new Telerik.Charting.ChartSeriesItem(_value);
                    WebUI.Common.ChartBarSerieItemStyle(_point, _target);
                    _point.ActiveRegion.Tooltip = _tooltip;
                    _points.Add(_point);

                    //Store date
                    _dates[x] = _item.Instant.Month.ToString() + '-' + _item.Instant.Year.ToString();
                    x += 1;
                }

                if (_points.Count > 0)
                {
                    if (co2)
                    {
                        LoadXAxis(radChartSeriesCO2, _dates);
                        _series.AddItem(_points);
                        radChartSeriesCO2.Series.Add(_series);
                        LoadTargetZones(radChartSeriesCO2, _target, _max);
                    }
                    else
                    {
                        LoadXAxis(radChartSeriesConsumption, _dates);
                        _series.AddItem(_points);
                        radChartSeriesConsumption.Series.Add(_series);
                        LoadTargetZones(radChartSeriesConsumption, _target, _max);
                    }
                }

            }


        }
        private void LoadChartAggregates(String title, String titleSerie, Library.Objects.Metrics.MetricComposite values, Boolean co2)
        {
            //Create serie
            Telerik.Charting.ChartSeries _series = new Telerik.Charting.ChartSeries(titleSerie, Telerik.Charting.ChartSeriesType.Pie);
            if (co2)
                WebUI.Common.ChartPieSerieStyle(radChartAggregatesCO2, _series);
            else
                WebUI.Common.ChartPieSerieStyle(radChartAggregatesConsumption, _series);
            Telerik.Charting.ChartSeriesItemsCollection _points = new Telerik.Charting.ChartSeriesItemsCollection();

            Double _share;
            String _unitSymbol = values.Unit.Symbol; String _legend = String.Empty;
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

            if(co2)
                radChartAggregatesCO2.Series.Add(_series);
            else
                radChartAggregatesConsumption.Series.Add(_series);

        }
        
        private void LoadTargetZones(Telerik.Web.UI.RadChart chart, Double target, Double max)
        {
            if (target > 0)
            {
                Telerik.Charting.ChartMarkedZone _zone = new Telerik.Charting.ChartMarkedZone();
                
                _zone = new Telerik.Charting.ChartMarkedZone();
                _zone.ValueStartY = target;
                _zone.ValueEndY = max + max * 0.1;
                WebUI.Common.ChartZoneStyle(_zone);
                chart.PlotArea.MarkedZones.Add(_zone);
            }
        }
        private void LoadXAxis(Telerik.Web.UI.RadChart chart, String[] dates)
        {
            int x;

            chart.PlotArea.XAxis.AutoScale = false;
            chart.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = 90;
            chart.PlotArea.XAxis.AddRange(1, dates.Length, 1);
            for (x = 0; x < dates.Length; x++)
            {
                chart.PlotArea.XAxis.Items[x].TextBlock.Text = dates[x];
            }
        }

        #endregion

        #region Events

        void rptLoadSeries_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Sites.Meters.Series.FuelSerie _meterSerie = (Library.Objects.Sites.Meters.Series.FuelSerie)e.Item.DataItem;
                
                Label _lbl = ((Label)e.Item.FindControl("lblDate"));
                _lbl.Text = _meterSerie.Date.ToShortDateString();

                _lbl = ((Label)e.Item.FindControl("lblFuel"));
                _lbl.Text = _meterSerie.FuelType.Name;

                _lbl = ((Label)e.Item.FindControl("lblUnit"));
                _lbl.Text = _meterSerie.Unit.Name;

                _lbl = ((Label)e.Item.FindControl("lblTotal"));
                _lbl.Text = _meterSerie.Value.ToString();

                _lbl = ((Label)e.Item.FindControl("lblTotalCO2"));
                _lbl.Text = _meterSerie.TotalCO2.ToString();

                _lbl = ((Label)e.Item.FindControl("lblOperator"));
                _lbl.Text = _meterSerie.Operator.Fullname;
            }
        }
       
        #endregion
    }
}