<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucGuide.ascx.cs" Inherits="CSI.WebUI.Console.Controls.ucGuide" %>

<!--*********************************************************************-->
<!--********************* Message for Guiding Users     *****************-->
<!--*********************************************************************-->
<asp:HiddenField ID="hdnMessageGuideTitle" runat="server" />
<asp:HiddenField ID="hdnMessageGuide" runat="server" />

<div id="divGuide" runat="server">
    
</div>

<script type="text/javascript">

    $(document).ready(GuideCheckMessage());

    function GuideCheckMessage() {

        var _message = $('#<%=hdnMessageGuide.ClientID%>').val();
        if (_message != '') {

            $("#<%=divGuide.ClientID%>").html(_message).dialog({
                autoOpen: true,
                modal: true,
                title: $('#<%=hdnMessageGuideTitle.ClientID%>').val(),
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
            $get('<%=hdnMessageGuideTitle.ClientID%>').value = "";
            $get('<%=hdnMessageGuide.ClientID%>').value = "";
        }
    }
    
</script>