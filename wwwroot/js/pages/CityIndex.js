$(document).ready(function () {
    loadTable();
});

function loadContent() {
    loadTable();
}

function loadTable() {
    $('#tblCity').DataTable().clear().destroy();
    $('#tblCity').DataTable({
        processing: true,
        serverSide: true,
        lengthMenu: [5, 10, 15],
        filter: true,
        orderMulti: false,
        ajax: {
            url: "/api/address/cities",
            type: "POST",
            dataType: "json"
        },
        columns: [
            { "data": "cityID", "name": "cityID", "autoWidth": true },
            { "data": "cityName", "name": "cityName", "autoWidth": true },
            { "data": "provinceName", "name": "provinceName", "autoWidth": true },
            {
                "render": function (data, type, row) { return "<button class='btn btn-sm btn-outline-success mr-2 showMe' data-href='/address/city/edit/?cityID=" + row.cityID + "'><i class='fal fa-edit'></i> Edit</button>"; }
            }
        ],
        order: [[0, "desc"]]
    });

    $(document).on('shown.bs.modal', function () {
        $('#sCity').select2({
            placeholder: 'Pilih Provinsi...',
            dropdownParent: $('#myModal'),
            allowClear: true,
            ajax: {
                url: "/api/address/searchprovince",
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
                                text: item.namaprovinsi,
                                id: item.id
                            }
                        })
                    }
                },
                cache: true
            }
        });
    });
}

function showSuccessMessage() {
    Swal.fire(
        {
            position: 'top-end',
            type: 'success',
            title: 'Data berhasil disimpan!',
            showConfirmButton: false,
            timer: 1000
        }).then(function () {
            loadContent();
        });
}