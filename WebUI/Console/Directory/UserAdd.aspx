<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="UserAdd.aspx.cs" Inherits="CSI.WebUI.Console.Directory.UserAdd" %>

<asp:Content ContentPlaceHolderID="cphhead" runat="server" ID="cntHead">
    <script type="text/javascript" src="../../Scripts/msBeautify.js"></script>
    <script type="text/javascript" src="../../Scripts/helper.js"></script>
</asp:Content>

<asp:Content ID="cntUserAdd" ContentPlaceHolderID="cphContent" runat="server">
    <script lang="javascript" type="text/javascript">

        window.onload = function () {
            var _email2 = $get('<%=txtEmail2.ClientID%>');
            _email2.onpaste = function (e) {
                e.preventDefault();
            }
            var _password2 = $get('<%=txtPassword2.ClientID%>');
            _password2.onpaste = function (e) {
                e.preventDefault();
            }
        }
        
    </script>
    
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

        <!-- Image -->
        <div id="divImage" class="divColumn column3 img">
            <div class="divCenterLogo">
                <p>
                    <span>
                        <asp:Image ID="imgImage" runat="server" />
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
            <asp:RequiredFieldValidator ID="rfvFirstname" runat="server" ValidationGroup="Add"
                ControlToValidate="txtFirstname" EnableClientScript="true" CssClass="rfvRequested"
                Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtFirstname" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column3 last">
            <asp:Label ID="lblLastname" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvLastname" runat="server" ValidationGroup="Add"
                ControlToValidate="txtLastname" EnableClientScript="true" CssClass="rfvRequested"
                Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtLastname" runat="server" CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblEmail" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ValidationGroup="Add"
                ControlToValidate="txtEmail" EnableClientScript="true" CssClass="rfvRequested"
                Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ValidationGroup="Add"
                ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                CssClass="rfvRequested" Display="Dynamic"></asp:RegularExpressionValidator>
            <asp:TextBox ID="txtEmail" runat="server" AutoCompleteType="Email" ValidationGroup="Add"
                CssClass="lblValue"></asp:TextBox>
            <asp:Label ID="lblEmailFormatMessage" runat="server" CssClass="lblMessage"></asp:Label>
        </div>

        <div class="divColumn column3 last">
            <asp:Label ID="lblEmail2" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvEmail2" runat="server" ValidationGroup="Add" ControlToValidate="txtEmail2"
                EnableClientScript="true" CssClass="rfvRequested" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvEmail" ControlToValidate="txtEmail2" ControlToCompare="txtEmail"
                Operator="Equal" Type="String" ValidationGroup="Add" runat="server" CssClass="rfvRequested"
                Display="Dynamic" />
            <asp:TextBox ID="txtEmail2" runat="server" AutoCompleteType="Email" ValidationGroup="Add"
                CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="clear">
        </div>
        <div class="divColumn column1 text">
            <asp:Label ID="lblImageFormatMessage" runat="server" CssClass="lblMessage"></asp:Label>
            <asp:RegularExpressionValidator ID="revPicture" runat="server" ValidationExpression="^.*\.(jpg|JPG|gif|GIF|png|PNG|bmp|BMP)$"
                ControlToValidate="fuPicture" ValidationGroup="Add" CssClass="rfvRequested"></asp:RegularExpressionValidator>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblPassword" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ValidationGroup="Add"
                ControlToValidate="txtPassword" EnableClientScript="true" CssClass="rfvRequested"
                Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtPassword" ID="revPassword" CssClass="rfvRequested"
                runat="server" ValidationGroup="Add" ValidationExpression="^(?=.{6,}$)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)\S*$">
            </asp:RegularExpressionValidator>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" ValidationGroup="Add"
                CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column3">
            <asp:Label ID="lblPassword2" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:CompareValidator ID="cvPassword" ControlToValidate="txtPassword2" ControlToCompare="txtPassword" CssClass="rfvRequested"
                Operator="Equal" Type="String" ValidationGroup="Add" runat="server" Display="Dynamic" EnableClientScript="true" />
            <asp:TextBox ID="txtPassword2" runat="server" TextMode="Password" ValidationGroup="Add"
                CssClass="lblValue"></asp:TextBox>
        </div>
        <div class="divColumn column3 last">
            <asp:Label ID="lblLanguage" runat="server" CssClass="lblTitle"></asp:Label>
            <asp:CompareValidator ID="cvLanguage" runat="server" ValidationGroup="Add" ControlToValidate="ddlLanguage"
                Operator="NotEqual" ValueToCompare="0" CssClass="rfvRequested"></asp:CompareValidator>
            <asp:DropDownList ID="ddlLanguage" runat="server" AutoPostBack="false" ValidationGroup="Add">
            </asp:DropDownList>
        </div>
        <div class="divColumn column3 margin text">
            <asp:CheckBox ID="chkIsManager" runat="server" />
            <asp:Label ID="lblIsManager" runat="server" CssClass="lblTitle checkbox"></asp:Label>
        </div>
        <div class="divColumn column3 margin text">
            <asp:CheckBox ID="chkIsActive" runat="server" />
            <asp:Label ID="lblIsActive" runat="server" CssClass="lblTitle checkbox"></asp:Label>
        </div>
        
        <!--*****************************************************-->
        <!--********************* Save **************************-->
        <!--*****************************************************-->
        <div class="divContentSave">
            <asp:Button ID="btnSave" runat="server" ValidationGroup="Add" CssClass="btnActions btnSave" />
        </div>
    </div>

    
</asp:Content>
