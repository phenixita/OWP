$(document).ready(function () {
    $('#workitems thead tr:eq(1) th').each(function (i) {
        $('input, select', this).on('keyup change', function () {
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

var map;

async function loadMapScenario() {
    map = new Microsoft.Maps.Map(document.getElementById('issueMap'),
        {
            /* No need to set credentials if already passed in URL */
            center: new Microsoft.Maps.Location(51.4617734, -0.9274358),
            zoom: 12
        });

    var items = await getWorkItems();

    const summaryDescriptionValueEle = document.getElementById("SummaryDescriptionValue");
    const summaryStatusValueEle = document.getElementById("SummaryStatusValue");
    const summaryTypeEle = document.getElementById("SummaryTypeValue");
    const summaryAssignedToEle = document.getElementById("SummaryAssigngedToValue");
    const summaryWidValueEle = document.getElementById("SummaryWidValue");
    const summaryAddressValueEle = document.getElementById("SummaryAddressValue");

    const editLinkEle = document.getElementById("editLink");
    
    const clearAndReplace = (ele, value) => {
        ele.innerText = "";
        if (value) {
            ele.innerText = value;
        }
    };

    const colorPinMap = ['gray', 'blue', 'orange', 'red'];

    for (let i = 0; i < items.length; i++) {
        const item = items[i];
        if (item && item.longitude) {
            let pushpin = new Microsoft.Maps.Pushpin(new Microsoft.Maps.Location(item.latitude, item.longitude), null);
            pushpin.workItemId = item.workItemId;

            const pinColor = item.workItemPriority ? colorPinMap[item.workItemPriority] : 'purple';

            pushpin.setOptions({ enableHoverStyle: true, enableClickedStyle: false, color: pinColor });
            Microsoft.Maps.Events.addHandler(pushpin, 'click', (e) => {
                console.log(e.target);

                $('#summaryCardNoContent').hide();
                $('#summaryCard').show();

                $('#priorityGlyph')[0].style = 'color:' + pinColor;

                clearAndReplace(summaryDescriptionValueEle, item.description);
                clearAndReplace(summaryStatusValueEle, item.statusName);
                clearAndReplace(summaryTypeEle, item.typeName);
                clearAndReplace(summaryWidValueEle, item.workItemId);
                clearAndReplace(summaryAddressValueEle, item.address);

                summaryStatusValueEle.innerText = item.statusName;
                if (item.assignedTo) {
                    summaryAssignedToEle.innerText = item.assignedTo.principalDisplayName;
                }

                editLinkEle.href = "/Workitems/Edit/" + item.workItemId;
            });
            map.entities.push(pushpin);
        }
    }
}

function mapShow(lat, long, wid) {
    document.getElementById('issueMap').scrollIntoView();
    map.setView({
        center: new Microsoft.Maps.Location(lat, long),
        zoom:15
    });

    const entities = map.entities.getPrimitives();

    for (let i = 0; i < entities.length; i++) {
        if (entities[i].workItemId === wid) {
            Microsoft.Maps.Events.invoke(entities[i], 'click', '');
        }
    }
}