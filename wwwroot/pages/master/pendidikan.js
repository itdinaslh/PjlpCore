$(document).ready(function () {
    loadTable();
});

function loadContent() {
    loadTable();
}

function loadTable() {
    $('#tblPendidikan').DataTable().clear().destroy();
    $('#tblPendidikan').DataTable({
        processing: false,
        serverSide: true,
        lengthMenu: [5, 10, 25, 50],
        stateSave: true,
        filter: true,
        orderMulti: false,
        ajax: {
            url: "/api/master/pendidikan",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { data: "pendidikanID", name: "pendidikanID", autoWidth: true },
            { data: "namaPendidikan", name: "namaPendidikan", autoWidth: true },
            {
                render: function (data, type, row) { return "<button type='button' class='btn btn-sm btn-outline-success mr-2 showMe' style='width:100%;' data-href='/master/pendidikan/edit/?pendidikanId=" + row.pendidikanID + "'><i class='fal fa-edit'></i> Edit</button>" }
            }
        ],
        order: [[0, "desc"]]
    })
}

