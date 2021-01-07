<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="SiteEmissionFactors.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.EmissionFactors.SiteEmissionFactors" %>

<asp:Content ContentPlaceHolderID="cphhead" ID="cntHead" runat="server">
    <script type="text/javascript" src="../../../Scripts/mapReferences.js"></script>
</asp:Content>
<asp:Content ID="cntContent" ContentPlaceHolderID="cphContent" runat="server">

    <script type="text/javascript">

        //*********************************************************//
        //***************** Table Sort and Paging *****************//
        //*********************************************************//
        $(document).ready(function () {

            $('#tblElectricity').dataTable({
                "bPaginate": false,
                "bFilter": false,
                "bSort": false,
                "bInfo": false,
                "bAutoWidth": true
            });

            $('#tblFuels').dataTable({
               "bPaginate": true,
                "bLengthChange": true,
                "bFilter": true,
                "bSort": true,
                "bInfo": true,
                "bAutoWidth": true,
                "sPaginationType": "full_numbers",
                "iDisplayLength": 10
            });
            $('#tblTransport').dataTable({
                "bPaginate": true,
                "bLengthChange": true,
                "bFilter": true,
                "bSort": true,
                "bInfo": true,
                "bAutoWidth": true,
                "sPaginationType": "full_numbers",
                "iDisplayLength": 10
            });
            $('#tblWaste').dataTable({
                "bPaginate": true,
                "bLengthChange": true,
                "bFilter": true,
                "bSort": true,
                "bInfo": true,
                "bAutoWidth": true,
                "sPaginationType": "full_numbers",
                "iDisplayLength": 10
            });
            $('#tblWater').dataTable({
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
   
    <div class="divLineSeparatorHeader C02">
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

            <div class="divColumn column3">
                <asp:Label ID="lblTitle" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblTitleValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3 last">
                <asp:Label ID="lblStart" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblStartValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <asp:Label ID="lblWeeks" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblWeeksValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3 last">
                <asp:Label ID="lblNumber" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblNumberValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <asp:Label ID="lblValue" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblValueValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <asp:Label ID="lblFloorSpace" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblFloorSpaceValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3 last">
                <asp:Label ID="lblUnits" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblUnitsValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column1 margin">
                <asp:Label ID="lblLocation" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblLocationValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
        </div>
        <div class="clear">
        </div>
        <br /><br />

        <!--*********************************************** -->
        <!--*************** EF Specs ********************** -->
        <!--*********************************************** -->
        <div id="divEFData">
            <!-- Electricity -->
            <div class="divTitle">
                <asp:Label ID="lblEFElectricity" runat="server"></asp:Label>
            </div>
            <table id="tblElectricity" class="tbl">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblEFElectricityHeaderValue" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblEFElectricityHeaderUnit" runat="server"></asp:Label>
                        </th>
                        <th class="thlast">
                            <asp:Label ID="lblEFElectricityHeaderDescription" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:Label ID="lblEFElectricityValue" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblEFElectricityUnit" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                            <asp:Label ID="lblEFElectricityDescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
            <!-- Fuels -->
            <div class="divTitle">
                <asp:Label ID="lblEFFuels" runat="server"></asp:Label>
            </div>
            <table id="tblFuels" class="tbl">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblEFFuelsHeaderName" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblEFFuelsHeaderValue" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblEFFuelsHeaderUnit" runat="server"></asp:Label>
                        </th>
                        <th class="thlast">
                            <asp:Label ID="lblEFFuelsHeaderDescription" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptEFFuels" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEFFuelsTypeName" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEFFuelsValue" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEFFuelsUnit" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblEFFuelsDescription" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>

                    </asp:Repeater>
                </tbody>
            </table>
            <!-- Transport -->
            <div class="divTitle">
                <asp:Label ID="lblEFTransport" runat="server"></asp:Label>
            </div>
            <table id="tblTransport" class="tbl">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblEFTransportHeaderName" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblEFTransportHeaderValue" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblEFTransportHeaderUnit" runat="server"></asp:Label>
                        </th>
                        <th class="thlast">
                            <asp:Label ID="lblEFTransportHeaderDescription" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptEFTransport" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEFTransportTypeName" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEFTransportValue" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEFTransportUnit" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblEFTransportDescription" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="trAlternating">
                                <td>
                                    <asp:Label ID="lblEFTransportTypeName" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEFTransportValue" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEFTransportUnit" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblEFTransportDescription" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <!-- Waste -->
            <div class="divTitle">
                <asp:Label ID="lblEFWaste" runat="server"></asp:Label>
            </div>
            <table id="tblWaste" class="tbl">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblEFWasteHeaderName" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblEFWasteHeaderValue" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblEFWasteHeaderUnit" runat="server"></asp:Label>
                        </th>
                        <th class="thlast">
                            <asp:Label ID="lblEFWasteHeaderDescription" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptEFWaste" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblEFWasteTypeName" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEFWasteValue" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEFWasteUnit" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblEFWasteDescription" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="trAlternating">
                                <td>
                                    <asp:Label ID="lblEFWasteTypeName" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEFWasteValue" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblEFWasteUnit" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblEFWasteDescription" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
            <!-- Water -->
            <div class="divTitle">
                <asp:Label ID="lblEFWater" runat="server"></asp:Label>
            </div>
            <table id="tblWater" class="tbl">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblEFWaterHeaderValue" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblEFWaterHeaderUnit" runat="server"></asp:Label>
                        </th>
                        <th class="thlast">
                            <asp:Label ID="lblEFWaterHeaderDescription" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <asp:Label ID="lblEFWaterValue" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblEFWaterUnit" runat="server"></asp:Label>
                        </td>
                        <td class="tdlast">
                            <asp:Label ID="lblEFWaterDescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
