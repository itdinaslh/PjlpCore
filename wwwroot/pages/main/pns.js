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
        responsive: true,
        filter: true,
        stateSave: true,
        orderMulti: false,
        ajax: {
            url: "/api/pegawai/pns",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { data: "namaPegawai", name: "namaPegawai", autoWidth: true },
            { data: "nik", name: "nik", autoWidth: true },
            { data: "nip", name: "bidang", autoWidth: true },
            { data: "nrk", name: "tglLahir", autoWidth: true },
            { data: "noHP", name: "noHP", autoWidth: true },
            {
                render: function (data, type, row) { return "<a class='btn btn-sm btn-outline-success mr-2' href='/pegawai/pns/details/?bid=" + row.bidangID + "&pid=" + row.pegawaiID + "'><i class='fal fa-edit'></i> Detail</a>" }
            }
        ]
    })
}