//let map;

//async function initMap() {
//    //@ts-ignore
//    const { Map } = await google.maps.importLibrary("maps");

//    map = new Map(document.getElementById("map"), {
//        center: { lat: -34.397, lng: 150.644 },
//        zoom: 8,
//    });

//}


// Add a click listener to the Calculate Route button to calculate the route
let map;
let markers = [];

function initMap() {


    // Initialize the map
    map = new google.maps.Map(document.getElementById('map'), {
        center: { lat: 45.8134, lng: 15.9771 },
        zoom: 13
    });

    // Add a click listener to the map to add markers
    map.addListener('click', function (event) {
        addMarker(event.latLng);
    });


}

// Add a marker to the map and push it to the markers array
function addMarker(location) {
    let marker = new google.maps.Marker({
        position: location,
        map: map
    });

    markers.push(marker);
}


// Calculate the route using the Directions API
function calculateRoute() {
    if (markers.length < 2) {
        return;
    }

    let directionsService = new google.maps.DirectionsService();
    let directionsRenderer = new google.maps.DirectionsRenderer();

    let request = {
        origin: markers[0].getPosition(),
        destination: markers[markers.length - 1].getPosition(),
        waypoints: markers.slice(1, -1).map(function (marker) {
            return { location: marker.getPosition() };
        }),
        travelMode: 'WALKING'
    };

    directionsService.route(request, function (result, status) {
        if (status == 'OK') {
            directionsRenderer.setDirections(result);
            directionsRenderer.setMap(map);
        }
    });
}
document.getElementById('btnRender').addEventListener('click', function () {
    calculateRoute();
});

document.getElementById('btnRefresh').addEventListener('click', function () {
    location.reload();
});

window.onload = function () {
    //initMap();

    initMap();
    //document.getElementById('calculate-route').addEventListener('click', function () {
    //calculateRoute();
};




