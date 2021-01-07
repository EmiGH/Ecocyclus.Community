<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="MeterElectricityLoads.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Meters.MeterElectricityLoads" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<asp:Content ID="cntMeterElectricity" ContentPlaceHolderID="cphContent" runat="server">

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
            <div id="divInitialReading" runat="server" style="display: none">
                <div class="divColumn column3 last">
                    <asp:Label ID="lblInitialReading" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblInitialReadingValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
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
    <div id="divLoads">
        <div class="divTitle">
            <asp:Label ID="lblLoads" runat="server"></asp:Label>
        </div>
        <table class="tbl" id="tblLoads">
            <thead>
                <tr>
                    <th>
                    </th>
                    <th>
                    </th>
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
                            <td class="tdIconstwo">
                                <asp:Button runat="server" ID="btnLoadEdit" CommandName="Edit" CssClass="btnSitesEdit" />
                                <asp:Button runat="server" ID="btnLoadDelete" CommandName="Delete" CssClass="btnSitesDelete" />
                            </td>
                            <td>
                                <asp:Button ID="btnSitesAlert" CssClass="btnSitesAlert" CommandName="Alert" runat="server" />
                            </td>
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
                            <td class="tdIconstwo">
                                <asp:Button runat="server" ID="btnLoadEdit" CommandName="Edit" CssClass="btnSitesEdit" />
                                <asp:Button runat="server" ID="btnLoadDelete" CommandName="Delete" CssClass="btnSitesDelete" />
                            </td>
                            <td>
                                <asp:Button ID="btnSitesAlert" CssClass="btnSitesAlert" CommandName="Alert" runat="server" />
                            </td>
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
                    Width="690" Height="300px" AutoLayout="True" BorderWidth="0px">
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
