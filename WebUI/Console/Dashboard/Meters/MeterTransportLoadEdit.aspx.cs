using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard.Meters
{
    public partial class MeterTransportLoadEdit : BasePage
    {
        private Library.Objects.Sites.Meters.TransportMeter _Meter;
        private Library.Objects.Sites.Meters.Series.TransportSerie _Serie;
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
        public String Previous
        {
            set
            {
                ViewState["Previous"] = value.Replace(',', '.');
            }
            get
            {
                return ViewState["Previous"].ToString();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            _Meter = I.GetTransportMeter(Convert.ToInt64(Request.QueryString["Meter"]));

            //Permissions
            Library.Security.Authority.PermissionTypes _permission = ((Library.Objects.Sites.SiteMine)_Meter.Site).CurrentPermission();
            if (_permission != Library.Security.Authority.PermissionTypes.SiteManager && _permission != Library.Security.Authority.PermissionTypes.SiteOperator)
            {
                throw new ApplicationException(Resources.Messages.AccessDenied);
            }
                        
            _SitePosition = _Meter.Site.Contact.Location.Position;
            _Serie = _Meter.GetSerie(Convert.ToInt64(Request.QueryString["Serie"]));
            BindControls();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadMeter();
            if (!IsPostBack)
            {
                SetMenu();
                LoadTexts();
                LoadMeter();
                LoadUnits();
                LoadTypes(); 
                LoadTransportSerie();
                SetEnabledDates();
            }
        }

        #region Methods

        private void BindControls()
        {
            rdDistance.Attributes.Add("onClick", "js:switchLoadType()");
            rdLocation.Attributes.Add("onClick", "js:switchLoadType()");

            btnSave.Click += new EventHandler(btnSave_Click);
        }
        private void SetMenu()
        {
            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)_Meter.Site;
            ((Main)Page.Master).MenuNavigation.Initialize(_site.IdSite, _site.Title, _Meter.IdMeter, _Meter.Identification, Console.Controls.ucMenuNavigation.MeterType.Transport, Console.Controls.ucMenuNavigation.MenuItem.None, WebUI.Common.GetPermissionFromContext(I,_site));
         
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
        private void LoadTransportSerie()
        {
            hdnLoadDate.Value = _Serie.Date.ToShortDateString();
            txtLoadDate.Text = _Serie.Date.ToShortDateString();
            txtLoadPlateNumber.Text = _Serie.PlateNumber;
            chkIsRoundtrip.Checked = _Serie.IsRoundtrip;
            
            Double _value = _Serie.Value;
            if (_Serie.IsRoundtrip)
                _value /= 2;
            
            if (_Serie is Library.Objects.Sites.Meters.Series.TransportSerieDistance)
            {
                rdDistance.Checked = true;
                Library.Objects.Sites.Meters.Series.TransportSerieDistance _cast = (Library.Objects.Sites.Meters.Series.TransportSerieDistance)_Serie;
                txtLoadDistanceValue.Text = _value.ToString();
                ddlLoadDistanceUnits.SelectedValue = _cast.Unit.IdUnit.ToString();
                Previous = "";
            }
            else {
                rdLocation.Checked = true;
                divLoadDistance.Style.Add("display", "none");
                Library.Objects.Sites.Meters.Series.TransportSerieLocation _cast = (Library.Objects.Sites.Meters.Series.TransportSerieLocation)_Serie;
                Previous = _cast.Address;
            }
        }
        private void LoadUnits()
        {
            ddlLoadDistanceUnits.Items.Clear();
            foreach (Library.Objects.Auxiliaries.Units.Unit _unit in I.GetTransportUnits().Values)
            {
                ListItem _item = new ListItem(_unit.Symbol + " [" + _unit.Name + "]", _unit.IdUnit.ToString());
                ddlLoadDistanceUnits.Items.Add(_item);
            }
        }
        private void LoadTypes()
        {
            Int64 _idTransportType = _Serie.TransportType.IdTransportType;
            ddlLoadTypes.Items.Clear();
            foreach (Library.Objects.Auxiliaries.Types.TransportType _type in I.GetTransportTypes(_Meter.Site.Country.IdCountry).Values)
            {
                ListItem _item = new ListItem(_type.Name, _type.IdTransportType.ToString());
                if (_idTransportType == _type.IdTransportType)
                    _item.Selected = true;
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

            lblLoad.Text = Resources.Data.Load;
            rdDistance.Text = Resources.Data.Distance;
            rdLocation.Text = Resources.Data.Location;

            lblLoadInputType.Text = Resources.Data.TransportInputType;
            lblLoadDistanceHeaderValue.Text = Resources.Data.Distance + _helpPostfix;
            lblLoadDistanceHeaderUnit.Text = Resources.Data.Unit + _helpPostfix;

            lblIsRoundtrip.Text = Resources.Data.Roundtrip + _helpPostfix;
            lblLoad.Text = Resources.Data.Load;
            lblLoadHeaderDate.Text = Resources.Data.Date + _mandatoryField + _helpPostfix;
            lblLoadHeaderPlateNumber.Text = Resources.Data.PlateNumber + _helpPostfix;
            lblLoadHeaderTypes.Text = Resources.Data.TransportType + _mandatoryField + _helpPostfix;

            calLoadDate.Format = CurrentCultureInfo.DateTimeFormat.ShortDatePattern;

            //Validators
            cvLoadTypes.Text = cvLoadDate.Text = Resources.Messages.SummaryErrorCharacter;

            btnSave.Text = Resources.Data.Save;

        }
        private Library.Objects.Sites.Meters.Series.TransportData BuildData()
        {
            Int16 _index = 0;
            DateTime _date = Convert.ToDateTime(hdnLoadDate.Value);
            Boolean _isRoundtrip = chkIsRoundtrip.Checked;
            String _plateNumber = txtLoadPlateNumber.Text;
            Library.Objects.Auxiliaries.Types.TransportType _type = I.GetTransportType(Convert.ToInt64(ddlLoadTypes.SelectedValue));

            String _address = hdnLoadLocationAddress.Value;
            if (_address != "")
            {
                String _location = hdnLoadLocationPoint.Value;
                Double _drivingDistance = 0;
                Double.TryParse(hdnLoadLocationDistance.Value, out _drivingDistance);
                if (_drivingDistance > 0)
                    return new Library.Objects.Sites.Meters.Series.TransportDataLocation(_index, _date, _address, _plateNumber, _isRoundtrip, _SitePosition, new Library.Objects.Auxiliaries.Geographic.Position(_location), _type, _drivingDistance, I);
                else
                    return new Library.Objects.Sites.Meters.Series.TransportDataLocation(_index, _date, _address, _plateNumber, _isRoundtrip, _SitePosition, new Library.Objects.Auxiliaries.Geographic.Position(_location), _type, I);
            }
            else
            {
                Int64 _idUnit = Convert.ToInt64(ddlLoadDistanceUnits.SelectedValue);
                Double _value = Convert.ToDouble(txtLoadDistanceValue.Text);
                return new Library.Objects.Sites.Meters.Series.TransportDataDistance(_index, _date, _plateNumber, _isRoundtrip, _type, _value, I.GetUnit(_idUnit));
            }

            
        }
        private void SaveData()
        {
            if (Page.IsValid)
            {
                try
                {
                    I.ModifyTransportData(_Meter, _Serie, BuildData());

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

        void btnSave_Click(object sender, EventArgs e)
        {
            if (IsValid)
                SaveData();
        }

        #endregion
    }
}