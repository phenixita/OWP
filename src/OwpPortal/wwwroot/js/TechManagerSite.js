$(document).ready(function () {
    $('#workitems').DataTable();
});

async function getWorkItems() {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: "/api/geo",
            dataType: "json",
            success: function (data) {
                resolve(data);
            }
        });
    });
}