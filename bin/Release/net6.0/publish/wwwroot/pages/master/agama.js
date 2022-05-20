$(document).ready(function () {
    loadTable();
});

function loadContent() {
    loadTable();
}

function loadTable() {
    $('#tblAgama').DataTable().clear().destroy();
    $('#tblAgama').DataTable({
        processing: false,
        serverSide: true,
        lengthMenu: [5,10,25,50],
        filter: true,
        orderMulti: false,
        ajax: {
            url: "/api/master/agama",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { data: "agamaID", name: "agamaID", autoWidth: true },
            { data: "namaAgama", name: "namaAgama", autoWidth: true },
            {
                render: function (data, type, row) { return "<button type='button' class='btn btn-sm btn-outline-success mr-2 showMe' style='width:100%;' data-href='/master/agama/edit/?agamaId=" + row.agamaID + "'><i class='fal fa-edit'></i> Edit</button>" }
            }
        ],
        order: [[0, "desc"]]
    })
}

