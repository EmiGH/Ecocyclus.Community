<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="MeterWasteLoads.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Meters.MeterWasteLoads" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Charting" tagprefix="telerik" %>

<asp:Content ID="cntMeterWaste" ContentPlaceHolderID="cphContent" runat="server">
    
    <script type="text/javascript">

        //*********************************************************//
        //***************** Table Sort and Paging *****************//
        //*********************************************************//
        $(document).ready(function () {
            $('#tblLoads').dataTable({
                "bPaginate": true,
                "bLengthChange": true,
                "bFilter": true,
                "bSort": true,
                "bInfo": true,
                "bAutoWidth": true,
                "sPaginationType": "full_numbers",
                "iDisplayLength": 10
            });
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

        <!--*******************************************************-->
        <!--**************** Meter Properties *********************-->
        <!--*******************************************************-->
        <div class="divTitle">
            <asp:Label ID="lblSiteProperties" runat="server"></asp:Label>
            <div class="divActions">
                <asp:HyperLink ID="hplPrint" runat="server" CssClass="hplPrint"></asp:HyperLink>
            </div>
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
        <table class="tbl" id="tblLoads">
            <thead>
                <tr>
                    <th>
                    </th>
                    <th>
                    </th>
                    <th>
                        <asp:Label ID="lblHeaderDate" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblHeaderWaste" runat="server"></asp:Label>
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
                            <td class="tdIconstwo">
                                <asp:Button runat="server" ID="btnLoadEdit" CommandName="Edit" CssClass="btnSitesEdit" />
                                <asp:Button runat="server" ID="btnLoadDelete" CommandName="Delete" CssClass="btnSitesDelete" />
                            </td>
                            <td>
                                <asp:Button ID="btnSitesAlert" CssClass="btnSitesAlert" CommandName="Alert" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblWaste" runat="server"></asp:Label>
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
                            <td class="tdIconstwo">
                                <asp:Button runat="server" ID="btnLoadEdit" CommandName="Edit" CssClass="btnSitesEdit" />
                                <asp:Button runat="server" ID="btnLoadDelete" CommandName="Delete" CssClass="btnSitesDelete" />
                            </td>
                            <td>
                                <asp:Button ID="btnSitesAlert" CssClass="btnSitesAlert" CommandName="Alert" runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="lblDate" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblWaste" runat="server"></asp:Label>
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
                        <asp:Label ID="lblPerformanceHeaderWaste" runat="server"></asp:Label>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>
                        <asp:Label ID="lblPerformanceWasteUnits" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceWasteUnit" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPerformanceWasteKPI" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceWasteKPIUnit" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPerformanceWasteKPIMoney" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceWasteKPIMoneyUnit" runat="server"></asp:Label>
                    </td>
                    <td class="tdlast">
                        <asp:Label ID="lblPerformanceWasteCO2" runat="server"></asp:Label>
                        <asp:Label ID="lblPerformanceWasteCO2Unit" runat="server"></asp:Label>
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
        <div class="divCombo">
            <asp:DropDownList ID="ddlChartSeries" runat="server" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        <asp:UpdatePanel ID="upChartSeries" runat="server" UpdateMode="Always">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlChartSeries" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <rad:RadChart ID="radChartSeries" IntelligentLabelsEnabled="true" runat="server"
                    Width="712" Height="300px" AutoLayout="True" >
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
    <div class="divTitle">
        <asp:Label ID="lblChartAggregates" runat="server"></asp:Label>
    </div>
    <div id="divChartAggregates" class="divControlChart">
        <div class="divCombo">
            <asp:DropDownList ID="ddlChartAggregates" runat="server" AutoPostBack="true">
            </asp:DropDownList>
        </div>
        <asp:UpdatePanel ID="upChartAggregates" runat="server" UpdateMode="Always">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlChartAggregates" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <rad:RadChart ID="radChartAggregates" IntelligentLabelsEnabled="true" runat="server"
                        Width="712" Height="300px" AutoLayout="True" >
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
</asp:Content>
