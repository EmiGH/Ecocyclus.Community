using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CSI.Library.Objects.Users;


namespace CSI.WebUI.Console.Dashboard.Meters
{
    public partial class MeterElectricityLoadAdd : BasePage
    {
        private Library.Objects.Sites.Meters.ElectricityMeter _Meter;

        protected void Page_Init(object sender, EventArgs e)
        {
            _Meter = I.GetElectricityMeter(Convert.ToInt64(Request.QueryString["Meter"])); //Permissions
            
            //Permissions
            Library.Security.Authority.PermissionTypes _permission = ((Library.Objects.Sites.SiteMine)_Meter.Site).CurrentPermission();
            if (_permission != Library.Security.Authority.PermissionTypes.SiteManager && _permission != Library.Security.Authority.PermissionTypes.SiteOperator)
            {
                throw new ApplicationException(Resources.Messages.AccessDenied);
            }
           
            BindControls();
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMenu();
                LoadFrequencyUnits();
                LoadUnits();
                LoadTexts();
                LoadData();
            }
        }

        #region Methods

        private void BindControls()
        {
            btnSave.Click += new EventHandler(btnSave_Click);
            lnkLoadSetGrid.Click += new EventHandler(lnkLoadSetGrid_Click);
            rptLoadGrid.ItemDataBound += new RepeaterItemEventHandler(rptLoadGrid_ItemDataBound);
        }


