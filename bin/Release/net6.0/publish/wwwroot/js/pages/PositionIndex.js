$(document).ready(function () {
    loadTable();
});

function loadContent() {
    loadTable();
}

function loadTable() {
    $('#tblJabatan').DataTable().clear().destroy();
    $('#tblJabatan').DataTable({
        processing: true,
        serverSide: true,
        lengthMenu: [5, 10, 15],
        filter: true,
        orderMulti: false,
        ajax: {
            url: "/api/position",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { "data": "positionID", "name": "positionID", "autoWidth": true },
            { "data": "positionName", "name": "positionName", "autoWidth": true },
            {
                render: function (data, type, row) { return "<button type='button' class='btn btn-sm btn-outline-success mr-2 showMe' data-href='/position/edit/?jabID=" + row.positionID + "'><i class='fal fa-edit'></i> Edit</button>" }
            }
        ],
        order: [[0, "desc"]]
    });
}