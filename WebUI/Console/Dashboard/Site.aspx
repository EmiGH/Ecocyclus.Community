<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="Site.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Site" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>

<asp:Content ContentPlaceHolderID="cphhead" ID="cntHead" runat="server">
    <script type="text/javascript" src="../../Scripts/mapReferences.js"></script>
</asp:Content>
<asp:Content ID="cntSite" ContentPlaceHolderID="cphContent" runat="server">
    
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
                "bAutoWidth": true
            });
        });
    
    </script>
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

    <div class="divLineSeparatorHeader Sites">
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
            <!--
            <div class="divColumn column3">
                <asp:Label ID="lblWeeks" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblWeeksValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            -->
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

        <!--*********************************************** -->
        <!--*************** Performance ******************* -->
        <!--*********************************************** -->
        <div class="clear">
        </div>
        <div class="divTitle">
            <asp:Label ID="lblPerformance" runat="server"></asp:Label>
        </div>
        <div id="divPerformance">
            <table class="tbl" id="tblPerformance">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblPerformanceHeaderElectricity" runat="server" CssClass="lblHeaderElectricity"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblPerformanceHeaderFuels" runat="server" CssClass="lblHeaderFuel"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblPerformanceHeaderTransport" runat="server" CssClass="lblHeaderTransport"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblPerformanceHeaderWaste" runat="server" CssClass="lblHeaderWaste"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblPerformanceHeaderWater" runat="server" CssClass="lblHeaderWater"></asp:Label>
                        </th>
                        <th class="tdlast">
                            <asp:Label ID="lblPerformanceHeaderTotalCO2" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:Label ID="lblPerformanceElectricityUnits" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceElectricityUnit" runat="server"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblPerformanceElectricityCO2" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceElectricityCO2Unit" runat="server"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblPerformanceElectricityKPI" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceElectricityKPIUnit" runat="server"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblPerformanceElectricityKPIMoney" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceElectricityKPIMoneyUnit" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPerformanceFuelsUnits" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceFuelsUnit" runat="server"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblPerformanceFuelsCO2" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceFuelsCO2Unit" runat="server"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblPerformanceFuelsKPI" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceFuelsKPIUnit" runat="server"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblPerformanceFuelsKPIMoney" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceFuelsKPIMoneyUnit" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPerformanceTransportUnits" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceTransportUnit" runat="server"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblPerformanceTransportCO2" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceTransportCO2Unit" runat="server"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblPerformanceTransportKPI" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceTransportKPIUnit" runat="server"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblPerformanceTransportKPIMoney" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceTransportKPIMoneyUnit" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPerformanceWasteUnits" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceWasteUnit" runat="server"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblPerformanceWasteCO2" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceWasteCO2Unit" runat="server"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblPerformanceWasteKPI" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceWasteKPIUnit" runat="server"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblPerformanceWasteKPIMoney" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceWasteKPIMoneyUnit" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPerformanceWaterUnits" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceWaterUnit" runat="server"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblPerformanceWaterCO2" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceWaterCO2Unit" runat="server"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblPerformanceWaterKPI" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceWaterKPIUnit" runat="server"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblPerformanceWaterKPIMoney" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceWaterKPIMoneyUnit" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                            <br />
                            <br />
                            <asp:Label ID="lblPerformanceTotalCO2" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceTotalCO2Unit" runat="server"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblPerformanceTotalCO2KPI" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceTotalCO2KPIUnit" runat="server"></asp:Label><br />
                            <br />
                            <asp:Label ID="lblPerformanceTotalCO2KPIMoney" runat="server"></asp:Label>
                            <asp:Label ID="lblPerformanceTotalCO2KPIMoneyUnit" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <!--*********************************************** -->
        <!--***************** Charts ********************** -->
        <!--*********************************************** -->
        <div class="divTitle">
            <asp:Label ID="lblChart" runat="server"></asp:Label>
        </div>
        <div id="divChart" class="divControlChart">
            <asp:DropDownList ID="ddlChartProtocols" runat="server" AutoPostBack="true">
            </asp:DropDownList>
            <div class="divContainerChat">
                <asp:UpdatePanel ID="upChart" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlChartProtocols" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <rad:RadChart ID="radChart" IntelligentLabelsEnabled="true" runat="server" Width="690"
                            Height="300px" AutoLayout="True"  BorderWidth="0px">
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
            <asp:Label ID="lblChartAggregates" runat="server"></asp:Label><br />
        </div>
        <div id="divChartAggregates" class="divControlChart">
            <asp:DropDownList ID="ddlChartAggregates" runat="server" AutoPostBack="true">
            </asp:DropDownList>
            <div class="divContainerChat">
                <asp:UpdatePanel ID="upChartAggregates" runat="server" UpdateMode="Conditional">
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
