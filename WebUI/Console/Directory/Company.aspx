<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="Company.aspx.cs" Inherits="CSI.WebUI.Console.Directory.Company" %>

<asp:Content ContentPlaceHolderID="cphhead" runat="server" ID="cntHead">
    <script type="text/javascript" src="../../Scripts/confirm.js"></script>
    <script type="text/javascript" src="../../Scripts/mapReferences.js"></script>
    <script type="text/javascript" src="../../Scripts/jquery.datatables.js"></script>
</asp:Content>

<asp:Content ID="cntCompany" ContentPlaceHolderID="cphContent" runat="server">
    
    <script type="text/javascript">

        //*********************************************************//
        //***************** Table Sort and Paging *****************//
        //*********************************************************//
        $(document).ready(function () {
            $('#tblUsers').dataTable({
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

        //********************************************************//
        //***************** Map Functions  ***********************//
        //********************************************************//
        $(document).ready(loadMap());

        function loadMap() {
            var _script = document.createElement("script");
            _script.type = "text/javascript";
            _script.src = "http://maps.googleapis.com/maps/api/js?key=AIzaSyDzj5Mp1wUYuTzITA9F_Vw0Yjaid30o6dg&sensor=false&callback=mapsLoaded&libraries=places";
            document.body.appendChild(_script);

        }

    </script>
    


    <div class="divLineSeparatorHeader Dashboard"></div>

    <div class="divDetail">

        <!--*********************************************** -->
        <!--************* Company Properties ************** -->
        <!--*********************************************** -->
        <div class="divTitle">
            <asp:Label ID="lblSiteProperties" runat="server"></asp:Label>
            <div class="divActions">
                <asp:LinkButton ID="lnkEditCompany" runat="server" PostBackUrl="~/Console/Directory/CompanyEdit.aspx"
                    CssClass="lnkModify"></asp:LinkButton>
            </div>
        </div>

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
        <div id="divContact">
            <div class="divColumn column1-2 last">
                <asp:Label ID="lblName" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblNameValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <asp:Label ID="lblEmail" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblEmailValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3 last">
                <asp:Label ID="lblUrl" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblUrlValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblTelephone" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:Label ID="lblTelephoneValue" runat="server" CssClass="lblValue"></asp:Label>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblFacebook" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:Label ID="lblFacebookValue" runat="server" CssClass="lblValue"></asp:Label>
        </div>
        <div class="divColumn column3 last">
            <asp:Label ID="lblTwitter" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:Label ID="lblTwitterValue" runat="server" CssClass="lblValue"></asp:Label>
        </div>
        <div class="divColumn column1 margin">
            <asp:Label runat="server" ID="lblAddress" CssClass="lblTitle"></asp:Label>
            <asp:Label ID="lblAddressValue" runat="server" CssClass="lblValue"></asp:Label>
        </div>

        <div class="clear"></div>

        <div class="divTitle">
            <asp:Label ID="lblLocation" runat="server"></asp:Label>
        </div>

        <!-- ********************************-->
        <!-- ************* Map **************-->
        <!-- ********************************-->
        <div id="divMap">
            <input type="hidden" id="hdnMapPoint" name="hdnMapPoint" value="<%=Location%>" />
            <div id="divMapCanvas" class="divMapCanvas divMapDetail divLarge">
            </div>
        </div>

    </div>
    <!--*********************************************** -->
    <!--***************** Users *********************** -->
    <!--*********************************************** -->
    <div id="divUsers" runat="server">
        <div class="divTitle">
            <asp:Label ID="lblUsers" runat="server"></asp:Label>
        </div>
        <table class="tbl" id="tblUsers">
            <thead>
                <tr>
                    <th>
                    </th>
                    <th>
                        <asp:Label ID="lblHeaderFullname" runat="server"></asp:Label>
                    </th>
                    <th>
                        <asp:Label ID="lblHeaderEmail" runat="server"></asp:Label>
                    </th>
                    <th class="thlast">
                        <asp:Label ID="lblHeaderIsManager" runat="server"></asp:Label>
                    </th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="rptUsers" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Button ID="btnUserView" runat="server" CommandName="View" CssClass="btnSitesView" />
                                <asp:Button ID="btnUserEdit" runat="server" CommandName="Edit" CssClass="btnSitesEdit" />
                                <asp:Button ID="btnUserDelete" runat="server" CommandName="Delete" CssClass="btnSitesDelete" />
                            </td>
                            <td>
                                <asp:Label ID="lblFullname" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                            </td>
                            <td class="tdlast">
                                <asp:Label ID="lblIsManager" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr class="trAlternating">
                            <td>
                                <asp:Button ID="btnUserView" runat="server" CommandName="View" CssClass="btnSitesView" />
                                <asp:Button ID="btnUserEdit" runat="server" CommandName="Edit" CssClass="btnSitesEdit" />
                                <asp:Button ID="btnUserDelete" runat="server" CommandName="Delete" CssClass="btnSitesDelete" />
                            </td>
                            <td>
                                <asp:Label ID="lblFullname" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                            </td>
                            <td class="tdlast">
                                <asp:Label ID="lblIsManager" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>

</asp:Content>
