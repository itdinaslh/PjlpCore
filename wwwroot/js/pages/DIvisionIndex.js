$(document).ready(function () {
    loadTable();
});

function loadContent() {
    loadTable();
}

function loadTable() {
    $('#tblDivision').DataTable().clear().destroy();
    $('#tblDivision').DataTable({
        processing: true,
        serverSide: true,
        lengthMenu: [5, 10, 15],
        filter: true,
        orderMulti: false,
        ajax: {
            url: "/api/division",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { "data": "divisionID", "name": "divisionID", "autoWidth": true },
            { "data": "divisionName", "name": "divisionName", "autoWidth": true },
            {
                "render": function (data, type, row) { return "<button class='btn btn-success showMe' data-href='/division/edit/?divID=" + row.divisionID + "'>Edit</button>"; }
            }
        ],
        order: [[0, "desc"]]
    });
}