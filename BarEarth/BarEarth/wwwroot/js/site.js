// Write your JavaScript code.
let map = null;
let latitude = 57.7089;
let longitude = 11.9746;
let currentPosition;
let key = 'AIzaSyD4nV1sw-I7t74JHMhwkprurMSzM_WB_V8';
let markers = [];
let infoWindow;
let barPosition;
let directionsDisplay;
let directionsService;

console.log('SITE.JS!!!');

function initMap() {

    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {

            UpdatePosition(position.coords.latitude, position.coords.longitude);
        });
    }
    else {
        console.log("couldn't get position");
    }
    currentPosition = {
        lat: latitude,
        lng: longitude
    };
    let myMap = document.getElementById('map');
    if (!myMap) return;

    map = new google.maps.Map(myMap, {
        center: currentPosition,
        zoom: 15
    });
    SearchBox();

    let service = new google.maps.places.PlacesService(map);

    nearbySearch(currentPosition,service);
}

function createMarkers(places) {


    infowindow = new google.maps.InfoWindow();
    

    directionsDisplay = new google.maps.DirectionsRenderer();
    directionsService = new google.maps.DirectionsService();

    markers.forEach(function (marker) {
        marker.setMap(null);
    });

    markers = [];

    for (let i = 0; i < places.length; i++) {

        let place = places[i];

        let image = {
            url: place.icon,
            size: new google.maps.Size(71, 71),
            origin: new google.maps.Point(0, 0),
            anchor: new google.maps.Point(17, 34),
            scaledSize: new google.maps.Size(25, 25)
        };

        let marker = new google.maps.Marker({
            map: map,
            icon: image,
            title: place.name,
            position: place.geometry.location,
        });

        console.log('placetest: ', place);
        marker.addListener('click', function () {
            let latitude = this.position.lat();
            let longitude = this.position.lng();

            infowindow.setContent('<form method="post" action="/Bar/barName?name=' + place.name + '"><button type=submit>' + place.name + '</button></form>' +
                '<br />' +
                '<div><button type="button" class="btn btn-success" id="btnDirection">Direction</button> </div>');
            infowindow.open(map, marker);
            $('#btnDirection').bind('click', function () {
                calcRoute(currentPosition, latitude, longitude, directionsService, directionsDisplay);
            }); 
        });
        markers.push(marker);

    }
    console.log(markers.length);
}

function nearbySearch(position, service) {

    
    let getNextPage = null;

    // Perform a nearby search.
    service.nearbySearch({
        location: position,
        radius: 1500,
        type: ['bar']
    },
        function (results, status, pagination) {
            if (status !== 'OK') return;

            createMarkers(results);
            getPlaceInfo(results);
        });
}
function UpdatePosition(lati, longi) {
    longitude = longi;
    latitude = lati;

    let service = new google.maps.places.PlacesService(map);

    currentPosition = {
        lat: lati,
        lng: longi
    };
    nearbySearch(currentPosition, service);

    let marker = new google.maps.Marker({
        map: map,
        position: { lat: lati, lng: longi }
    });

    if (map)
        map.setCenter({ lat: lati, lng: longi });

    //initMap();
};

