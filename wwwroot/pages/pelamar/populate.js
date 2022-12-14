$(document).ready(function () {
    PopulateBidang();
    PopulateStatus();

    var bid = $('#myBId option:selected').val();

    $('#sJabatan').select2({
        placeholder: 'Pilih Jabatan'
    });

    if (bid != null) {
        PopulateJabatan(bid);
    }
});

$('#sBidang').change(function () {
    $('#sJabatan').val(null).trigger('change');
    var theID = $('#sBidang option:selected').val();
    PopulateJabatan(theID);
    $('#sJabatan').select2('focus');    
    loadContent();
});

$('#sJabatan').change(function () {    
    loadContent();
});

$('#sStatus').change(function () {    
    loadContent();
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
    });
}

function PopulateJabatan(BidangID) {
    //$('#sJabatan').prop('disabled', false);
    //$('#sJabatan').select2('destroy');
    $('#sJabatan').select2({
        placeholder: 'Pilih Jabatan...',
        allowClear: true,
        ajax: {
            url: "/api/master/jabatan/search/?bidang=" + BidangID,
            contentType: "application/json; charset=utf-8",
            data: function (params) {
                var query = {
                    term: params.term
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
    });
}

function PopulateStatus() {
    $('#sStatus').select2({
        placeholder: 'Pilih Status...',
        allowClear: true,
        ajax: {
            url: "/api/master/status/search",
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
                            text: item.namaStatus,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
    });
}