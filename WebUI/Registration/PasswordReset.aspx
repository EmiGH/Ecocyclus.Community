<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PasswordReset.aspx.cs"
    Inherits="CSI.WebUI.Registration.PasswordReset" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
    <title>CSI</title>
    <style type="text/css">
        *:focus
        {
            outline: none;
        }
        body
        {
            font-family: Arial;
            background-image: url(../Images/Login/bg.png);
            background-position: bottom center;
            background-repeat: no-repeat;
            background-color: #f7f6ed;
            background-attachment: fixed;
            overflow: auto !important;
            min-height: 100% !important;
            width: auto;
            height: auto;
            font-size: 12px;
        }
        #divContainer
        {
            border: 1px solid #eee;
            background-color: #e5e4dd;
            width: 480px;
            height: 170px;
            position: absolute;
            left: 50%;
            top: 45%;
            margin-left: -280px;
            margin-top: -70px;
            padding: 20px 40px;
            text-align: center;
        }
        
        #lblMessage
        {
            color: #8E8880;
            font-size: 13px;
            padding: 15px 0;
            display: block;
        }
        #txtPassword,
        #txtPassword2
        
        {
            width: 380px;
            height: 35px;
            line-height: 35px;
            border: 0px;
            background-color: #d8d7d2;
            -webkit-border-radius: 4px;
            -moz-border-radius: 4px;
            border-radius: 4px;
            font-size: 14px;
            font-weight: bold;
            color: #8e8880;
            text-align: center;
            text-decoration: none;
            margin-bottom:10px;
        }
        .Validator
        {
            font-size: 16px;
            margin-left: 5px;
            font-weight: bold;
            color: #E9837A;
        }
        #divContainerMessage
        {
            width: 480px;
            height: 80px;
            position: absolute;
            top: 220px;
            text-align: center;
            font-size: 11px;
        }
        
        #divContainerMessage #vsRecover
        {
            color: #E9837A;
        }
        #divContainerMessage #lblResults
        {
            color: #8E8880;
        }
        #divContainerMessage a
        {
            color: #8E8880;
        }
        #btnReset
        {
            position: absolute;
            bottom: 0px;
            left: 50%;
            margin-left: -75px;
            width: 150px;
            height: 35px;
            line-height: 35px;
            border: 0px;
            background-color: #E27771;
            cursor: pointer;
            font-size: 14px;
            font-weight: bold;
            color: #fff;
            text-align: center;
            text-decoration: none;
            text-transform: uppercase;
        }
    </style>
</head>
<body>
    <form id="frmContent" runat="server">

        <ajaxToolkit:ToolkitScriptManager ID="smSite" runat="server"></ajaxToolkit:ToolkitScriptManager>

        <div id="divContainer">
            
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            
            <!-- Password -->
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" ValidationGroup="Reset"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ValidationGroup="Reset"
                ControlToValidate="txtPassword" EnableClientScript="true" Display="Dynamic" CssClass="Validator"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ControlToValidate="txtPassword" ID="revPassword"
                runat="server" ValidationGroup="Reset" ValidationExpression="^(?=.{6,}$)(?=.*[a-z])(?=.*[A-Z])(?=.*\d)\S*$"
                Display="Dynamic" CssClass="Validator">
            </asp:RegularExpressionValidator>
            
            <!-- Retype Password-->
            <asp:TextBox ID="txtPassword2" runat="server" TextMode="Password" ValidationGroup="Reset"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword2" runat="server" ValidationGroup="Reset"
                ControlToValidate="txtPassword2" EnableClientScript="true" CssClass="Validator" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvPassword" ControlToValidate="txtPassword2" ControlToCompare="txtPassword"
                Operator="Equal" Type="String" ValidationGroup="Reset" runat="server" Display="Dynamic"
                CssClass="Validator" />

            <asp:Button ID="btnReset" ValidationGroup="Reset" runat="server" />

        </div>
        
        <!-- ***************************************-->
        <!-- ************* Save  *******************-->
        <!-- ***************************************-->
        <asp:UpdatePanel ID="upSave" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnReset" />
            </Triggers>
            <ContentTemplate>
                
                <!-- Message -->
                <asp:HiddenField ID="hdnMessageTitle" runat="server" />
                <asp:HiddenField ID="hdnMessage" runat="server" />

            </ContentTemplate>
        </asp:UpdatePanel>

        <!--**************************************************************-->
        <!--********************* Result Box **************************-->
        <!--**************************************************************-->
        <div id="divMessageBox"></div>
        <script type="text/javascript">
            $(document).ready(CheckServerMessage());

            function CheckServerMessage() {

                var _message = $('#<%=hdnMessage.ClientID%>').val();
                if (_message != '') {

                    $("#divMessageBox").html(_message).dialog({
                        autoOpen: true,
                        modal: true,
                        title: $('#<%=hdnMessageTitle.ClientID%>').val(),
                        draggable: true,
                        resizable: false,
                        close: function (event, ui) { $(this).dialog("destroy"); },
                        buttons: {
                            'Ok': function () {
                                $(this).dialog("destroy");
                            }
                        },
                        overlay: {
                            opacity: 0.45,
                            background: "black"
                        }
                    });
                }
            }
        </script>
            
        <!--**************************************************************-->
        <!--********************* Progress Icon **************************-->
        <!--**************************************************************-->
        <asp:UpdateProgress ID="upProgress" runat="server" AssociatedUpdatePanelID="upSave">
            <ProgressTemplate>
                <img src="../../Images/ProgressIndicator.gif" alt="<%=Resources.Data.Loading%>" />
                <div id="divLoading">
                    <%=Resources.Data.Loading%></div>
            </ProgressTemplate>
        </asp:UpdateProgress>

    </form>
</body>
</html>
