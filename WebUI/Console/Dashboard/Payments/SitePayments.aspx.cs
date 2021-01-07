using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using CSI.Library.Objects.Users;
using System.Globalization;
using Stripe;

namespace CSI.WebUI.Console.Dashboard.Payments
{
    public partial class SitePayments : BasePage
    {
        private Library.Objects.Sites.SiteMineOpen _Site;
        public String Location
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

        protected void Page_Init(object sender, EventArgs e)
        {
            _Site = (Library.Objects.Sites.SiteMineOpen)I.GetSite(Convert.ToInt64(Request.QueryString["Site"]));
            BindControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetMenu();
                LoadTexts();
                LoadDates();
                LoadSite();
                LoadPayments();
                LoadPaymentMethods();
                LoadCountries();
            }
        }

        #region Private Methods

        private void BindControls()
        {
            rdPaymentFrom.Attributes.Add("onclick", "js:setDates();");
            rdPaymentTo.Attributes.Add("onclick", "js:setDates();");

            ddlPaymentMonthFrom.Attributes.Add("onchange", "js:calculateAmount();");
            ddlPaymentMonthTo.Attributes.Add("onchange", "js:calculateAmount();");
            ddlPaymentYearFrom.Attributes.Add("onchange", "js:calculateAmount();");
            ddlPaymentYearTo.Attributes.Add("onchange", "js:calculateAmount();");

            ddlPaymentPopupType.Attributes.Add("onchange", "js:setMasks();");

            rptPayments.ItemDataBound += new RepeaterItemEventHandler(rptPayments_ItemDataBound);
            btnPaymentPopupOk.Click += new EventHandler(btnPaymentPopupOk_Click);

        }
        private void SetMenu()
        {
            ((Main)Page.Master).MenuNavigation.Initialize(_Site.IdSite, _Site.Title, Console.Controls.ucMenuNavigation.MenuItem.Payments, WebUI.Common.GetPermissionFromContext(I,_Site));
        }
        private void LoadTexts()
        {
            String _helpPostfix = Resources.Data.HelpPostfix;
            String _mandatoryField = Resources.Data.MandatoryField;

            lblSiteProperties.Text = Resources.Data.Properties;

            lblTitle.Text = Resources.Data.Title;
            lblType.Text = Resources.Data.SiteType;
            lblLoadStatus.Text = Resources.Data.SiteLoadStatus;
            lblLiveStatus.Text = Resources.Data.SiteLiveStatus;
            lblLocation.Text = Resources.Data.Location;
            lblStart.Text = Resources.Data.Start;
            lblWeeks.Text = Resources.Data.Weeks;
            lblNumber.Text = Resources.Data.Number;
            lblValue.Text = Resources.Data.Value;
            lblFloorSpace.Text = Resources.Data.FloorSpace;
            lblUnits.Text = Resources.Data.Units;

            lblPayments.Text = Resources.Data.Payments;
            lblPaymentsHeaderDate.Text = Resources.Data.Date;
            lblPaymentHeaderOperator.Text = Resources.Data.Operator;
            lblPaymentsHeaderFrom.Text = Resources.Data.From;
            lblPaymentsHeaderTo.Text = Resources.Data.To;
            lblPaymentsHeaderAmount.Text = Resources.Data.Amount;

            lblPayment.Text = Resources.Data.PaymentAdd;
            lblMandatoryFieldsExplanation.Text = Resources.Data.MandatoryFieldExplanation;
            lblPaymentHeaderAmount.Text = Resources.Data.Amount + _helpPostfix;
            lblPaymentHeaderFrom.Text = Resources.Data.From + _mandatoryField + _helpPostfix;
            lblPaymentHeaderTo.Text = Resources.Data.To + _mandatoryField + _helpPostfix;

            lblPaymentTotalTitle.Text = Resources.Data.Total;
            lblPaymentTotalMonthsTitle.Text = Resources.Data.Months;
            lblPaymentTotalMonthlyTitle.Text = Resources.Data.Subtotal;
            lblPaymentTotalMonthlyFeeTitle.Text = Resources.Data.MonthlyFee + ":";
            lblPaymentTotalSetupFeeTitle.Text = Resources.Data.SetupFee + ":";
            
            btnSave.Text = Resources.Data.Pay;

            lblPaymentPopupCardInfoTitle.Text = Resources.Data.CreditCardInfo;
            lblPaymentPopupPersonalInfo.Text = Resources.Data.CreditCardHolderInfo;
            lblPaymentPopupCity.Text = Resources.Data.City;
            lblPaymentPopupCountry.Text = Resources.Data.Country;
            lblPaymentPopupExpirationDate.Text = Resources.Data.ExpirationDate;
            lblPaymentPopupHolderName.Text = Resources.Data.Name;
            lblPaymentPopupNumber.Text = Resources.Data.CreditCardNumber;
            lblPaymentPopupSecurityCode.Text = Resources.Data.CreditCardCode;
            lblPaymentPopupStreet.Text = Resources.Data.BillingAddress;
            lblPaymentPopupType.Text = Resources.Data.PaymentTypes;
            lblPaymentPopupZip.Text = Resources.Data.ZipCode;

            rfvPaymentPopupCity.Text = rfvPaymentPopupHolderName.Text =
            rfvPaymentPopupNumber.Text = rfvPaymentPopupSecurityCode.Text = rfvPaymentPopupStreet.Text =
            rfvPaymentPopupZip.Text = Resources.Data.MandatoryField;

            btnPaymentPopupCancel.Text = Resources.Data.Cancel;
            btnPaymentPopupOk.Text = Resources.Data.Pay;
            
        }
        private void LoadDates()
        {
            ListItem _item;
            for (int i = 1; i < 13; i++)
            {
                _item = new ListItem(CurrentCultureInfo.DateTimeFormat.GetMonthName(i), i.ToString());
                ddlPaymentMonthFrom.Items.Add(_item);
                ddlPaymentMonthTo.Items.Add(_item);
                ddlPaymentPopupExpirationMonth.Items.Add(_item);
            }

            for (int i = 1970; i < DateTime.Now.Year + 6; i++)
            {
                _item = new ListItem(i.ToString(), i.ToString());
                ddlPaymentYearFrom.Items.Add(_item);
                ddlPaymentYearTo.Items.Add(_item);
            }

            for (int i = DateTime.Now.Year; i < DateTime.Now.Year + 15; i++)
            {
                _item = new ListItem(i.ToString(), i.ToString());
                ddlPaymentPopupExpirationYear.Items.Add(_item);
            }
        }
        private void LoadPaymentMethods()
        { 
            ListItem _item = new ListItem("Visa","visa");
            ddlPaymentPopupType.Items.Add(_item);
            _item = new ListItem("MasterCard", "mastercard");
            ddlPaymentPopupType.Items.Add(_item);
            _item = new ListItem("Amex", "amex");
            ddlPaymentPopupType.Items.Add(_item);
            
        }
        private void LoadSite()
        {
            Library.Objects.Auxiliaries.Geographic.Contact _contact = _Site.Contact;
            Location = _contact.Location.Position.Coordenates;

            lblTitleValue.Text = _Site.Title;
            lblLocationValue.Text = _contact.Location.Address;
            lblLoadStatusValue.Text = (_Site is Library.Objects.Sites.SiteMineOpen ? Resources.Data.SiteOpened : Resources.Data.SiteClosed);
            lblLiveStatusValue.Text = (_Site.IsFinished ? Resources.Data.SiteFinished : Resources.Data.SiteLive);
            lblStartValue.Text = _Site.Start.ToShortDateString();
            lblWeeksValue.Text = _Site.Weeks.ToString();
            lblNumberValue.Text = _Site.Number;
            lblValueValue.Text = _Site.Currency.Symbol + " " + _Site.Value.ToString() + " [" + _Site.Currency.Name + "]";
            lblFloorSpaceValue.Text = _Site.FloorSpace.ToString();
            lblUnitsValue.Text = _Site.Units.ToString();
            
            String _currentFrom = _Site.LoadTimeRange.Start.ToShortDateString(), _currentTo = _Site.LoadTimeRange.End.ToShortDateString();
            lblPaymentCurrentPeriod.Text = Resources.Data.PaymentCurrentPeriod.Replace("[currentTo]", _currentTo).Replace("[currentFrom]", _currentFrom);
            rdPaymentFrom.Text = Resources.Data.PaymentAddFrom.Replace("[currentFrom]", _currentTo);
            rdPaymentTo.Text = Resources.Data.PaymentAddTo.Replace("[currentTo]", _currentFrom);

            LoadHiddenVariables();

        }
        private void LoadHiddenVariables()
        {
            //Dates
            DateTime _from = _Site.LoadTimeRange.Start.AddDays(-1);
            DateTime _to = _Site.LoadTimeRange.End.AddDays(1);
            hdnValidLoadFromMonth.Value = _from.Month.ToString();
            hdnValidLoadFromYear.Value = _from.Year.ToString();
            hdnValidLoadToMonth.Value = _to.Month.ToString();
            hdnValidLoadToYear.Value = _to.Year.ToString();

            //Money
            Library.Objects.Sites.Payments.PaymentScale _scale = _Site.PaymentScale;
            Library.Objects.Auxiliaries.Units.Currency _currency = _scale.Currency;
            hdnPaymentIdCurrency.Value = _currency.IdCurrency.ToString();
            hdnPaymentCurrencySymbol.Value = _currency.Symbol;
            hdnLoadAmountPerMonth.Value = _scale.Amount.ToString();
            if (_Site.Payments().Count == 0)
            {
                hdnLoadSetupFee.Value = _scale.FirstPayment.ToString();
                lblPaymentTotalMonthlyFee.Text = _currency.Symbol + " " + _scale.FirstPayment.ToString();
            }
            else
            {
                lblPaymentTotalMonthlyFee.Text = Resources.Data.SetupFeePaid;
            }
            
        }
        private void LoadPayments()
        {
            rptPayments.DataSource = _Site.Payments().Values;
            rptPayments.DataBind();
        }
        private void LoadCountries()
        {
            foreach (Library.Objects.Auxiliaries.Geographic.Country _country in I.GetCountries().Values)
            {
                ListItem _item = new ListItem(_country.LanguageOption.Name, _country.PaymentSystemCode);
                ddlPaymentPopupCountry.Items.Add(_item);
            }
        }
        private void CleanForm()
        {
            txtPaymentPopupHolderName.Text = "";
            txtPaymentPopupNumber.Text = "";
            txtPaymentPopupSecurityCode.Text = "";

            txtPaymentPopupStreet.Text = "";
            txtPaymentPopupCity.Text = "";
            txtPaymentPopupZip.Text = "";
        }
        
