// #region data input
    getDataInput("#namalengkap", "#v-nama-lengkap")
    getDataInput("#no_tel", "#v-tel")
    getDataInput("#no_ktp", "#v-ktp")
    getDataInput("#no_kk", "#v-kk")
    getDataSelect("#kelamin", "#v-kelamin")
    getDataSelect("#gol_darah", "#v-darah")
    getDataSelect("#agama", "#v-agama")
    getDataInput("#pendidikan", "#v-pendidikan")
    getDataInput("#alamat_ktp", "#v-alamat")
    getDataInput("#alamat_dom", "#v-alamat-dom")
    getDataInput("#rt", "#v-rt")
    getDataInput("#rw", "#v-rw")
    getDataInput("#rt_dom", "#v-rt-dom")
    getDataInput("#rw_dom", "#v-rw-dom")
    getDataSelect("#kelurahan", "#v-kelurahan")
    getDataSelect("#kecamatan", "#v-kecamatan")
    getDataSelect("#kota", "#v-kota")
    getDataSelect("#provinsi", "#v-provinsi")
    getDataSelect("#kelurahan_dom", "#v-kelurahan-dom")
    getDataSelect("#kecamatan_dom", "#v-kecamatan-dom")
    getDataSelect("#kota_dom", "#v-kota-dom")
    getDataSelect("#provinsi_dom", "#v-provinsi-dom")
    getDataInput("#kode_pos", "#v-kode-pos")
    getDataInput("#kode_pos_dom", "#v-kode-pos-dom")
    getDataDate("#tgl_lahir", "#v-tgl-lahir")
    getDataInput("#tempat_lahir", "#v-tempat-lahir")
    getDataSelect("#pendidikan", "#v-pendidikan")
    getDataInput("#npwp", "#v-npwp")
    getDataInput("#bpjs", "#v-bpjs")
    getDataInput("#bpjsk", "#v-bpjsk")
    getDataSelect("#status_bpjs", "#v-status-bpjs")
    getDataInput("#sim_data", "#v-sim-data")
    getDataDate("#sim_date", "#v-sim-date")
    getDataInput("#nama_bank", "#v-nama-bank")
    getDataInput("#no_rek", "#v-no-rek")
    getDataSelect("#jabatan", "#v-jabatan")
    getDataSelect("#bidang", "#v-bidang")
// #endregion

$(document).ready(function() {
    PopulateProvince();
    PopulateProvinceDom();
    PopulateAgama();
    PopulatePendidikan();

    $('#kota').select2({
        placeholder: 'Pilih Kota...'
    });

    $('#kecamatan').select2({
        placeholder: 'Pilih Kecamatan...'
    });

    $('#kelurahan').select2({
        placeholder: 'Pilih Kelurahan...'
    });

    $('#kota_dom').select2({
        placeholder: 'Pilih Kota Domisili...'
    });

    $('#kecamatan_dom').select2({
        placeholder: 'Pilih Kecamatan Domisili...'
    });

    $('#kelurahan_dom').select2({
        placeholder: 'Pilih Kelurahan Domisili...'
    });

    $('#jabatan').select2({
        placeholder: 'Pilih Jabatan...'
    });

    PopulateBidang();

    var bidang = $('#bidang option:selected').val();

    PopulateJabatan(bidang);

    $('.dtpicker').datepicker({
        format: 'dd-mm-yyyy',
        orientation: 'bottom'
    });
});

$('#chkSame').change(function () {
    if (!this.checked) {
        $('#myDomisili').show();
        $('#nextSame').hide();
        $('.thisDom').prop('required', true);
    } else {
        $('#myDomisili').hide();
        $('#nextSame').show();
        $('.thisDom').prop('required', false);
    }
})

// #region getData
    function getDataSelect(form, preview){
        $(form).change(function() {
            $(preview).text($(form+" option").filter(':selected').text())
        })
    }

    function getDataDate(form, preview){
        $(preview).text($(form).val())
        $(form).change(function(){
            $(preview).text($(this).val())
        })
    }

    function getDataInput(form, preview){
        $(preview).text($(form).val())
        $(form).change(function() {
            $(preview).text($(this).val())
        })
    }
// #endregion

