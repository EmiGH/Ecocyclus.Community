<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="MeterElectricityLoadAdd.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Meters.MeterElectricityLoadAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>



<asp:Content ID="ctnMain" ContentPlaceHolderID="cphContent" runat="server">
    <script type="text/javascript">

        //*********************************************************//
        //***************** Table Sort and Paging *****************//
        //*********************************************************//
        $(document).ready(function () {
            $('#tblGrid').dataTable({
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

        //Init values for fields with hidden values on postbacks
        $(document).ready(function () {
            if ($("#<%=hdnLoadFrom.ClientID%>").val() != "") {
                $('#<%=txtLoadFrom.ClientID%>').val($("#<%=hdnLoadFrom.ClientID%>").val());
            }
            if ($("#<%=hdnLoadTo.ClientID%>").val() != "") {
                $('#<%=txtLoadTo.ClientID%>').val($("#<%=hdnLoadTo.ClientID%>").val());
            }

        });

        function validateGrid(source, args) {

            if ($('#tblGrid tr').length > 1) {
                args.IsValid = true;
            }
            else {
                args.IsValid = false;
            }
        }

        function validateDates(source, args) {

            var _txtFrom = $get('<%=txtLoadFrom.ClientID%>').value;
            var _txtTo = $get('<%=txtLoadTo.ClientID%>').value;

            if (_txtFrom == '' || _txtTo == '') {
                args.IsValid = false;
            }
            else {
                $get('<%=hdnLoadFrom.ClientID%>').value = _txtFrom;
                $get('<%=hdnLoadTo.ClientID%>').value = _txtTo;
                args.IsValid = true;
            }
        }

    </script>
    <div class="divLineSeparatorHeader Meter">
    </div>
    <!--*******************************************************-->
    <!--**************** Meter Properties *********************-->
    <!--*******************************************************-->
    <div class="divDetail">
        <!--*******************************************************-->
        <!--**************** Meter Properties *********************-->
        <!--*******************************************************-->
        <div class="divTitle">
            <asp:Label ID="lblSiteProperties" runat="server"></asp:Label>
        </div>
        <div id="divProperties">
            <div class="divColumn column3">
                <asp:Label ID="lblIdentification" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblIdentificationValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <!-- Is Physical -->
                <asp:Label ID="lblIsPhysical" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblIsPhysicalValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3 last">
                <!-- Units -->
                <asp:Label ID="lblUnit" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblUnitValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <!-- EF Value -->
                <asp:Label ID="lblEF" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblEFValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <!-- Frequency Quantity -->
                <asp:Label ID="lblFrequencyQuantity" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblFrequencyQuantityValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3 last">
                <!-- Frequency Units -->
                <asp:Label ID="lblFrequencyUnits" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblFrequencyUnitsValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="clear">
            </div>
            <div class="divColumn column3">
                <!-- Alert Before -->
                <asp:Label ID="lblAlertBefore" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblAlertBeforeValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <!-- Alert After -->
                <asp:Label ID="lblAlertAfter" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblAlertAfterValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3 last">
                <!-- Alert On Start -->
                <asp:Label ID="lblAlertOnStart" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblAlertOnStartValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <!-- Initial Reading -->
            <div id="divInitialReading" runat="server" style="display: none" class="divColumn column3">
                <asp:Label ID="lblInitialReading" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblInitialReadingValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div id="divInitialDate" runat="server" style="display: none" class="divColumn column3">
                <asp:Label ID="lblInitialDate" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblInitialDateValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column1 margin">
                <asp:Label ID="lblDescription" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblDescriptionValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
        </div>
        <div class="clear">
        </div>
    </div>
    <!--*******************************************************-->
    <!--****************** Load Values ************************-->
    <!--*******************************************************-->
    <div id="divLoad" class="divForm AddSerie">
        <div class="divTitle">
            <asp:Label ID="lblLoadHeader" runat="server"></asp:Label>
        </div>
        <asp:Label Style="color: Red" ID="lblMandatoryFieldsExplanation" runat="server"></asp:Label>
        <br />
        <br />
        <div>
            <asp:Label ID="lblValidLoadRange" runat="server"></asp:Label>
        </div>
        <br />
        <br />
        <div class="divColumn column4">
            <asp:Label ID="lblLoadHeaderFrom" runat="server" CssClass="lblTitle"></asp:Label>
            <ajaxToolkit:CalendarExtender ID="calLoadFrom" runat="server" DefaultView="Days"
                TargetControlID="txtLoadFrom" PopupButtonID="btnLoadFrom">
            </ajaxToolkit:CalendarExtender>
            <asp:CustomValidator ID="cvLoadDates" runat="server" ValidationGroup="SetGrid" EnableClientScript="true"
                ClientValidationFunction="validateDates" Display="Dynamic" CssClass="rfvRequested"></asp:CustomValidator>
            <asp:HiddenField runat="server" ID="hdnLoadFrom" />
            <asp:TextBox ID="txtLoadFrom" runat="server" ReadOnly="true" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column4">
            <asp:Label ID="lblLoadHeaderTo" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:CompareValidator ID="cvDates" ControlToValidate="txtLoadTo" runat="server" Operator="GreaterThan"
                ControlToCompare="txtLoadFrom" Type="Date" ValidationGroup="SetGrid" Display="Dynamic"
                CssClass="rfvRequested"></asp:CompareValidator>
            <asp:HiddenField runat="server" ID="hdnLoadTo" />
            <asp:TextBox ID="txtLoadTo" runat="server" ReadOnly="true" CssClass="lblValue"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="calLoadTo" runat="server" DefaultView="Days" TargetControlID="txtLoadTo"
                PopupButtonID="btnLoadTo">
            </ajaxToolkit:CalendarExtender>
        </div>
        <div class="divColumn column4">
            <asp:Label ID="lblLoadHeaderInterval" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:CompareValidator ID="cvLoadFrequencyQuantity" runat="server" ControlToValidate="txtLoadFrequency"
                Type="Integer" ValidationGroup="SetGrid" Operator="GreaterThan" ValueToCompare="0"
                Display="Dynamic" CssClass="rfvRequested"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="rfvLoadFrequencyQuantity" runat="server" ControlToValidate="txtLoadFrequency"
                ValidationGroup="Add" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvLoadFrequencyUnit" runat="server" ControlToValidate="ddlLoadFrequencyUnit"
                Operator="NotEqual" ValueToCompare="0" ValidationGroup="SetGrid" Display="Dynamic"
                CssClass="rfvRequested"></asp:CompareValidator>
            <asp:TextBox ID="txtLoadFrequency" runat="server" CssClass="lblValue"></asp:TextBox>
            <asp:DropDownList ID="ddlLoadFrequencyUnit" runat="server">
            </asp:DropDownList>
        </div>
        <div class="divColumn column4 last">
            <asp:Label ID="lblLoadHeaderUnit" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:CompareValidator ID="cvLoadUnit" runat="server" ControlToValidate="ddlLoadUnits"
                Operator="NotEqual" ValueToCompare="0" ValidationGroup="SetGrid" Display="Dynamic"
                CssClass="rfvRequested"></asp:CompareValidator>
            <asp:DropDownList ID="ddlLoadUnits" runat="server">
            </asp:DropDownList>
        </div>
        <div class="clear">
        </div>
        <asp:LinkButton ID="lnkLoadSetGrid" runat="server" ValidationGroup="SetGrid" CssClass="btnMeterAdd" />

        <div class="divTitle">
            <asp:Label ID="lblLoadGrid" runat="server"></asp:Label>
        </div>
        <asp:UpdatePanel ID="upGrid" UpdateMode="Always" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkLoadSetGrid" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:Label ID="lblFirstDataWarning" runat="server" Style="display: none"></asp:Label>
                <br />    
                <table id="tblGrid" class="tbl">
                    <thead>
                        <tr>
                            <th>
                                <asp:Label ID="lblLoadGridHeaderFrom" runat="server"></asp:Label>
                            </th>
                            <th>
                                <asp:Label ID="lblLoadGridHeaderTo" runat="server"></asp:Label>
                            </th>
                            <th>
                                <asp:Label ID="lblLoadGridHeaderValue" runat="server"></asp:Label>
                            </th>
                            <th class="thlast">
                                <asp:Label ID="lblLoadGridHeaderUnit" runat="server"></asp:Label>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptLoadGrid" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLoadGridFrom" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblLoadGridTo" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLoadGridValue" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvLoadGridValue" ControlToValidate="txtLoadGridValue"
                                            ValidationGroup="Add" runat="server"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvLoadGridValue" runat="server" ControlToValidate="txtLoadGridValue"
                                            Type="Double" Operator="DataTypeCheck" ValidationGroup="Add"></asp:CompareValidator>
                                    </td>
                                    <td class="tdlast">
                                        <asp:Label ID="lblLoadGridUnit" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="trAlternating">
                                    <td>
                                        <asp:Label ID="lblLoadGridFrom" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblLoadGridTo" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLoadGridValue" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvLoadGridValue" ControlToValidate="txtLoadGridValue"
                                            ValidationGroup="Add" runat="server"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="cvLoadGridValue" runat="server" ControlToValidate="txtLoadGridValue"
                                            Type="Double" Operator="DataTypeCheck" ValidationGroup="Add"></asp:CompareValidator>
                                    </td>
                                    <td class="tdlast">
                                        <asp:Label ID="lblLoadGridUnit" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </tbody>
                    <tfoot>
                        <tr>
                            <td colspan="4" class="last">
                            </td>
                        </tr>
                    </tfoot>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:CustomValidator runat="server" ID="cvGrid" ValidationGroup="Add" ClientValidationFunction="validateGrid"></asp:CustomValidator>
    </div>

    <!--**************************************************************-->
    <!--********************* Save  **********************************-->
    <!--**************************************************************-->
    <div class="divContentSave">
        <asp:Button ID="btnSave" runat="server" ValidationGroup="Add" CssClass="btnSave btnActions" />
    </div>
</asp:Content>
