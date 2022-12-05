$(document).ready(function () {
    loadTable();
});

function loadContent() {
    loadTable();
}

function loadTable() {
    $('#tblData').DataTable().clear().destroy();
    $('#tblData').DataTable({
        processing: false,
        serverSide: true,
        lengthMenu: [5,10,25,50],
        filter: true,
        orderMulti: false,
        ajax: {
            url: "/api/userbidang/list",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { data: "userID", name: "userID", autoWidth: true },
            { data: "userName", name: "userName", autoWidth: true },
            { data: "name", name: "name", autoWidth: true },            
            { data: "roleName", name: "roleName", autoWidth: true },
            {
                render: function (data, type, row) { return "<button type='button' class='btn btn-sm btn-outline-success mr-2 showMe' style='width:100%;' data-href='/userbidang/manage/?userID=" + row.userID + "'><i class='fal fa-edit'></i> Manage</button>" }
            }
        ],
        order: [[0, "desc"]]
    })
}