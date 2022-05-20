$(document).ready(function () {
    $("#sDivision").select2({
        placeholder: 'Pilih Divisi...',
        allowClear: true,
        ajax: {
            url: "/api/division/searchdiv",
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
                            text: item.namadiv,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
    });

    $("#sPosition").select2({
        placeholder: 'Pilih Posisi...',
        allowClear: true,
        ajax: {
            url: "/api/position/searchpos",
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
                            text: item.namapos,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
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

    $('#frmContractData').submit(function (e) {
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
});