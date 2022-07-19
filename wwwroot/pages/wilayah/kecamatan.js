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
        stateSave: true,
        orderMulti: false,
        ajax: {
            url: "https://localhost:7248/api/master/kecamatan",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { data: "kecamatanID", name: "kecamatanID", autoWidth: true },
            { data: "namaKecamatan", name: "namaKecamatan", autoWidth: true },
            { data: "namaKabupaten", name: "namaKabupaten", autoWidth: true },
            { data: "namaProvinsi", name: "namaProvinsi", autoWidth: true },
            { data: "latitude", name: "latitude", autoWidth: true },
            { data: "longitude", name: "longitude", autoWidth: true }
        ],
        order: [[0, "desc"]]
    })
}

$(document).on('shown.bs.modal', function () {
    $('#sKabupaten').select2({
        placeholder: 'Pilih Kabupaten/Kota...',
        dropdownParent: $('#myModal'),
        allowClear: true,
        ajax: {
            url: "/api/wilayah/kabupaten/search",
            contentType: "application/json; charset=utf-8",
            data: function (params) {
                var query = {
                    term: params.term,
                };
                return query;
            },
            processResults: function (data) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.namaKabupaten,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
    });
});