<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="SiteTargets.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.SiteTargets" %>
<%@ Register tagprefix="uc" tagname="MenuNavigation" src="~/Console/Controls/ucMenuNavigation.ascx" %>

<asp:Content ContentPlaceHolderID="cphhead" ID="cntHead" runat="server">
    <script type="text/javascript" src="../../../Scripts/mapReferences.js"></script>
</asp:Content>

<asp:Content ID="cntContent" ContentPlaceHolderID="cphContent" runat="server">
    
    <script type="text/javascript">

        //*********************************************************//
        //***************** Table Sort and Paging *****************//
        //*********************************************************//
        $(document).ready(function () {
            $('#tblTargets').dataTable({
                "bPaginate": false,
                "bFilter": false,
                "bSort": false,
                "bInfo": false,
                "bAutoWidth": true,
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

    <div class="divLineSeparatorHeader Target">
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

            <div class="divColumn column1p last">
                <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblTitleValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column1p last">
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
            <div class="divColumn column3">
                <asp:Label ID="lblWeeks" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblWeeksValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
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
        <div class="clear">
        </div>
        <br /><br />

        <!--**************************************************-->
        <!--*************** Site Targets *********************-->
        <!--**************************************************-->
        <div class="divActions">
            <asp:LinkButton ID="lnkEdit" runat="server" CssClass="lnkModify"></asp:LinkButton>
        </div>
        <div id="divTargets">
            <table class="tbl" id="tblTargets">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblHeaderTarget" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblHeaderElectricity" runat="server" CssClass="lblHeaderElectricity"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblHeaderFuel" runat="server" CssClass="lblHeaderFuel"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblHeaderTransport" runat="server" CssClass="lblHeaderTransport"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblHeaderWaste" runat="server" CssClass="lblHeaderWaste"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblHeaderWater" runat="server" CssClass="lblHeaderWater"></asp:Label>
                        </th>
                        <th class="tdlast">
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:Label ID="lblConsumption" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblElectricityConsumption" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFuelConsumption" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTransportConsumption" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWasteConsumption" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWaterConsumption" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                        </td>
                    </tr>
                    <tr class="trAlternating">
                        <td>
                            <asp:Label ID="lblConsumptionByMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblElectricityConsumptionByMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFuelConsumptionByMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTransportConsumptionByMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWasteConsumptionByMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWaterConsumptionByMoney" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblConsumptionByMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblElectricityConsumptionByMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFuelConsumptionByMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTransportConsumptionByMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWasteConsumptionByMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWaterConsumptionByMts" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                        </td>
                    </tr>
                    <tr class="trAlternating">
                        <td>
                            <asp:Label ID="lblCO2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblElectricityCO2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFuelCO2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTransportCO2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWasteCO2" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWaterCO2" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                            <asp:Label ID="lblTotalCO2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCO2ByMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblElectricityCO2ByMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFuelCO2ByMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTransportCO2ByMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWasteCO2ByMoney" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWaterCO2ByMoney" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                            <asp:Label ID="lblTotalCO2ByMoney" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="trAlternating">
                        <td>
                            <asp:Label ID="lblCO2ByMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblElectricityCO2ByMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblFuelCO2ByMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTransportCO2ByMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWasteCO2ByMts" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblWaterCO2ByMts" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                            <asp:Label ID="lblTotalCO2ByMts" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
