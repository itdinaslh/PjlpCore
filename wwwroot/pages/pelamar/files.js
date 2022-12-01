$(document).ready(function () {
    PopulateBidang();

    $('.myChk').prop('checked', false);

    $('#sJabatan').select2({
        placeholder: 'Pilih Jabatan...'
    });
});

function PopulateBidang() {
    $('#sBidang').select2({
        placeholder: 'Pilih Bidang...',
        allowClear: true,
        ajax: {
            url: "/api/master/bidang/search",
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
                            text: item.namaBidang,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
    }).on('change', function () {
        $('.myChk').prop('checked', false);
        $('#sJabatan').val(null).trigger('change');
        var theID = $('#sBidang option:selected').val();
        PopulateJabatan(theID);
    });
}

function PopulateJabatan(bid) {
    $('#sJabatan').select2({
        placeholder: 'Pilih Jabatan...',
        allowClear: true,
        dropdownPosition: 'below',
        ajax: {
            url: "/api/master/jabatan/search?bidang=" + bid,
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
                            text: item.namaJabatan,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
    }).on('change', function () {
        checkFiles();
    });
}

$('#formdata').submit(function (e) {
    e.preventDefault();

    $.ajax({
        url: this.action,
        type: this.method,
        data: $(this).serialize(),
        success: function (result) {
            if (result.success) {
                showSuccessMessage();
            }
        }
    })
});

function checkFiles() {
    var baru = $('#theNew option:selected').val();
    var jabatan = $('#sJabatan option:selected').val();

    $.ajax({
        url: '/api/pelamar/files/check/?jab=' + jabatan + '&isNew=' + baru,
        type: 'GET',
        success: function (result) {
            if (!result.failed) {
                $('.myChk').prop('checked', false);
                $.each(result, function (id, val) {                    
                    $('#chk' + val).prop('checked', true);
                });
            } else {
                $('.myChk').prop('checked', false);
            }
        }
    });
}

$('#SelectAll').click(function () {
    $('.myChk').prop('checked', true);
});

$('#ClearAll').click(function () {
    $('.myChk').prop('checked', false);
});

function showSuccessMessage() {
    swal({
        position: 'top-end',
        type: 'success',
        title: 'Data berhasil disimpan',
        showConfirmButton: false,
        timer: 1000
    });
}