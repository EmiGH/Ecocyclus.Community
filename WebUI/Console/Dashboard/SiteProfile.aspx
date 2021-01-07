<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="SiteProfile.aspx.cs" Inherits="CSI.WebUI.Console.Dashboard.SiteProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>

<asp:Content ContentPlaceHolderID="cphhead" ID="cntHead" runat="server">
    <script type="text/javascript" src="../../Scripts/mapReferences.js"></script>
</asp:Content>
<asp:Content ID="cntSite" ContentPlaceHolderID="cphContent" runat="server">

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

    <div class="divDetail">
        
        <div class="divLineSeparatorHeader Sites">
        </div>
        <!--**************************************************-->
        <!--*************** Site Properties ******************-->
        <!--**************************************************-->
        <div class="divTitle">
            <asp:Label ID="lblSiteProperties" runat="server"></asp:Label>
            <div class="divActions">
                <asp:LinkButton ID="lnkToggleSite" runat="server" CssClass="lnkToggle"></asp:LinkButton>
                <asp:LinkButton ID="lnkModifySite" runat="server" CssClass="lnkModify"></asp:LinkButton>
                <asp:LinkButton ID="lnkRemoveSite" runat="server" CssClass="lnkRemove"></asp:LinkButton>
            </div>
        </div>
        <!--**************************************************-->
        <!--*************** Site Actions *********************-->
        <!--**************************************************-->
        <div id="divProperties" runat="server">
            <div id="divImage" class="divColumn column3 img">
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
            <div class="divColumn column3 margin  last">
                <asp:Label ID="lblValue" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblValueValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3 margin">
                <asp:Label ID="lblFloorSpace" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblFloorSpaceValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3 last margin">
                <asp:Label ID="lblUnits" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblUnitsValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="clear">
            </div>
            <div class="divTitle">
                <asp:Label ID="lblSiteClient" runat="server"></asp:Label>
            </div>
            <div class="divColumn column2">
                <asp:Label ID="lblClient" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblClientValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column2 last">
                <asp:Label ID="lblAgent" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblAgentValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column2">
                <asp:Label ID="lblContractor" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblContractorValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column2 last">
                <asp:Label ID="lblResponsible" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblResponsibleValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <asp:Label ID="lblManager" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblManagerValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <!-- Contact -->
                <asp:Label ID="lblTelephone" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblTelephoneValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3 last">
                <asp:Label ID="lblEmail" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblEmailValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
            <div class="divColumn column3">
                <asp:Label ID="lblUrl" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblUrlValue" runat="server" CssClass="lblValue"></asp:Label>
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
                <asp:Label ID="lblDescription" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblDescriptionValue" runat="server" CssClass="lblValue"></asp:Label>
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="divTitle">
            <asp:Label ID="lblMap" runat="server"></asp:Label>
        </div>
        <div class="divColumn column1">
            <asp:Label ID="lblLocation" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:Label ID="lblLocationValue" runat="server" CssClass="lblValue"></asp:Label>
        </div>
        <div class="clear">
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

    
</asp:Content>
