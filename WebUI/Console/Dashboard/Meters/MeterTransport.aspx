<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="MeterTransport.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Meters.TransportMeter" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="cntMeterTransport" ContentPlaceHolderID="cphContent" runat="server">
    
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
            <div class="divColumn column1">
                <asp:Label ID="lblDescription" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblDescriptionValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <!-- Units -->
                <asp:Label ID="lblUnit" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblUnitValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
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
                        <td class="last">
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
        <div class="divTitle">
            <asp:Label ID="lblChartAggregates" runat="server"></asp:Label>
        </div>
        <div id="divChartAggregates" class="divControlChart">
            <asp:DropDownList ID="ddlChartAggregates" runat="server" AutoPostBack="true">
            </asp:DropDownList>
            <div class="divContainerChat">
                <asp:UpdatePanel ID="upChartAggregates" runat="server" UpdateMode="Always">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlChartAggregates" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <rad:RadChart ID="radChartAggregates" IntelligentLabelsEnabled="true" runat="server"
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
    </div>

</asp:Content>
