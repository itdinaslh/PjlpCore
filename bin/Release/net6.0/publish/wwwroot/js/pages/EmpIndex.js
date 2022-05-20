$(document).ready(function () {
    loadTable();
});

function loadContent() {
    loadTable();
}

function loadTable() {
    $('#tblPegawai').DataTable().clear().destroy();
    $('#tblPegawai').DataTable({
        processing: true,
        serverSide: true,
        responsive: true,
        lengthMenu: [5, 10, 15],
        filter: true,
        orderMulti: false,
        ajax: {
            url: "/api/employee/lists",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { "data": "id", "name": "id" },
            { "data": "empName", "name": "empName" },
            { "data": "empID", "name": "empID" },            
            { "data": "divName", "name": "divName" },
            { "data": "posName", "name": "posName" },
            {
                render: function (data, type, row) { return "<a type='button' class='btn btn-sm btn-outline-success mr-2 showMe' style='width:100%;' href='/emp/details?id=" + row.id + "'><i class='fal fa-edit'></i> Edit</button>" }
            }
        ],
        order: [[0, "desc"]]
    });
}