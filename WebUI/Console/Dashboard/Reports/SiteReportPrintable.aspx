<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Print.Master" AutoEventWireup="true" CodeBehind="SiteReportPrintable.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Reports.SiteReportPrintable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>

<asp:Content ID="cntMain" ContentPlaceHolderID="cphContent" runat="server">
    <script type="text/javascript" src="../../../Scripts/mapReferences.js"></script>

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
    <script type="text/javascript">

        function validateFilter(source, args) {

            var _txt = $get('txtFrom').value;
            if (_txt != '') {
                $get('hdnFrom').value = _txt;
            }
            alert(_txt);
            alert($get('hdnFrom').value);

            _txt = $get('txtTo').value;
            if (_txt != '') {
                $get('hdnTo').value = _txt;
            }

            args.IsValid = true;
        }
    
    </script>
    
    <div class="divLineSeparatorHeader Report">
    </div>
    <div class="divDetail">
        <div id="divReport" runat="server">

            <!--**************************************************-->
            <!--*************** Site Properties ******************-->
            <!--**************************************************-->
            <div class="divTitle">
                <asp:Label ID="lblSiteProperties" runat="server"></asp:Label>
                <div class="divActions">
                        <asp:LinkButton ID="lnkExport" runat="server" CssClass="lnkExport" style="display:none;"></asp:LinkButton>
                </div>
            </div>

            <div id="divProperties" runat="server">
                <div id="divImage" class="divColumn column3r img">
                    <div class="divCenter">
                        <p>
                            <span>
                                <img id="imgImage" runat="server" alt="" />
                            </span>
                        </p>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <div class="divColumn column3r">
                    <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblTitleValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
                <div class="divColumn column3r last">
                    <asp:Label ID="lblStart" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblStartValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
                <!--
                <div class="divColumn column3r">
                    <asp:Label ID="lblWeeks" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblWeeksValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
                -->
                <div class="divColumn column3r last">
                    <asp:Label ID="lblNumber" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblNumberValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
                <div class="divColumn column3r margin">
                    <asp:Label ID="lblValue" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblValueValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
                <div class="divColumn column3r margin">
                    <asp:Label ID="lblFloorSpace" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblFloorSpaceValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
                <div class="divColumn column3r last margin">
                    <asp:Label ID="lblUnits" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblUnitsValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
                <div class="clear">
                </div>
                <div class="divTitle">
                    <asp:Label ID="lblSiteClient" runat="server"></asp:Label>
                </div>
                <div class="divColumn column2r">
                    <asp:Label ID="lblClient" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblClientValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
                <div class="divColumn column2r last">
                    <asp:Label ID="lblAgent" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblAgentValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
                <div class="divColumn column2r">
                    <asp:Label ID="lblContractor" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblContractorValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
                <div class="divColumn column2r last">
                    <asp:Label ID="lblResponsible" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblResponsibleValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
                <div class="divColumn column3r">
                    <asp:Label ID="lblManager" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblManagerValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
                <div class="divColumn column3r">
                    <!-- Contact -->
                    <asp:Label ID="lblTelephone" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblTelephoneValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
                <div class="divColumn column3r last">
                    <asp:Label ID="lblEmail" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblEmailValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
                <div class="divColumn column3r">
                    <asp:Label ID="lblUrl" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblUrlValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
                <div class="divColumn column3r">
                    <asp:Label ID="lblFacebook" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblFacebookValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
                <div class="divColumn column3r last">
                    <asp:Label ID="lblTwitter" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblTwitterValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
                <div class="divColumn column1r">
                    <asp:Label ID="lblDescription" runat="server" CssClass="lblTitle"></asp:Label>
                    <asp:Label ID="lblDescriptionValue" runat="server" CssClass="lblValue"></asp:Label>
                </div>
            </div>
            <div class="divColumn column1r margin">
                <asp:Label ID="lblLocation" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblLocationValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="clear">
            </div>
            <div class="divTitle">
                <asp:Label ID="lblMap" runat="server"></asp:Label>
            </div>

            <!--****************************************** -->
            <!--*************** Map ********************** -->
            <!--****************************************** -->
            <div id="divMap">
                <input type="hidden" id="hdnMapPoint" name="hdnMapPoint" value="<%=Location%>" />
                <div id="divMapCanvas" class="divMapCanvas divMapDetail divLarge">
                </div>
            </div>

            <br />
            <br />

            <!--*********************************************** -->
            <!--*************** Dates Filter ****************** -->
            <!--*********************************************** -->
            <div id="divDatesFilter">
                <asp:Label ID="lblFrom" runat="server"></asp:Label>
                <asp:HiddenField runat="server" ID="hdnFrom" />
                <asp:TextBox ID="txtFrom" runat="server" ReadOnly="true"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calFrom" runat="server" DefaultView="Days" TargetControlID="txtFrom"
                    PopupButtonID="btnFrom">
                </ajaxToolkit:CalendarExtender>
                <asp:Label ID="lblTo" runat="server"></asp:Label>
                <asp:HiddenField runat="server" ID="hdnTo" />
                <asp:TextBox ID="txtTo" runat="server" ReadOnly="true"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="calTo" runat="server" DefaultView="Days" TargetControlID="txtTo"
                    PopupButtonID="btnTo">
                </ajaxToolkit:CalendarExtender>
                <asp:CustomValidator ID="cvFilter" runat="server" ValidationGroup="Filter" EnableClientScript="true"
                    ClientValidationFunction="validateFilter" Display="Dynamic" CssClass="rfvRequested"></asp:CustomValidator>
                <asp:CompareValidator ID="rvFilter" runat="server" ValidationGroup="Filter" EnableClientScript="true"
                    Type="Date" ControlToCompare="txtFrom" ControlToValidate="txtTo" Operator="GreaterThanEqual"></asp:CompareValidator>
                <asp:Button ID="btnFilter" runat="server" ValidationGroup="Filter" />
            </div>
            <br /><br />

            <!--*********************************************** -->
            <!--*************** CO2 Data ********************** -->
            <!--*********************************************** -->
            <div id="divCO2" runat="server">
                <div class="divTitle">
                    <asp:Label ID="lblCO2Data" runat="server"></asp:Label>
                </div>
                <table class="tbl" id="tblCO2">
                    <thead>
                        <tr>
                            <th>
                                <asp:Label ID="lblCO2TargetsHeader" runat="server"></asp:Label>
                            </th>
                            <th>
                                <asp:Label ID="lblCO2TargetsHeaderPKIMoney" runat="server"></asp:Label>
                            </th>
                            <th class="tdlast">
                                <asp:Label ID="lblCO2TargetsHeaderPKIMts" runat="server"></asp:Label>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblCO2Targets" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCO2TargetsPKIMoney" runat="server"></asp:Label>
                            </td>
                            <td class="tdlast">
                                <asp:Label ID="lblCO2TargetsPKIMts" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <div class="divContainertbl">
                    <div class="divWraptbl">
                        <table class="tbl" id="tblCO2Evolution">
                            <thead>
                                <tr>
                                    <th>
                                    </th>
                                    <th colspan="3">
                                        <asp:Label ID="lblCO2MonthlyEvolutionHeaderElectricity" runat="server"></asp:Label>
                                    </th>
                                    <th colspan="3">
                                        <asp:Label ID="lblCO2MonthlyEvolutionHeaderFuels" runat="server"></asp:Label>
                                    </th>
                                    <th colspan="3">
                                        <asp:Label ID="lblCO2MonthlyEvolutionHeaderTransport" runat="server"></asp:Label>
                                    </th>
                                    <th colspan="3">
                                        <asp:Label ID="lblCO2MonthlyEvolutionHeaderWaste" runat="server"></asp:Label>
                                    </th>
                                    <th colspan="3">
                                        <asp:Label ID="lblCO2MonthlyEvolutionHeaderWater" runat="server"></asp:Label>
                                    </th>
                                    <th colspan="3" class="tdlast">
                                        <asp:Label ID="lblCO2MonthlyEvolutionHeaderCO2" runat="server"></asp:Label>
                                    </th>
                                </tr>
                                <tr>
                                    <th>
                                    </th>
                                    <th nowrap>
                                        <asp:Label ID="lblCO2MonthlyEvolutionElectricityCO2Units" runat="server"></asp:Label>
                                    </th>
                                    <th nowrap>
                                        <asp:Label ID="lblCO2MonthlyEvolutionElectricityCO2KPIMtsUnits" runat="server"></asp:Label>
                                    </th>
                                    <th nowrap>
                                        <asp:Label ID="lblCO2MonthlyEvolutionElectricityCO2KPIMoneyUnits" runat="server"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFuelsCO2Units" runat="server"></asp:Label>
                                    </th>
                                    <th nowrap>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFuelsCO2KPIMtsUnits" runat="server"></asp:Label>
                                    </th>
                                    <th nowrap>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFuelsCO2KPIMoneyUnits" runat="server"></asp:Label>
                                    </th>
                                    <th nowrap>
                                        <asp:Label ID="lblCO2MonthlyEvolutionTransportCO2Units" runat="server"></asp:Label>
                                    </th>
                                    <th nowrap>
                                        <asp:Label ID="lblCO2MonthlyEvolutionTransportCO2KPIMtsUnits" runat="server"></asp:Label>
                                    </th>
                                    <th nowrap>
                                        <asp:Label ID="lblCO2MonthlyEvolutionTransportCO2KPIMoneyUnits" runat="server"></asp:Label>
                                    </th>
                                    <th nowrap>
                                        <asp:Label ID="lblCO2MonthlyEvolutionWasteCO2Units" runat="server"></asp:Label>
                                    </th>
                                    <th nowrap>
                                        <asp:Label ID="lblCO2MonthlyEvolutionWasteCO2KPIMtsUnits" runat="server"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label ID="lblCO2MonthlyEvolutionWasteCO2KPIMoneyUnits" runat="server"></asp:Label>
                                    </th>
                                    <th nowrap>
                                        <asp:Label ID="lblCO2MonthlyEvolutionWaterCO2Units" runat="server"></asp:Label>
                                    </th>
                                    <th nowrap>
                                        <asp:Label ID="lblCO2MonthlyEvolutionWaterCO2KPIMtsUnits" runat="server"></asp:Label>
                                    </th>
                                    <th nowrap>
                                        <asp:Label ID="lblCO2MonthlyEvolutionWaterCO2KPIMoneyUnits" runat="server"></asp:Label>
                                    </th>
                                    <th nowrap>
                                        <asp:Label ID="lblCO2MonthlyEvolutionTotalCO2Units" runat="server"></asp:Label>
                                    </th>
                                    <th nowrap>
                                        <asp:Label ID="lblCO2MonthlyEvolutionTotalCO2KPIMtsUnits" runat="server"></asp:Label>
                                    </th>
                                    <th nowrap class="tdlast">
                                        <asp:Label ID="lblCO2MonthlyEvolutionTotalCO2KPIMoneyUnits" runat="server"></asp:Label>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptCO2MonthlyEvolution" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td nowrap>
                                                <asp:Label ID="lblCO2MonthlyEvolutionMonth" runat="server"></asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="lblCO2MonthlyEvolutionElectricityCO2" runat="server"></asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="lblCO2MonthlyEvolutionElectricityCO2KPIMts" runat="server"></asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="lblCO2MonthlyEvolutionElectricityCO2KPIMoney" runat="server"></asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="lblCO2MonthlyEvolutionFuelsCO2" runat="server"></asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="lblCO2MonthlyEvolutionFuelsCO2KPIMts" runat="server"></asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="lblCO2MonthlyEvolutionFuelsCO2KPIMoney" runat="server"></asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="lblCO2MonthlyEvolutionTransportCO2" runat="server"></asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="lblCO2MonthlyEvolutionTransportCO2KPIMts" runat="server"></asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="lblCO2MonthlyEvolutionTransportCO2KPIMoney" runat="server"></asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="lblCO2MonthlyEvolutionWasteCO2" runat="server"></asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="lblCO2MonthlyEvolutionWasteCO2KPIMts" runat="server"></asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="lblCO2MonthlyEvolutionWasteCO2KPIMoney" runat="server"></asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="lblCO2MonthlyEvolutionWaterCO2" runat="server"></asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="lblCO2MonthlyEvolutionWaterCO2KPIMts" runat="server"></asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="lblCO2MonthlyEvolutionWaterCO2KPIMoney" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblCO2MonthlyEvolutionTotalCO2" runat="server"></asp:Label>
                                            </td>
                                            <td nowrap>
                                                <asp:Label ID="lblCO2MonthlyEvolutionTotalCO2KPIMts" runat="server"></asp:Label>
                                            </td>
                                            <td nowrap class="tdlast">
                                                <asp:Label ID="lblCO2MonthlyEvolutionTotalCO2KPIMoney" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterElectricityCO2" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterElectricityCO2KPIMts" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterElectricityCO2KPIMoney" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterFuelsCO2" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterFuelsCO2KPIMts" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterFuelsCO2KPIMoney" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterTransportCO2" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterTransportCO2KPIMts" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterTransportCO2KPIMoney" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterWasteCO2" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterWasteCO2KPIMts" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterWasteCO2KPIMoney" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterWaterCO2" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterWaterCO2KPIMts" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterWaterCO2KPIMoney" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterTotalCO2" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterTotalCO2KPIMts" runat="server"></asp:Label>
                                    </td>
                                    <td class="tdlast">
                                        <asp:Label ID="lblCO2MonthlyEvolutionFooterTotalCO2KPIMoney" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>
                </div>
                <br /><br />

                <div class="divControlChart">
                    <rad:RadChart ID="radChartCO2Evolution" IntelligentLabelsEnabled="true" runat="server"
                    Width="850" Height="300px" AutoLayout="True"  BorderWidth="0px">
                    <Appearance Border-Width="0" FillStyle-MainColor="#FFFFFF">
                    </Appearance>
                        <Legend Visible="False">
                            <Appearance Visible="False">
                            </Appearance>
                        </Legend>
                        <PlotArea>
                            <Appearance>
                                <Border Width="0" />
                                <FillStyle FillType="Solid" MainColor="#FFFFFF">
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
                </div>
                <div class="divControlChart">
                    <rad:RadChart ID="radChartCO2ByProtocol" IntelligentLabelsEnabled="true" runat="server"
                    Width="850" Height="300px" AutoLayout="True"  BorderWidth="0px">
                    <Appearance Border-Width="0" FillStyle-MainColor="#FFFFFF">
                    </Appearance>
                        <Legend Visible="False">
                            <Appearance Visible="False">
                            </Appearance>
                        </Legend>
                        <PlotArea>
                        <Appearance>
                            <Border Width="0" />
                            <FillStyle FillType="Solid" MainColor="#FFFFFF">
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
                </div>
            </div>
            <!--*********************************************** -->
            <!--*************** Electricity Data ************** -->
            <!--*********************************************** -->
            <div class="divTitle">
                <asp:Label ID="lblElectricityData" runat="server"></asp:Label>
            </div>
            <table class="tbl" id="tblElectricity">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblElectricityTargetsHeaderConsumption" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblElectricityTargetHeaderConsumptionPKIMoney" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblElectricityTargetHeaderConsumptionPKIMts" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblElectricityTargetHeaderCO2" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblElectricityTargetHeaderCO2PKIMoney" runat="server"></asp:Label>
                        </th>
                        <th class="tdlast">
                            <asp:Label ID="lblElectricityTargetHeaderCO2PKIMts" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:Label ID="lblElectricityTargetsConsumption" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblElectricityTargetsConsumptionPKIMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblElectricityTargetsConsumptionPKIMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblElectricityTargetsCO2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblElectricityTargetsCO2PKIMoney" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                            <asp:Label ID="lblElectricityTargetsCO2PKIMts" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table class="tbl" id="tblElectricityEvolution">
                <thead>
                    <tr>
                        <th>
                        </th>
                        <th colspan="6">
                            <asp:Label ID="lblElectricityMonthlyEvolutionHeader" runat="server"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <th>
                            <asp:Label ID="lblElectricityMonthlyEvolutionUnits" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblElectricityMonthlyEvolutionKPIMtsUnits" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblElectricityMonthlyEvolutionKPIMoneyUnits" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblElectricityMonthlyEvolutionCO2Units" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblElectricityMonthlyEvolutionCO2KPIMtsUnits" runat="server"></asp:Label>
                        </th>
                        <th class="tdlast">
                            <asp:Label ID="lblElectricityMonthlyEvolutionCO2KPIMoneyUnits" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptElectricityMonthlyEvolution" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblElectricityMonthlyEvolutionMonth" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblElectricityMonthlyEvolutionUnit" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblElectricityMonthlyEvolutionKPIMts" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblElectricityMonthlyEvolutionKPIMoney" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblElectricityMonthlyEvolutionCO2" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblElectricityMonthlyEvolutionCO2KPIMts" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblElectricityMonthlyEvolutionCO2KPIMoney" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblElectricityMonthlyEvolutionFooterUnit" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblElectricityMonthlyEvolutionFooterKPIMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblElectricityMonthlyEvolutionFooterKPIMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblElectricityMonthlyEvolutionFooterCO2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblElectricityMonthlyEvolutionFooterCO2KPIMts" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                            <asp:Label ID="lblElectricityMonthlyEvolutionFooterCO2KPIMoney" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <br /><br />

            <div class="divControlChart">
                <rad:RadChart ID="radChartElectricityEvolution" IntelligentLabelsEnabled="true" runat="server"
                    Width="850" Height="300px" AutoLayout="True"  BorderWidth="0px">
                    <Appearance Border-Width="0" FillStyle-MainColor="#FFFFFF">
                    </Appearance>
                    <Legend Visible="False">
                        <Appearance Visible="False">
                        </Appearance>
                    </Legend>
                    <PlotArea>
                        <Appearance>
                            <Border Width="0" />
                            <FillStyle FillType="Solid" MainColor="#FFFFFF">
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
            </div>
            <!--<div class="divTitle">
        <asp:Label runat="server" ID="lblElectricityMeters"></asp:Label><br />
    </div>-->
            <br />
            <table class="tbl" id="tblElectricityMeters">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblElectricityMetersHeaderIdentification" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblElectricityMetersHeaderLastDate" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblElectricityMetersHeaderSum" runat="server"></asp:Label>
                        </th>
                        <th class="tdlast">
                            <asp:Label ID="lblElectricityMetersHeaderSumCO2" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptElectricityMeters" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblElectricityMeterIdentification" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblElectricityMeterLastDate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblElectricityMeterTotal" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblElectricityMeterTotalCO2" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="trAlternating">
                                <td>
                                    <asp:Label ID="lblElectricityMeterIdentification" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblElectricityMeterLastDate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblElectricityMeterTotal" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblElectricityMeterTotalCO2" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <br />
            <!--*********************************************** -->
            <!--*************** Fuels Data ******************** -->
            <!--*********************************************** -->
            <div class="divTitle">
                <asp:Label ID="lblFuelsData" runat="server"></asp:Label>
            </div>
            <table class="tbl" id="tblFuels">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblFuelsTargetsHeaderConsumption" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblFuelsTargetHeaderConsumptionPKIMoney" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblFuelsTargetHeaderConsumptionPKIMts" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblFuelsTargetHeaderCO2" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblFuelsTargetHeaderCO2PKIMoney" runat="server"></asp:Label>
                        </th>
                        <th class="tdlast">
                            <asp:Label ID="lblFuelsTargetHeaderCO2PKIMts" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:Label ID="lblFuelsTargetsConsumption" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFuelsTargetsConsumptionPKIMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFuelsTargetsConsumptionPKIMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFuelsTargetsCO2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFuelsTargetsCO2PKIMoney" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                            <asp:Label ID="lblFuelsTargetsCO2PKIMts" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table class="tbl" id="tblFuelsEvolution">
                <thead>
                    <tr>
                        <th>
                        </th>
                        <th colspan="6" class="tdlast">
                            <asp:Label ID="lblFuelsMonthlyEvolutionHeader" runat="server"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <th>
                            <asp:Label ID="lblFuelsMonthlyEvolutionUnits" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblFuelsMonthlyEvolutionKPIMtsUnits" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblFuelsMonthlyEvolutionKPIMoneyUnits" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblFuelsMonthlyEvolutionCO2Units" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblFuelsMonthlyEvolutionCO2KPIMtsUnits" runat="server"></asp:Label>
                        </th>
                        <th class="tdlast">
                            <asp:Label ID="lblFuelsMonthlyEvolutionCO2KPIMoneyUnits" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptFuelsMonthlyEvolution" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFuelsMonthlyEvolutionMonth" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFuelsMonthlyEvolutionUnit" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFuelsMonthlyEvolutionKPIMts" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFuelsMonthlyEvolutionKPIMoney" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFuelsMonthlyEvolutionCO2" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFuelsMonthlyEvolutionCO2KPIMts" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblFuelsMonthlyEvolutionCO2KPIMoney" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblFuelsMonthlyEvolutionFooterUnit" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFuelsMonthlyEvolutionFooterKPIMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFuelsMonthlyEvolutionFooterKPIMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFuelsMonthlyEvolutionFooterCO2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFuelsMonthlyEvolutionFooterCO2KPIMts" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                            <asp:Label ID="lblFuelsMonthlyEvolutionFooterCO2KPIMoney" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <br /><br />

            <div class="divControlChart">
                <rad:RadChart ID="radChartFuelsEvolution" IntelligentLabelsEnabled="true" runat="server"
                    Width="850" Height="300px" AutoLayout="True"  BorderWidth="0px">
                    <Appearance Border-Width="0" FillStyle-MainColor="#FFFFFF">
                    </Appearance>
                    <Legend Visible="False">
                        <Appearance Visible="False">
                        </Appearance>
                    </Legend>
                        <PlotArea>
                        <Appearance>
                            <Border Width="0" />
                            <FillStyle FillType="Solid" MainColor="#FFFFFF">
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
            </div>
            <div class="divControlChart">
                <rad:RadChart ID="RadChartFuelsByTypes" IntelligentLabelsEnabled="true" runat="server"
                    Width="863" Height="300px" AutoLayout="True" >
                    <Appearance Border-Width="0" FillStyle-MainColor="#FFFFFF">
                    </Appearance>
                    <Legend Visible="False">
                        <Appearance Visible="False">
                        </Appearance>
                    </Legend>
                    <PlotArea>
                        <Appearance>
                            <Border Width="0" />
                            <FillStyle FillType="Solid" MainColor="#FFFFFF">
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
            </div>
            <!--<div class="divTitle">
        <asp:Label runat="server" ID="lblFuelsMeters"></asp:Label><br />
    </div>-->
            <br />
            <table class="tbl" id="tblFuelsMeters">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblFuelsMetersHeaderIdentification" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblFuelsMetersHeaderLastDate" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblFuelsMetersHeaderSum" runat="server"></asp:Label>
                        </th>
                        <th class="tdlast">
                            <asp:Label ID="lblFuelsMetersHeaderSumCO2" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptFuelsMeters" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFuelsMeterIdentification" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFuelsMeterLastDate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFuelsMeterTotal" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblFuelsMeterTotalCO2" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="trAlternating">
                                <td>
                                    <asp:Label ID="lblFuelsMeterIdentification" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFuelsMeterLastDate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblFuelsMeterTotal" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblFuelsMeterTotalCO2" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <br />
            <!--*********************************************** -->
            <!--*************** Transport Data **************** -->
            <!--*********************************************** -->
            <div class="divTitle">
                <asp:Label ID="lblTransportData" runat="server"></asp:Label>
            </div>
            <asp:Label ID="lblTransportTargets" runat="server"></asp:Label><br />
            <table class="tbl" id="tblTransport">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblTransportTargetsHeaderConsumption" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblTransportTargetHeaderConsumptionPKIMoney" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblTransportTargetHeaderConsumptionPKIMts" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblTransportTargetHeaderCO2" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblTransportTargetHeaderCO2PKIMoney" runat="server"></asp:Label>
                        </th>
                        <th class="tdlast">
                            <asp:Label ID="lblTransportTargetHeaderCO2PKIMts" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:Label ID="lblTransportTargetsConsumption" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTransportTargetsConsumptionPKIMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTransportTargetsConsumptionPKIMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTransportTargetsCO2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTransportTargetsCO2PKIMoney" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                            <asp:Label ID="lblTransportTargetsCO2PKIMts" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table class="tbl" id="tblTransportEvolution">
                <thead>
                    <tr>
                        <th>
                        </th>
                        <th colspan="6" class="tdlast">
                            <asp:Label ID="lblTransportMonthlyEvolutionHeader" runat="server"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <th>
                            <asp:Label ID="lblTransportMonthlyEvolutionUnits" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblTransportMonthlyEvolutionKPIMtsUnits" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblTransportMonthlyEvolutionKPIMoneyUnits" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblTransportMonthlyEvolutionCO2Units" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblTransportMonthlyEvolutionCO2KPIMtsUnits" runat="server"></asp:Label>
                        </th>
                        <th class="tdlast">
                            <asp:Label ID="lblTransportMonthlyEvolutionCO2KPIMoneyUnits" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptTransportMonthlyEvolution" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTransportMonthlyEvolutionMonth" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTransportMonthlyEvolutionUnit" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTransportMonthlyEvolutionKPIMts" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTransportMonthlyEvolutionKPIMoney" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTransportMonthlyEvolutionCO2" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTransportMonthlyEvolutionCO2KPIMts" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTransportMonthlyEvolutionCO2KPIMoney" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblTransportMonthlyEvolutionFooterUnit" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTransportMonthlyEvolutionFooterKPIMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTransportMonthlyEvolutionFooterKPIMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTransportMonthlyEvolutionFooterCO2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTransportMonthlyEvolutionFooterCO2KPIMts" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                            <asp:Label ID="lblTransportMonthlyEvolutionFooterCO2KPIMoney" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <br /><br />

            <div class="divControlChart">
                <rad:RadChart ID="radChartTransportEvolution" IntelligentLabelsEnabled="true" runat="server"
                    Width="850" Height="300px" AutoLayout="True"  BorderWidth="0px">
                    <Appearance Border-Width="0" FillStyle-MainColor="#FFFFFF">
                    </Appearance>
                    <Legend Visible="False">
                        <Appearance Visible="False">
                        </Appearance>
                    </Legend>
                    <PlotArea>
                        <Appearance>
                            <Border Width="0" />
                            <FillStyle FillType="Solid" MainColor="#FFFFFF">
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
            </div>
            <div class="divControlChart">
                <rad:RadChart ID="RadChartTransportByTypes" IntelligentLabelsEnabled="true" runat="server"
                    Width="850" Height="300px" AutoLayout="True"  BorderWidth="0px">
                    <Appearance Border-Width="0" FillStyle-MainColor="#FFFFFF">
                    </Appearance>
                    <Legend Visible="False">
                        <Appearance Visible="False">
                        </Appearance>
                    </Legend>
                        <PlotArea>
                        <Appearance>
                            <Border Width="0" />
                            <FillStyle FillType="Solid" MainColor="#FFFFFF">
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
            </div>
            <!--<div class="divTitle">
        <asp:Label runat="server" ID="lblTransportMeters"></asp:Label><br />
    </div>-->
            <br />
            <table class="tbl" id="tblTransportMeters">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblTransportMetersHeaderIdentification" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblTransportMetersHeaderLastDate" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblTransportMetersHeaderSum" runat="server"></asp:Label>
                        </th>
                        <th class="tdlast">
                            <asp:Label ID="lblTransportMetersHeaderSumCO2" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptTransportMeters" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTransportMeterIdentification" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTransportMeterLastDate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTransportMeterTotal" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblTransportMeterTotalCO2" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="trAlternating">
                                <td>
                                    <asp:Label ID="lblTransportMeterIdentification" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTransportMeterLastDate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblTransportMeterTotal" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblTransportMeterTotalCO2" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <br />
            <!--*********************************************** -->
            <!--*************** Waste Data ******************** -->
            <!--*********************************************** -->
            <div class="divTitle">
                <asp:Label ID="lblWasteData" runat="server"></asp:Label>
            </div>
            <table class="tbl" id="tblWaste">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblWasteTargetsHeaderConsumption" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWasteTargetHeaderConsumptionPKIMoney" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWasteTargetHeaderConsumptionPKIMts" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWasteTargetHeaderCO2" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWasteTargetHeaderCO2PKIMoney" runat="server"></asp:Label>
                        </th>
                        <th class="tdlast">
                            <asp:Label ID="lblWasteTargetHeaderCO2PKIMts" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:Label ID="lblWasteTargetsConsumption" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWasteTargetsConsumptionPKIMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWasteTargetsConsumptionPKIMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWasteTargetsCO2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWasteTargetsCO2PKIMoney" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                            <asp:Label ID="lblWasteTargetsCO2PKIMts" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table class="tbl" id="tblWasteEvolution">
                <thead>
                    <tr>
                        <th>
                        </th>
                        <th colspan="6" class="lblWasteMonthlyEvolutionHeader">
                            <asp:Label ID="lblWasteMonthlyEvolutionHeader" runat="server"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <th>
                            <asp:Label ID="lblWasteMonthlyEvolutionUnits" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWasteMonthlyEvolutionKPIMtsUnits" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWasteMonthlyEvolutionKPIMoneyUnits" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWasteMonthlyEvolutionCO2Units" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWasteMonthlyEvolutionCO2KPIMtsUnits" runat="server"></asp:Label>
                        </th>
                        <th class="tdlast">
                            <asp:Label ID="lblWasteMonthlyEvolutionCO2KPIMoneyUnits" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptWasteMonthlyEvolution" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblWasteMonthlyEvolutionMonth" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWasteMonthlyEvolutionUnit" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWasteMonthlyEvolutionKPIMts" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWasteMonthlyEvolutionKPIMoney" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWasteMonthlyEvolutionCO2" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWasteMonthlyEvolutionCO2KPIMts" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblWasteMonthlyEvolutionCO2KPIMoney" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblWasteMonthlyEvolutionFooterUnit" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWasteMonthlyEvolutionFooterKPIMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWasteMonthlyEvolutionFooterKPIMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWasteMonthlyEvolutionFooterCO2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWasteMonthlyEvolutionFooterCO2KPIMts" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                            <asp:Label ID="lblWasteMonthlyEvolutionFooterCO2KPIMoney" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <br /><br />

            <div class="divControlChart">
                <rad:RadChart ID="radChartWasteEvolution" IntelligentLabelsEnabled="true" runat="server"
                    Width="850" Height="300px" AutoLayout="True"  BorderWidth="0px">
                    <Appearance Border-Width="0" FillStyle-MainColor="#FFFFFF">
                    </Appearance>
                    <Legend Visible="False">
                        <Appearance Visible="False">
                        </Appearance>
                    </Legend>
                    <PlotArea>
                        <Appearance>
                            <Border Width="0" />
                            <FillStyle FillType="Solid" MainColor="#FFFFFF">
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
            </div>
            <div class="divControlChart">
                <rad:RadChart ID="RadChartWasteByTypes" IntelligentLabelsEnabled="true" runat="server"
                    Width="850" Height="300px" AutoLayout="True"  BorderWidth="0px">
                    <Appearance Border-Width="0" FillStyle-MainColor="#FFFFFF">
                    </Appearance>
                    <Legend Visible="False">
                        <Appearance Visible="False">
                        </Appearance>
                    </Legend>
                    <PlotArea>
                        <Appearance>
                            <Border Width="0" />
                            <FillStyle FillType="Solid" MainColor="#FFFFFF">
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
            </div>
            <!--<div class="divTitle">
        <asp:Label runat="server" ID="lblWasteMeters"></asp:Label><br />
    </div>-->
            <br />
            <table class="tbl" id="tblWasteMeters"> 
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblWasteMetersHeaderIdentification" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWasteMetersHeaderLastDate" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWasteMetersHeaderSum" runat="server"></asp:Label>
                        </th>
                        <th class="tdlast">
                            <asp:Label ID="lblWasteMetersHeaderSumCO2" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptWasteMeters" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblWasteMeterIdentification" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWasteMeterLastDate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWasteMeterTotal" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblWasteMeterTotalCO2" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="trAlternating">
                                <td>
                                    <asp:Label ID="lblWasteMeterIdentification" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWasteMeterLastDate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWasteMeterTotal" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblWasteMeterTotalCO2" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <br />
            <!--*********************************************** -->
            <!--*************** Water Data ******************** -->
            <!--*********************************************** -->
            <div class="divTitle">
                <asp:Label ID="lblWaterData" runat="server"></asp:Label>
            </div>
            <table class="tbl" id="tblWater">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblWaterTargetsHeaderConsumption" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWaterTargetHeaderConsumptionPKIMoney" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWaterTargetHeaderConsumptionPKIMts" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWaterTargetHeaderCO2" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWaterTargetHeaderCO2PKIMoney" runat="server"></asp:Label>
                        </th>
                        <th class="tdlast">
                            <asp:Label ID="lblWaterTargetHeaderCO2PKIMts" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:Label ID="lblWaterTargetsConsumption" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWaterTargetsConsumptionPKIMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWaterTargetsConsumptionPKIMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWaterTargetsCO2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWaterTargetsCO2PKIMoney" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                            <asp:Label ID="lblWaterTargetsCO2PKIMts" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
            <br />
            <table class="tbl" id="tblWaterEvolution">
                <thead>
                    <tr>
                        <th>
                        </th>
                        <th colspan="6" class="tdlast">
                            <asp:Label ID="lblWaterMonthlyEvolutionHeader" runat="server"></asp:Label>
                        </th>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <th>
                            <asp:Label ID="lblWaterMonthlyEvolutionUnits" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWaterMonthlyEvolutionKPIMtsUnits" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWaterMonthlyEvolutionKPIMoneyUnits" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWaterMonthlyEvolutionCO2Units" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWaterMonthlyEvolutionCO2KPIMtsUnits" runat="server"></asp:Label>
                        </th>
                        <th class="tdlast">
                            <asp:Label ID="lblWaterMonthlyEvolutionCO2KPIMoneyUnits" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptWaterMonthlyEvolution" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblWaterMonthlyEvolutionMonth" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWaterMonthlyEvolutionUnit" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWaterMonthlyEvolutionKPIMts" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWaterMonthlyEvolutionKPIMoney" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWaterMonthlyEvolutionCO2" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWaterMonthlyEvolutionCO2KPIMts" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblWaterMonthlyEvolutionCO2KPIMoney" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
                <tfoot>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblWaterMonthlyEvolutionFooterUnit" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWaterMonthlyEvolutionFooterKPIMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWaterMonthlyEvolutionFooterKPIMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWaterMonthlyEvolutionFooterCO2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWaterMonthlyEvolutionFooterCO2KPIMts" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                            <asp:Label ID="lblWaterMonthlyEvolutionFooterCO2KPIMoney" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tfoot>
            </table>
            <br /><br />

            <div class="divControlChart">
                <rad:RadChart ID="radChartWaterEvolution" IntelligentLabelsEnabled="true" runat="server"
                    Width="850" Height="300px" AutoLayout="True"  BorderWidth="0px">
                    <Appearance Border-Width="0" FillStyle-MainColor="#FFFFFF">
                    </Appearance>
                    <Legend Visible="False">
                        <Appearance Visible="False">
                        </Appearance>
                    </Legend>
                    <PlotArea>
                        <Appearance>
                            <Border Width="0" />
                            <FillStyle FillType="Solid" MainColor="#FFFFFF">
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
            </div>
            <!--<div class="divTitle">
        <asp:Label runat="server" ID="lblWaterMeters"></asp:Label><br />
    </div>-->
            <br />
            <table class="tbl" id="tblWaterMeters">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblWaterMetersHeaderIdentification" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWaterMetersHeaderLastDate" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblWaterMetersHeaderSum" runat="server"></asp:Label>
                        </th>
                        <th class="tdlast">
                            <asp:Label ID="lblWaterMetersHeaderSumCO2" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptWaterMeters" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblWaterMeterIdentification" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWaterMeterLastDate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWaterMeterTotal" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblWaterMeterTotalCO2" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="trAlternating">
                                <td>
                                    <asp:Label ID="lblWaterMeterIdentification" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWaterMeterLastDate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblWaterMeterTotal" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblWaterMeterTotalCO2" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>


</asp:Content>
