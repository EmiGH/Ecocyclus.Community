<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="MeterFuelLoadAdd.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Meters.MeterFuelLoadAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="cntMain" ContentPlaceHolderID="cphContent" runat="server">
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
            if ($("#<%=hdnLoadDate.ClientID%>").val() != "") {
                $('#<%=txtLoadDate.ClientID%>').val($("#<%=hdnLoadDate.ClientID%>").val());
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

        function validateDate(source, args) {

            var _txt = $get('<%=txtLoadDate.ClientID%>').value;
            if (_txt == '') {
                args.IsValid = false;
            }
            else {
                $get('<%=hdnLoadDate.ClientID%>').value = _txt;
                args.IsValid = true;
            }
        }

    </script>
    <div class="divLineSeparatorHeader Meter">
    </div>
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
            <div class="divColumn column1 margin">
                <asp:Label ID="lblDescription" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblDescriptionValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
    <!--*******************************************************-->
    <!--****************** Load Values ************************-->
    <!--*******************************************************-->
    <div id="divLoad" class="divForm AddSerie">
        <div class="divTitle">
            <asp:Label ID="lblLoad" runat="server"></asp:Label>
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
            <asp:Label ID="lblLoadDate" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:CustomValidator ID="cvLoadDate" runat="server" ValidationGroup="SetGrid" EnableClientScript="true"
                ClientValidationFunction="validateDate" Display="Dynamic" CssClass="rfvRequested"></asp:CustomValidator>
            <asp:HiddenField runat="server" ID="hdnLoadDate" />
            <asp:TextBox ID="txtLoadDate" runat="server" ReadOnly="true" CssClass="lblValue"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="calLoadDate" runat="server" DefaultView="Days"
                TargetControlID="txtLoadDate" PopupButtonID="btnLoadDate">
            </ajaxToolkit:CalendarExtender>
        </div>
        <div class="divColumn column4">
            <asp:Label ID="lblLoadTypes" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:CompareValidator ID="cvLoadTypes" runat="server" ControlToValidate="ddlLoadTypes"
                Operator="NotEqual" ValueToCompare="0" ValidationGroup="SetGrid" Display="Dynamic"
                CssClass="rfvRequested"></asp:CompareValidator>
            <asp:DropDownList ID="ddlLoadTypes" runat="server">
            </asp:DropDownList>
        </div>
        <div class="divColumn column4">
            <asp:Label ID="lblLoadValue" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvLoadValue" ControlToValidate="txtLoadValue" ValidationGroup="SetGrid"
                runat="server" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvLoadValue" runat="server" ControlToValidate="txtLoadValue"
                Type="Double" Operator="DataTypeCheck" ValidationGroup="SetGrid" Display="Dynamic"
                CssClass="rfvRequested"></asp:CompareValidator>
            <asp:TextBox ID="txtLoadValue" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column4 last">
            <asp:Label ID="lblLoadUnits" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:CompareValidator ID="cvLoadUnits" runat="server" ControlToValidate="ddlLoadUnits"
                Operator="NotEqual" ValueToCompare="0" ValidationGroup="SetGrid" Display="Dynamic"
                CssClass="rfvRequested"></asp:CompareValidator>
            <asp:DropDownList ID="ddlLoadUnits" runat="server">
            </asp:DropDownList>
        </div>
        <div class="clear">
        </div>
        <asp:LinkButton ID="lnkLoadSetGrid" runat="server" ValidationGroup="SetGrid" CssClass="btnMeterAdd" />
    </div>
    <!--*******************************************************-->
    <!--****************** Loaded Values ************************-->
    <!--*******************************************************-->
    <div class="divTitle">
        <asp:Label ID="lblGrid" runat="server"></asp:Label>
    </div>
    <div id="divGrid">
        <asp:UpdatePanel ID="upGrid" UpdateMode="Always" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkLoadSetGrid" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="rptGrid" EventName="ItemDataBound" />
            </Triggers>
            <ContentTemplate>
                <table id="tblGrid" class="tbl">
                    <thead>
                        <tr>
                            <th>
                            </th>
                            <th>
                                <asp:Label ID="lblGridHeaderDate" runat="server"></asp:Label>
                            </th>
                            <th>
                                <asp:Label ID="lblGridHeaderType" runat="server"></asp:Label>
                            </th>
                            <th>
                                <asp:Label ID="lblGridHeaderValue" runat="server"></asp:Label>
                            </th>
                            <th class="thlast">
                                <asp:Label ID="lblGridHeaderUnit" runat="server"></asp:Label>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptGrid" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnGridDelete" CommandName="Delete" runat="server" CssClass="btnSitesDelete" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGridDate" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hdnGridType" runat="server" />
                                        <asp:Label ID="lblGridType" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGridValue" runat="server"></asp:Label>
                                    </td>
                                    <td class="tdlast">
                                        <asp:HiddenField ID="hdnGridUnit" runat="server" />
                                        <asp:Label ID="lblGridUnit" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr class="trAlternating">
                                    <td>
                                        <asp:Button ID="btnGridDelete" CommandName="Delete" runat="server" CssClass="btnSitesDelete" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGridDate" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:HiddenField ID="hdnGridType" runat="server" />
                                        <asp:Label ID="lblGridType" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblGridValue" runat="server"></asp:Label>
                                    </td>
                                    <td class="tdlast">
                                        <asp:HiddenField ID="hdnGridUnit" runat="server" />
                                        <asp:Label ID="lblGridUnit" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                    </tbody>
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
