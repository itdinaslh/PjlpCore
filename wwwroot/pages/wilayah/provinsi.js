$(document).ready(function () {
    loadTable();
});

function loadContent() {
    loadTable();
}

function loadTable() {
    $('#tblProvinsi').DataTable().clear().destroy();
    $('#tblProvinsi').DataTable({
        processing: false,
        serverSide: true,
        lengthMenu: [5,10,25,50],
        filter: true,
        stateSave: true,
        orderMulti: false,
        ajax: {
            url: "https://localhost:7248/api/master/provinsi",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { data: "provinsiID", name: "provinsiID", autoWidth: true },
            { data: "namaProvinsi", name: "namaProvinsi", autoWidth: true },
            { data: "hcKey", name: "hcKey", autoWidth: true },
            { data: "latitude", name: "latitude", autoWidth: true },
            { data: "longitude", name: "longitude", autoWidth: true }
        ],
        order: [[0, "desc"]]
    })
}