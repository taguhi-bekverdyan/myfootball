function initAutocomplete() {

  var map = initPitchMap();
  addSearchToMap(map);
}

function initPitchMap() {
  var map;
  var center = { lat: 40.177, lng: 44.513 };

  // #region ADD NEW PITCH map

  if ($('#new-pitch-map').length > 0) {
    
    var marker;
    map = new google.maps.Map(document.getElementById('new-pitch-map'), {
      center: center,
      zoom: 13,
      mapTypeId: 'roadmap'
    });

    google.maps.event.addListener(map, 'click', function (event) {

      // Clean all markers
      if (marker) {
        marker.setMap(null);
      }

      // Set new marker
      marker = placeMarker(event.latLng, this);

      fillLongLatField(event.latLng);
    });
  }

  // #endregion

  // #region single PITCH map

  if ($('#single-pitch-map').length > 0) {
    
    map = new google.maps.Map(document.getElementById('single-pitch-map'), {
      center: mapLocation,
      zoom: 13,
      mapTypeId: 'roadmap'
    });    

    new google.maps.Marker({
      position: mapLocation,
      map: map
    });
  }

  // #endregion

  // #region PITCHFINDER map

  if ($('#pitchfinder-map').length > 0) {

    var markers = [];
    map = new google.maps.Map(document.getElementById('pitchfinder-map'), {
      center: center,
      zoom: 13,
      mapTypeId: 'roadmap'
    });

    pitches.forEach(function (pitch) {
      markers.push(new google.maps.Marker({
        position: { lat: parseFloat(pitch.Lat), lng: parseFloat(pitch.Lng) },
        url: pitch.Url,
        title: pitch.Title,
        map: map
      }));

      google.maps.event.addListener(markers[markers.length - 1], 'click', function () {
        window.location.href = this.url;
      });
    });
  }

  // #endregion

  return map;
}

// #region SEARCH

// This example adds a search box to a map, using the Google Place Autocomplete
// feature. People can enter geographical searches. The search box will return a
// pick list containing a mix of places and predicted search terms.

// This example requires the Places library. Include the libraries=places
// parameter when you first load the API. For example:
// <script src="https://maps.googleapis.com/maps/api/js?key=YOUR_API_KEY&libraries=places">
function addSearchToMap(map) {


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
    var places = searchBox.getPlaces();

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

      if (place.geometry.viewport) {
        // Only geocodes have viewport.
        bounds.union(place.geometry.viewport);
      } else {
        bounds.extend(place.geometry.location);
      }
    });
    map.fitBounds(bounds);
  });
}

// #endregion

// #region HELPERS

// Place a marker on map click
function placeMarker(location, map) {

  return new google.maps.Marker({
    position: location,
    map: map
  });  
}

// Set longitude and latitude field values
function fillLongLatField(location) {
  $('.pitch-longitude').val(location.lng);
  $('.pitch-latitude').val(location.lat);
}

// #endregion