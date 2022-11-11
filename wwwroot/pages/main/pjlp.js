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
        lengthMenu: [5, 10, 25, 50],
        filter: true,
        stateSave: true,
        orderMulti: false,
        ajax: {
            url: "/api/pegawai/pjlp",
            type: "POST",
            dataType: "json"
        },
        columns: [            
            { data: "namaPegawai", name: "namaPegawai", autoWidth: true },
            { data: "nik", name: "nik", autoWidth: true },
            { data: "bidang", name: "bidang", autoWidth: true },
            { data: "tglLahir", name: "tglLahir", autoWidth: true },
            { data: "noHP", name: "noHP", autoWidth: true }
        ],
        order: [[0, "desc"]]
    })
}