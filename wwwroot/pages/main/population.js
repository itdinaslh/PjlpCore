$('#sGolDarah').select2({
    placeholder: 'Pilih Golongan Darah',
    allowClear: true
});

$('#sKelamin').select2({
    placeholder: 'Pilih Jenis Kelamin',
    allowClear: true
});

$('#sKabDom').select2({
    placeholder: 'Pilih kota/kabupaten'
});

$('#sKecDom').select2({
    placeholder: 'Pilih kecamatan'
});

$('#sKelDom').select2({
    placeholder: 'Pilih kelurahan'
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

function PopulateProvinsi() {
    $('#sProvKTP').select2({
        placeholder: 'Pilih provinsi...',
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
    }).on('change', function () {
        $('#sKabKTP').val(null).trigger('change');
        var theID = $('#sProvKTP option:selected').val();
        PopulateKota(theID);
        $('#sKabKTP').select2('focus');
    });
}

function PopulateProvinsiDom() {
    $('#sProvDom').select2({
        placeholder: 'Pilih provinsi...',
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
    }).on('change', function () {
        $('#sKabDom').val(null).trigger('change');
        var theID = $('#sProvDom option:selected').val();
        PopulateKotaDom(theID);
        $('#sKabDom').select2('focus');
    });
}

function PopulateKota(prov) {
    $('#sKabKTP').select2({
        placeholder: 'Pilih kota/kabupaten...',
        allowClear: true,
        ajax: {
            url: "/api/wilayah/kabupaten/search?prov=" + prov,
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
    }).on('change', function () {
        $('#sKecKTP').val(null).trigger('change');
        var theID = $('#sKabKTP option:selected').val();
        PopulateKecamatan(theID);
        $('#sKecKTP').select2('focus');
    });
}

function PopulateKotaDom(prov) {
    $('#sKabDom').select2({
        placeholder: 'Pilih kota/kabupaten...',
        allowClear: true,
        ajax: {
            url: "/api/wilayah/kabupaten/search?prov=" + prov,
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
    }).on('change', function () {
        $('#sKecDom').val(null).trigger('change');
        var theID = $('#sKabDom option:selected').val();
        PopulateKecamatanDom(theID);
        $('#sKecDom').select2('focus');
    });
}

function PopulateKecamatan(kab) {
    $('#sKecKTP').select2({
        placeholder: 'Pilih kecamatan...',
        allowClear: true,
        ajax: {
            url: "/api/wilayah/kecamatan/search?kab=" + kab,
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
                            text: item.namaKecamatan,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
    }).on('change', function () {
        $('#sKelKTP').val(null).trigger('change');
        var theID = $('#sKecKTP option:selected').val();
        PopulateKelurahan(theID);
        $('#sKelKTP').select2('focus');
    });
}

function PopulateKecamatanDom(kab) {
    $('#sKecDom').select2({
        placeholder: 'Pilih kecamatan...',
        allowClear: true,
        ajax: {
            url: "/api/wilayah/kecamatan/search?kab=" + kab,
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
                            text: item.namaKecamatan,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
    }).on('change', function () {
        $('#sKelDom').val(null).trigger('change');
        var theID = $('#sKecDom option:selected').val();
        PopulateKelurahanDom(theID);
        $('#sKelDom').select2('focus');
    });
}

function PopulateKelurahan(kec) {
    $('#sKelKTP').select2({
        placeholder: 'Pilih kelurahan...',
        allowClear: true,
        ajax: {
            url: "/api/wilayah/kelurahan/search?kec=" + kec,
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
                            text: item.namaKelurahan,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
    });
}

function PopulateKelurahanDom(kec) {
    $('#sKelDom').select2({
        placeholder: 'Pilih kelurahan...',
        allowClear: true,
        ajax: {
            url: "/api/wilayah/kelurahan/search?kec=" + kec,
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
                            text: item.namaKelurahan,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
    });
}

function PopulatePendidikan() {
    $('#sPendidikan').select2({
        placeholder: 'Pilih Pendidikan...',
        allowClear: true,
        ajax: {
            url: "/api/master/pendidikan/search",
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
                            text: item.namaPendidikan,
                            id: item.id
                        }
                    })
                }
            },
            cache: true
        }
    });
}

function PopulateTanggungan() {
    $('#sTanggungan').select2({
        placeholder: 'Pilih Tanggungan...',
        allowClear: true
    });
}