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
        fixedHeader: true,
        dom: 'tlp'
    });
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