// #region Populate Select2
    function PopulateProvince() {
        $('#provinsi').select2({
            placeholder: 'Pilih Provinsi...',
            allowClear: true,
            ajax: {
                url: "/api/wilayah/provinsi/search",
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
                                text: item.namaProvinsi,
                                id: item.id
                            }
                        })
                    }
                },
                cache: true
            }
        }).on('change', function() {
            $('#kota').val(null).trigger('change');
            var theID = $('#provinsi option:selected').val();
            PopulateCity(theID);
            $('#kota').select2('focus');
        });
    }

    function PopulateCity(ProvID) {
        $('#kota').prop('disabled', false);
        $('#kota').select2('destroy');
        $('#kota').select2({
            placeholder: 'Pilih Kota/Kabupaten...',
            allowClear: true,
            ajax: {
                url: "/api/wilayah/kabupaten/search/?prov=" + ProvID,
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
                                text: item.namaKabupaten,
                                id: item.id
                            }
                        })
                    }
                },
                cache: true
            }
        }).on('change', function() {
            $('#kecamatan').val(null).trigger('change');
            var theID = $('#kota option:selected').val();
            PopulateKecamatan(theID);
            $('#kecamatan').select2('focus');
        });
    }

    function PopulateKecamatan(CityID) {
        $('#kecamatan').prop('disabled', false);
        $('#kecamatan').select2('destroy');
        $('#kecamatan').select2({
            placeholder: 'Pilih Kecamatan...',
            allowClear: true,
            ajax: {
                url: "/api/wilayah/kecamatan/search/?kab=" + CityID,
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
                                text: item.namaKecamatan,
                                id: item.id
                            }
                        })
                    }
                },
                cache: true
            }
        }).on('change', function() {
            $('#kelurahan').val(null).trigger('change');
            var theID = $('#kecamatan option:selected').val();
            PopulateKelurahan(theID);
            $('#kelurahan').select2('focus');
        });
    }

    function PopulateKelurahan(KecamatanID) {
        $('#kelurahan').prop('disabled', false);
        $('#kelurahan').select2('destroy');
        $('#kelurahan').select2({
            placeholder: 'Pilih Kelurahan...',
            allowClear: true,
            ajax: {
                url: "/api/wilayah/kelurahan/search/?kec=" + KecamatanID,
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

    function PopulateProvinceDom() {
        $('#provinsi_dom').select2({
            placeholder: 'Pilih Provinsi Domisili...',
            allowClear: true,
            ajax: {
                url: "/api/wilayah/provinsi/search",
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
                                text: item.namaProvinsi,
                                id: item.id
                            }
                        })
                    }
                },
                cache: true
            }
        }).on('change', function () {
            $('#kota_dom').val(null).trigger('change');
            var theID = $('#provinsi_dom option:selected').val();
            PopulateCityDom(theID);
            $('#kota_dom').select2('focus');
        });
    }

    function PopulateCityDom(ProvID) {
        $('#kota_dom').prop('disabled', false);
        $('#kota_dom').select2('destroy');
        $('#kota_dom').select2({
            placeholder: 'Pilih Kota/Kabupaten Domisili...',
            allowClear: true,
            ajax: {
                url: "/api/wilayah/kabupaten/search/?prov=" + ProvID,
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
                                text: item.namaKabupaten,
                                id: item.id
                            }
                        })
                    }
                },
                cache: true
            }
        }).on('change', function() {
            $('#kecamatan_dom').val(null).trigger('change');
            var theID = $('#kota_dom option:selected').val();
            PopulateKecamatanDom(theID);
            $('#kecamatan_dom').select2('focus');
        });
    }

    function PopulateKecamatanDom(CityID) {
        $('#kecamatan_dom').prop('disabled', false);
        $('#kecamatan_dom').select2('destroy');
        $('#kecamatan_dom').select2({
            placeholder: 'Pilih Kecamatan Domisili...',
            allowClear: true,
            ajax: {
                url: "/api/wilayah/kecamatan/search/?kab=" + CityID,
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
                                text: item.namaKecamatan,
                                id: item.id
                            }
                        })
                    }
                },
                cache: true
            }
        }).on('change', function() {
            $('#kelurahan_dom').val(null).trigger('change');
            var theID = $('#kecamatan_dom option:selected').val();
            PopulateKelurahanDom(theID);
            $('#kelurahan_dom').select2('focus');
        });
    }

    function PopulateKelurahanDom(KecamatanID) {
        $('#kelurahan_dom').prop('disabled', false);
        $('#kelurahan_dom').select2('destroy');
        $('#kelurahan_dom').select2({
            placeholder: 'Pilih Kelurahan Domisili...',
            allowClear: true,
            ajax: {
                url: "/api/wilayah/kelurahan/search/?kec=" + KecamatanID,
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

    function PopulateBidang() {
        $('#bidang').select2({
            placeholder: 'Pilih Bidang...',
            allowClear: true,
            ajax: {
                url: "/api/master/bidang/search",
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
        $('#jabatan').prop('disabled', false);
        $('#jabatan').select2('destroy');
        $('#jabatan').select2({
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

    $('#bidang').change(function () {
        $('#jabatan').val(null).trigger('change');
        var theID = $('#bidang option:selected').val();
        PopulateJabatan(theID);
        $('#jabatan').select2('focus');
    });

    function PopulatePendidikan() {
        $('#pendidikan').select2({
            placeholder: 'Pilih Pendidikan...',
            allowClear: true,
            ajax: {
                url: "/api/master/pendidikan/search",
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

    function PopulateAgama() {
        $('#agama').select2({
            placeholder: 'Pilih Agama...',
            allowClear: true,
            ajax: {
                url: "/api/master/agama/search",
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
// #endregion


