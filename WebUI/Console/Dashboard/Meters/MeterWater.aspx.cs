using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Sites.Meters;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard.Meters
{
    public partial class MeterWater : BasePage
    {
        private Library.Objects.Sites.Meters.WaterMeter _Meter;

        protected void Page_Init(object sender, EventArgs e)
        {
            _Meter = I.GetWaterMeter(Convert.ToInt64(Request.QueryString["Meter"].ToString()));

            //If no sites then show guide
            if (_Meter.GetLoads().Count == 0)
            {
                ((Main)Page.Master).Guide.SetMessage(Resources.Messages.FirstTimeNeedHelp, Resources.Messages.FirstTimeGuideMeter, 100, 100);
                ((Main)Page.Master).MenuNavigation.Blink(Console.Controls.ucMenuNavigation.MenuItem.MeterLoad);
            }
            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMenu();
                LoadTexts();
                LoadMeter();
                LoadChartSeries();
                LoadPerformance();
            }
        }

        private void BindControls()
        {
            ddlChartSeries.SelectedIndexChanged += new EventHandler(ddlChartSeries_SelectedIndexChanged);
            lnkRemove.Click += new EventHandler(lnkRemove_Click);
            AddConfirmRequest((WebControl)lnkRemove, Resources.Messages.ConfirmDeleteTitle, Resources.Messages.ConfirmDeleteMessage);
        }
        private void SetMenu()
        {
            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)_Meter.Site;
            ((Main)Page.Master).MenuNavigation.Initialize(_site.IdSite, _site.Title, _Meter.IdMeter, _Meter.Identification, Console.Controls.ucMenuNavigation.MeterType.Water, Console.Controls.ucMenuNavigation.MenuItem.Meter, WebUI.Common.GetPermissionFromContext(I,_site));
           
            //Permissions
            Library.Security.Authority.PermissionTypes _permission = _site.CurrentPermission();
            if (I is UserOperatorMeManager || _permission == Library.Security.Authority.PermissionTypes.SiteManager)
            {
                lnkEdit.ToolTip = Resources.Data.Modify;
                lnkEdit.PostBackUrl = "MeterWaterEdit.aspx?Site=" + _site.IdSite.ToString() + "&Meter=" + _Meter.IdMeter.ToString();
                lnkRemove.ToolTip = Resources.Data.Remove;
            }
            else
            {
                lnkEdit.Visible = false;
                lnkRemove.Visible = false;
            }
        }
        private void LoadTexts()
        {

            lblSiteProperties.Text = Resources.Data.Properties;
            
            lblChartSeries.Text = Resources.Data.ChartsMonthlyEvolutions;
            
            lblIdentification.Text = Resources.Data.Identification;
            lblDescription.Text = Resources.Data.Description;
            lblIsPhysical.Text = Resources.Data.IsPhysical;
            if (_Meter is Library.Objects.Sites.Meters.WaterMeterPhysical)
            {
                divInitialDate.Style.Add("display", "block");
                divInitialReading.Style.Add("display", "block");
                lblInitialDate.Text = Resources.Data.InitialDate;
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
        }
        private void LoadMeter()
        {
            lblIdentificationValue.Text = _Meter.Identification;
            lblDescriptionValue.Text = _Meter.LanguageOption.Description;
            if (_Meter is Library.Objects.Sites.Meters.WaterMeterPhysical)
            {
                Library.Objects.Sites.Meters.WaterMeterPhysical _casted = (Library.Objects.Sites.Meters.WaterMeterPhysical)_Meter;
                lblInitialDateValue.Text = _casted.InitialDate.ToShortDateString();
                lblInitialReadingValue.Text = _casted.InitialReading.ToString();
            }
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
            lblPerformanceWaterKPIMoneyUnit.Text = _symbol + _currency;
            lblPerformanceWaterKPIUnit.Text = _symbol + "/" + Resources.Data.KPIM2Unit;
        }

        private void LoadChartSeries()
        {
            ddlChartSeries.Items.Add(new ListItem(Resources.Data.Consumption, "0"));
            ddlChartSeries.Items.Add(new ListItem(Resources.Data.CO2Generation, "1"));
            SetChart();
        }

        #region Charts

        private void SetChart()
        {
            radChartSeries.Series.Clear();
            radChartSeries.PlotArea.MarkedZones.Clear();

            //Targets
            Double _targetValue;
            Library.Objects.Auxiliaries.Units.Unit _targetUnit;
            Library.Objects.Sites.Targets _target = ((Library.Objects.Sites.SiteMine)_Meter.Site).Targets;

            Boolean _co2 = false;
            if (ddlChartSeries.SelectedValue == "1")
            {
                _co2 = true;
                _targetValue = _target.WaterCO2;
                _targetUnit = I.CO2DefaultUnit();

            }
            else
            {
                _targetValue = _target.WaterConsumption;
                _targetUnit = _target.WaterUnit;
            }

            LoadChart(Resources.Data.ChartWaterSumMonthly, Resources.Data.Water, _Meter.GetSerieMonthly(), _co2, _targetValue, _targetUnit);

        }

        private void LoadChart(String title, String titleSerie, Dictionary<DateTime, Library.Objects.Metrics.MetricInstant> values, Boolean co2, Double target, Library.Objects.Auxiliaries.Units.Unit targetUnit)
        {
            radChartSeries.ChartTitle.TextBlock.Text = "";

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
                    LoadXAxis(_dates);
                    _series.AddItem(_points);
                    radChartSeries.Series.Add(_series);
                    LoadTargetZones(_target, _max);
                }

            }

        }
        private void LoadTargetZones(Double target, Double max)
        {
            if (target > 0)
            {
                Telerik.Charting.ChartMarkedZone _zone = new Telerik.Charting.ChartMarkedZone();
                //_zone.ValueStartY = 0;
                //_zone.ValueEndY = target;
                //_zone.Appearance.FillStyle.MainColor = System.Drawing.Color.LightGreen;
                //radChartSeries.PlotArea.MarkedZones.Add(_zone);

                _zone = new Telerik.Charting.ChartMarkedZone();
                _zone.ValueStartY = target;
                _zone.ValueEndY = max + max * 0.1;
                WebUI.Common.ChartZoneStyle(_zone);
                radChartSeries.PlotArea.MarkedZones.Add(_zone);
            }
        }
        private void LoadXAxis(String[] dates)
        {
            int x;

            radChartSeries.PlotArea.XAxis.AutoScale = false;
            radChartSeries.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = 90;
            radChartSeries.PlotArea.XAxis.AddRange(1, dates.Length, 1);
            for (x = 0; x < dates.Length; x++)
            {
                radChartSeries.PlotArea.XAxis.Items[x].TextBlock.Text = dates[x];
            }
        }


        #endregion


        #region Events
        
        void ddlChartSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetChart();
        }
        void lnkRemove_Click(object sender, EventArgs e)
        {
            I.RemoveMeterWater(_Meter);
            Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "Meters.aspx?Site=" + _Meter.Site.IdSite.ToString());
        }
        #endregion
    }
}