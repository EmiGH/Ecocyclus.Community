using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard.Meters
{
    public partial class MeterTransportLoadAdd : BasePage
    {
        private Library.Objects.Sites.Meters.TransportMeter _Meter;
        private Library.Objects.Auxiliaries.Geographic.Position _SitePosition;

        public String SiteLocation
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
        public String SiteAddress
        {
            set
            {
                ViewState["SiteAddress"] = value.Replace(',', '.');
            }
            get
            {
                return ViewState["SiteAddress"].ToString();
            }
        }
        private List<Library.Objects.Sites.Meters.Series.TransportData> _Data;

        protected void Page_Init(object sender, EventArgs e)
        {
            _Meter = I.GetTransportMeter(Convert.ToInt64(Request.QueryString["Meter"]));

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
            LoadMeter();
            
            if (!IsPostBack)
            {
                SetMenu();
                LoadTexts();
                LoadUnits();
                LoadTypes();
                SetEnabledDates();
            }
            LoadData();
        }

        #region Methods

        private void BindControls()
        {
            rdDistance.Attributes.Add("onClick", "js:switchLoadType()");
            rdLocation.Attributes.Add("onClick", "js:switchLoadType()");

            btnSave.Click += new EventHandler(btnSave_Click);
            lnkLoadAdd.Click += new EventHandler(lnkLoadAdd_Click);
            rptGrid.ItemDataBound += new RepeaterItemEventHandler(rptGrid_ItemDataBound);
            rptGrid.ItemCommand += new RepeaterCommandEventHandler(rptGrid_ItemCommand);
        }
        private void SetMenu()
        {
            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)_Meter.Site;
            ((Main)Page.Master).MenuNavigation.Initialize(_site.IdSite, _site.Title, _Meter.IdMeter, _Meter.Identification, Console.Controls.ucMenuNavigation.MeterType.Transport, Console.Controls.ucMenuNavigation.MenuItem.MeterLoad, WebUI.Common.GetPermissionFromContext(I,_site));
         
        }
        private void LoadMeter()
        {
            lblIdentificationValue.Text = _Meter.Identification;
            lblDescriptionValue.Text = _Meter.Description;

            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)_Meter.Site;

            String _validRangeLabel = Resources.Data.ValidLoadRangeExplanation;
            _validRangeLabel = _validRangeLabel.Replace("[from]", _site.LoadTimeRange.Start.ToShortDateString());
            _validRangeLabel = _validRangeLabel.Replace("[to]", _site.LoadTimeRange.End.ToShortDateString());
            lblValidLoadRange.Text = _validRangeLabel; 

            Library.Objects.Auxiliaries.Geographic.Location _siteLocation = _site.Contact.Location;
            _SitePosition = _siteLocation.Position;
            SiteLocation = _SitePosition.Coordenates;
            SiteAddress = _siteLocation.Address;
                        
        }
        private void SetEnabledDates()
        {
            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)_Meter.Site;
            calLoadDate.StartDate = _site.LoadTimeRange.Start;
            calLoadDate.EndDate = _site.LoadTimeRange.End;
        }
        private void LoadUnits()
        {
            Int64 _meterUnit = _Meter.DefaultUnit.IdUnit;

            ddlLoadDistanceUnits.Items.Clear();
            foreach (Library.Objects.Auxiliaries.Units.Unit _unit in I.GetTransportUnits().Values)
            {
                ListItem _item = new ListItem(_unit.Symbol + " [" + _unit.Name + "]", _unit.IdUnit.ToString());
                if (_unit.IdUnit == _meterUnit) _item.Selected = true;

                ddlLoadDistanceUnits.Items.Add(_item);
            }
        }
        private void LoadTypes()
        {
            ddlLoadTypes.Items.Clear();
            foreach (Library.Objects.Auxiliaries.Types.TransportType _type in I.GetTransportTypes(_Meter.Site.Country.IdCountry).Values)
            {
                ListItem _item = new ListItem(_type.Name, _type.IdTransportType.ToString());
                ddlLoadTypes.Items.Add(_item);
            }
        }
        private void LoadTexts()
        {
            String _helpPostfix = Resources.Data.HelpPostfix;
            String _mandatoryField = Resources.Data.MandatoryField;

            lblProperties.Text = Resources.Data.Properties;
            lblMandatoryFieldsExplanation.Text = Resources.Data.MandatoryFieldExplanation;

            lblIdentification.Text = Resources.Data.Identification;
            lblDescription.Text = Resources.Data.Description;

            calLoadDate.Format = CurrentCultureInfo.DateTimeFormat.ShortDatePattern;

            rdDistance.Text = Resources.Data.Distance;
            rdLocation.Text = Resources.Data.Location;

            lblLoadInputType.Text = Resources.Data.TransportInputType;
            lblLoadDistanceHeaderValue.Text = Resources.Data.Distance + _helpPostfix;
            lblLoadDistanceHeaderUnit.Text = Resources.Data.Unit + _helpPostfix;

            lblLoad.Text = Resources.Data.Load;
            lblGrid.Text = Resources.Data.LoadGrid;
            lblGridHeaderDate.Text = lblLoadHeaderDate.Text = Resources.Data.Date + _mandatoryField + _helpPostfix;
            lblGridHeaderPlateNumber.Text = lblLoadHeaderPlateNumber.Text = Resources.Data.PlateNumber + _helpPostfix;
            lblGridHeaderTransportType.Text = lblLoadHeaderTypes.Text = Resources.Data.TransportType + _mandatoryField + _helpPostfix;
            lblGridHeaderIsRoundtrip.Text = lblLoadHeaderIsRoundtrip.Text = Resources.Data.Roundtrip + _helpPostfix;

            //Validators
            cvGrid.Text = cvLoadTypes.Text = cvLocation.Text = cvLoadDate.Text = Resources.Messages.SummaryErrorCharacter;

            lnkLoadAdd.Text = Resources.Data.Add;
            btnSave.Text = Resources.Data.Save;

        }
        private void LoadData()
        {
            _Data = new List<Library.Objects.Sites.Meters.Series.TransportData>();
            Library.Objects.Sites.Meters.Series.TransportData _transportData = null;
            String _location;
            Double _drivingDistance;

            foreach (RepeaterItem _item in rptGrid.Items)
            {
                if (_item.ItemType == ListItemType.AlternatingItem || _item.ItemType == ListItemType.Item)
                {
                    Int32 _index = Convert.ToInt32(((Button)_item.FindControl("btnGridDelete")).CommandArgument);
                    DateTime _date = Convert.ToDateTime(((Label)_item.FindControl("lblGridDate")).Text);
                    Boolean _isRoundtrip = false; //Convert.ToBoolean(((HiddenField)_item.FindControl("hdnGridIsRoundtrip")).Value);
                    String _plateNumber = ((Label)_item.FindControl("lblGridPlateNumber")).Text;
                    Library.Objects.Auxiliaries.Types.TransportType _type = I.GetTransportType(Convert.ToInt64(((HiddenField)_item.FindControl("hdnGridIdTransportType")).Value));

                    String _address = ((HiddenField)_item.FindControl("hdnGridAddress")).Value;
                    if(_address!="")
                    {
                        _location = ((HiddenField)_item.FindControl("hdnGridLocation")).Value;
                        _drivingDistance = 0;
                        Double.TryParse(((HiddenField)_item.FindControl("hdnGridDrivingDistance")).Value, out _drivingDistance);

                        if(_drivingDistance>0)
                            _transportData = new Library.Objects.Sites.Meters.Series.TransportDataLocation(_index, _date, _address, _plateNumber, _isRoundtrip, _SitePosition, new Library.Objects.Auxiliaries.Geographic.Position(_location), _type,  _drivingDistance, I);
                        else
                            _transportData = new Library.Objects.Sites.Meters.Series.TransportDataLocation(_index, _date, _address, _plateNumber, _isRoundtrip, _SitePosition, new Library.Objects.Auxiliaries.Geographic.Position(_location), _type, I);
                    }
                    else
                    {
                        Int64 _idUnit = Convert.ToInt64(((HiddenField)_item.FindControl("hdnGridIdUnit")).Value);

                        Double _value = Convert.ToDouble(((HiddenField)_item.FindControl("hdnGridDistance")).Value);
                        _transportData = new Library.Objects.Sites.Meters.Series.TransportDataDistance(_index, _date, _plateNumber, _isRoundtrip, _type, _value, I.GetUnit(_idUnit)); 
                    }

                    _Data.Add(_transportData);
                }
            }
        }
        private void RemoveData(Int32 index)
        {
            for (int i = 0; i < _Data.Count; i++)
                if (_Data[i].Index == index)
                {
                    _Data.RemoveAt(i);
                    break;
                }
        }
        private void RefreshData()
        {
            rptGrid.DataSource = _Data;
            rptGrid.DataBind();
        }
        private void SaveData()
        {
            if (Page.IsValid)
            {
                try
                {
                    I.AddTransportData(_Meter, _Data);
                    
                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterTransport.aspx?Meter=" + _Meter.IdMeter.ToString(), false);
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

        void rptGrid_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":
                    RemoveData(Convert.ToInt32(e.CommandArgument));
                    RefreshData();
                    break;
            }
        }
        void rptGrid_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Sites.Meters.Series.TransportData _data = (Library.Objects.Sites.Meters.Series.TransportData)e.Item.DataItem;

                Button _btn = ((Button)e.Item.FindControl("btnGridDelete"));
                _btn.CommandArgument = _data.Index.ToString();
                _btn.Attributes.Add("onclick", "alertOnLoadDelete();");
                AddConfirmRequest((WebControl)_btn, Resources.Messages.ConfirmDeleteTitle, Resources.Messages.ConfirmDeleteMessage);

                Label _lbl = ((Label)e.Item.FindControl("lblGridDate"));
                _lbl.Text = _data.Date.ToShortDateString();
                
                _lbl = ((Label)e.Item.FindControl("lblGridPlateNumber"));
                _lbl.Text = _data.PlateNumber;

                _lbl = ((Label)e.Item.FindControl("lblGridIsRoundtrip"));
                _lbl.Text = WebUI.Common.GetBooleanTranslation(_data.IsRoundtrip);
                HiddenField _hdn = (HiddenField)e.Item.FindControl("hdnGridIsRoundtrip");
                _hdn.Value = _data.IsRoundtrip.ToString();
                
                Library.Objects.Auxiliaries.Types.TransportType _type = _data.TransportType;
                _lbl = ((Label)e.Item.FindControl("lblGridTransportType"));
                _lbl.Text = _type.Name;
                _hdn = (HiddenField)e.Item.FindControl("hdnGridIdTransportType");
                _hdn.Value = _type.IdTransportType.ToString();
                
                _lbl = ((Label)e.Item.FindControl("lblGridValue"));
                if (_data is Library.Objects.Sites.Meters.Series.TransportDataLocation)
                {
                    Library.Objects.Sites.Meters.Series.TransportDataLocation _cast = (Library.Objects.Sites.Meters.Series.TransportDataLocation)_data;
                    _lbl.Text = _cast.Address +  " " + (_data.Value/1000).ToString("N2") + "km ";
                    _hdn = (HiddenField)e.Item.FindControl("hdnGridLocation");
                    _hdn.Value = _cast.Location.Coordenates;
                    _hdn = (HiddenField)e.Item.FindControl("hdnGridDrivingDistance");
                    _hdn.Value = _data.Value.ToString();
                    _hdn = (HiddenField)e.Item.FindControl("hdnGridAddress");
                    _hdn.Value = _cast.Address;
                }
                else {
                    Library.Objects.Sites.Meters.Series.TransportDataDistance _cast = (Library.Objects.Sites.Meters.Series.TransportDataDistance)_data;
                    _lbl.Text = _data.Value.ToString() + " " + _data.Unit.Symbol;
                    _hdn = (HiddenField)e.Item.FindControl("hdnGridIdUnit");
                    _hdn.Value = _cast.Unit.IdUnit.ToString();
                    _hdn = (HiddenField)e.Item.FindControl("hdnGridDistance");
                    _hdn.Value = _cast.Value.ToString();

                }
                
            }
        }
        void lnkLoadAdd_Click(object sender, EventArgs e)
        {
            Library.Objects.Sites.Meters.Series.TransportData _transportData = null;

            if (rdDistance.Checked)
                _transportData = new Library.Objects.Sites.Meters.Series.TransportDataDistance(_Data.Count, Convert.ToDateTime(hdnLoadDate.Value), txtLoadPlateNumber.Text, chkIsRoundtrip.Checked, I.GetTransportType(Convert.ToInt64(ddlLoadTypes.SelectedValue)), Convert.ToDouble(txtLoadDistanceValue.Text), I.GetUnit(Convert.ToInt64(ddlLoadDistanceUnits.SelectedValue)));
            else
            {
                Double _drivingDistance = 0;
                Double.TryParse(hdnLoadLocationDistance.Value, out _drivingDistance);
                if(_drivingDistance>0)
                    _transportData = new Library.Objects.Sites.Meters.Series.TransportDataLocation(_Data.Count, Convert.ToDateTime(hdnLoadDate.Value), hdnLoadLocationAddress.Value, txtLoadPlateNumber.Text, chkIsRoundtrip.Checked, _SitePosition, new Library.Objects.Auxiliaries.Geographic.Position(hdnLoadLocationPoint.Value), I.GetTransportType(Convert.ToInt64(ddlLoadTypes.SelectedValue)), _drivingDistance, I);
                else
                    _transportData = new Library.Objects.Sites.Meters.Series.TransportDataLocation(_Data.Count, Convert.ToDateTime(hdnLoadDate.Value), hdnLoadLocationAddress.Value, txtLoadPlateNumber.Text, chkIsRoundtrip.Checked, _SitePosition, new Library.Objects.Auxiliaries.Geographic.Position(hdnLoadLocationPoint.Value), I.GetTransportType(Convert.ToInt64(ddlLoadTypes.SelectedValue)), I);
            }
            _Data.Add(_transportData);

            RefreshData();
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
                SaveData();
        }

        #endregion
    }
}