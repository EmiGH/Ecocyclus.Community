<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="CompanyEdit.aspx.cs" Inherits="CSI.WebUI.Console.Directory.CompanyEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="rad" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ContentPlaceHolderID="cphhead" ID="cntHead" runat="server">
    <script type="text/javascript" src="../../Scripts/map.js"></script>
    <script type="text/javascript" src="../../Scripts/helper.js"></script>
</asp:Content>

<asp:Content ID="cntCompanyEdit" ContentPlaceHolderID="cphContent" runat="server">

    <script type="text/javascript">

        $(document).ready(function () {

            //Init values for fields with hidden values on postbacks
            if ($("#<%=hdnMapLocationPoint.ClientID%>").val() != "") {
                $('#txtMapLocator').val($("#<%=hdnMapLocationAddress.ClientID%>").val());
                $('#hdnMapPoint').val($("#<%=hdnMapLocationPoint.ClientID%>").val());
            }

            loadMap();
        });

        function loadMap() {
            var _script = document.createElement("script");
            _script.type = "text/javascript";
            _script.src = "http://maps.googleapis.com/maps/api/js?key=AIzaSyDzj5Mp1wUYuTzITA9F_Vw0Yjaid30o6dg&sensor=false&callback=mapsLoaded&libraries=places";
            document.body.appendChild(_script);
        }

        //**********************************************************************//
        //***************** Load Variables for Postback  ***********************//
        //**********************************************************************//
        function mapPickup() {

            var _coordenates = $('#hdnMapPoint').val();
            var _address = $('#hdnMapAddress').val();

            var _hdnAddress = $get('<%=hdnMapLocationAddress.ClientID%>');
            var _hdnCoords = $get('<%=hdnMapLocationPoint.ClientID%>');
            var _lblAddress = $get('<%=lblMapLocationSelected.ClientID%>');

            if (_coordenates != '') {
                _hdnCoords.value = _coordenates;
                _hdnAddress.value = _address;
                _lblAddress.innerText = _address;

            }
            else {
                _hdnCoords.value = "";
                _hdnAddress.value = "";
                _lblAddress.innerText = "";
            }
        }


    </script>
    <script lang="javascript" type="text/javascript">

        function validateLocation(source, args) {

            var _lbl = $get('<%=hdnMapLocationPoint.ClientID%>');

            if (_lbl.value == '') {
                args.IsValid = true;  //false;
            }
            else {
                args.IsValid = true;
            }
        }
                
    </script>
    

    <div class="divLineSeparatorHeader Dashboard"></div>

    <div class="divForm AddDashboard">
        <!--*********************************************** -->
        <!--************* Company Properties ************** -->
        <!--*********************************************** -->
        <div class="divTitle">
            <asp:Label ID="lblSiteProperties" runat="server"></asp:Label>
        </div>
        <asp:Label style="color:Red" ID="lblMandatoryFieldsExplanation" runat="server"></asp:Label>
        <br /><br />

        <div id="divImage" class="divColumn column3 img">
            <div class="divCenterLogo">
                <p>
                    <span>
                        <asp:Image ID="imgLogo" runat="server" CssClass="logo" />
                    </span>
                </p>
            </div>
            <span class="file-wrapper">
                <asp:FileUpload ID="fuLogo" runat="server" />
                <asp:Label ID="lblChoosePicture" runat="server" CssClass="button"></asp:Label>
            </span>
            <div class="clear">
            </div>
        </div>
        <div class="divColumn column1-2 last">
            <asp:Label ID="lblName" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvName" runat="server" ValidationGroup="Edit" ControlToValidate="txtName"
                EnableClientScript="true" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtName" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblEmail" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ValidationGroup="Edit"
                ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                Display="Dynamic" CssClass="rfvRequested"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="lblValue"></asp:TextBox>
            <asp:Label ID="lblEmailFormatMessage" runat="server" CssClass="lblMessage"></asp:Label>
        </div>
        <div class="divColumn column3 last">
            <asp:Label ID="lblUrl" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RegularExpressionValidator ID="revUrl" runat="server" ValidationGroup="Edit"
                ControlToValidate="txtUrl" ValidationExpression="(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?"
                Display="Dynamic" CssClass="rfvRequested"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtUrl" runat="server" CssClass="lblValue"></asp:TextBox>
            <asp:Label ID="lblUrlFormatMessage" runat="server" CssClass="lblMessage"></asp:Label>
        </div>
        <div class="divColumn column1 text margin">
            <asp:Label ID="lblImageFormatMessage" runat="server" CssClass="lblMessage"></asp:Label>
            <asp:RegularExpressionValidator ID="revLogo" runat="server" ValidationExpression="^.*\.(jpg|JPG|gif|GIF|png|PNG|bmp|BMP)$"
                ControlToValidate="fuLogo" ValidationGroup="Edit"></asp:RegularExpressionValidator>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblTelephone" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:TextBox ID="txtTelephone" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblFacebook" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:TextBox ID="txtFacebook" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column3 last">
            <asp:Label ID="lblTwitter" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:TextBox ID="txtTwitter" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divDetail">
            <div class="divColumn column1 margin">
                <asp:Label runat="server" ID="lblAddress" CssClass="lblTitle"></asp:Label>
                <asp:CustomValidator ID="cuvLocation" runat="server" ValidationGroup="Edit" EnableClientScript="true"
                    ClientValidationFunction="validateLocation" Display="Dynamic" CssClass="rfvRequested"></asp:CustomValidator>
                <asp:Label ID="lblMapLocationSelected" runat="server" CssClass="lblValue border"></asp:Label>
            </div>
        </div>
        <div class="clear">
        </div>
        <div class="divTitle">
            <asp:Label ID="lblLocation" runat="server"></asp:Label>
        </div>

        <!-- ***************************************-->
        <!-- ************* Map Pickup **************-->
        <!-- ***************************************-->
        <!-- Location -->
        <input type="hidden" id="hdnMapLocationPoint" runat="server" />
        <input type="hidden" id="hdnMapLocationAddress" runat="server" />
        <!-- Previous Location -->
        <div id="divMapPickup">
            <div id="divMapLocator" class="divColumn column1">
                <asp:Label ID="lblMapLocator" runat="server" CssClass="lblTitle"></asp:Label>
                <input type="text" id="txtMapLocator" value="" class="lblValue" />
            </div>
            <div class="clear">
            </div>
            <div id="divMapCanvas" class="divMapCanvas divMapDetail divLarge">
            </div>
            <div id="divMapGeocoding">
            </div>
            <div>
                <input id="hdnMapPoint" type="hidden" value="<%=Location%>" />
                <input id="hdnMapAddress" type="hidden"  value="<%=Address%>" />
            </div>
        </div>

    </div>

    <!-- ***************************************-->
    <!-- ************* Save  *******************-->
    <!-- ***************************************-->
    <div class="divContentSave">
        <asp:Button ID="btnSave" runat="server" ValidationGroup="Edit" CssClass="btnActions btnSave" UseSubmitBehavior="False" />
    </div>
    
</asp:Content>
