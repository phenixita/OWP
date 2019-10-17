$(document).ready(function () {
    $('#workitems thead tr:eq(1) th').each(function (i) {
        $('input', this).on('keyup change', function () {
            if (table.column(i).search() !== this.value) {
                table
                    .column(i)
                    .search(this.value)
                    .draw();
            }
        });
    });

    var table = $('#workitems').DataTable({
        orderCellsTop: true,
        fixedHeader: true
    });

    if (document.getElementById("image") != null && document.getElementById("image") != "undefined") {
        document.getElementById("image").onchange = function () {
            var reader = new FileReader();

            reader.onload = function (e) {
                // get loaded data and render thumbnail.
                document.getElementById("imgpreview").src = e.target.result;
                document.getElementById('imgpreview').hidden = false;
            };

            // read the image file as a data URL.
            reader.readAsDataURL(this.files[0]);
        };
    }

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
