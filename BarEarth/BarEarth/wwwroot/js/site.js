// Write your JavaScript code.

window.addEventListener('load', function () {

    map.addListener('bounds_changed', function () {
        console.log("bounds changed");
    });

});


let map = null;
let latitude = 57.7089;
let longitude = 11.9746;
let currentPosition;
let key = 'AIzaSyCLckiV9wHca0kEmxx40Ch9RnHOIvVgdFE';





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

    let service = new google.maps.places.PlacesService(map);
    let getNextPage = null;

    // Perform a nearby search.
    service.nearbySearch({
        location: currentPosition,
        radius: 5000,
        type: ['bar']
    },
        function (results, status, pagination) {
            if (status !== 'OK') return;

            createMarkers(results);
            getPlaceInfo(results[0].place_id);
        });
}

function createMarkers(places) {

    for (let i = 0; i<places.length; i++) {

        let place = places[i];

        let infowindow = new google.maps.InfoWindow({
            content: place.name
        });

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
            infowindow.open(map, marker);
        });

    }

}

function UpdatePosition(lati, longi) {
    longitude = longi;
    latitude = lati;

    let marker = new google.maps.Marker({
        map: map,
        position: { lat: lati, lng: longi }
    });

    if (map)
        map.setCenter({ lat: lati, lng: longi });
    //initMap();
};

function getPlaceInfo(PlaceId) {
    let url = 'https://maps.googleapis.com/maps/api/place/details/json';

    let placeId = PlaceId;

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
            console.log(obj.result.name)
        })
        .catch(message => {
            console.log('Något gick fel: ' + message);
        })
}