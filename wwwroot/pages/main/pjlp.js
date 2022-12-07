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
            url: "/api/pegawai/pjlp",
            type: "POST",
            dataType: "json"
        },
        columns: [            
            { data: "namaPegawai", name: "namaPegawai", autoWidth: true },
            { data: "nik", name: "nik", autoWidth: true },
            { data: "bidang", name: "bidang", autoWidth: true },           
            { data: "noHP", name: "noHP", autoWidth: true },
            {
                render: function (data, type, row) { return "<a class='btn btn-sm btn-outline-success mr-2' href='/pegawai/pjlp/details/?bid=" + row.bidangID + "&pid=" + row.pegawaiID + "'><i class='fal fa-edit'></i> Detail</a>" }
            }
        ],
        fnRowCallback: function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
            if (aData.blacklist == true) {
                $('td', nRow).addClass('bg-danger-200');
            }
            if (aData.usia > 55 && aData.isk2 == false) {
                $('td', nRow).addClass('bg-warning-200');
            }
            if (aData.isk2 == true && aData.blacklist != true) {
                $('td', nRow).addClass('bg-info-200');
            }
        }
    })
}