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

$(document).ready(function () {

    let imgElement = document.getElementById("WorkItem_Image");
    if (imgElement) {
        imgElement.onchange = function () {
            var reader = new FileReader();

            reader.onload = function (e) {
                // get loaded data and render thumbnail.
                $('#submitButton').attr('disabled', 'disabled');
                $('#imgLoader').parent().show();
                document.getElementById("imgpreview").src = e.target.result;
                document.getElementById('imgpreview').hidden = false;
                $('#imgpreview').css('opacity', 0.5);
            };

            // read the image file as a data URL.
            if (this.files && this.files[0]) {
                uploadImage(this.files[0]);
                reader.readAsDataURL(this.files[0]);
                document.getElementById('fileLoaded').innerText = this.files[0].name;
            }
        };
    }
});

function uuidv4() {
    return ([1e7] + -1e3 + -4e3 + -8e3 + -1e11).replace(/[018]/g, c =>
        (c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> c / 4).toString(16)
    );
}

function uploadImage(file) {
    // If one file has been selected in the HTML file input element
    var customBlockSize = file.size > 1024 * 1024 * 32 ? 1024 * 1024 * 4 : 1024 * 512;
    var blobService = AzureStorage.Blob.createBlobServiceWithSas(blobParams.blobUri, blobParams.sasToken);

    blobService.singleBlobPutThresholdInBytes = customBlockSize;

    var finishedOrError = false;
    var ext = file.name.substr(file.name.lastIndexOf('.'));
    var fileName = uuidv4() + ext;

    var speedSummary = blobService.createBlockBlobFromBrowserFile(blobParams.containerName, fileName, file, { blockSize: customBlockSize }, function (error, result, response) {
        finishedOrError = true;
        if (error) {
            // Upload blob failed
        } else {
            // Upload successfully
            $('#WorkItem_ImageUrl').val(blobParams.blobUri + "/" + blobParams.containerName +"/" + fileName);
            $('#imgpreview').css('opacity', 1);
        }
        $('#imgLoader').parent().hide();
        $('#submitButton').removeAttr('disabled');
    });

    speedSummary.on('progress', function () {
        var process = speedSummary.getCompletePercent();
        displayProcess(process);
        $('#imgLoader').css('width', process + '%');
    });

    //refreshProgress();
}

function rereshProgress() {

}

function displayProcess(process) {
    console.info(process);
}