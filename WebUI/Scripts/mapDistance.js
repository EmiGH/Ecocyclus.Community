var _Map;
var _Geocoder;
var _DirectionsService;
var _DirectionsDisplay;
var _Infowindow;
var _Marker;
var _Site;

//*********************************************************//
//***************** Load Functions  ***********************//
//*********************************************************//

function mapsLoaded() {

    _Geocoder = new google.maps.Geocoder();
    _Infowindow = new google.maps.InfoWindow();
    _DirectionsService = new google.maps.DirectionsService();
    _DirectionsDisplay = new google.maps.DirectionsRenderer();

    _Map = new google.maps.Map($get('divMapCanvas'), {
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        zoom: 10,
        center: new google.maps.LatLng(0, 0)
    });

    _DirectionsDisplay.setMap(_Map);

    _search = $get('txtMapLocator');
    _Autocomplete = new google.maps.places.Autocomplete(_search);
    _Autocomplete.bindTo('bounds', _Map);

    google.maps.event.addListener(_Autocomplete, 'place_changed', function () {

        //_search.className = '';
        var place = _Autocomplete.getPlace();

        if (!place.geometry) {
            // Inform the user that the place was not found and return.
            //_search.className = 'notfound';
            return;
        }

        if (place.geometry.viewport) {
            _Map.fitBounds(place.geometry.viewport);
        } else {
            _Map.setCenter(place.geometry.location);
            _Map.setZoom(15);
        }

        showAddress();
    });

    setSite();
    setPrevious();
    switchLoadType();
}

//*********************************************************//
//***************** Site Position   ***********************//
//*********************************************************//
function setSite() {

    var _point = $('#hdnMapSite').val();
    
    if (_point) {

        var _bounds = new google.maps.LatLngBounds();

        if (_point.indexOf(";") > 0) {

            //Saco los miembro de cada string
            var _members = _point.split(";");

            if (!isNaN(_members[0]) && !isNaN(_members[1])) {

                //Armo la referencia
                var _location = new google.maps.LatLng(_members[0], _members[1]);

                //Agrego el marcador
                _Site = new google.maps.Marker({
                    position: _location,
                    map: _Map,
                    //shadow: shadow,
                    animation: google.maps.Animation.DROP
                    //icon: image,
                    //shape: shape,

                });

                //Extiendo los limites del mapa
                _bounds.extend(_location);

                //Establezco las dimensiones
                _Map.setCenter(_bounds.getCenter());
                _Map.setZoom(15);
            }
        }
    }
}

//*********************************************************//
//***************** Previous Position**********************//
//*********************************************************//
function setPrevious() {

    var _address = $('#hdnMapPrevious').val();
    if (_address) {

        $get('txtMapLocator').value = _address;
        showAddress();
    }
}

//*********************************************************//
//***************** Save Functions  ***********************//
//*********************************************************//
function saveLatLong() {
    if (_Marker) {
        
        var _coords = $get('hdnMapPoint');
        var _location = $get('hdnMapAddress');

        var _latitude = _Marker.getPosition().lat();
        var _longitude = _Marker.getPosition().lng();

        var _latlng = new google.maps.LatLng(_latitude, _longitude);

        _coords.value = _latitude + ";" + _longitude;
        _location.value = $('#txtMapLocator').val();

        mapPickup();
    }
}

//*********************************************************//
//***************** Seek Functions  ***********************//
//*********************************************************//
function showAddress() {

    if (_Geocoder) {

        var _address = $('#txtMapLocator').val();

        _Geocoder.geocode({ 'address': _address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {

                _Map.setCenter(results[0].geometry.location);
                if (_Marker) {
                    _Marker.setPosition(results[0].geometry.location);
                }
                else {
                    _Marker = new google.maps.Marker({
                        map: _Map,
                        position: results[0].geometry.location
                    });
                }

                var _origin, destination;
                if ($('#cphContent_rdLocation').is(':checked')) {
                    _origin = _Marker.getPosition();
                    _destination = _Site.getPosition();
                }
                else {
                    _origin = _Site.getPosition();
                    _destination = _Marker.getPosition();
                }

                var request = {
                    origin: _origin,
                    destination: _destination,
                    travelMode: google.maps.DirectionsTravelMode.DRIVING
                };
                
                _DirectionsService.route(request, function (response, status) {
                    if (status == google.maps.DirectionsStatus.OK) {

                        $get('hdnMapDistance').value = response.routes[0].legs[0].distance.value;
                        _DirectionsDisplay.setDirections(response);
                        saveLatLong();
                    }
                
                });

            } else {
                alert(status);
            }
        });
    }
}


