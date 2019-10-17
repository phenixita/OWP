// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



function getCurrentLocation() {
    if (navigator.geolocation) {

        const addrEle = document.getElementById('WorkItem_Address');
        addrEle.placeholder = "Fetching location...";

        const latEle = document.getElementById('WorkItem_Latitude');
        const longEle = document.getElementById('WorkItem_Longitude');

        navigator.geolocation.getCurrentPosition(async (geoPos) => {
            const lat = geoPos.coords.latitude;
            const long = geoPos.coords.longitude;

            latEle.value = lat;
            longEle.value = long;

            const data = await lookupLocation(lat, long);

            addrEle.placeholder = "";

            if (data && data.resources && data.resources.length > 0) {
                addrEle.value = data.resources[0].name;
            } else {
                alert('Could not resolve current location to an address - please enter manually');
            }

        }, errorPosition);
    } else {
        alert('Location not available');
    }
}

async function lookupLocation(lat, long) {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: "https://dev.virtualearth.net/REST/v1/Locations/" + lat + ',' + long + '?key=AhzXPtrjsvIyqZezEQdu9f- yPHPncwyVhlQ2 - gchufOGsO - HN1iYQLTCHZZlP859',
            dataType: "jsonp",
            jsonp: "jsonp",
            success: function (data) {
                resolve(data.resourceSets[0]);
            }
        });
    });
}


function errorPosition(position) {
    alert('unable to determine your current location')
}