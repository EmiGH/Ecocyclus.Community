<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="SitePayments.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Payments.SitePayments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="cntHead" ContentPlaceHolderID="cphhead" runat="server">
    <script type="text/javascript" src="../../../Scripts/mapReferences.js"></script>
</asp:Content>
<asp:Content ID="cntContent" ContentPlaceHolderID="cphContent" runat="server">
    <script type="text/javascript">

        //*********************************************************//
        //***************** Map  Functions  ***********************//
        //*********************************************************//
        $(document).ready(loadMap());

        function loadMap() {
            var _script = document.createElement("script");
            _script.type = "text/javascript";
            _script.src = "http://maps.googleapis.com/maps/api/js?key=AIzaSyDzj5Mp1wUYuTzITA9F_Vw0Yjaid30o6dg&sensor=false&callback=mapsLoaded&libraries=places";
            document.body.appendChild(_script);

        }

    </script>
    <script type="text/javascript">

        //*********************************************************//
        //***************** Table Sort and Paging *****************//
        //*********************************************************//
        $(document).ready(function () {
            $('#tblPayments').dataTable({
                "bPaginate": true,
                "bLengthChange": true,
                "bFilter": true,
                "bSort": true,
                "bInfo": true,
                "bAutoWidth": true,
                "sPaginationType": "full_numbers",
                "iDisplayLength": 10
            });
        });

    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            
            //Init values for fields with hidden values on postbacks
            if ($('#<%=hdnPaymentMonthFrom.ClientID%>').val() != "") 
                $('#<%=ddlPaymentMonthFrom.ClientID%>').val($('#<%=hdnPaymentMonthFrom.ClientID%>').val());
            if ($('#<%=hdnPaymentMonthTo.ClientID%>').val() != "")
                $('#<%=ddlPaymentMonthTo.ClientID%>').val($('#<%=hdnPaymentMonthTo.ClientID%>').val());
            if ($('#<%=hdnPaymentYearFrom.ClientID%>').val() != "")
                $('#<%=ddlPaymentYearFrom.ClientID%>').val($('#<%=hdnPaymentYearFrom.ClientID%>').val());
            if ($('#<%=hdnPaymentYearTo.ClientID%>').val() != "")
                $('#<%=ddlPaymentYearTo.ClientID%>').val($('#<%=hdnPaymentYearTo.ClientID%>').val());

            //Dates
            setDates();

            //Mask
            setMasks();
        })

        function setMasks()
        {
            if ($('#<%=ddlPaymentPopupType.ClientID%>').val() == 'amex') {

                $('#<%=txtPaymentPopupNumber.ClientID%>').mask('9999 9999 9999 999');
                $('#<%=txtPaymentPopupSecurityCode.ClientID%>').mask('9999');
                
            }
            else {

                $('#<%=txtPaymentPopupNumber.ClientID%>').mask('9999 9999 9999 9999');
                $('#<%=txtPaymentPopupSecurityCode.ClientID%>').mask('999');
            }
        }

        function setDates() {

            var _month, _year;
            
            if ($('#<%=rdPaymentFrom.ClientID%>').is(':checked')) {

                _month = $('#<%=hdnValidLoadFromMonth.ClientID%>').val();
                _year = $('#<%=hdnValidLoadFromYear.ClientID%>').val();

                $("#<%=ddlPaymentMonthTo.ClientID%>").prop("disabled", true);
                $("#<%=ddlPaymentYearTo.ClientID%>").prop("disabled", true);
                $("#<%=ddlPaymentMonthFrom.ClientID%>").prop("disabled", false);
                $("#<%=ddlPaymentYearFrom.ClientID%>").prop("disabled", false);
                
            }
            else {
                
                _month = $('#<%=hdnValidLoadToMonth.ClientID%>').val();
                _year = $('#<%=hdnValidLoadToYear.ClientID%>').val();

                $("#<%=ddlPaymentMonthFrom.ClientID%>").prop("disabled", true);
                $("#<%=ddlPaymentYearFrom.ClientID%>").prop("disabled", true);
                $("#<%=ddlPaymentMonthTo.ClientID%>").prop("disabled", false);
                $("#<%=ddlPaymentYearTo.ClientID%>").prop("disabled", false);
                
            }

            $('#<%=ddlPaymentMonthTo.ClientID%>').val(_month);
            $('#<%=ddlPaymentYearTo.ClientID%>').val(_year);
            $('#<%=ddlPaymentMonthFrom.ClientID%>').val(_month);
            $('#<%=ddlPaymentYearFrom.ClientID%>').val(_year);
            
            calculateAmount();
        }

        function calculateAmount() {
            
            //Clear texts
            $('#<%=lblPaymentTotalMonths.ClientID%>').text("");
            $('#<%=lblPaymentTotalMonthlyFee.ClientID%>').text("");
            $('#<%=lblPaymentTotalMonthly.ClientID%>').text("");
            $('#<%=lblPaymentTotalSetupFee.ClientID%>').text("");
            $('#<%=lblPaymentTotal.ClientID%>').text("");

            //Clear Hidden
            $('#<%=hdnPaymentAmount.ClientID%>').val("");
            $('#<%=hdnPaymentMonthFrom.ClientID%>').val("");
            $('#<%=hdnPaymentMonthTo.ClientID%>').val("");
            $('#<%=hdnPaymentYearFrom.ClientID%>').val("");
            $('#<%=hdnPaymentYearTo.ClientID%>').val("");

            //Calculate 
            var _monthFrom = parseInt($('#<%=ddlPaymentMonthFrom.ClientID%>').val(), 10);
            var _monthTo = parseInt($('#<%=ddlPaymentMonthTo.ClientID%>').val(), 10);
            var _yearFrom = parseInt($('#<%=ddlPaymentYearFrom.ClientID%>').val(), 10);
            var _yearTo = parseInt($('#<%=ddlPaymentYearTo.ClientID%>').val(), 10);

            var _months = ((_monthTo + (_yearTo * 12)) - (_monthFrom + (_yearFrom * 12)));
            if (_months > 0) {

                var _amount = parseFloat(_months * $('#<%=hdnLoadAmountPerMonth.ClientID%>').val().replace(",",".")).toFixed(2) //Math.round(_months * $('#<%=hdnLoadAmountPerMonth.ClientID%>').val() * 100 / 100);
                
                //Set texts
                $('#<%=lblPaymentTotalMonths.ClientID%>').text(_months);
                $('#<%=lblPaymentTotalMonthlyFee.ClientID%>').text($('#<%=hdnPaymentCurrencySymbol.ClientID%>').val() + ' ' + $('#<%=hdnLoadAmountPerMonth.ClientID%>').val());
                $('#<%=lblPaymentTotalMonthly.ClientID%>').text($('#<%=hdnPaymentCurrencySymbol.ClientID%>').val() + ' ' + _amount);
                if ($('#<%=hdnLoadSetupFee.ClientID%>').val() != "") {
                    $('#<%=lblPaymentTotalSetupFee.ClientID%>').text($('#<%=hdnPaymentCurrencySymbol.ClientID%>').val() + ' ' + $('#<%=hdnLoadSetupFee.ClientID%>').val());
                    _amount = (+_amount + +parseFloat($('#<%=hdnLoadSetupFee.ClientID%>').val())).toFixed(2);
                }
                else {
                    $('#<%=lblPaymentTotalSetupFee.ClientID%>').text(' - ');
                }
                $('#<%=lblPaymentTotal.ClientID%>').text($('#<%=hdnPaymentCurrencySymbol.ClientID%>').val() + ' ' + _amount);

                //Return values
                $('#<%=hdnPaymentAmount.ClientID%>').val(_amount.toString());
                $('#<%=hdnPaymentMonthFrom.ClientID%>').val(_monthFrom.toString());
                $('#<%=hdnPaymentMonthTo.ClientID%>').val(_monthTo.toString());
                $('#<%=hdnPaymentYearFrom.ClientID%>').val(_yearFrom.toString());
                $('#<%=hdnPaymentYearTo.ClientID%>').val(_yearTo.toString());
            }
            
        }

        function validate() {

            var _total = parseFloat($('#<%=hdnPaymentAmount.ClientID%>').val());
            return _total > 0;
        }

        function popupPayment() {
            if (validate()) {
                $('#divPaymentPopup, #divPaymentPopupBg').toggle();
            }
            else {
                alert("<%=Resources.Messages.ErrorInvalidDateRange%>");
            }
        }

    </script>
    <style type="text/css">
        select
        {
            color: #8E8880;
            background-color: #E8E7E2;
            -webkit-border-radius: 3px;
            -moz-border-radius: 3px;
            border-radius: 3px;
            padding: 5px 5px;
            min-height: 17px;
            border: 0px !important;
            width:164px;
            height:30px;
            margin-top:6px;
        }
        #divPaymentPopup select
        {
            background-color: transparent;
            border: 1px solid #6E5C6D !important;
            margin-top:2px;
        }
    </style>

    <div class="divLineSeparatorHeader C02">
    </div>
    <div class="divDetail">
        <!--**************************************************-->
        <!--*************** Site Properties ******************-->
        <!--**************************************************-->
        <div class="divTitle">
            <asp:Label ID="lblSiteProperties" runat="server"></asp:Label>
        </div>
        <div id="divProperties" runat="server">
            <!-- ********************************-->
            <!-- ************* Map **************-->
            <!-- ********************************-->
            <div id="divMap" class="divColumn column3 map">
                <input type="hidden" id="hdnMapPoint" name="hdnMapPoint" value="<%=Location%>" />
                <div id="divMapCanvas" class="divMapCanvas divDetail">
                </div>
            </div>
            <div class="divColumn  column1p last">
                <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblTitleValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn  column1p last">
                <asp:Label ID="lblType" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblTypeValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <asp:Label ID="lblLiveStatus" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblLiveStatusValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <asp:Label ID="lblLoadStatus" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblLoadStatusValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3 last">
                <asp:Label ID="lblStart" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblStartValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <asp:Label ID="lblWeeks" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblWeeksValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <asp:Label ID="lblNumber" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblNumberValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3 last">
                <asp:Label ID="lblValue" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblValueValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <asp:Label ID="lblFloorSpace" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblFloorSpaceValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <asp:Label ID="lblUnits" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblUnitsValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column1 margin last">
                <asp:Label ID="lblLocation" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblLocationValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
        </div>
        <div class="clear">
        </div>
        <br />
        <br />

        <!--*********************************************** -->
        <!--*************** Payments ********************** -->
        <!--*********************************************** -->
        <div id="divPaymentsData">
            <div class="divTitle">
                <asp:Label ID="lblPayments" runat="server"></asp:Label>
            </div>
            <table id="tblPayments" class="tbl">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblPaymentsHeaderDate" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblPaymentsHeaderFrom" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblPaymentsHeaderTo" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblPaymentsHeaderAmount" runat="server"></asp:Label>
                        </th>
                        <th class="thlast">
                            <asp:Label ID="lblPaymentHeaderOperator" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptPayments" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPaymentsDate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPaymentsFrom" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPaymentsTo" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPaymentsAmount" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblPaymentsOperator" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="trAlternating">
                                <td>
                                    <asp:Label ID="lblPaymentsDate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPaymentsFrom" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPaymentsTo" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPaymentsAmount" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblPaymentsOperator" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
        <div class="clear">
        </div>
    </div>
    <!--***************************************************-->
    <!--****************** Payment ************************-->
    <!--***************************************************-->
    <div id="divLoad" class="divForm">
        <div class="divTitle">
            <asp:Label ID="lblPayment" runat="server"></asp:Label>
            <img src="../../../Images/stripe_logo_small.png" style="vertical-align:top" />
        </div>
        <asp:Label Style="color: Red" ID="lblMandatoryFieldsExplanation" runat="server"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lblPaymentCurrentPeriod" runat="server"></asp:Label>

        <asp:HiddenField ID="hdnValidLoadFromYear" runat="server" />
        <asp:HiddenField ID="hdnValidLoadFromMonth" runat="server" />
        <asp:HiddenField ID="hdnValidLoadToYear" runat="server" />
        <asp:HiddenField ID="hdnValidLoadToMonth" runat="server" />

        <asp:HiddenField ID="hdnLoadSetupFee" runat="server" />
        <asp:HiddenField ID="hdnLoadAmountPerMonth" runat="server" />
        <asp:HiddenField ID="hdnPaymentCurrencySymbol" runat="server" />
        <asp:HiddenField ID="hdnPaymentIdCurrency" runat="server" />
        <asp:HiddenField ID="hdnPaymentTransactionId" runat="server" />
        <asp:HiddenField ID="hdnPaymentAmount" runat="server" />

        <br />
        <br />
        <asp:RadioButton ID="rdPaymentFrom" runat="server" GroupName="Payment" Checked="true" /><br />
        <asp:RadioButton ID="rdPaymentTo" runat="server" GroupName="Payment" />
        <br />
        <br />

        <!-- From -->
        <div class="divColumn column4">
            <asp:Label ID="lblPaymentHeaderFrom" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:DropDownList ID="ddlPaymentMonthFrom" runat="server">
            </asp:DropDownList>
        </div>
        <div class="divColumn column4">
            <span class="lblTitle">&nbsp;</span>
            <asp:DropDownList ID="ddlPaymentYearFrom" runat="server">
            </asp:DropDownList>
        </div>
        <asp:HiddenField ID="hdnPaymentMonthFrom" runat="server" />
        <asp:HiddenField ID="hdnPaymentYearFrom" runat="server" />

        <!-- To -->
        <div class="divColumn column4">
            <asp:Label ID="lblPaymentHeaderTo" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:DropDownList ID="ddlPaymentMonthTo" runat="server">
            </asp:DropDownList>
        </div>
        <div class="divColumn column4 last">
            <span class="lblTitle">&nbsp;</span>
            <asp:DropDownList ID="ddlPaymentYearTo" runat="server">
            </asp:DropDownList>
        </div>
        <asp:HiddenField ID="hdnPaymentMonthTo" runat="server" />
        <asp:HiddenField ID="hdnPaymentYearTo" runat="server" />


        <div class="clear">
        </div>
    </div>
    <div class="divDetail">
        <!-- Amount -->
        <div class="divTitle">
            <asp:Label ID="lblPaymentHeaderAmount" runat="server" CssClass="lblTitle"></asp:Label>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblPaymentTotalMonthsTitle" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:Label ID="lblPaymentTotalMonths" runat="server" CssClass="lblValue"></asp:Label>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblPaymentTotalMonthlyFeeTitle" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:Label ID="lblPaymentTotalMonthlyFee" runat="server" CssClass="lblValue"></asp:Label>
        </div>
        <div class="divColumn column3 last">
            <asp:Label ID="lblPaymentTotalMonthlyTitle" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:Label ID="lblPaymentTotalMonthly" runat="server" CssClass="lblValue"></asp:Label>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblPaymentTotalSetupFeeTitle" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:Label ID="lblPaymentTotalSetupFee" runat="server" CssClass="lblValue"></asp:Label>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblPaymentTotalTitle" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:Label ID="lblPaymentTotal" runat="server" CssClass="lblValue"></asp:Label>
        </div>

        <div class="clear">
        </div>
    </div>
    <div class="divContentSave">
        <asp:Button ID="btnSave" runat="server" CssClass="btnSave btnActions" OnClientClick="popupPayment();return false;" />
    </div>
    
    <br />
    <br />

    <!--**************************************************************-->
    <!--********************* Payment Popup **************************-->
    <!--**************************************************************-->
    <div id="divPaymentPopupBg" style="display:none ;background-color:#fff; opacity:0.5; z-index:97; position:fixed; width:100%; height:100%; left: 0px; top: 0px;"></div>
    <div style="display: block; position: fixed; left: 0px; top: 0px; width: 100%; top: 50%; height: 0px; z-index:98;">
                
        <div id="divPaymentPopup" style="display: none; margin: 0 auto; padding: 20px; width: 732px; height:460px; z-index: 100; background-color: White; margin-top:-210px; border:1px solid #666;">
            <img src="../../../Images/stripe_logo.png" /><br />
            <div class="divForm AddMeter">
                <div class="divTitle" style="margin-top:10px;">
                    <asp:Label ID="lblPaymentPopupCardInfoTitle" runat="server"></asp:Label>
                </div>

                <div class="divColumn column4">
                    <asp:Label ID="lblPaymentPopupType" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:DropDownList ID="ddlPaymentPopupType" runat="server"></asp:DropDownList>

                </div>

                <div class="divColumn column4">
                    <asp:Label ID="lblPaymentPopupNumber" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:TextBox ID="txtPaymentPopupNumber" runat="server"  CssClass="lblValue"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPaymentPopupNumber" runat="server" ControlToValidate="txtPaymentPopupNumber" ValidationGroup="PaymentPopup" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>
                </div>
                <div class="divColumn column4 last">
                    <asp:Label ID="lblPaymentPopupSecurityCode" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:TextBox ID="txtPaymentPopupSecurityCode" runat="server" MaxLength="3" CssClass="lblValue"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPaymentPopupSecurityCode" ControlToValidate="txtPaymentPopupSecurityCode" runat="server" ValidationGroup="PaymentPopup" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>
                </div>
                <div class="clear"></div>
                <div class="divColumn column4">
                    <asp:Label ID="lblPaymentPopupExpirationDate" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:DropDownList ID="ddlPaymentPopupExpirationMonth" runat="server"></asp:DropDownList>

                </div>
                <div class="divColumn column4 last">
                    <span class="lblTitle">&nbsp;</span>
                    <asp:DropDownList ID="ddlPaymentPopupExpirationYear" runat="server"></asp:DropDownList>

                </div>
                
                <div class="divTitle" style="margin-top:10px;">
                    <asp:Label ID="lblPaymentPopupPersonalInfo" runat="server"></asp:Label>
                </div>

                <div class="divColumn column4">
                    <asp:Label ID="lblPaymentPopupHolderName" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:TextBox ID="txtPaymentPopupHolderName" runat="server" CssClass="lblValue"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPaymentPopupHolderName" ControlToValidate="txtPaymentPopupHolderName" runat="server" ValidationGroup="PaymentPopup" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>

                </div>
                <div class="divColumn column4">

                    <asp:Label ID="lblPaymentPopupStreet" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:TextBox ID="txtPaymentPopupStreet" runat="server" CssClass="lblValue"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPaymentPopupStreet" ControlToValidate="txtPaymentPopupStreet" runat="server" ValidationGroup="PaymentPopup" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>

                </div>
                <div class="divColumn column4 last">
                    <asp:Label ID="lblPaymentPopupZip" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:TextBox ID="txtPaymentPopupZip" runat="server" CssClass="lblValue"></asp:TextBox><br />
                    <asp:RequiredFieldValidator ID="rfvPaymentPopupZip" ControlToValidate="txtPaymentPopupZip" runat="server" ValidationGroup="PaymentPopup" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>

                </div>
                <div class="divColumn column4 margin">
                    <asp:Label ID="lblPaymentPopupCity" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:TextBox ID="txtPaymentPopupCity" runat="server" CssClass="lblValue"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPaymentPopupCity" ControlToValidate="txtPaymentPopupCity" runat="server" ValidationGroup="PaymentPopup" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>

                </div>
                <div class="divColumn column4 margin">
                    <asp:Label ID="lblPaymentPopupCountry" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:DropDownList ID="ddlPaymentPopupCountry" runat="server"></asp:DropDownList>
                </div>

                <div class="clear"></div>
            
            </div>

            <div class="divContentSave">
                <asp:Button ID="btnPaymentPopupCancel" runat="server" OnClientClick="popupPayment();return false;" CssClass="btnSave btnActions" />
                <asp:Button ID="btnPaymentPopupOk" runat="server" ValidationGroup="PaymentPopup" CssClass="btnSave btnActions" />
            </div>
                
             <div class="clear"></div>
        </div>
    </div>

</asp:Content>
