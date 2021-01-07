var _Map;

function mapsLoaded() {

    _Map = new google.maps.Map(document.getElementById('divMapCanvas'), {
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        zoom: 10,
        center: new google.maps.LatLng(0, 0)
    });

    mapLoadPoints();

}

//*********************************************************//
//*****************   Load Points   ***********************//
//*********************************************************//
function mapLoadPoints() {

    var _marker;
    var _location;
    var _bounds = new google.maps.LatLngBounds();
    var _points = $("#hdnMapPoint").val().split("|");

    var _pointCount = 0;

    for (var x = 0; x < _points.length; x++) {
        if (_points[x].indexOf(";") > 0) {

            _pointCount++;

            //Saco los miembro de cada string
            var _members = _points[x].split(";");

            if (!isNaN(_members[0]) && !isNaN(_members[1])) {

                //Armo la referencia
                _location = new google.maps.LatLng(_members[0], _members[1]);

                //Agrego el marcador
                _marker = new google.maps.Marker({
                    position: _location,
                    map: _Map,
                    //shadow: shadow,
                    animation: google.maps.Animation.DROP
                    //icon: image,
                    //shape: shape,

                });

                //Ventanas de Informacion
                mapAddWindowInfo(_marker, _members[2]);

                //Extiendo los limites del mapa
                _bounds.extend(_location);
            }
        }
    }

    if (_pointCount > 0) {
        //Establezco las dimensiones
        _Map.setCenter(_bounds.getCenter());

        if (_pointCount == 1)
            _Map.setZoom(14);
        else
            _Map.fitBounds(_bounds);

    }
    else {
        if (navigator.geolocation) navigator.geolocation.getCurrentPosition(
            function (pos) {
                var me = new google.maps.LatLng(pos.coords.latitude, pos.coords.longitude);
                _Map.setCenter(me);
                _Map.setZoom(10);
            }, function (error) {

            }
        );
    }
}

function mapAddWindowInfo(marker, info) {

    var _info = new google.maps.InfoWindow({ content: info });
    google.maps.event.addListener(marker, 'click', function () {
        _info.open(_Map, marker);
    });
}