$(document).ready(function () {
    $("#sProvince").select2({
        placeholder: 'Pilih Provinsi...',
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

    $('#sCity').select2({
        placeholder: 'Pilih Kota/Kabupaten...'
    });
    checkProvinceExist();
});

$('#sProvince').change(function () {
    $('#sCity').val(null).trigger('change');
    var theID = $('#sProvince option:selected').val();
    PopulateCity(theID);
    $('#sCity').select2('focus');
});

$('#ContactData').submit(function (e) {
    e.preventDefault();
    $.ajax({
        url: this.action,
        type: this.method,
        data: $(this).serialize(),
        success: function (result) {
            if (result.success) {
                showSuccessMessage();
            } else {
                alert('Gagal Simpan Data');
            }
        }
    });
});

function showSuccessMessage() {
    Swal.fire({
        position: 'top-end',
        type: 'success',
        title: 'Data berhasil disimpan!',
        showConfirmButton: false,
        timer: 1000
    }).then(function () {
        loadContent();
    });
}

function PopulateCity(CityID) {
    $('#sCity').prop('disabled', false);
    $('#sCity').select2('destroy');
    $("#sCity").select2({
        placeholder: 'Pilih Kota/Kabupaten...',
        allowClear: true,
        ajax: {
            url: "/api/address/searchcity/" + CityID,
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
}

function checkProvinceExist() {
    var provID = $('#sProvince option:selected').val();

    if (provID != null) {
        PopulateCity(provID);
    }
}

