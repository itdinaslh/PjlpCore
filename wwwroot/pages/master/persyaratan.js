$(document).ready(function () {
    loadTable();
});

function loadContent() {
    loadTable();
}

function loadTable() {
    $('#tblPersyaratan').DataTable().clear().destroy();
    $('#tblPersyaratan').DataTable({
        processing: false,
        serverSide: true,
        lengthMenu: [5,10,25,50],
        filter: true,
        orderMulti: false,
        ajax: {
            url: "/api/master/persyaratan",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { data: "persyaratanID", name: "persyaratanID", autoWidth: true },
            { data: "namaPersyaratan", name: "namaPersyaratan", autoWidth: true },
            {
                render: function (data, type, row) { return "<button type='button' class='btn btn-sm btn-outline-success mr-2 showMe' style='width:100%;' data-href='/master/persyaratan/edit/?persyaratanId=" + row.persyaratanID + "'><i class='fal fa-edit'></i> Edit</button>" }
            }
        ],
        order: [[0, "desc"]]
    })
}

