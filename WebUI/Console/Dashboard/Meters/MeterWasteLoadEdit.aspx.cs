using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSI.Library.Objects.Users;

namespace CSI.WebUI.Console.Dashboard.Meters
{
    public partial class MeterWasteLoadEdit : BasePage
    {
        private Library.Objects.Sites.Meters.WasteMeter _Meter;
        private Library.Objects.Sites.Meters.Series.WasteSerie _Serie;
        
        protected void Page_Init(object sender, EventArgs e)
        {
            _Meter = I.GetWasteMeter(Convert.ToInt64(Request.QueryString["Meter"]));

            //Permissions
            Library.Security.Authority.PermissionTypes _permission = ((Library.Objects.Sites.SiteMine)_Meter.Site).CurrentPermission();
            if (_permission != Library.Security.Authority.PermissionTypes.SiteManager && _permission != Library.Security.Authority.PermissionTypes.SiteOperator)
            {
                throw new ApplicationException(Resources.Messages.AccessDenied);
            }

            _Serie = _Meter.GetSerie(Convert.ToInt64(Request.QueryString["Serie"]));

            BindControls();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
           if (!IsPostBack)
            {
                SetMenu();
                LoadTexts();
                LoadMeter();
                LoadUnits();
                LoadTypes(); 
                LoadData();
                SetEnabledDates();
            }
            
        }

        #region Methods

        private void BindControls()
        {
            btnSave.Click += new EventHandler(btnSave_Click);
        }
        private void SetMenu()
        {
            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)_Meter.Site;
            ((Main)Page.Master).MenuNavigation.Initialize(_site.IdSite, _site.Title, _Meter.IdMeter, _Meter.Identification, Console.Controls.ucMenuNavigation.MeterType.Waste, Console.Controls.ucMenuNavigation.MenuItem.None, WebUI.Common.GetPermissionFromContext(I,_site));
         
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

        }
        private void SetEnabledDates()
        {
            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)_Meter.Site;
            calLoadDate.StartDate = _site.LoadTimeRange.Start;
            calLoadDate.EndDate = _site.LoadTimeRange.End;
        }
        private void LoadUnits()
        {
            Int64 _idUnit = _Serie.Unit.IdUnit;

            ddlLoadUnits.Items.Clear();
            foreach (Library.Objects.Auxiliaries.Units.Unit _unit in I.GetWasteUnits().Values)
            {
                ListItem _item = new ListItem(_unit.Symbol + " [" + _unit.Name + "]", _unit.IdUnit.ToString());
                if (_unit.IdUnit == _idUnit)
                    _item.Selected = true;

                ddlLoadUnits.Items.Add(_item);
            }
        }
        private void LoadTypes()
        {
            Int64 _idWasteType = _Serie.WasteType.IdWasteType;

            ddlLoadTypes.Items.Clear();
            foreach (Library.Objects.Auxiliaries.Types.WasteType _type in I.GetWasteTypes().Values)
            {
                ListItem _item = new ListItem(_type.Name, _type.IdWasteType.ToString());
                if (_type.IdWasteType == _idWasteType)
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
            lblLoadDate.Text = Resources.Data.Date + _mandatoryField + _helpPostfix;
            lblLoadTypes.Text = Resources.Data.WasteType + _mandatoryField + _helpPostfix;
            lblLoadValue.Text = Resources.Data.Value + _mandatoryField + _helpPostfix;
            lblLoadUnits.Text = Resources.Data.Unit + _mandatoryField + _helpPostfix;

            calLoadDate.Format = CurrentCultureInfo.DateTimeFormat.ShortDatePattern;

            //Validators
            rfvLoadValue.Text = cvLoadDate.Text = cvLoadTypes.Text = cvLoadUnits.Text =
            cvLoadValue.Text = Resources.Messages.SummaryErrorCharacter;

            btnSave.Text = Resources.Data.Save;

        }
        private void LoadData()
        {
            txtLoadDate.Text = hdnLoadDate.Value = _Serie.Date.ToShortDateString();
            calLoadDate.SelectedDate = _Serie.Date;
            txtLoadValue.Text = _Serie.Value.ToString();
        }
        private void SaveData()
        {
            if (Page.IsValid)
            {
                try
                {
                    Library.Objects.Sites.Meters.Series.WasteData _data = new Library.Objects.Sites.Meters.Series.WasteData(0, Convert.ToDateTime(hdnLoadDate.Value), I.GetWasteType(Convert.ToInt64(ddlLoadTypes.SelectedValue)), Convert.ToDouble(txtLoadValue.Text), I.GetUnit(Convert.ToInt64(ddlLoadUnits.SelectedValue))); 
                    I.ModifyWasteData(_Meter, _Serie, _data);

                    Response.Redirect(WebUI.Common.GetPath(WebUI.Common.eFolders.Meters, Request) + "MeterWaste.aspx?Meter=" + _Meter.IdMeter.ToString(), false);
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