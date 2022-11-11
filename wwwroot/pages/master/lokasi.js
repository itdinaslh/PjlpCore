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
        orderMulti: false,
        ajax: {
            url: "/api/master/lokasi-kerja",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { data: "lokasiKerjaID", name: "lokasiKerjaID", autoWidth: true },
            { data: "namaLokasi", name: "namaLokasi", autoWidth: true },
            {
                render: function (data, type, row) { return "<button type='button' class='btn btn-sm btn-outline-success mr-2 showMe' style='width:100%;' data-href='/master/lokasi-kerja/edit/?locID=" + row.lokasiKerjaID + "'><i class='fal fa-edit'></i> Edit</button>" }
            }
        ],
        order: [[0, "desc"]]
    })
}

