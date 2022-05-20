$(document).ready(function () {
    loadTable();
});

function loadContent() {
    loadTable();
}

function loadTable() {
    $('#tblProvince').DataTable().clear().destroy();
    $('#tblProvince').DataTable({
        processing: true,
        serverSide: true,
        lengthMenu: [5, 10, 15],
        filter: true,
        orderMulti: false,
        ajax: {
            url: "/api/address/provinces",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { "data": "provinceID", "name": "provinceID", "autoWidth": true },
            { "data": "provinceName", "name": "provinceName", "autoWidth": true },
            { "data": "isoNum", "name": "isoNum", "autoWidth": true, searchable: false },
            { "data": "capitalName", "name": "capitalName", "autoWidth": true },
            {
                "render": function (data, type, row) { return "<button class='btn btn-sm btn-outline-success mr-2 showMe' data-href='/address/province/edit/?provID=" + row.provinceID + "'><i class='fal fa-edit'></i> Edit</button>"; }
            }
        ],
        order: [[0, "desc"]]
    });
}