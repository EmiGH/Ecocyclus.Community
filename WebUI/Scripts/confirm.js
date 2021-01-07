function showConfirmRequest(callBackFunction, title, content) {
    $("#divConfirm").html(content).dialog({
        autoOpen: true,
        modal: true,
        title: title,
        draggable: true,
        resizable: false,
        close: function (event, ui) { $(this).dialog("destroy"); },
        buttons: {
            'Ok': function () { callBackFunction(); },
            'Cancel': function () {
                $(this).dialog("destroy");
            }
        },
        overlay: {
            opacity: 0.45,
            background: "black"
        }
    });
}


function alertOnLoadDelete() {
    $('#divAlert').dialog({ modal: true });
}
