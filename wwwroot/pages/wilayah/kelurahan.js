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
            url: "/api/wilayah/kelurahan",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { data: "kelurahanID", name: "kelurahanID", autoWidth: true },
            { data: "namaKelurahan", name: "namaKelurahan", autoWidth: true },
            { data: "namaKecamatan", name: "namaKecamatan", autoWidth: true },
            { data: "namaKabupaten", name: "namaKabupaten", autoWidth: true },
            { data: "namaProvinsi", name: "namaProvinsi", autoWidth: true },            
            {
                render: function (data, type, row) { return "<button type='button' class='btn btn-sm btn-outline-success mr-2 showMe' style='width:100%;' data-href='/wilayah/kelurahan/edit/?kelurahanID=" + row.kelurahanID + "'><i class='fal fa-edit'></i> Edit</button>" }
            }
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
            processResults: function (result) {
                return {
                    results: $.map(result, function (item) {
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