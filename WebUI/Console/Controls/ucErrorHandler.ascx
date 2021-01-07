<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucErrorHandler.ascx.cs" Inherits="CSI.WebUI.Console.Controls.ucErrorHandler" %>

<!--*********************************************************************-->
<!--********************* Error Message Clean Functions *****************-->
<!--*********************************************************************-->
<script type="text/javascript">

    //Clean Messages
    var old__doPostBack = __doPostBack;
    __doPostBack = function (eventTarget, eventArgument) {
        $get('<%=hdnMessageTitle.ClientID%>').value = "";
        $get('<%=hdnMessage.ClientID%>').value = "";
        old__doPostBack(eventTarget, eventArgument);
    };

</script>

<!--**************************************************************-->
<!--********************* Error Message Variables*****************-->
<!--**************************************************************-->
<asp:HiddenField ID="hdnMessageTitle" runat="server" />
<asp:HiddenField ID="hdnMessage" runat="server" />

<!--**************************************************************-->
<!--********************* Result Box *****************************-->
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

            //Clean Messages
            $get('<%=hdnMessageTitle.ClientID%>').value = "";
            $get('<%=hdnMessage.ClientID%>').value = "";
        }
    }

</script>