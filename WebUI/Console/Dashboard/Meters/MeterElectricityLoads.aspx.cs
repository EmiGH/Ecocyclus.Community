using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard.Meters
{
    public partial class MeterElectricityLoads : BasePage
    {
        private Library.Objects.Sites.Meters.ElectricityMeter _Meter;
        Library.Security.Authority.PermissionTypes _Permission;

        protected void Page_Init(object sender, EventArgs e)
        {
            _Meter = I.GetElectricityMeter(Convert.ToInt64(Request.QueryString["Meter"].ToString()));
            _Permission = ((Library.Objects.Sites.SiteMine)_Meter.Site).CurrentPermission();
            
            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMenu();
                LoadTexts();
                LoadMeter();
                LoadLoadSeries();
                LoadChartSeries();
                LoadPerformance();

            }
        }

        private void BindControls()
        {
            hplPrint.Attributes.Add("onclick", "window.open('" + WebUI.Common.GetPath(WebUI.Common.eFolders.Reports, Request) + "MeterElectricityReportPrintable.aspx?Meter=" + _Meter.IdMeter.ToString() + "', '_blank');return false;");

            rptLoads.ItemDataBound += new RepeaterItemEventHandler(rptLoadSeries_ItemDataBound);
            rptLoads.ItemCommand += new RepeaterCommandEventHandler(rptLoadSeries_ItemCommand);
            ddlChartSeries.SelectedIndexChanged += new EventHandler(ddlChartSeries_SelectedIndexChanged);
        }
        private void SetMenu()
        {
            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)_Meter.Site;
            ((Main)Page.Master).MenuNavigation.Initialize(_site.IdSite, _site.Title, _Meter.IdMeter, _Meter.Identification, Console.Controls.ucMenuNavigation.MeterType.Electricity, Console.Controls.ucMenuNavigation.MenuItem.MeterSeries, WebUI.Common.GetPermissionFromContext(I,_site));
         
        }
        private void LoadTexts()
        {
            lblSiteProperties.Text = Resources.Data.Properties;
            lblLoads.Text = Resources.Data.Series;
    
            //Printable
            hplPrint.ToolTip = Resources.Data.PrintableVersion;
            
            lblChartSeries.Text = Resources.Data.ChartsMonthlyEvolutions;

            lblIdentification.Text = Resources.Data.Identification;
            lblDescription.Text = Resources.Data.Description;
            lblIsPhysical.Text = Resources.Data.IsPhysical;
            if (_Meter is Library.Objects.Sites.Meters.ElectricityMeterPhysical)
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
            lblPerformanceHeaderElectricity.Text = Resources.Data.Electricity;

            lblLoadsHeaderFrom.Text = Resources.Data.From;
            lblLoadsHeaderTo.Text = Resources.Data.To;
            if (_Meter is Library.Objects.Sites.Meters.ElectricityMeterPhysical)
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
            if (_Meter is Library.Objects.Sites.Meters.ElectricityMeterPhysical)
                lblInitialReadingValue.Text = ((Library.Objects.Sites.Meters.ElectricityMeterPhysical)_Meter).InitialReading.ToString();
            
            lblIsPhysicalValue.Text = WebUI.Common.GetBooleanTranslation(_Meter is Library.Objects.Sites.Meters.ElectricityMeterPhysical);

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
            lblPerformanceElectricityUnits.Text = WebUI.Common.RoundAndFormat(_stats.Sum).ToString();
            lblPerformanceElectricityUnit.Text = _symbol;
            lblPerformanceElectricityCO2.Text = WebUI.Common.RoundAndFormat(_stats.SumCO2).ToString();
            lblPerformanceElectricityCO2Unit.Text = Resources.Data.CO2Unit;
            lblPerformanceElectricityKPI.Text = WebUI.Common.RoundAndFormat((Double)(_area > 0 ? _stats.Sum / _area : 0)).ToString();
            lblPerformanceElectricityKPIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _stats.Sum / _costValue : 0)).ToString();
            lblPerformanceElectricityKPIMoneyUnit.Text = _symbol + "/" + _currency;
            lblPerformanceElectricityKPIUnit.Text = _symbol + "/" + Resources.Data.KPIM2Unit;
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
                _targetValue = _target.ElectricityCO2;
                _targetUnit = I.CO2DefaultUnit();

            }
            else
            {
                _targetValue = _target.ElectricityConsumption;
                _targetUnit = _target.ElectricityUnit;
            }

            LoadChart(Resources.Data.ChartElectricitySumMonthly, Resources.Data.Electricity, _Meter.GetSerieMonthly(), _co2, _targetValue, _targetUnit);

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

        void rptLoadSeries_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Sites.Meters.Series.ElectricityLoad _meterLoad = (Library.Objects.Sites.Meters.Series.ElectricityLoad)e.Item.DataItem;

                String _idLoad = _meterLoad.IdLoad.ToString();

                Button _btnEdit = ((Button)e.Item.FindControl("btnLoadEdit"));
                _btnEdit.ToolTip = Resources.Data.Edit;
                Button _btnRemove = ((Button)e.Item.FindControl("btnLoadDelete"));
                _btnRemove.ToolTip = Resources.Data.Delete;
                if (_Meter.Site is Library.Objects.Sites.SiteMineOpen && _Permission == Library.Security.Authority.PermissionTypes.SiteManager || _Permission == Library.Security.Authority.PermissionTypes.SiteOperator)
                {
                    _btnEdit.CommandArgument = _idLoad;
                    _btnRemove.CommandArgument = _idLoad;

                    _btnRemove.Attributes.Add("onclick", "alertOnLoadDelete();");
                    AddConfirmRequest((WebControl)_btnRemove, Resources.Messages.ConfirmDeleteTitle, Resources.Messages.ConfirmDeleteMessage);
                }
                else
                {
                    _btnEdit.Visible = false;
                    _btnRemove.Visible = false;
                }

                Label _lbl = ((Label)e.Item.FindControl("lblLoadFrom"));
                _lbl.Text = _meterLoad.From.ToShortDateString();

                _lbl = ((Label)e.Item.FindControl("lblLoadTo"));
                _lbl.Text = _meterLoad.To.ToShortDateString();

                if (_Meter is Library.Objects.Sites.Meters.ElectricityMeterPhysical)
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
        void rptLoadSeries_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterElectricityLoadEdit.aspx?Meter=" + _Meter.IdMeter + "&Load=" + e.CommandArgument, false);
                    Context.ApplicationInstance.CompleteRequest();
                    break;

                case "Delete":
                    try
                    {
                        I.RemoveElectricityData(_Meter, _Meter.GetLoad(Convert.ToInt64(e.CommandArgument)));
                    }
                    catch (Exception exception)
                    {
                        String _error = Resources.Messages.StandardError;
                        _error = _error.Replace("[error]", exception.Message);
                        _error = _error.Replace("[mail]", Resources.Data.HelpDeskMailAddress);

                        ((Main)Page.Master).ErrorHandler.SetMessage(Resources.Data.Error, _error);
                    }
                    LoadLoadSeries();
                    LoadPerformance();
                    LoadChartSeries();
                    break;

                default:
                    break;
            }

        }
        void ddlChartSeries_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetChart();
        }

        #endregion

    }
}