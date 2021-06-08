$(document).delegate('span', 'mouseover', function (e) {

    //$(this).css( "cursor", "help" );

    var _helper = $("#divHelper");
    var _lang = _helper.attr("lang");
    var _controlId = $(this).attr("id");
    var _controlTitle = $(this).text();
    
    $.ajax({
        type: "POST",
        url: getAbsolutePath() + "/Helper",
        data: "{'control':'" + _controlId + "', 'lang':'" + _lang + "', 'title':'" + _controlTitle + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            if ($.trim(data.d) != "") {
                _helper.html(data.d);
                _helper.css({
                    position: "absolute",
                    top: e.pageY - 70,
                    left: e.pageX + 5,
                }).fadeIn();
            }
        },
        error: function (req, status, error) {
        }
    });
});

$(document).delegate('span', 'mouseleave', function () {
    $("#divHelper").fadeOut();

});

function getAbsolutePath() {
            
    var sPath = window.location.pathname;
    var sPage = sPath.substring(sPath.lastIndexOf('/') + 1);

    return sPage;
}
