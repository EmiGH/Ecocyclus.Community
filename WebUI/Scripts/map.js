var _Map;
var _Geocoder;
var _Infowindow;
var _Marker;
var _Autocomplete;

//*********************************************************//
//***************** Load Functions  ***********************//
//*********************************************************//
function mapsLoaded() {
    _Geocoder = new google.maps.Geocoder();
    _Infowindow = new google.maps.InfoWindow();

    _Map = new google.maps.Map($get('divMapCanvas'), {
        mapTypeId: google.maps.MapTypeId.ROADMAP,
        zoom: 10,
        center: new google.maps.LatLng(0, 0)
    });

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
            _Map.setZoom(15);  // Why 17? Because it looks good.
        }
        mapShowAddress();
    });

    maploadPoint();

    if (!_Marker) {
        if (navigator.geolocation) navigator.geolocation.getCurrentPosition(

                    function (pos) {
                        var me = new google.maps.LatLng(pos.coords.latitude, pos.coords.longitude);
                        _Map.setCenter(me);
                        _Map.setZoom(15);
                    }, function (error) {

                    }
                );
    }
}

//*********************************************************//
//*****************   Set Functions ***********************//
//*********************************************************//
function maploadPoint() {

    var _bounds = new google.maps.LatLngBounds();

    var _point = $('#hdnMapPoint').val();
    if (_point) {
        if (_point.indexOf(";") > 0) {

            //Saco los miembro de cada string
            var _members = _point.split(";");

            if (!isNaN(_members[0]) && !isNaN(_members[1])) {

                //Armo la referencia
                var _location = new google.maps.LatLng(_members[0], _members[1]);

                //Agrego el marcador
                _Marker = new google.maps.Marker({
                    position: _location,
                    map: _Map,
                    //shadow: shadow,
                    animation: google.maps.Animation.DROP
                    //icon: image,
                    //shape: shape,

                });

                //Extiendo los limites del mapa
                _bounds.extend(_location);
                _Map.setCenter(_bounds.getCenter());
                _Map.setZoom(15);

            }
        }
    }
}

//*********************************************************//
//***************** Seek Functions  ***********************//
//*********************************************************//
function mapShowAddress() {

    if (_Geocoder) {

        var _address = $('#txtMapLocator').val();

        _Geocoder.geocode({ 'address': _address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                _Map.setCenter(results[0].geometry.location);
                _Marker = new google.maps.Marker({
                    map: _Map,
                    position: results[0].geometry.location
                });

                _Infowindow.setContent(results[0].formatted_address);
                _Infowindow.open(_Map, _Marker);

                mapSaveLatLong();
            } else {
                alert(status);
            }
        });
    }
}

//*********************************************************//
//***************** Save Functions  ***********************//
//*********************************************************//
function mapSaveLatLong() {

    if (_Marker) {

        var _coords = $get('hdnMapPoint');
        var _address = $get('hdnMapAddress');
        var _country = $get('hdnMapCountry');

        var _latitude = _Marker.getPosition().lat();
        var _longitude = _Marker.getPosition().lng();

        var _latlng = new google.maps.LatLng(_latitude, _longitude);

        _coords.value = _latitude + ";" + _longitude;
        _address.value = $('#txtMapLocator').val();

        if (_Geocoder) {

            _Geocoder.geocode({ 'latLng': _latlng }, function (results, status) {
                if (status == google.maps.GeocoderStatus.OK) {
                    if(_country!=null)
                        for (var j = 0; j < results[0].address_components.length; j++) {
                            if (results[0].address_components[j].types[0] == 'country')
                                _country.value = results[0].address_components[j].short_name;
                        }
                    mapPickup();
                }
            });
        }
    }
}

