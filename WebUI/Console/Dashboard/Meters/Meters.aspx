<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="Meters.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Meters.Meters" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>

<asp:Content ID="cnhHead" ContentPlaceHolderID="cphhead" runat="server">
    <link href="../../../Styles/Meters.css" rel="Stylesheet" />
    <script type="text/javascript" src="../../../Scripts/mapReferences.js"></script>
</asp:Content>

<asp:Content ID="cntSite" ContentPlaceHolderID="cphContent" runat="server">
    
    
    <script type="text/javascript">

        //*********************************************************//
        //***************** Table Sort and Paging *****************//
        //*********************************************************//
        $(document).ready(function () {
            $('#tblMeters').dataTable({
                "bPaginate": true,
                "bLengthChange": true,
                "bFilter": true,
                "bSort": true,
                "bInfo": true,
                "bAutoWidth": true,
                "sPaginationType": "full_numbers",
                "iDisplayLength": 10
            });
        });
    
    </script>
    <script type="text/javascript">

        //*********************************************************//
        //***************** Load Functions  ***********************//
        //*********************************************************//
        $(document).ready(loadMap());

        function loadMap() {
            var _script = document.createElement("script");
            _script.type = "text/javascript";
            _script.src = "http://maps.googleapis.com/maps/api/js?key=AIzaSyDzj5Mp1wUYuTzITA9F_Vw0Yjaid30o6dg&sensor=false&callback=mapsLoaded&libraries=places";
            document.body.appendChild(_script);

        }

    </script>

    <div class="divLineSeparatorHeader Meter">
    </div>
    <div class="divDetail">

        <!--**************************************************-->
        <!--*************** Site Properties ******************-->
        <!--**************************************************-->
        <div class="divTitle">
            <asp:Label ID="lblSiteProperties" runat="server"></asp:Label>
        </div>

        <div id="divProperties" runat="server">

            <!--****************************************** -->
            <!--*************** Map ********************** -->
            <!--****************************************** -->
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

        <asp:UpdatePanel ID="upMeters" runat="server" UpdateMode="Always">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkMeterTypeElectricity" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="lnkMeterTypeFuels" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="lnkMeterTypeTransport" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="lnkMeterTypeWaste" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="lnkMeterTypeWater" EventName="Click" />
            </Triggers>
            <ContentTemplate>
        <div class="divTitle">
            <asp:Label ID="lblMeters" runat="server"></asp:Label>
            <asp:LinkButton ID="btnMeterAdd" runat="server" CssClass="btnMeterAdd"></asp:LinkButton>
        </div>

        <!--**************************************************-->
        <!--*************** Site Meters **********************-->
        <!--**************************************************-->

                <div id="divMetersControl">
                    <div id="divMeterTypes">
                        <asp:LinkButton ID="lnkMeterTypeElectricity" runat="server" CssClass="active">
                            <asp:ImageButton ID="imgMeterTypeElectricity" runat="server" Style="display: none;" />
                            <asp:Label ID="lblMeterTypeElectricity" runat="server" Style="display: none;" />
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkMeterTypeFuels" runat="server">
                            <asp:ImageButton ID="imgMeterTypeFuels" runat="server" Style="display: none;" />
                            <asp:Label ID="lblMeterTypeFuels" runat="server" Style="display: none;" />
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkMeterTypeTransport" runat="server">
                            <asp:ImageButton ID="imgMeterTypeTransport" runat="server" Style="display: none;" />
                            <asp:Label ID="lblMeterTypeTransport" runat="server" Style="display: none;" />
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkMeterTypeWaste" runat="server">
                            <asp:ImageButton ID="imgMeterTypeWaste" runat="server" Style="display: none;" />
                            <asp:Label ID="lblMeterTypeWaste" runat="server" Style="display: none;" />
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkMeterTypeWater" runat="server">
                            <asp:ImageButton ID="imgMeterTypeWater" runat="server" Style="display: none;" />
                            <asp:Label ID="lblMeterTypeWater" runat="server" Style="display: none;" />
                        </asp:LinkButton>
                    </div>
                    <div id="divMeters">
                        <table class="tbl" id="tblMeters">
                            <thead>
                                <tr>
                                    <th>
                                    </th>
                                    <th>
                                    </th>
                                    <th>
                                        <asp:Label ID="lblMetersHeaderIdentification" runat="server"></asp:Label>
                                    </th>
                                    <th>
                                        <asp:Label ID="lblMetersHeaderLastDate" runat="server"></asp:Label>
                                    </th>
                                    <th colspan="2">
                                        <asp:Label ID="lblMetersHeaderSum" runat="server"></asp:Label>
                                    </th>
                                    <th colspan="2" class="thlast">
                                        <asp:Label ID="lblMetersHeaderSumCO2" runat="server"></asp:Label>
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                                <asp:Repeater ID="rptMeters" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnMeterView" CommandName="View" runat="server" CssClass="btnSitesView" />
                                                <asp:Button ID="btnMeterEdit" CommandName="Edit" runat="server" CssClass="btnSitesEdit" />
                                                <asp:Button ID="btnMeterDelete" CommandName="Delete" runat="server" CssClass="btnSitesDelete" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnMeterAlert" CssClass="btnSitesAlert" CommandName="Alert" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMeterIdentification" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMeterLastDate" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMeterTotal" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMeterTotalUnit" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMeterTotalCO2" runat="server"></asp:Label>
                                            </td>
                                            <td class="thlast">
                                                <asp:Label ID="lblMeterTotalCO2Unit" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="trAlternating">
                                            <td>
                                                <asp:Button ID="btnMeterView" CommandName="View" runat="server" CssClass="btnSitesView" />
                                                <asp:Button ID="btnMeterEdit" CommandName="Edit" runat="server" CssClass="btnSitesEdit" />
                                                <asp:Button ID="btnMeterDelete" CommandName="Delete" runat="server" CssClass="btnSitesDelete" />
                                            </td>
                                            <td>
                                                <asp:Button ID="btnMeterAlert" CssClass="btnSitesAlert" CommandName="Alert" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMeterIdentification" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMeterLastDate" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMeterTotal" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMeterTotalUnit" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMeterTotalCO2" runat="server"></asp:Label>
                                            </td>
                                            <td class="thlast">
                                                <asp:Label ID="lblMeterTotalCO2Unit" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                       
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>       

    </div>
    
</asp:Content>
