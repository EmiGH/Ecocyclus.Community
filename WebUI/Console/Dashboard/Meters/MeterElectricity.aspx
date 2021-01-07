<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="MeterElectricity.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Meters.MeterElectricity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="cntMeterElectricity" ContentPlaceHolderID="cphContent" runat="server">

    <script type="text/javascript">

        //*********************************************************//
        //***************** Table Sort and Paging *****************//
        //*********************************************************//
        $(document).ready(function () {
            $('#tblPerformance').dataTable({
                "bPaginate": false,
                "bFilter": false,
                "bSort": false,
                "bInfo": false,
                "bAutoWidth": true,
            });
        });
    
    </script>
    
    <div class="divLineSeparatorHeader Meter">
    </div>
    <div class="divDetail">
        <!--**************************************************-->
        <!--*************** Meter Actions ********************-->
        <!--**************************************************-->
        <div class="divTitle">
            <asp:Label ID="lblSiteProperties" runat="server"></asp:Label>
            <div class="divActions">
                <asp:LinkButton ID="lnkEdit" runat="server" CssClass="lnkModify"></asp:LinkButton>
                <asp:LinkButton ID="lnkRemove" runat="server" CssClass="lnkRemove"></asp:LinkButton>
            </div>
        </div>
        <!--*******************************************************-->
        <!--**************** Meter Properties *********************-->
        <!--*******************************************************-->
        <div id="divProperties">
            <div class="divColumn column3">
                <asp:Label ID="lblIdentification" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblIdentificationValue" runat="server" CssClass="lblValue"></asp:Label>
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
            <div class="divColumn column3">
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
            <div class="clear">
            </div>
            <div class="divColumn column3">
                <!-- Is Physical -->
                <asp:Label ID="lblIsPhysical" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblIsPhysicalValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>

                <!-- Initial Date -->
                <div id="divInitialDate" runat="server" style="display: none">
                    <div class="divColumn column3">
                        <asp:Label ID="lblInitialDate" runat="server" CssClass="lblTitle"></asp:Label>
                        <asp:Label ID="lblInitialDateValue" runat="server" CssClass="lblValue"></asp:Label>
                    </div>
                </div>
                <!-- Initial Reading -->
                <div id="divInitialReading" runat="server" style="display: none">
                    <div class="divColumn column3 last">
                        <asp:Label ID="lblInitialReading" runat="server" CssClass="lblTitle"></asp:Label>
                        <asp:Label ID="lblInitialReadingValue" runat="server" CssClass="lblValue"></asp:Label>
                    </div>
                </div>

            <div class="clear">
            </div>
            <div class="divColumn column1 margin">
                <asp:Label ID="lblDescription" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblDescriptionValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
    <!--*********************************************** -->
    <!--*************** Performance ******************* -->
    <!--*********************************************** -->
    <div class="divTitle">
        <asp:Label ID="lblPerformance" runat="server"></asp:Label>
    </div>
    <div id="divPerformance">
        <table class="tbl" id="tblPerformance">
            <thead>
                <tr>
                    <th colspan="4" class="thlast">
                        <asp:Label ID="lblPerformanceHeaderElectricity" runat="server"></asp:Label>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:Label ID="lblPerformanceElectricityUnits" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceElectricityUnit" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPerformanceElectricityKPI" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceElectricityKPIUnit" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPerformanceElectricityKPIMoney" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceElectricityKPIMoneyUnit" runat="server"></asp:Label>
                    </td>
                    <td class="tdlast">
                        <asp:Label ID="lblPerformanceElectricityCO2" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceElectricityCO2Unit" runat="server"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <!--*********************************************** -->
    <!--***************** Charts ********************** -->
    <!--***********************************************-->
    <div class="divTitle">
        <asp:Label ID="lblChartSeries" runat="server"></asp:Label>
    </div>
    <div id="divChartSeries" class="divControlChart">
        <asp:DropDownList ID="ddlChartSeries" runat="server" AutoPostBack="true">
        </asp:DropDownList>
        <div class="divContainerChat">
            <asp:UpdatePanel ID="upChartSeries" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlChartSeries" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <rad:RadChart ID="radChartSeries" IntelligentLabelsEnabled="true" runat="server"
                        Width="690" Height="300px" AutoLayout="True"  BorderWidth="0px">
                        <Appearance Border-Width="0" FillStyle-MainColor="#E5E4DD">
                        </Appearance>
                        <Legend Visible="False">
                            <Appearance Visible="False">
                            </Appearance>
                        </Legend>
                        <PlotArea>
                            <Appearance>
                                <Border Width="0" />
                                <FillStyle FillType="Solid" MainColor="#E5E4DD">
                                </FillStyle>
                            </Appearance>
                            <EmptySeriesMessage Visible="False">
                                <Appearance Visible="False">
                                </Appearance>
                            </EmptySeriesMessage>
                        </PlotArea>
                        <ChartTitle Visible="False">
                            <Appearance Visible="False">
                            </Appearance>
                        </ChartTitle>
                    </rad:RadChart>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
