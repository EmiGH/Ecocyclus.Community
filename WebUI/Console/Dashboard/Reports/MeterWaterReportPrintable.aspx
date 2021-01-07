<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Print.Master" AutoEventWireup="true" CodeBehind="MeterWaterReportPrintable.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Reports.MeterWaterReportPrintable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>

<asp:Content ID="ctnReport" ContentPlaceHolderID="cphContent" runat="server">
    
    <div class="divDetail">
        <!--*******************************************************-->
        <!--**************** Meter Properties *********************-->
        <!--*******************************************************-->
        <div class="divTitle">
            <asp:Label ID="lblSiteProperties" runat="server"></asp:Label>
        </div>
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
        <div class="divColumn column3 last">
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
        <!-- Initial Reading -->
        <div id="divInitialReading" runat="server" style="display: none" class="divColumn column3">
            <asp:Label ID="lblInitialReading" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:Label ID="lblInitialReadingValue" runat="server" CssClass="lblValue"></asp:Label>
        </div>
        <div class="divColumn column1 margin">
            <asp:Label ID="lblDescription" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:Label ID="lblDescriptionValue" runat="server" CssClass="lblValue"></asp:Label>
        </div>
        <div class="clear">
        </div>
    </div>

    <!--*******************************************************-->
    <!--****************** Meter Load Series ******************-->
    <!--*******************************************************-->
    <div id="divLoads">
        <div class="divTitle">
            <asp:Label ID="lblLoads" runat="server"></asp:Label>
        </div>
        <table class="tbl" id="tblLoads">
            <thead>
                <tr>
                    <th>
                        <asp:Label ID="lblLoadsHeaderFrom" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblLoadsHeaderTo" runat="server"></asp:Label>
                    </th>
                    <th id="thLoadsHeaderReading" runat="server">
                        <asp:Label ID="lblLoadsHeaderReading" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblLoadsHeaderTotal" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblLoadsHeaderTotalCO2" runat="server"></asp:Label>
                    </th>
                    <th class="tdlast">
                        <asp:Label ID="lblLoadsHeaderOperator" runat="server"></asp:Label>
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptLoads" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblLoadFrom" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblLoadTo" runat="server"></asp:Label>
                            </td>
                            <td id="tdLoadReading" runat="server">
                                <asp:Label ID="lblLoadReading" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblLoadTotal" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblLoadTotalCO2" runat="server"></asp:Label>
                            </td>
                            <td class="tdlast">
                                <asp:Label ID="lblLoadOperator" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="trAlternating">
                            <td>
                                <asp:Label ID="lblLoadFrom" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblLoadTo" runat="server"></asp:Label>
                            </td>
                            <td id="tdLoadReading" runat="server">
                                <asp:Label ID="lblLoadReading" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblLoadTotal" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblLoadTotalCO2" runat="server"></asp:Label>
                            </td>
                            <td class="tdlast">
                                <asp:Label ID="lblLoadOperator" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
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
                    <th colspan="4" class="tdlast">
                        <asp:Label ID="lblPerformanceHeaderWater" runat="server"></asp:Label>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:Label ID="lblPerformanceWaterUnits" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceWaterUnit" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPerformanceWaterKPI" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceWaterKPIUnit" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPerformanceWaterKPIMoney" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceWaterKPIMoneyUnit" runat="server"></asp:Label>
                    </td>
                    <td class="tdlast">
                        <asp:Label ID="lblPerformanceWaterCO2" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceWaterCO2Unit" runat="server"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>

    <!--*********************************************** -->
    <!--***************** Charts ********************** -->
    <!--*********************************************** -->
    <div class="divTitle">
        <asp:Label ID="lblChartSeriesConsumption" runat="server"></asp:Label>
    </div>
    <div id="divChartSeriesConsumption" class="divControlChart">
        
        <rad:RadChart ID="radChartSeriesConsumption" IntelligentLabelsEnabled="true" runat="server"
                Width="960" Height="400" AutoLayout="True"  BorderWidth="0px">
            <Appearance Border-Width="0" FillStyle-MainColor="#ffffff">
            </Appearance>
            <Legend Visible="False">
                <Appearance Visible="False">
                </Appearance>
            </Legend>
            <PlotArea>
                <EmptySeriesMessage Visible="False">
                    <Appearance Visible="False">
                    </Appearance>
                </EmptySeriesMessage>
                <Appearance>    
                    <Border Width="0" />
                    <FillStyle FillType="Solid" MainColor="#ffffff">
                    </FillStyle>
                </Appearance>
            </PlotArea>
            <ChartTitle Visible="False">
                <Appearance Visible="False">
                </Appearance>
            </ChartTitle>
        </rad:RadChart>
        
        
    </div>
    <br />

    <div class="divTitle">
        <asp:Label ID="lblChartSeriesCO2" runat="server"></asp:Label>
    </div>
    <div id="divChartSeriesCO2" class="divControlChart">
        
        <rad:RadChart ID="radChartSeriesCO2" IntelligentLabelsEnabled="true" runat="server"
                Width="960" Height="400" AutoLayout="True"  BorderWidth="0px">
            <Appearance Border-Width="0" FillStyle-MainColor="#ffffff">
            </Appearance>
            <Legend Visible="False">
                <Appearance Visible="False">
                </Appearance>
            </Legend>
            <PlotArea>
                <EmptySeriesMessage Visible="False">
                    <Appearance Visible="False">
                    </Appearance>
                </EmptySeriesMessage>
                <Appearance>    
                    <Border Width="0" />
                    <FillStyle FillType="Solid" MainColor="#ffffff">
                    </FillStyle>
                </Appearance>
            </PlotArea>
            <ChartTitle Visible="False">
                <Appearance Visible="False">
                </Appearance>
            </ChartTitle>
        </rad:RadChart>

    </div>

</asp:Content>
