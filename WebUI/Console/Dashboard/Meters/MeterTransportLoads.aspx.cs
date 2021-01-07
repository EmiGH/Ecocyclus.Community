using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard.Meters
{
    public partial class MeterTransportLoads : BasePage
    {
        private Library.Objects.Sites.Meters.TransportMeter _Meter;
        Library.Security.Authority.PermissionTypes _Permission;

        protected void Page_Init(object sender, EventArgs e)
        {
            _Meter = I.GetTransportMeter(Convert.ToInt64(Request.QueryString["Meter"].ToString()));
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
                LoadChartAggregatesOptions();

                LoadChartSeries();
                LoadPerformance();
            }
        }

        private void BindControls()
        {
            hplPrint.Attributes.Add("onclick", "window.open('" + WebUI.Common.GetPath(WebUI.Common.eFolders.Reports, Request) + "MeterTransportReportPrintable.aspx?Meter=" + _Meter.IdMeter.ToString() + "', '_blank');return false;");

            rptLoadSeries.ItemDataBound += new RepeaterItemEventHandler(rptLoadSeries_ItemDataBound);
            rptLoadSeries.ItemCommand += new RepeaterCommandEventHandler(rptLoadSeries_ItemCommand);

            ddlChartSeries.SelectedIndexChanged += new EventHandler(ddlChartSeries_SelectedIndexChanged);
            ddlChartAggregates.SelectedIndexChanged += new EventHandler(ddlChartAggregates_SelectedIndexChanged);

        }
        private void SetMenu()
        {
            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)_Meter.Site;
            ((Main)Page.Master).MenuNavigation.Initialize(_site.IdSite, _site.Title, _Meter.IdMeter, _Meter.Identification, Console.Controls.ucMenuNavigation.MeterType.Transport, Console.Controls.ucMenuNavigation.MenuItem.MeterSeries, WebUI.Common.GetPermissionFromContext(I,_site));
        }
        private void LoadTexts()
        {

            lblSiteProperties.Text = Resources.Data.Properties;
            lblSeries.Text = Resources.Data.Series;

            //Printable
            hplPrint.ToolTip = Resources.Data.PrintableVersion;

            lblChartSeries.Text = Resources.Data.ChartsMonthlyEvolutions;
            lblChartAggregates.Text = Resources.Data.ChartsCompositions;


            lblIdentification.Text = Resources.Data.Identification;
            lblDescription.Text = Resources.Data.Description;

            lblPerformance.Text = Resources.Data.Performance;
            lblPerformanceHeaderTransport.Text = Resources.Data.Transport;

            lblHeaderDate.Text = Resources.Data.Date;
            lblHeaderTransport.Text = Resources.Data.TransportType;
            lblHeaderPlateNumber.Text = Resources.Data.PlateNumber;
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
            lblPerformanceTransportUnits.Text = WebUI.Common.RoundAndFormat(_stats.Sum).ToString();
            lblPerformanceTransportUnit.Text = _stats.Unit.Symbol;
            lblPerformanceTransportCO2.Text = WebUI.Common.RoundAndFormat(_stats.SumCO2).ToString();
            lblPerformanceTransportCO2Unit.Text = Resources.Data.CO2Unit;
            lblPerformanceTransportKPI.Text = WebUI.Common.RoundAndFormat((Double)(_area > 0 ? _stats.Sum / _area : 0)).ToString();
            lblPerformanceTransportKPIMoney.Text = WebUI.Common.RoundAndFormat((Double)(_costValue > 0 ? _stats.Sum / _costValue : 0)).ToString();
            lblPerformanceTransportKPIMoneyUnit.Text = _symbol + "/" + _currency;
            lblPerformanceTransportKPIUnit.Text = _symbol + "/" + Resources.Data.KPIM2Unit;
        }

        private void LoadChartSeries()
        {
            ddlChartSeries.Items.Add(new ListItem(Resources.Data.Consumption, "0"));
            ddlChartSeries.Items.Add(new ListItem(Resources.Data.CO2Generation, "1"));

            SetChart();
        }
        private void LoadChartAggregatesOptions()
        {
            ddlChartAggregates.Items.Add(new ListItem(Resources.Data.ChartCompositionByTypes, "0"));
            ddlChartAggregates.Items.Add(new ListItem(Resources.Data.ChartCO2CompositionByTypes, "1"));

            SetChartAggregates();
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
                _targetValue = _target.TransportCO2;
                _targetUnit = I.CO2DefaultUnit();

            }
            else
            {
                _targetValue = _target.TransportConsumption;
                _targetUnit = _target.TransportUnit;
            }

            LoadChart(Resources.Data.ChartTransportSumMonthly, Resources.Data.Transport, _Meter.GetSerieMonthly(), _co2, _targetValue, _targetUnit);

        }
        private void SetChartAggregates()
        {
            radChartAggregates.Series.Clear();

            if (ddlChartAggregates.SelectedValue == "0")
                LoadChartAggregates(Resources.Data.ChartCompositionByTypes, Resources.Data.Types, _Meter.GetStatisticsByTypes());
            else
                LoadChartAggregates(Resources.Data.ChartCO2CompositionByTypes, Resources.Data.Types, _Meter.GetStatisticsCO2ByTypes());

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
                Library.Objects.Sites.Meters.Series.TransportSerie _meterSerie = (Library.Objects.Sites.Meters.Series.TransportSerie)e.Item.DataItem;

                String _idSerie = _meterSerie.IdSerie.ToString();

                Button _btnEdit = ((Button)e.Item.FindControl("btnLoadEdit"));
                _btnEdit.ToolTip = Resources.Data.Edit;
                Button _btnRemove = ((Button)e.Item.FindControl("btnLoadDelete"));
                _btnRemove.ToolTip = Resources.Data.Delete;

                if (_Meter.Site is Library.Objects.Sites.SiteMineOpen && _Permission == Library.Security.Authority.PermissionTypes.SiteManager || _Permission == Library.Security.Authority.PermissionTypes.SiteOperator)
                {
                    _btnEdit.CommandArgument = _idSerie;
                    _btnRemove.CommandArgument = _idSerie;

                    _btnRemove.Attributes.Add("onclick", "alertOnLoadDelete();");
                    AddConfirmRequest((WebControl)_btnRemove, Resources.Messages.ConfirmDeleteTitle, Resources.Messages.ConfirmDeleteMessage);
                }
                else
                {
                    _btnEdit.Visible = false;
                    _btnRemove.Visible = false;
                }

                Label _lbl = ((Label)e.Item.FindControl("lblDate"));
                _lbl.Text = _meterSerie.Date.ToShortDateString();

                _lbl = ((Label)e.Item.FindControl("lblTransport"));
                _lbl.Text = _meterSerie.TransportType.Name;

                _lbl = ((Label)e.Item.FindControl("lblPlateNumber"));
                _lbl.Text = _meterSerie.PlateNumber;

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
        void rptLoadSeries_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Edit":
                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterTransportLoadEdit.aspx?Meter=" + _Meter.IdMeter + "&Serie=" + e.CommandArgument);
                    break;

                case "Delete":
                    try 
                    {
                        I.RemoveTransportData(_Meter, _Meter.GetSerie(Convert.ToInt64(e.CommandArgument)));
                    }
                    catch (Exception exception)
                    {
                        String _error = Resources.Messages.StandardError;
                        _error = _error.Replace("[error]", exception.Message);
                        _error = _error.Replace("[mail]", Resources.Data.HelpDeskMailAddress);

                        ((Main)Page.Master).ErrorHandler.SetMessage(Resources.Data.Error, _error);
                    }
                    LoadChartSeries();
                    LoadChartAggregatesOptions();

                    LoadLoadSeries();
                    LoadPerformance();
                    break;

                default:
                    break;
            }

        }
        void ddlChartSeries_SelectedIndexChanged(object sender, EventArgs e)
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