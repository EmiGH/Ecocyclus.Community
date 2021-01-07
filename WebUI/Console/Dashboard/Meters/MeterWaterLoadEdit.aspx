<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="MeterWaterLoadEdit.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Meters.MeterWaterLoadEdit" %>

<asp:Content ID="cntMain" ContentPlaceHolderID="cphContent" runat="server">
    <div class="divLineSeparatorHeader Meter">
    </div>
    <div class="divTitle">
        <asp:Label ID="lblSiteProperties" runat="server"></asp:Label>
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
            <div class="divColumn column1">
                <asp:Label ID="lblDescription" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblDescriptionValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <!-- Is Physical -->
                <asp:Label ID="lblIsPhysical" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblIsPhysicalValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <!-- Initial Reading -->
            <div id="divInitialReading" runat="server" style="display: none">
                <asp:Label ID="lblInitialDate" runat="server"></asp:Label>
                <asp:Label ID="lblInitialDateValue" runat="server"></asp:Label>
                <asp:Label ID="lblInitialReading" runat="server"></asp:Label>
                <asp:Label ID="lblInitialReadingValue" runat="server"></asp:Label>
            </div>
            <div class="divColumn column3">
                <!-- Units -->
                <asp:Label ID="lblUnit" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblUnitValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3 last">
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
        </div>
        <div class="clear">
        </div>
        <!--*******************************************************-->
        <!--**************** Edit Load Data   *********************-->
        <!--*******************************************************-->
        <div class="divTitle">
            <asp:Label ID="lblLoadEdit" runat="server"></asp:Label>
        </div>
        <asp:Label Style="color: Red" ID="lblMandatoryFieldsExplanation" runat="server"></asp:Label>
        <br />
        <br />
        <div>
            <asp:Label ID="lblValidLoadRange" runat="server"></asp:Label>
        </div>
        <br />
        <br />
        <div id="divLoadEdit">
            <div class="divColumn column5 date margin">
                <asp:Label ID="lblLoadFrom" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblLoadFromValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column5 date margin">
                <asp:Label ID="lblLoadTo" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblLoadToValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divForm AddSerie">
                <div class="divColumn column5 input margin">
                    <asp:Label ID="lblLoadValue" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvLoadValue" ControlToValidate="txtLoadValue" ValidationGroup="Edit"
                        runat="server" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="cvLoadValue" runat="server" ControlToValidate="txtLoadValue"
                        Type="Double" Operator="DataTypeCheck" ValidationGroup="Edit" Display="Dynamic"
                        CssClass="rfvRequested"></asp:CompareValidator>
                    <asp:TextBox ID="txtLoadValue" runat="server" CssClass="lblValue"></asp:TextBox>
                </div>
                <div class="divColumn column5 margin">
                    <asp:Label ID="lblLoadUnit" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:DropDownList ID="ddlLoadUnits" runat="server">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="cvLoadUnit" runat="server" ControlToValidate="ddlLoadUnits"
                        Operator="NotEqual" ValueToCompare="0" ValidationGroup="Edit"></asp:CompareValidator>
                </div>
            </div>
            <div class="divColumn column5 name last margin">
                <asp:Label ID="lblLoadLastOperator" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblLoadLastOperatorValue" runat="server" CssClass="lblValue"></asp:Label>
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
    </div>
</asp:Content>