        private void SetMenu()
        {
            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)_Meter.Site;
            ((Main)Page.Master).MenuNavigation.Initialize(_site.IdSite, _site.Title, _Meter.IdMeter, _Meter.Identification, Console.Controls.ucMenuNavigation.MeterType.Electricity, Console.Controls.ucMenuNavigation.MenuItem.MeterLoad, WebUI.Common.GetPermissionFromContext(I,_site));
           
        }
        private void LoadUnits()
        {
            Int64 _meterUnit = _Meter.DefaultUnit.IdUnit;

            ddlLoadUnits.Items.Clear();
            foreach (Library.Objects.Auxiliaries.Units.Unit _unit in I.GetElectricityUnits().Values)
            {
                ListItem _item = new ListItem(_unit.Symbol + " [" + _unit.Name + "]", _unit.IdUnit.ToString());
                if (_unit.IdUnit == _meterUnit) _item.Selected = true;

                ddlLoadUnits.Items.Add(_item);
            }
        }
        private void LoadFrequencyUnits()
        {
            Int16 _meterFrequencyUnit = _Meter.FrequencyUnit;

            ddlLoadFrequencyUnit.Items.Clear();
            foreach (Library.Objects.Auxiliaries.Units.TimeUnit.Units _unit in Enum.GetValues(typeof(Library.Objects.Auxiliaries.Units.TimeUnit.Units)))
            {
                ListItem _item = new ListItem(Enum.GetName(typeof(Library.Objects.Auxiliaries.Units.TimeUnit.Units), _unit), _unit.ToString());
                if ((Int16)_unit == _meterFrequencyUnit) _item.Selected = true;
                
                ddlLoadFrequencyUnit.Items.Add(_item);
            }
        }
        private void LoadTexts()
        {
            String _helpPostfix = Resources.Data.HelpPostfix;
            String _mandatoryField = Resources.Data.MandatoryField;

            lblSiteProperties.Text = Resources.Data.Properties;
            lblMandatoryFieldsExplanation.Text = Resources.Data.MandatoryFieldExplanation;

            lblIdentification.Text = Resources.Data.Identification;
            lblDescription.Text = Resources.Data.Description;
            lblIsPhysical.Text = Resources.Data.IsPhysical;
            if (_Meter is Library.Objects.Sites.Meters.ElectricityMeterPhysical)
            {
                divInitialReading.Style.Add("display", "block");
                lblInitialDate.Text = Resources.Data.InitialDate;
                lblInitialReading.Text = Resources.Data.InitialReading;

                lnkLoadSetGrid.Text = Resources.Data.SetReading;
            } 
            else
                lnkLoadSetGrid.Text = Resources.Data.SetConsumption;

            lblEF.Text = Resources.Data.EF;
            lblUnit.Text = Resources.Data.DefaultUnit;
            lblFrequencyQuantity.Text = Resources.Data.Frequency;
            lblFrequencyUnits.Text = Resources.Data.TimeUnit;
            lblAlertBefore.Text = Resources.Data.AlertBefore;
            lblAlertAfter.Text = Resources.Data.AlertAfter;
            lblAlertOnStart.Text = Resources.Data.AlertOnStart;

            lblLoadHeader.Text = Resources.Data.Load;
            lblLoadHeaderFrom.Text = Resources.Data.From + _mandatoryField + _helpPostfix;
            lblLoadHeaderTo.Text = Resources.Data.To + _mandatoryField + _helpPostfix;
            lblLoadHeaderUnit.Text = Resources.Data.Unit + _mandatoryField + _helpPostfix;
            lblLoadHeaderInterval.Text = Resources.Data.Interval + _mandatoryField + _helpPostfix;
            calLoadFrom.Format = calLoadTo.Format = CurrentCultureInfo.DateTimeFormat.ShortDatePattern;
            
            lblLoadGrid.Text = Resources.Data.LoadGrid;
            lblLoadGridHeaderFrom.Text = Resources.Data.From + _helpPostfix;
            lblLoadGridHeaderTo.Text = Resources.Data.To + _helpPostfix;
            lblLoadGridHeaderUnit.Text = Resources.Data.Unit + _helpPostfix;
            if (_Meter is Library.Objects.Sites.Meters.ElectricityMeterPhysical)
            {
                lblLoadGridHeaderValue.Text = Resources.Data.Reading + _mandatoryField + _helpPostfix;
                if (!_Meter.HasValue())
                {
                    lblFirstDataWarning.Text = Resources.Data.FirstValueWarning;
                    lblFirstDataWarning.Style.Add("display", "block");
                }
            }
            else
                lblLoadGridHeaderValue.Text = Resources.Data.Value + _mandatoryField + _helpPostfix;

            btnSave.Text = Resources.Data.Save;

            cvLoadDates.Text = cvDates.Text = rfvLoadFrequencyQuantity.Text = cvLoadFrequencyQuantity.Text = 
            cvLoadFrequencyUnit.Text = cvLoadUnit.Text = Resources.Messages.SummaryErrorCharacter;
        }
        private void LoadData()
        {
            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)_Meter.Site;
            
            String _validRangeLabel = Resources.Data.ValidLoadRangeExplanation;
            _validRangeLabel = _validRangeLabel.Replace("[from]", _site.LoadTimeRange.Start.ToShortDateString());
            _validRangeLabel = _validRangeLabel.Replace("[to]", _site.LoadTimeRange.End.ToShortDateString());
            lblValidLoadRange.Text = _validRangeLabel; 

            lblIdentificationValue.Text = _Meter.Identification;
            lblDescriptionValue.Text = _Meter.LanguageOption.Description;

            if (_Meter is Library.Objects.Sites.Meters.ElectricityMeterPhysical)
                lblInitialReadingValue.Text = ((Library.Objects.Sites.Meters.ElectricityMeterPhysical)_Meter).InitialReading.ToString();
                        
            lblIsPhysicalValue.Text = WebUI.Common.GetBooleanTranslation(_Meter is Library.Objects.Sites.Meters.ElectricityMeterPhysical);

            Library.Objects.Auxiliaries.EmissionFactors.EmissionFactor _ef = _Meter.EmissionFactor;
            lblEFValue.Text = _ef.Value.ToString() + " " + Resources.Data.CO2Unit + "/" + _ef.Unit.Symbol;
            
            lblUnitValue.Text = _Meter.DefaultUnit.Name;
            txtLoadFrequency.Text = lblFrequencyQuantityValue.Text = _Meter.FrequencyQuantity.ToString();
            ddlLoadFrequencyUnit.SelectedValue = lblFrequencyUnitsValue.Text = Enum.GetName(typeof(Library.Objects.Auxiliaries.Units.TimeUnit.Units), _Meter.FrequencyUnit);
            lblAlertBeforeValue.Text = _Meter.AlertBeforeInDays.ToString();
            lblAlertAfterValue.Text = _Meter.AlertAfterInDays.ToString();
            lblAlertOnStartValue.Text = WebUI.Common.GetBooleanTranslation(_Meter.AlertOnStart);

            String _nextDate;
            if (_Meter.HasValue())
                _nextDate = _Meter.GetNextDate().Value.ToShortDateString();
            else
                if (_Meter is Library.Objects.Sites.Meters.ElectricityMeterPhysical)
                {
                    txtLoadFrom.Attributes.Add("readonly", "readonly");
                    _nextDate = ((Library.Objects.Sites.Meters.ElectricityMeterPhysical)_Meter).InitialDate.ToShortDateString();
                }
                else
                    _nextDate = _Meter.Site.Start.ToShortDateString();

            txtLoadFrom.Text = _nextDate;
            calLoadFrom.Enabled = false;
            SetEnabledDates();
    
        }
        private void SetEnabledDates()
        {
            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)_Meter.Site;
            calLoadFrom.StartDate = calLoadTo.StartDate = _site.LoadTimeRange.Start;
            calLoadFrom.EndDate = calLoadTo.EndDate = _site.LoadTimeRange.End;
        }
        private void SetGrid()
        {
            Library.Objects.Auxiliaries.Units.TimeUnit.Units _timeUnit = (Library.Objects.Auxiliaries.Units.TimeUnit.Units)Enum.Parse(typeof(Library.Objects.Auxiliaries.Units.TimeUnit.Units), ddlLoadFrequencyUnit.Text);
            
            DataTable _dt = new DataTable();
            _dt.Columns.Add(new DataColumn("From", typeof(DateTime)));
            _dt.Columns.Add(new DataColumn("To", typeof(DateTime)));

            Int16 _intervalQuantity = Convert.ToInt16(txtLoadFrequency.Text);

            DateTime _currentDate = Convert.ToDateTime(hdnLoadFrom.Value);
            DateTime _tillDate = Convert.ToDateTime(hdnLoadTo.Value);
            DateTime _nextDate = _currentDate;
            
            while(_currentDate<_tillDate)
            {
                switch (_timeUnit)
                {
                    case CSI.Library.Objects.Auxiliaries.Units.TimeUnit.Units.Period:
                        _nextDate = _tillDate;
                        break;
                    case CSI.Library.Objects.Auxiliaries.Units.TimeUnit.Units.Day:
                        _nextDate = _currentDate.AddDays(_intervalQuantity);                                    
                        break;
                    case CSI.Library.Objects.Auxiliaries.Units.TimeUnit.Units.Week:
                        _nextDate = _currentDate.AddDays(_intervalQuantity*7);                                    
                        break;
                    case CSI.Library.Objects.Auxiliaries.Units.TimeUnit.Units.Month:
                        _nextDate = _currentDate.AddMonths(_intervalQuantity);                                    
                        break;
                    case CSI.Library.Objects.Auxiliaries.Units.TimeUnit.Units.Year:
                        _nextDate = _currentDate.AddYears(_intervalQuantity);                                    
                        break;
                    default:
                        break;
                }
                _dt.Rows.Add(_currentDate, _nextDate);
                _currentDate = _nextDate;
            }

            rptLoadGrid.DataSource = _dt;
            rptLoadGrid.DataBind();
        }
        private List<Library.Objects.Sites.Meters.Series.ElectricityData> BuildData()
        {
            List<Library.Objects.Sites.Meters.Series.ElectricityData> _data = new List<Library.Objects.Sites.Meters.Series.ElectricityData>();

            foreach (RepeaterItem _item in rptLoadGrid.Items)
            {
                if (_item.ItemType == ListItemType.Item || _item.ItemType == ListItemType.AlternatingItem)
                {
                    DateTime _from = Convert.ToDateTime(((Label)(_item.FindControl("lblLoadGridFrom"))).Text);
                    DateTime _to = Convert.ToDateTime(((Label)(_item.FindControl("lblLoadGridTo"))).Text);
                    Double _value = Convert.ToDouble(((TextBox)(_item.FindControl("txtLoadGridValue"))).Text);

                    _data.Add(new Library.Objects.Sites.Meters.Series.ElectricityData(_from, _to, _value)); 
                }
            }

            return _data;
        }
        private void SaveData()
        {
            if (Page.IsValid)
            {
                try
                {
                    I.AddElectricityData(_Meter, BuildData());

                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterElectricityLoads.aspx?Meter=" + _Meter.IdMeter.ToString(), false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception exception)
                {
                    String _error = Resources.Messages.StandardError;
                    _error = _error.Replace("[error]", exception.Message);
                    _error = _error.Replace("[mail]", Resources.Data.HelpDeskMailAddress);

                    ((Main)Page.Master).ErrorHandler.SetMessage(Resources.Data.Error, _error);
                }
            }
            else
            {
                ((Main)Page.Master).ErrorHandler.SetMessage(Resources.Data.Information, Resources.Messages.SummaryCheck);

            }
        }

        #endregion

        #region Page Events

        void rptLoadGrid_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView _dr = (DataRowView)e.Item.DataItem;

                Label _lbl = ((Label)e.Item.FindControl("lblLoadGridFrom"));
                _lbl.Text = ((DateTime)_dr["From"]).ToShortDateString();

                _lbl = ((Label)e.Item.FindControl("lblLoadGridTo"));
                _lbl.Text = ((DateTime)_dr["To"]).ToShortDateString();

                _lbl = ((Label)e.Item.FindControl("lblLoadGridUnit"));
                _lbl.Text = ddlLoadUnits.SelectedItem.Text;
            }
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        void lnkLoadSetGrid_Click(object sender, EventArgs e)
        {
            SetGrid();
        }

        #endregion
    }
}