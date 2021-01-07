<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="User.aspx.cs" Inherits="CSI.WebUI.Console.Directory.User" %>

<asp:Content ContentPlaceHolderID="cphhead" runat="server" ID="cntHead">
    <script type="text/javascript" src="../../Scripts/jquery.datatables.js"></script>
</asp:Content>

<asp:Content ID="cntUser" ContentPlaceHolderID="cphContent" runat="server">

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
         });
    
    </script>
    <div class="divLineSeparatorHeader Dashboard">
    </div>
    <div class="divDetail">
        <div class="divTitle">
            <asp:Label ID="lblSiteProperties" runat="server"></asp:Label>
            <div class="divActions">
                <asp:LinkButton ID="lnkEditUser" runat="server" CssClass="lnkModify"></asp:LinkButton>
                <asp:LinkButton ID="lnkPermissions" runat="server" CssClass="lnkPermissions"></asp:LinkButton>
            </div>
        </div>
        <!--*********************************************** -->
        <!--*************** User Properties *************** -->
        <!--*********************************************** -->
        <!-- Image -->
        <div id="divImage" class="divColumn column3 img">
            <div class="divCenterLogo">
                <p>
                    <span>
                        <img id="imgPicture" runat="server" class="logo" />
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
        <div class="divColumn column3">
            <asp:Label ID="lblIsActive" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:Label ID="lblIsActiveValue" runat="server" CssClass="lblValue"></asp:Label>
        </div>
        <!--*********************************************** -->
        <!--************** User Permissions *************** -->
        <!--*********************************************** -->
        <div id="divPermissions" runat="server" style="display: none">
            <div class="divTitle">
                <asp:Label ID="lblPermissions" runat="server"></asp:Label>
            </div>
            <table class="tbl" id="tblPermissions">
                <thead>
                    <tr>
                        <th>
                            <asp:Label ID="lblPermissionsSite" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblPermissionsDate" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblPermissionsManage" runat="server"></asp:Label>
                        </th>
                        <th>
                            <asp:Label ID="lblPermissionsLoad" runat="server"></asp:Label>
                        </th>
                        <th class="thlast">
                            <asp:Label ID="lblPermissionsView" runat="server"></asp:Label>
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="rptPermissions" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPermissionSite" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPermissionDate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPermissionManage" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPermissionLoad" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblPermissionView" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr class="trAlternating">
                                <td>
                                    <asp:Label ID="lblPermissionSite" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPermissionDate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPermissionManage" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPermissionLoad" runat="server"></asp:Label>
                                </td>
                                <td class="tdlast">
                                    <asp:Label ID="lblPermissionView" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
