$(document).ready(function () {
    PopulateAgama();
    PopulateBidang();

    $('#sGolDarah').select2({
        placeholder: 'Pilih Golongan Darah',
        allowClear: true
    });

    $('#sKelamin').select2({
        placeholder: 'Pilih Jenis Kelamin',
        allowClear: true
    });
});

function PopulateAgama() {
    $('#sAgama').select2({
        placeholder: 'Pilih Agama...',        
        allowClear: true,
        ajax: {
            url: "/api/master/agama/search",
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
                            text: item.namaAgama,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
    });
}

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
    });
}