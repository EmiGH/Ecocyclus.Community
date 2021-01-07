<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="UserPermissions.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.Permissions.UserPermissions" %>

<asp:Content ID="cntContent" ContentPlaceHolderID="cphContent" runat="server">
    <script type="text/javascript">

        //*********************************************************//
        //***************** Table Sort and Paging *****************//
        //*********************************************************//
        $(document).ready(function () {
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
    <div class="divLineSeparatorHeader Dashboard">
    </div>
    <div class="divDetail">
        <div class="divTitle">
            <asp:Label ID="lblSiteProperties" runat="server"></asp:Label>
        </div>
        <!--*********************************************** -->
        <!--*************** User Properties *************** -->
        <!--*********************************************** -->
        <!-- Image -->
        <div id="divImage" class="divColumn column3 img">
            <div class="divCenterLogo">
                <p>
                    <span>
                        <img id="imgLogo" runat="server" class="logo" />
                    </span>
                </p>
            </div>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblDate" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:Label ID="lblDateValue" runat="server" CssClass="lblValue"></asp:Label>
        </div>
        <div class="divColumn column3 last">
            <asp:Label ID="lblName" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:Label ID="lblNameValue" runat="server" CssClass="lblValue"></asp:Label>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblEmail" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:Label ID="lblEmailValue" runat="server" CssClass="lblValue"></asp:Label>
        </div>
        <div class="divColumn column3 last">
            <asp:Label ID="lblIsManager" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:Label ID="lblIsManagerValue" runat="server" CssClass="lblValue"></asp:Label>
        </div>
        <div class="clear">
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
                            <asp:Label ID="lblPermissionsGrantedHeaderSite" runat="server"></asp:Label>
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
                        <th class="thlast">
                            <asp:Label ID="lblPermissionsGrantedHeaderNoAccess" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptPermissionsGranted" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hdnPermissionsGrantedIdSite" runat="server" />
                                    <asp:Label ID="lblPermissionsGrantedSite" runat="server"></asp:Label>
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
                                    <asp:HiddenField ID="hdnPermissionsGrantedIdSite" runat="server" />
                                    <asp:Label ID="lblPermissionsGrantedSite" runat="server"></asp:Label>
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
                            <asp:Label ID="lblPermissionsAvailableHeaderSite" runat="server"></asp:Label>
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
                        <th class="thlast">
                            <asp:Label ID="lblPermissionsAvailableHeaderNoAccess" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptPermissionsAvailable" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="hdnPermissionsAvailableIdSite" runat="server" />
                                    <asp:Label ID="lblPermissionsAvailableSite" runat="server"></asp:Label>
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
                                    <asp:HiddenField ID="hdnPermissionsAvailableIdSite" runat="server" />
                                    <asp:Label ID="lblPermissionsAvailableSite" runat="server"></asp:Label>
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
        <asp:Button ID="btnSave" runat="server" CssClass="btnActions btnSave" UseSubmitBehavior="false" />
    </div>

</asp:Content>
