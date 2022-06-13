$(document).ready(function () {
    loadTable();
});

function loadContent() {
    loadTable();
}

function loadTable() {
    $('#tblKabupaten').DataTable().clear().destroy();
    $('#tblKabupaten').DataTable({
        processing: false,
        serverSide: true,
        lengthMenu: [5,10,25,50],
        filter: true,
        stateSave: true,
        orderMulti: false,
        ajax: {
            url: "https://localhost:7248/api/master/kabupaten",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { data: "kabupatenID", name: "kabupatenID", autoWidth: true },
            { data: "namaKabupaten", name: "namaKabupaten", autoWidth: true },
            { data: "namaProvinsi", name: "namaProvinsi", autoWidth: true },
            { data: "latitude", name: "latitude", autoWidth: true },
            { data: "longitude", name: "longitude", autoWidth: true }
        ],
        order: [[0, "desc"]]
    })
}

$(document).on('shown.bs.modal', function () {
    $('#sProvinsi').select2({
        placeholder: 'Pilih Provinsi...',
        dropdownParent: $('#myModal'),
        allowClear: true,
        ajax: {
            url: "/api/wilayah/provinsi/search",
            contentType: "application/json; charset=utf-8",
            data: function (params) {
                var query = {
                    term: params.term,
                };
                return query;
            },
            processResults: function (result) {
                return {
                    results: $.map(result, function (item) {
                        return {
                            text: item.namaProvinsi,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
    });
});