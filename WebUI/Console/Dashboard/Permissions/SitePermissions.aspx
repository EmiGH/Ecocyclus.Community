<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="SitePermissions.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Permissions.SitePermissions" %>

<asp:Content ContentPlaceHolderID="cphhead" ID="cntHead" runat="server">
    <script type="text/javascript" src="../../../Scripts/mapReferences.js"></script>
</asp:Content>
<asp:Content ID="cntContent" ContentPlaceHolderID="cphContent" runat="server">

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

        //*********************************************************//
        //***************** Table Sort and Paging *****************//
        //*********************************************************//
        $(document).ready(function () {
            $('#tblPermissions').dataTable({
                "bPaginate": true,
                "bLengthChange": true,
                "bFilter": true,
                "bSort": true,
                "bInfo": true,
                "bAutoWidth": true,
                "sPaginationType": "full_numbers",
                "iDisplayLength": 10
            });
            $('#tblGranted').dataTable({
                "bPaginate": true,
                "bLengthChange": true,
                "bFilter": true,
                "bSort": true,
                "bInfo": true,
                "bAutoWidth": true,
                "sPaginationType": "full_numbers",
                "iDisplayLength": 10
            });
            $('#tblAvailable').dataTable({
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
    <div class="divLineSeparatorHeader Permission">
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
    <!--************** Managers Permissions **************-->
    <!--**************************************************-->
    <div id="divPermissionsManagers">
        <div class="divTitle">
            <asp:Label ID="lblPermissionsManagers" runat="server"></asp:Label>
        </div>
        <table class="tbl" id="tblPermissions">
            <thead>
                <tr>
                    <th>
                        <asp:Label ID="lblPermissionsManagersHeaderUser" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblPermissionsManagersHeaderEmail" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblPermissionsManagersHeaderIsManager" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblPermissionsManagersHeaderIsOperator" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblPermissionsManagersHeaderIsReader" runat="server"></asp:Label>
                    </th>
                    <th class="tdlast">
                        <asp:Label ID="lblPermissionsManagersHeaderNoAccess" runat="server"></asp:Label>
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptPermissionsManagers" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblPermissionsManagersUser" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPermissionsManagersEmail" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsManagersIsManager" runat="server" />
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsManagersIsOperator" runat="server" />
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsManagersIsReader" runat="server" />
                            </td>
                            <td class="tdlast">
                                <asp:RadioButton ID="rdPermissionsManagersNoAccess" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="trAlternating">
                            <td>
                                <asp:Label ID="lblPermissionsManagersUser" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPermissionsManagersEmail" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsManagersIsManager" runat="server" />
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsManagersIsOperator" runat="server" />
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsManagersIsReader" runat="server" />
                            </td>
                            <td class="tdlast">
                                <asp:RadioButton ID="rdPermissionsManagersNoAccess" runat="server" />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <br /><br />

    <!--**************************************************-->
    <!--*************** Granted Permissions **************-->
    <!--**************************************************-->
    <div id="divPermissionsGranted">
        <div class="divTitle">
            <asp:Label ID="lblPermissionsGranted" runat="server"></asp:Label>
        </div>
        <table class="tbl" id="tblGranted">
            <thead>
                <tr>
                    <th>
                        <asp:Label ID="lblPermissionsGrantedHeaderUser" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblPermissionsGrantedHeaderEmail" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblPermissionsGrantedHeaderIsManager" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblPermissionsGrantedHeaderIsOperator" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblPermissionsGrantedHeaderIsReader" runat="server"></asp:Label>
                    </th>
                    <th class="tdlast">
                        <asp:Label ID="lblPermissionsGrantedHeaderNoAccess" runat="server"></asp:Label>
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptPermissionsGranted" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdnPermissionsGrantedIdOperator" runat="server" />
                                <asp:Label ID="lblPermissionsGrantedUser" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPermissionsGrantedEmail" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsGrantedIsManager" runat="server" />
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsGrantedIsOperator" runat="server" />
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsGrantedIsReader" runat="server" />
                            </td>
                            <td class="tdlast">
                                <asp:RadioButton ID="rdPermissionsGrantedNoAccess" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="trAlternating">
                            <td>
                                <asp:HiddenField ID="hdnPermissionsGrantedIdOperator" runat="server" />
                                <asp:Label ID="lblPermissionsGrantedUser" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPermissionsGrantedEmail" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsGrantedIsManager" runat="server" />
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsGrantedIsOperator" runat="server" />
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsGrantedIsReader" runat="server" />
                            </td>
                            <td class="tdlast">
                                <asp:RadioButton ID="rdPermissionsGrantedNoAccess" runat="server" />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <br /><br />

    <!--**************************************************-->
    <!--************* Available Permissions **************-->
    <!--**************************************************-->
    <div id="divPermissionsAvailable">
        <div class="divTitle">
            <asp:Label ID="lblPermissionsAvailable" runat="server"></asp:Label>
        </div>
        <table class="tbl" id="tblAvailable">
            <thead>
                <tr>
                    <th>
                        <asp:Label ID="lblPermissionsAvailableHeaderUser" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblPermissionsAvailableHeaderEmail" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblPermissionsAvailableHeaderIsManager" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblPermissionsAvailableHeaderIsOperator" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblPermissionsAvailableHeaderIsReader" runat="server"></asp:Label>
                    </th>
                    <th class="tdlast">
                        <asp:Label ID="lblPermissionsAvailableHeaderNoAccess" runat="server"></asp:Label>
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptPermissionsAvailable" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdnPermissionsAvailableIdOperator" runat="server" />
                                <asp:Label ID="lblPermissionsAvailableUser" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPermissionsAvailableEmail" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsAvailableIsManager" runat="server" />
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsAvailableIsOperator" runat="server" />
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsAvailableIsReader" runat="server" />
                            </td>
                            <td class="tdlast">
                                <asp:RadioButton ID="rdPermissionsAvailableNoAccess" runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="trAlternating">
                            <td>
                                <asp:HiddenField ID="hdnPermissionsAvailableIdOperator" runat="server" />
                                <asp:Label ID="lblPermissionsAvailableUser" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPermissionsAvailableEmail" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsAvailableIsManager" runat="server" />
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsAvailableIsOperator" runat="server" />
                            </td>
                            <td>
                                <asp:RadioButton ID="rdPermissionsAvailableIsReader" runat="server" />
                            </td>
                            <td class="tdlast">
                                <asp:RadioButton ID="rdPermissionsAvailableNoAccess" runat="server" />
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    </div>
    <br /><br />

    <!--**************************************************************-->
    <!--********************* Save ***********************************-->
    <!--**************************************************************-->
    <div class="divContentSave">
        <asp:Button ID="btnSave" runat="server" UseSubmitBehavior="false"  CssClass="btnActions btnSave"/>
    </div>

</asp:Content>
