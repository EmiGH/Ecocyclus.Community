<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="ProfileEdit.aspx.cs" Inherits="CSI.WebUI.Console.Directory.ProfileEdit" %>
    
<asp:Content ContentPlaceHolderID="cphhead" runat="server" ID="cntHead">
    <script type="text/javascript" src="../../Scripts/msBeautify.js"></script>
    <script type="text/javascript" src="../../Scripts/helper.js"></script>
</asp:Content>

<asp:Content ID="cntProfileEdit" ContentPlaceHolderID="cphContent" runat="server">
   
    <div class="divLineSeparatorHeader Dashboard">
    </div>
    <div class="divForm AddDashboard">
        <!--*********************************************** -->
        <!--*************** User Properties *************** -->
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
                        <asp:Image ID="imgPicture" runat="server" CssClass="logo" />
                    </span>
                </p>
            </div>
            <span class="file-wrapper">
                <asp:FileUpload ID="fuPicture" runat="server" />
                <asp:Label ID="lblChoosePicture" runat="server" CssClass="button"></asp:Label>
            </span>
            <div class="clear">
            </div>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblFirstname" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvFirstname" runat="server" ValidationGroup="Edit"
                ControlToValidate="txtFirstname" EnableClientScript="true" Display="Dynamic"
                CssClass="rfvRequested"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtFirstname" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column3 last">
            <asp:Label ID="lblLastname" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvLastname" runat="server" ValidationGroup="Edit"
                ControlToValidate="txtLastname" EnableClientScript="true" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtLastname" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblEmail" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ValidationGroup="Edit"
                ControlToValidate="txtEmail" EnableClientScript="true" CssClass="rfvRequested"
                Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ValidationGroup="Edit"
                ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                Display="Dynamic" CssClass="rfvRequested"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="lblValue"></asp:TextBox>
            <asp:Label ID="lblEmailFormatMessage" runat="server" CssClass="lblMessage"></asp:Label>
        </div>
        <div class="divColumn column3 last">
            <asp:Label ID="lblLanguage" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:CompareValidator ID="cvLanguage" runat="server" ValidationGroup="Edit" ControlToValidate="ddlLanguage"
                Operator="NotEqual" ValueToCompare="0" Display="Dynamic" CssClass="rfvRequested"></asp:CompareValidator>
            <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="false" ValidationGroup="Edit">
            </asp:DropDownList>
        </div>
        <div class="divColumn column1 text">
            <asp:Label ID="lblImageFormatMessage" runat="server" CssClass="lblMessage"></asp:Label>
            <asp:RegularExpressionValidator ID="revPicture" runat="server" ValidationExpression="^.*\.(jpg|JPG|gif|GIF|png|PNG|bmp|BMP)$"
                ControlToValidate="fuPicture" ValidationGroup="Edit" Display="Dynamic" CssClass="rfvRequested"></asp:RegularExpressionValidator>
        </div>
        <div class="divDetail">
            <div class="divColumn column3">
                <asp:Label ID="lblIsManager" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:Label ID="lblIsManagerValue" runat="server" CssClass="lblValue border"></asp:Label>
            </div>
        </div>
        <div class="clear">
        </div>
    
        <!--*****************************************************-->
        <!--********************* Save **************************-->
        <!--*****************************************************-->
        <asp:Button ID="btnSave" runat="server" ValidationGroup="Edit" CssClass="btnActions btnSave" />
    </div>

</asp:Content>
