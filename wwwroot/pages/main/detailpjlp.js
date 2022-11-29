$(document).ready(function () {
    PopulateAgama();
    PopulateBidang();
    PopulateProvinsi();    
    PopulatePendidikan();
    PopulateTanggungan();
    PopulateFileTypes();
    checkAddrIsSame();

    var provid = $('#provid').val();
    var kabid = $('#kabid').val();
    var kecid = $('#kecid').val();
    var provdomid = $('#provdomid').val();
    var kabdomid = $('#kabdomid').val();
    var kecdomid = $('#kecdomid').val();

    if ($('#chkSame').prop('checked')) {
        $('#domisili').hide();
        $('.domfield').removeAttr('required');
        
    } else {
        $('#domisili').show();
        $('.domfield').prop('required', true);
        PopulateProvinsiDom();      
    }

    $('.dtpicker').datepicker({
        format: 'dd-mm-yyyy',
        orientation: 'bottom'
    });

    if (!provid) {
        $('#sKabKTP').select2({
            placeholder: 'Pilih kota/kabupaten'
        });
    } else {
        PopulateKota(provid);
    }

    if (!kabid) {
        $('#sKecKTP').select2({
            placeholder: 'Pilih kecamatan'
        });
    } else {
        PopulateKecamatan(kabid);
    }

    if (!kecid) {
        $('#sKelKTP').select2({
            placeholder: 'Pilih kelurahan'
        });
    } else {
        PopulateKelurahan(kecid);
    }

    if (!provdomid) {
        $('#sKabDom').select2({
            placeholder: 'Pilih kota/kabupaten'
        });
    } else {
        PopulateKotaDom(provdomid);
    }

    if (!kabdomid) {
        $('#sKecDom').select2({
            placeholder: 'Pilih kecamatan'
        });
    } else {
        PopulateKecamatanDom(kabdomid);
    }

    if (!kecdomid) {
        $('#sKelDom').select2({
            placeholder: 'Pilih kelurahan'
        });
    } else {
        PopulateKelurahanDom(kecdomid);
    }

    $('#sStatusBPJS').select2({
        placeholder: 'Pilih Status BPJS'
    });
});

$('.formdata').submit(function (e) {
    e.preventDefault();

    $.ajax({
        url: this.action,
        type: this.method,
        data: $(this).serialize(),
        success: function (result) {
            if (result.success) {
                showUpdateSuccess();
            }
        }
    })
});

$('#UploadFile').submit(function (e) {
    e.preventDefault();

    var formdata = new FormData($('#UploadFile')[0]);

    $.ajax({
        url: this.action,
        type: this.method,
        data: formdata,
        processData: false,
        contentType: false,
        success: function (result) {
            if (result.success == "yes") {
                showUploadSuccess();

                if (result.isnew == "no") {
                    $('#' + result.oldid).remove();                    
                }

                if (result.isnewfoto == "yes") {
                    $('#pas-foto').attr('src', result.path);
                }

                addFiles(result.path, result.created_at, result.newid, result.type);

                clearUpload();
            }
        }
    })
});

$('#chkSame').change(function () {
    if (!this.checked) {
        $('#domisili').show();
        PopulateProvinsiDom();
        $('#sProvDom').prop('required', true);
        $('#sKabDom').prop('required', true);
        $('#sKecDom').prop('required', true);
        $('#sKelDom').prop('required', true);
        $('.domfield').prop('required', true);
    } else {
        $('#domisili').hide();
        $('#sProvDom').prop('required', false);
        $('#sKabDom').prop('required', false);
        $('#sKecDom').prop('required', true);
        $('#sKelDom').prop('required', true);
        $('.domfield').removeAttr('required');
    }
});

function checkAddrIsSame() {
    $('#chkSame').change(function () {
        if (!this.checked) {
            $('#domisili').show();
            PopulateProvinsiDom();
        } else {
            $('#domisili').hide();
        }
    });
}

function showUpdateSuccess() {
    swal({
        position: 'top-end',
        type: 'success',
        title: 'Data berhasil update',
        showConfirmButton: false,
        timer: 1000
    });
}

function showUploadSuccess() {
    swal({
        position: 'top-end',
        type: 'success',
        title: 'Data berhasil upload',
        showConfirmButton: false,
        timer: 1000
    });
}

function showFailedMessage() {
    swal({
        position: 'top-end',
        type: 'danger',
        title: 'Data gagal diupdate',
        showConfirmButton: false,
        timer: 1000
    });
}

function addFiles(filePath, created, newid, type) {
    var newFile = `<div class="col-lg-4" id="` + newid + `">
                            <div class="card mb-4">
                                <div class="card-header text-center bg-primary-500 bg-success-gradient">
                                    <h5>` + type + `</h5>
                                </div>
                                <img class="card-img-top" src="` + filePath + `" alt="file" style="height: 250px; ">
                                <div class="card-body">
                                    <table class="table">
                                        <tr>
                                            <td>Tgl Upload</td>
                                            <td style="padding-left:5px;padding-right:5px;">:</td>
                                            <td>` + created + `</td>
                                        </tr>
                                    </table>
                                    <div class="text-center">
                                        <button class="btn btn-primary">Download</button>
                                    </div>
                                </div>
                            </div>
                        </div>`

    $('#AllFiles').prepend(newFile);
}

function clearUpload() {
    $('#sFile').val(null).trigger('change');
    $('#filePilih').val(null).change();
}