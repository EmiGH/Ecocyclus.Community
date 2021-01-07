<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="MeterWasteLoadEdit.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Meters.MeterWasteLoadEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="cntMain" ContentPlaceHolderID="cphContent" runat="server">
    <script type="text/javascript">

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
    <div class="divTitle">
        <asp:Label ID="lblProperties" runat="server"></asp:Label>
    </div>
    <div class="divDetail">
        <!--*******************************************************-->
        <!--**************** Meter Properties *********************-->
        <!--*******************************************************-->
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
                <asp:CustomValidator ID="cvLoadDate" runat="server" ValidationGroup="Edit" EnableClientScript="true"
                    ClientValidationFunction="validateDate" Display="Dynamic" CssClass="rfvRequested"></asp:CustomValidator>
                <asp:HiddenField runat="server" ID="hdnLoadDate" />
                <asp:TextBox ID="txtLoadDate" runat="server" ReadOnly="true" CssClass="lblValue"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calLoadDate" runat="server" DefaultView="Days"
                    TargetControlID="txtLoadDate" PopupButtonID="btnLoadDate">
                </ajaxToolkit:CalendarExtender>
            </div>
            <div class="divColumn column4">
                <asp:Label ID="lblLoadValue" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvLoadValue" ControlToValidate="txtLoadValue" ValidationGroup="Load"
                    runat="server" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvLoadValue" runat="server" ControlToValidate="txtLoadValue"
                    Type="Double" Operator="DataTypeCheck" ValidationGroup="Edit" Display="Dynamic"
                    CssClass="rfvRequested"></asp:CompareValidator>
                <asp:TextBox ID="txtLoadValue" runat="server" CssClass="lblValue"></asp:TextBox>
            </div>
            <div class="divColumn column4">
                <asp:Label ID="lblLoadUnits" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:CompareValidator ID="cvLoadUnits" runat="server" ControlToValidate="ddlLoadUnits"
                    Operator="NotEqual" ValueToCompare="0" ValidationGroup="Edit" Display="Dynamic"
                    CssClass="rfvRequested"></asp:CompareValidator>
                <asp:DropDownList ID="ddlLoadUnits" runat="server">
                </asp:DropDownList>
            </div>
            <div class="divColumn column1">
                <asp:Label ID="lblLoadTypes" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:CompareValidator ID="cvLoadTypes" runat="server" ControlToValidate="ddlLoadTypes"
                    Operator="NotEqual" ValueToCompare="0" ValidationGroup="Edit" Display="Dynamic"
                    CssClass="rfvRequested"></asp:CompareValidator>
                <asp:DropDownList ID="ddlLoadTypes" runat="server">
                </asp:DropDownList>
            </div>
        </div>
        <div class="clear">
        </div>

        <!--**************************************************************-->
        <!--********************* Save  **********************************-->
        <!--**************************************************************-->
        <div class="divContentSave">
            <asp:Button ID="btnSave" runat="server" ValidationGroup="Edit" CssClass="btnSave btnActions" />
        </div>
</asp:Content>
