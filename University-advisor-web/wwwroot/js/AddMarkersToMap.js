const parsedObj = JSON.parse(document.getElementById('locationsData').value);
const locations = parsedObj.LocationsInRange;
let marker;

const map = L.map('map').setView([parsedObj.MapCenter.Latitude, parsedObj.MapCenter.Longitude], 13);

L.tileLayer('https://api.tiles.mapbox.com/v4/{id}/{z}/{x}/{y}.png?access_token={accessToken}', {
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    maxZoom: 18,
    id: 'mapbox.streets',
    accessToken: 'pk.eyJ1IjoidGFkYXNtIiwiYSI6ImNrMmo0dzNkZjFnYmozbXA1NnpwYzR3djcifQ.oHXh2g0sVpiqLu3wmcP5uw'
}).addTo(map);

locations.forEach(location => {
    marker = L.marker([location.Latitude, location.Longitude]);
    marker.bindPopup(`<a href='/Review/View/${location.Id}'>${location.Name}</a>`);
    marker.addTo(map);
})
