<%@ Page Title="" Language="C#" MasterPageFile="~/Console/Main.Master" AutoEventWireup="true"
    CodeBehind="ProfileChangePassword.aspx.cs" Inherits="CSI.WebUI.Console.Directory.ProfileChangePassword" %>

<asp:Content ContentPlaceHolderID="cphhead" runat="server" ID="cntHead">
    <script type="text/javascript" src="../../Scripts/helper.js"></script>
</asp:Content>

<asp:Content ID="cntContent" ContentPlaceHolderID="cphContent" runat="server">
    <div class="divLineSeparatorHeader Dashboard">
    </div>
    <div class="AddDashboard">
        <!--*********************************************** -->
        <!--*************** User Properties *************** -->
        <!--*********************************************** -->
        <div class="divTitle">
            <asp:Label ID="lblSiteProperties" runat="server"></asp:Label>
        </div>
        <asp:Label style="color:Red" ID="lblMandatoryFieldsExplanation" runat="server"></asp:Label>
        <br /><br />

        <div class="divDetail">
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
        </div>
        <div class="divForm AddDashboard">
            <!--************** Password Change ****************** -->
            <div class="divColumn column3">
                <asp:Label ID="lblOldPassowrd" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ValidationGroup="Save"
                    ControlToValidate="txtOldPassword" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>
                <asp:TextBox TextMode="Password" ID="txtOldPassword" runat="server" CssClass="lblValue"></asp:TextBox>
            </div>
            <div class="divColumn column3">
                <asp:Label ID="lblNewPassword" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ValidationGroup="Save"
                    ControlToValidate="txtNewPassword" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ControlToValidate="txtNewPassword" ID="revNewPassword"
                    runat="server" ValidationGroup="Save" ValidationExpression="^(?=.{6,}$)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)\S*$">
                </asp:RegularExpressionValidator>
                <asp:TextBox TextMode="Password" ID="txtNewPassword" runat="server" CssClass="lblValue"></asp:TextBox>
            </div>
            <div class="divColumn column3 last">
                <asp:Label ID="lblRetypePassword" runat="server" CssClass="lblTitle"></asp:Label>
                <asp:RequiredFieldValidator ID="rfvRetypePassword" runat="server" ValidationGroup="Save"
                    ControlToValidate="txtRetypePassword" Display="Dynamic" CssClass="rfvRequested"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvRetypePassword" ControlToValidate="txtRetypePassword"
                    ControlToCompare="txtNewPassword" Operator="Equal" Type="String" ValidationGroup="Save"
                    runat="server" Display="Dynamic" CssClass="rfvRequested" />
                <asp:TextBox TextMode="Password" ID="txtRetypePassword" runat="server" CssClass="lblValue"></asp:TextBox> 
            </div>

            <!--*****************************************************-->
            <!--********************* Save **************************-->
            <!--*****************************************************-->
            <asp:Button ID="btnSave" ValidationGroup="Save" runat="server" CssClass="btnActions btnSave"/>
        </div>
    </div>

</asp:Content>
