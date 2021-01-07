<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Print.Master" AutoEventWireup="true" CodeBehind="MeterFuelReportPrintable.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Reports.MeterFuelReportPrintable" %>

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
        <div id="divProperties">
            <div class="divColumn">
                <asp:Label ID="lblIdentification" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblIdentificationValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn">
                <asp:Label ID="lblDescription" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblDescriptionValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
        </div>

    </div>
        
    <!--*******************************************************-->
    <!--****************** Meter Load Series ******************-->
    <!--*******************************************************-->
    <div id="divSeries">
        <div class="divTitle">
            <asp:Label ID="lblSeries" runat="server"></asp:Label>
        </div>
        <table class="tbl" id="tblLoads">
            <thead>
                <tr>
                    <th>
                        <asp:Label ID="lblHeaderDate" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblHeaderFuel" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblHeaderTotal" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblHeaderUnit" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblHeaderTotalCO2" runat="server"></asp:Label>
                    </th>
                    <th class="tdlast">
                        <asp:Label ID="lblHeaderOperator" runat="server"></asp:Label>
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptLoadSeries" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFuel" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTotal" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblUnit" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTotalCO2" runat="server"></asp:Label>
                            </td>
                            <td class="tdlast">
                                <asp:Label ID="lblOperator" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="trAlternating">
                            <td>
                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblFuel" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTotal" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblUnit" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTotalCO2" runat="server"></asp:Label>
                            </td>
                            <td class="tdlast">
                                <asp:Label ID="lblOperator" runat="server"></asp:Label>
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
                        <asp:Label ID="lblPerformanceHeaderFuels" runat="server"></asp:Label>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:Label ID="lblPerformanceFuelsUnits" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceFuelsUnit" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPerformanceFuelsKPI" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceFuelsKPIUnit" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPerformanceFuelsKPIMoney" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceFuelsKPIMoneyUnit" runat="server"></asp:Label>
                    </td>
                    <td class="tdlast">
                        <asp:Label ID="lblPerformanceFuelsCO2" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceFuelsCO2Unit" runat="server"></asp:Label>
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
    <br />

    <div class="divTitle">
        <asp:Label ID="lblChartAggregatesConsumption" runat="server"></asp:Label><br />
    </div>
    <div id="divChartAggregatesConsumption" class="divControlChart">
                
        <rad:RadChart ID="radChartAggregatesConsumption" IntelligentLabelsEnabled="true" runat="server"
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
        <asp:Label ID="lblChartAggregatesCO2" runat="server"></asp:Label><br />
    </div>
    <div id="divChartAggregatesCO2" class="divControlChart">
        
        <rad:RadChart ID="radChartAggregatesCO2" IntelligentLabelsEnabled="true" runat="server"
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