function getPlaceInfo(Places) {
    let url = 'https://maps.googleapis.com/maps/api/place/details/json';
    let places = Places;

    for (let i = 0; i < places.length; i++) {

        let placeId = places[i].place_id;
        let barName;
        let barId;
        let barAdress;
        let openingHours;
        let website;
        let phoneNumber;
        let type;
        let email;
        let photoreference = "";
        let latitude;
        let longitude;

        let finalUrl = `${url}?placeid=${placeId}&key=${key}`;
        console.log('Hämtar data från: ' + finalUrl);
        fetch(finalUrl, { method: 'POST' })
            .then(response => {
                console.log('Svar från servern:', response);
                return response.json();
            })
            .then(obj => {
                console.log('Svaret som objekt: ', obj);
                console.log('Lyckades hämta data');
                barName = `${obj.result.name}`;
                barId = `${obj.result.place_id}`;
                barAdress = `${obj.result.formatted_address}`;
                openingHours = `${obj.result.opening_hours.weekday_text}`;
                phoneNumber = `${obj.result.international_phone_number}`;
                website = `${obj.result.website}`;
                type = `${obj.result.types}`;
                latitude = `${obj.result.geometry.location.lat}`;
                longitude = `${obj.result.geometry.location.lng}`;

                for (let j = 0; j < 5; j++) {

                    photoreference += `${obj.result.photos[j].photo_reference},`;
                }

                console.log(obj.result.geometry.location.lng);
                //console.log(openingHours);

                SendData(barName, barId,barAdress,openingHours,phoneNumber,type,website, photoreference,latitude,longitude);
            })
            .catch(message => {
                console.log('Något gick fel: ' + message);
            })
    }

    ////form encoded data
    //let dataType = 'application/x-www-form-urlencoded; charset=utf-8';
    //let data = $('form').serialize();
}

function SearchBox() {

    directionsDisplay = new google.maps.DirectionsRenderer();

    // Create the search box and link it to the UI element.
    var input = document.getElementById('pac-input');
    var searchBox = new google.maps.places.SearchBox(input);
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);

    // Bias the SearchBox results towards current map's viewport.
    map.addListener('bounds_changed', function () {
        searchBox.setBounds(map.getBounds());
    });

    var markers = [];
    // Listen for the event fired when the user selects a prediction and retrieve
    // more details for that place.
    searchBox.addListener('places_changed', function () {

        let places = searchBox.getPlaces();

        if (places.length === 0) {
            return;
        }

        // Clear out the old markers.
        markers.forEach(function (marker) {
            marker.setMap(null);
        });
        markers = [];
        
        // For each place, get the icon, name and location.
        var bounds = new google.maps.LatLngBounds();
        places.forEach(function (place) {
            if (!place.geometry) {
                console.log("Returned place contains no geometry");
                return;
            }
            var icon = {
                url: place.icon,
                size: new google.maps.Size(71, 71),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(17, 34),
                scaledSize: new google.maps.Size(25, 25)
            };

            // Create a marker for each place.
            markers.push(new google.maps.Marker({
                map: map,
                icon: icon,
                title: place.name,
                position: place.geometry.location
            }));

            barPosition = place.geometry.location;

            if (place.geometry.viewport) {
                // Only geocodes have viewport.
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }

            directionsDisplay.setMap(null);
        });
        map.fitBounds(bounds);

        let service = new google.maps.places.PlacesService(map);
        nearbySearch(barPosition, service);
    });
}
//JSON data
function SendData(barname,barId,barAddress,openingHours,phoneNumber,type,website, photoReference,latitude,longitude) {

    let dataType = 'application/json; charset=utf-8';
    let data = {
        Name: barname,
        PlaceId: barId,
        Address: barAddress,
        OpeningHours: openingHours,
        PhoneNumber: phoneNumber,
        Type: type,
        Website: website,
        PhotoReference: photoReference,
        Longitude: longitude,
        Latitude: latitude
    }

    console.log('Submitting form...');
    $.ajax({
        type: 'GET',
        url: '/MapApi/Get',
        dataType: 'json',
        contentType: dataType,
        data: data,
        success: function (result) {
            //console.log('Data received: ');
            //console.log(result);
        }
    });
}

function calcRoute(currentPosition, lati, longi, dirService,dirDisplay) {

    dirDisplay.setMap(map);

    let destination1 = { lat: lati, lng: longi };

    let request = {
        origin: currentPosition,
        destination: destination1,
        travelMode: 'WALKING'
    };
    
    dirService.route(request, function(response, status) {
        if (status === 'OK') {
            dirDisplay.setDirections(response);
        }
    });
}
