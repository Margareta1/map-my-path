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
        zoom: 13,
        styles: [
            {
                "featureType": "all",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#202c3e"
                    }
                ]
            },
            {
                "featureType": "all",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "gamma": 0.01
                    },
                    {
                        "lightness": 20
                    },
                    {
                        "weight": "1.39"
                    },
                    {
                        "color": "#ffffff"
                    }
                ]
            },
            {
                "featureType": "all",
                "elementType": "labels.text.stroke",
                "stylers": [
                    {
                        "weight": "0.96"
                    },
                    {
                        "saturation": "9"
                    },
                    {
                        "visibility": "on"
                    },
                    {
                        "color": "#000000"
                    }
                ]
            },
            {
                "featureType": "all",
                "elementType": "labels.icon",
                "stylers": [
                    {
                        "visibility": "off"
                    }
                ]
            },
            {
                "featureType": "landscape",
                "elementType": "geometry",
                "stylers": [
                    {
                        "lightness": 30
                    },
                    {
                        "saturation": "9"
                    },
                    {
                        "color": "#29446b"
                    }
                ]
            },
            {
                "featureType": "poi",
                "elementType": "geometry",
                "stylers": [
                    {
                        "saturation": 20
                    }
                ]
            },
            {
                "featureType": "poi.park",
                "elementType": "geometry",
                "stylers": [
                    {
                        "lightness": 20
                    },
                    {
                        "saturation": -20
                    }
                ]
            },
            {
                "featureType": "road",
                "elementType": "geometry",
                "stylers": [
                    {
                        "lightness": 10
                    },
                    {
                        "saturation": -30
                    }
                ]
            },
            {
                "featureType": "road",
                "elementType": "geometry.fill",
                "stylers": [
                    {
                        "color": "#193a55"
                    }
                ]
            },
            {
                "featureType": "road",
                "elementType": "geometry.stroke",
                "stylers": [
                    {
                        "saturation": 25
                    },
                    {
                        "lightness": 25
                    },
                    {
                        "weight": "0.01"
                    }
                ]
            },
            {
                "featureType": "water",
                "elementType": "all",
                "stylers": [
                    {
                        "lightness": -20
                    }
                ]
            }
        ]
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
async function calculateRoute() {
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

    directionsService.route(request, async function (result, status) {
        if (status == 'OK') {
            directionsRenderer.setDirections(result);
            directionsRenderer.setMap(map);

            const points = result.routes[0].legs.flatMap(leg => [leg.start_location, leg.end_location]);

            let uniquePlaces = {};

            for (const point of points) {
                //console.log("Starting...");
                await delay(200);
                //console.log("This message is displayed after 2 seconds");
                try {
                    const places = await fetchPlacesOfInterest(point);

                    for (const place of places) {
                        if (!uniquePlaces[place.place_id]) {
                            uniquePlaces[place.place_id] = place;

                            const marker = new google.maps.Marker({
                                position: place.geometry.location,
                                map: map,
                                title: place.name,
                                label: {
                                    text: place.name,
                                    color: "white",
                                    fontSize: "16px",
                                    fontWeight: "bold"
                                },
                                icon: {
                                    url: "/images/marker-3.png",
                                    scaledSize: new google.maps.Size(210, 45),
                                }
                            });

                            const infowindow = new google.maps.InfoWindow({
                                content: `
                                        <div style="font-family: Arial, sans-serif; border-top: 4px solid #334870; margin: 10px; overflow: hidden;">
                                            <h2 style="color: #334870; margin: 10px 0;">${place.name}</h2>
                                            <div style="display: flex; align-items: center;">
                                                ${place.photos && place.photos.length > 0 ? `<img src="${place.photos[0].getUrl()}" alt="${place.name}" style="max-width: 100px; max-height: 100px; margin-right: 10px;" />` : ''}
                                                <div>
                                                    <p style="margin: 0; color: #333;">${place.vicinity}</p>
                                                    ${place.rating ? `<p style="margin: 0; color: #333;"><strong>Rating:</strong> ${place.rating} stars</p>` : ''}
                                                </div>
                                            </div>
                                            <a href="https://www.google.com/maps/place/?q=place_id:${place.place_id}" style="display: inline-block; margin: 10px 0; padding: 5px 10px; background-color: #334870; color: #fff; text-decoration: none; border-radius: 4px;">View on Google Maps</a>
                                        </div>
                                    `
                            });

                            marker.addListener('click', function () {
                                infowindow.open(map, marker);
                            });
                        }
                    }
                } catch (error) {
                    console.error('Error fetching places of interest:', error);
                }
            }

            let departure = markers[0].getPosition();
            let destination = markers[markers.length - 1].getPosition();

            let response = await fetch('/Home/GetTaxiFareAjax', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    depLat: departure.lat(),
                    depLng: departure.lng(),
                    arrLat: destination.lat(),
                    arrLng: destination.lng()
                })
            });

            ////////// Taxi /////////
            let taxiFareResponse = await response.json();
            const taxiFareData = JSON.parse(taxiFareResponse);
            //////////
            document.getElementById('cityName').innerText = `${taxiFareData.journey.city_name}`;
            document.getElementById('distance').innerText = `${taxiFareData.journey.distance} km`;
            document.getElementById('pickup').innerText = `${taxiFareData.journey.department}`;
            document.getElementById('dropoff').innerText = `${taxiFareData.journey.arrival}`;
            document.getElementById('duration').innerText = `${taxiFareData.journey.duration} min`;
            document.getElementById('fare').innerText = `${taxiFareData.journey.fares[0].price_in_cents / 100} €`;
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

let retryCount = 0;
const maxRetryCount = 3;  
const retryDelay = 2000;

//interests
async function fetchPlacesOfInterest(location) {
    const interests = ['restaurant', 'museum', 'park', 'shopping_mall', 'atm', 'cafe', 'church', 'zoo', 'taxi_stand', 'synagogue', 'stadium', 'night_club'];
    const places = [];

    const service = new google.maps.places.PlacesService(map);

    for (let interest of interests) {
        const request = {
            location: location,
            radius: '500',
            type: [interest]
        };

        const results = await new Promise((resolve, reject) => {
            service.nearbySearch(request, (results, status) => {
                if (status === google.maps.places.PlacesServiceStatus.OK) {
                    resolve(results);
                } else if (status === google.maps.places.PlacesServiceStatus.ZERO_RESULTS) {
                    resolve([]); // If no places of interest, empty array
                }
                else if (status === google.maps.places.PlacesServiceStatus.OVER_QUERY_LIMIT) {
                    // If rate limit is exceeded, wait and try again
                    setTimeout(() => {
                        retryCount++;
                        if (retryCount <= maxRetryCount) {
                            fetchPlacesOfInterest(location).then(resolve).catch(reject);
                        } else {
                            reject(new Error(`Place fetching failed after ${maxRetryCount} attempts: ${status}`));
                        }
                    }, retryDelay);
                } else {
                    reject(new Error(`Place fetching failed: ${status}`));
                }

             
            });
        });

        // Sort results by rating in descending order and top 1
        const sortedResults = results.sort((a, b) => b.rating - a.rating).slice(0, 1);
        places.push(...sortedResults);
    }

    // filter duplicates
    const uniquePlaces = Array.from(new Set(places.map(place => place.place_id))).map(id => places.find(place => place.place_id === id));

    return uniquePlaces;
}

function delay(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}