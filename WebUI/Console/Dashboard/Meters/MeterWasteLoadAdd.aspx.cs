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
    public partial class MeterWasteLoadAdd : BasePage
    {
        private Library.Objects.Sites.Meters.WasteMeter _Meter;
        private List<Library.Objects.Sites.Meters.Series.WasteData> _Data;

        protected void Page_Init(object sender, EventArgs e)
        {
            _Meter = I.GetWasteMeter(Convert.ToInt64(Request.QueryString["Meter"]));
            
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
                LoadTexts();
                LoadMeter();
                LoadUnits();
                LoadTypes();
                SetEnabledDates();
            }
            LoadData();
        }
        
        #region Methods

        private void BindControls()
        {
            btnSave.Click += new EventHandler(btnSave_Click);
            ibLoadAdd.Click += new EventHandler(ibLoadAdd_Click);
            rptGrid.ItemDataBound += new RepeaterItemEventHandler(rptGrid_ItemDataBound);
            rptGrid.ItemCommand += new RepeaterCommandEventHandler(rptGrid_ItemCommand);
        }


        private void SetMenu()
        {
            Library.Objects.Sites.SiteMine _site = (Library.Objects.Sites.SiteMine)_Meter.Site;
            ((Main)Page.Master).MenuNavigation.Initialize(_site.IdSite, _site.Title, _Meter.IdMeter, _Meter.Identification, Console.Controls.ucMenuNavigation.MeterType.Waste, Console.Controls.ucMenuNavigation.MenuItem.MeterLoad, WebUI.Common.GetPermissionFromContext(I,_site));
         
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
            Int64 _meterUnit = _Meter.DefaultUnit.IdUnit;

            ddlLoadUnits.Items.Clear();
            foreach (Library.Objects.Auxiliaries.Units.Unit _unit in I.GetWasteUnits().Values)
            {
                ListItem _item = new ListItem(_unit.Symbol + " [" + _unit.Name + "]", _unit.IdUnit.ToString());
                if (_unit.IdUnit == _meterUnit) _item.Selected = true;

                ddlLoadUnits.Items.Add(_item);
            }
        }
        private void LoadTypes()
        {
            ddlLoadTypes.Items.Clear();
            foreach (Library.Objects.Auxiliaries.Types.WasteType _type in I.GetWasteTypes().Values)
            {
                ListItem _item = new ListItem(_type.Name, _type.IdWasteType.ToString());
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

            lblGrid.Text = Resources.Data.LoadGrid;
            lblGridHeaderDate.Text = Resources.Data.Date;
            lblGridHeaderType.Text = Resources.Data.WasteType;
            lblGridHeaderUnit.Text = Resources.Data.Unit;
            lblGridHeaderValue.Text = Resources.Data.Volume;

            //Validators
            rfvLoadValue.Text = cvLoadDate.Text = cvLoadTypes.Text = cvLoadUnits.Text =
            cvLoadValue.Text = Resources.Messages.SummaryErrorCharacter;

            ibLoadAdd.Text = Resources.Data.Add;
            btnSave.Text = Resources.Data.Save;

        }
        private void LoadData()
        {
            _Data = new List<Library.Objects.Sites.Meters.Series.WasteData>();

            foreach (RepeaterItem _item in rptGrid.Items)
            {
                if (_item.ItemType == ListItemType.AlternatingItem || _item.ItemType == ListItemType.Item)
                {
                    Int32 _index = Convert.ToInt32(((Button)_item.FindControl("btnGridDelete")).CommandArgument);
                    DateTime _date = Convert.ToDateTime(((Label)_item.FindControl("lblGridDate")).Text);
                    Double _value = Convert.ToDouble(((Label)_item.FindControl("lblGridValue")).Text);
                    Library.Objects.Auxiliaries.Types.WasteType _type = I.GetWasteType(Convert.ToInt64(((HiddenField)_item.FindControl("hdnGridType")).Value));
                    Library.Objects.Auxiliaries.Units.Unit _unit = I.GetUnit(Convert.ToInt64(((HiddenField)_item.FindControl("hdnGridUnit")).Value));

                    Library.Objects.Sites.Meters.Series.WasteData _wasteData = new Library.Objects.Sites.Meters.Series.WasteData(_index, _date, _type, _value, _unit);
                    _Data.Add(_wasteData);
                }
            }
        }
        private void RemoveData(Int32 index)
        {
            for (int i = 0; i < _Data.Count; i++)
                if (_Data[i].Index==index)
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
                    I.AddWasteData(_Meter, _Data);

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
                Library.Objects.Sites.Meters.Series.WasteData _data = (Library.Objects.Sites.Meters.Series.WasteData)e.Item.DataItem;

                Button _btn = ((Button)e.Item.FindControl("btnGridDelete"));
                _btn.CommandArgument = _data.Index.ToString();
                _btn.Attributes.Add("onclick", "alertOnLoadDelete();");
                AddConfirmRequest((WebControl)_btn, Resources.Messages.ConfirmDeleteTitle, Resources.Messages.ConfirmDeleteMessage);
                
                Label _lbl = ((Label)e.Item.FindControl("lblGridDate"));
                _lbl.Text = _data.Date.ToShortDateString();

                _lbl = ((Label)e.Item.FindControl("lblGridValue"));
                _lbl.Text = _data.Value.ToString();

                Library.Objects.Auxiliaries.Types.WasteType _type = _data.WasteType;
                _lbl = ((Label)e.Item.FindControl("lblGridType"));
                _lbl.Text = _type.Name;
                HiddenField _hdn = ((HiddenField)e.Item.FindControl("hdnGridType"));
                _hdn.Value = _type.IdWasteType.ToString();

                Library.Objects.Auxiliaries.Units.Unit _unit = _data.Unit;
                _lbl = ((Label)e.Item.FindControl("lblGridUnit"));
                _lbl.Text = _unit.Name;
                _hdn = ((HiddenField)e.Item.FindControl("hdnGridUnit"));
                _hdn.Value = _unit.IdUnit.ToString();
                
            }
        }

        void ibLoadAdd_Click(object sender, EventArgs e)
        {
            Library.Objects.Sites.Meters.Series.WasteData _wasteData = new Library.Objects.Sites.Meters.Series.WasteData(_Data.Count, Convert.ToDateTime(hdnLoadDate.Value), I.GetWasteType(Convert.ToInt64(ddlLoadTypes.SelectedValue)), Convert.ToDouble(txtLoadValue.Text), I.GetUnit(Convert.ToInt64(ddlLoadUnits.SelectedValue)));
            _Data.Add(_wasteData);

            RefreshData();
        }
        void btnSave_Click(object sender, EventArgs e)
        {
            if(IsValid)
                SaveData();
        }
        
        #endregion
    }
}