        private void SaveData()
        {
            try
            {
                Library.Objects.Sites.Payments.Payment _payment = AddPayment();

                StripeCharge _charge = CreateCharge();

                Boolean _paid = _charge.Paid.HasValue ? _charge.Paid.Value : false;
                DateTime _date = _charge.Created;
                String _message = _charge.Description;

                ((Library.Objects.Users.UserOperatorMeManager)I).ModifyPayment(_Site, _payment, _charge.Id, _paid, _date, _message);
                
                LoadPayments();
                CleanForm();

                String _result = Resources.Messages.PaymentSuccessful;
                ((Main)Page.Master).ErrorHandler.SetMessage(Resources.Data.Success, _result);

            }
            catch (Exception exception)
            {
                CleanForm();

                String _error = Resources.Messages.StandardError;
                _error = _error.Replace("[error]", exception.Message);
                _error = _error.Replace("[mail]", Resources.Data.HelpDeskMailAddress);

                ((Main)Page.Master).ErrorHandler.SetMessage(Resources.Data.Error, _error);

            }
        }
        private Library.Objects.Sites.Payments.Payment AddPayment()
        {
            DateTime _from, _to;
            _from = new DateTime(Convert.ToInt32(hdnPaymentYearFrom.Value), Convert.ToInt32(hdnPaymentMonthFrom.Value), 1);
            _to = new DateTime(Convert.ToInt32(hdnPaymentYearTo.Value), Convert.ToInt32(hdnPaymentMonthTo.Value), 1);

            Double _amount = Convert.ToDouble(hdnPaymentAmount.Value, CultureInfo.GetCultureInfo("en-US").NumberFormat);
            Library.Objects.Auxiliaries.Units.Currency _currency = I.GetCurrency(Convert.ToInt64(hdnPaymentIdCurrency.Value));

            return ((Library.Objects.Users.UserOperatorMeManager)I).AddPayment(_Site, _from, _to, _amount, _currency, hdnPaymentTransactionId.Value, "");
            
        }
      
