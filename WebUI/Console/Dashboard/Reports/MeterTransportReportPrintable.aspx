<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Print.Master" AutoEventWireup="true" CodeBehind="MeterTransportReportPrintable.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Reports.MeterTransportReportPrintable" %>

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
    </div>

    <!--*******************************************************-->
    <!--****************** Meter Load Series ******************-->
    <!--*******************************************************-->
    <div id="divSeries">
        <div class="divTitle">
            <asp:Label ID="lblSeries" runat="server"></asp:Label>
        </div>
        <table id="tblSerie" class="tbl" id="tblLoads">
            <thead>
                <tr>
                    <th>
                        <asp:Label ID="lblHeaderDate" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblHeaderTransport" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblHeaderPlateNumber" runat="server"></asp:Label>
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
                                <asp:Label ID="lblTransport" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPlateNumber" runat="server"></asp:Label>
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
                                <asp:Label ID="lblTransport" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPlateNumber" runat="server"></asp:Label>
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
                        <asp:Label ID="lblPerformanceHeaderTransport" runat="server"></asp:Label>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:Label ID="lblPerformanceTransportUnits" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceTransportUnit" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPerformanceTransportKPI" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceTransportKPIUnit" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPerformanceTransportKPIMoney" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceTransportKPIMoneyUnit" runat="server"></asp:Label>
                    </td>
                    <td class="tdlast">
                        <asp:Label ID="lblPerformanceTransportCO2" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceTransportCO2Unit" runat="server"></asp:Label>
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
