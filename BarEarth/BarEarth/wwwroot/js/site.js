// Write your JavaScript code.


let map = null;
let latitude = 57.7089;
let longitude = 11.9746;
let currentPosition;
let key = 'AIzaSyCNTmJ9FGN1shynaOZ8niPI3OQLRAUbP4o';
let markers = [];
let infoWindow;




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

    nearbySearch(currentPosition);

}

function createMarkers(places) {

    infowindow = new google.maps.InfoWindow();

    markers.forEach(function (marker) {
        marker.setMap(null);
    });

    markers = [];

    for (let i = 0; i<places.length; i++) {

    
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

        marker.addListener('click', function () {
            infowindow.setContent(place.name);
            infowindow.open(map, marker);
        });

        markers.push(marker);

    }
    console.log(markers.length)
}

function nearbySearch(position) {

    let service = new google.maps.places.PlacesService(map);
    let getNextPage = null;

    // Perform a nearby search.
    service.nearbySearch({
        location: position,
        radius: 50000,
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

    currentPosition = {
        lat: lati,
        lng: longi
    };
    nearbySearch(currentPosition);

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
                console.log(barName)
                SendData(barName, barId);
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

            currentPosition = place.geometry.location;

            if (place.geometry.viewport) {
                // Only geocodes have viewport.
                bounds.union(place.geometry.viewport);
            } else {
                bounds.extend(place.geometry.location);
            }
        });
        map.fitBounds(bounds);
        nearbySearch(currentPosition);
    });
}

//JSON data

function SendData(barname,barId) {

    let dataType = 'application/json; charset=utf-8';
    let data = {
        Name: barname,
        PlaceId: barId
    }

    console.log('Submitting form...');
    $.ajax({
        type: 'GET',
        url: '/MapApi/Get',
        dataType: 'json',
        contentType: dataType,
        data: data,
        success: function (result) {
            console.log('Data received: ');
            console.log(result);
        }
    });
}