        #region Stripe
                
        private StripeToken CreateToken()
        {
            var myToken = new StripeTokenCreateOptions();

            // set these properties if using a card
            myToken.CardAddressCountry = ddlPaymentPopupCountry.SelectedValue.Trim();
            myToken.CardAddressLine1 = txtPaymentPopupStreet.Text.Trim();
            //myToken.CardAddressLine2 = "Unit B";
            myToken.CardAddressCity = txtPaymentPopupCity.Text.Trim();
            //myToken.CardAddressState = "NC";
            myToken.CardAddressZip = txtPaymentPopupZip.Text.Trim();
            myToken.CardCvc = txtPaymentPopupSecurityCode.Text.Trim();
            myToken.CardExpirationMonth = ddlPaymentPopupExpirationMonth.SelectedValue.Trim();
            myToken.CardExpirationYear = ddlPaymentPopupExpirationYear.SelectedValue.Trim();
            myToken.CardName = txtPaymentPopupHolderName.Text.Trim();
            myToken.CardNumber = txtPaymentPopupNumber.Text.Trim();

            var tokenService = new StripeTokenService();
            return tokenService.Create(myToken);
           
        }
        private StripeCharge CreateCharge()
        {
            var myCharge = new StripeChargeCreateOptions();

            // always set these properties
            myCharge.Amount = (Int32)(Decimal.Parse(hdnPaymentAmount.Value, CultureInfo.GetCultureInfo("en-US").NumberFormat)*100); //(Int32)Math.Round(Decimal.Parse(hdnPaymentAmount.Value), 2) * 100;
            Library.Objects.Auxiliaries.Units.Currency _currency = I.GetCurrency(Convert.ToInt64(hdnPaymentIdCurrency.Value));
            myCharge.Currency = _currency.PaymentSystemCode;

            // set this if you want to
            DateTime _from, _to;
            _from = new DateTime(Convert.ToInt32(hdnPaymentYearFrom.Value), Convert.ToInt32(hdnPaymentMonthFrom.Value), 1);
            _to = new DateTime(Convert.ToInt32(hdnPaymentYearTo.Value), Convert.ToInt32(hdnPaymentMonthTo.Value), 1);
            myCharge.Description = "Payment for company: " + I.Company.Name + " for site: " + _Site.Title + " for period: " +  _from.ToShortDateString() + " - " + _to.ToShortDateString();

            // set this property if using a token
            StripeToken _token = CreateToken();
            myCharge.TokenId = _token.Id;

            // set this if you have your own application fees (you must have your application configured first within Stripe)
            //myCharge.ApplicationFee = 25;

            // (not required) set this to false if you don't want to capture the charge yet - requires you call capture later
            myCharge.Capture = true;

            var chargeService = new StripeChargeService();
            return chargeService.Create(myCharge);
        }

        #endregion

        #endregion

        #region Page Events

        void rptPayments_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Library.Objects.Sites.Payments.Payment _payment = (Library.Objects.Sites.Payments.Payment)e.Item.DataItem;

                Label lbl = (Label)e.Item.FindControl("lblPaymentsDate");
                lbl.Text = _payment.Timestamp.ToShortDateString();

                lbl = (Label)e.Item.FindControl("lblPaymentsOperator");
                lbl.Text = _payment.Operator.Fullname;

                lbl = (Label)e.Item.FindControl("lblPaymentsFrom");
                lbl.Text = _payment.From.ToShortDateString();

                lbl = (Label)e.Item.FindControl("lblPaymentsTo");
                lbl.Text = _payment.To.ToShortDateString();

                lbl = (Label)e.Item.FindControl("lblPaymentsAmount");
                lbl.Text = _payment.Currency.Symbol + " " + _payment.Amount.ToString();
            }
        }
        void btnPaymentPopupOk_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                SaveData();
            }
            else
            {
                ((Main)Page.Master).ErrorHandler.SetMessage(Resources.Data.Information, Resources.Messages.SummaryCheck);
            }
        }
        
        #endregion
    }
}