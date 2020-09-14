const parsedObj = JSON.parse(document.getElementById('locationsData').value);
const locations = parsedObj.LocationsInRange;
let marker;

const map = L.map('map').setView([parsedObj.MapCenter.Latitude, parsedObj.MapCenter.Longitude], 13);

L.tileLayer('https://api.mapbox.com/styles/v1/mapbox/streets-v11/tiles/{id}/{z}/{x}/{y}?access_token={accessToken}', {
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors, <a href="https://creativecommons.org/licenses/by-sa/2.0/">CC-BY-SA</a>, Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
    maxZoom: 18,
    id: '256',
    accessToken: 'pk.'
}).addTo(map);

locations.forEach(location => {
    marker = L.marker([location.Latitude, location.Longitude]);
    marker.bindPopup(`<a href='/Review/View/${location.Id}'>${location.Name}</a>`);
    marker.addTo(map);
})
