using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSI.WebUI.Console.Dashboard.Reports
{
    public partial class MeterWaterReportPrintable : BasePage
    {
        private Library.Objects.Sites.Meters.WaterMeter _Meter;
        Library.Security.Authority.PermissionTypes _Permission;

        protected void Page_Init(object sender, EventArgs e)
        {
            _Meter = I.GetWaterMeter(Convert.ToInt64(Request.QueryString["Meter"].ToString()));
            _Permission = ((Library.Objects.Sites.SiteMine)_Meter.Site).CurrentPermission();

            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTexts();
                LoadMeter();
                LoadLoadSeries();

                LoadPerformance();

                SetCharts();
            }
        }

        private void BindControls()
        {
            rptLoads.ItemDataBound += new RepeaterItemEventHandler(rptLoadSeries_ItemDataBound);
        }
        private void LoadTexts()
        {
            lblSiteProperties.Text = Resources.Data.Properties;

            lblLoads.Text = Resources.Data.Series;

            lblChartSeriesConsumption.Text = Resources.Data.ChartsMonthlyEvolutions + " - " + Resources.Data.Consumption;
            lblChartSeriesCO2.Text = Resources.Data.ChartsMonthlyEvolutions + " - " + Resources.Data.CO2Generation;

            lblIdentification.Text = Resources.Data.Identification;
            lblDescription.Text = Resources.Data.Description;
            lblIsPhysical.Text = Resources.Data.IsPhysical;
            if (_Meter is Library.Objects.Sites.Meters.WaterMeterPhysical)
            {
                divInitialReading.Style.Add("display", "block");
                //lblInitialDate.Text = Resources.Data.InitialDate; 
                lblInitialReading.Text = Resources.Data.InitialReading;
            }

            lblEF.Text = Resources.Data.EF;
            lblUnit.Text = Resources.Data.DefaultUnit;
            lblFrequencyQuantity.Text = Resources.Data.Frequency;
            lblFrequencyUnits.Text = Resources.Data.Unit;
            lblAlertBefore.Text = Resources.Data.AlertBefore;
            lblAlertAfter.Text = Resources.Data.AlertAfter;
            lblAlertOnStart.Text = Resources.Data.AlertOnStart;

            lblPerformance.Text = Resources.Data.Performance;
            lblPerformanceHeaderWater.Text = Resources.Data.Water;

            lblLoadsHeaderFrom.Text = Resources.Data.From;
            lblLoadsHeaderTo.Text = Resources.Data.To;
            if (_Meter is Library.Objects.Sites.Meters.WaterMeterPhysical)
                lblLoadsHeaderReading.Text = Resources.Data.Reading;
            else
            {
                System.Web.UI.HtmlControls.HtmlTableCell _row = (System.Web.UI.HtmlControls.HtmlTableCell)thLoadsHeaderReading;
                _row.Visible = false;
            }

            lblLoadsHeaderTotal.Text = Resources.Data.Total;
            lblLoadsHeaderTotalCO2.Text = Resources.Data.CO2Total;
            lblLoadsHeaderOperator.Text = Resources.Data.Operator;

        }
        private void LoadMeter()
        {
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

        }
        private void LoadLoadSeries()
        {
            rptLoads.DataSource = _Meter.GetLoads().Values;
            rptLoads.DataBind();
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
            lblPerformanceWaterUnits.Text = WebUI.Common.RoundAndFormat(_stats.Sum).ToString();
            lblPerformanceWaterUnit.Text = _stats.Unit.Symbol;
            lblPerformanceWaterCO2.Text = WebUI.Common.RoundAndFormat(_stats.SumCO2).ToString();
            lblPerformanceWaterCO2Unit.Text = Resources.Data.CO2Unit;
            lblPerformanceWaterKPI.Text = WebUI.Common.RoundAndFormat((Double)(_area > 0 ? _stats.Sum / _area : 0)).ToString();
            lblPerformanceWaterKPIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _stats.Sum / _costValue : 0)).ToString();
            lblPerformanceWaterKPIMoneyUnit.Text = _symbol + "/" + _currency;
            lblPerformanceWaterKPIUnit.Text = _symbol + "/" + Resources.Data.KPIM2Unit;
        }

        #region Charts

        private void SetCharts()
        {
            radChartSeriesConsumption.ChartTitle.TextBlock.Text = "";
            radChartSeriesCO2.ChartTitle.TextBlock.Text = "";

            //Targets
            Library.Objects.Sites.Targets _target = ((Library.Objects.Sites.SiteMine)_Meter.Site).Targets;

            LoadChart(Resources.Data.ChartWaterSumMonthly, Resources.Data.Water, _Meter.GetSerieMonthly(), false, _target.WaterConsumption, _target.WaterUnit);
            LoadChart(Resources.Data.ChartWaterSumMonthly, Resources.Data.Water, _Meter.GetSerieMonthly(), true, _target.WaterCO2, I.CO2DefaultUnit());

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

                    //Set value
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
                Library.Objects.Sites.Meters.Series.WaterLoad _meterLoad = (Library.Objects.Sites.Meters.Series.WaterLoad)e.Item.DataItem;

                Label _lbl = ((Label)e.Item.FindControl("lblLoadFrom"));
                _lbl.Text = _meterLoad.From.ToShortDateString();

                _lbl = ((Label)e.Item.FindControl("lblLoadTo"));
                _lbl.Text = _meterLoad.To.ToShortDateString();

                if (_Meter is Library.Objects.Sites.Meters.WaterMeterPhysical)
                {
                    _lbl = ((Label)e.Item.FindControl("lblLoadReading"));
                    _lbl.Text = _meterLoad.ValueInput.ToString();
                }
                else
                {
                    System.Web.UI.HtmlControls.HtmlTableCell _row = (System.Web.UI.HtmlControls.HtmlTableCell)e.Item.FindControl("tdLoadReading");
                    _row.Visible = false;
                }

                _lbl = ((Label)e.Item.FindControl("lblLoadTotal"));
                _lbl.Text = _meterLoad.Value.ToString();

                _lbl = ((Label)e.Item.FindControl("lblLoadTotalCO2"));
                _lbl.Text = _meterLoad.TotalCO2.ToString() + " " + Resources.Data.CO2Unit;

                _lbl = ((Label)e.Item.FindControl("lblLoadOperator"));
                _lbl.Text = _meterLoad.Operator.Fullname;
            }
        }

        #endregion
    }
}