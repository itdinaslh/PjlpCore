$(document).ready(function () {
    PopulateAgama();
    PopulateBidang();
    PopulateProvinsi();    
    PopulatePendidikan();
    PopulateTanggungan();
    PopulateFileTypes();
    checkAddrIsSame();

    var isValidFile = $('#IsValidFile').val();

    if (isValidFile == 'true') {
        $('#DownloadDiv').show();
    }

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

    var thisBid = $('#sBidang option:selected').val();

    PopulateJabatan(thisBid);

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
                $('#DownloadDiv').show();

                if (result.isnew == "no") {
                    $('#File-' + result.oldid).remove();                    
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
    var newFile = `<div id="File-` + newid + `" class="col - lg - 4">
                            <div class="card mb-4">
                                <div class="card-header text-center bg-primary-500 bg-success-gradient">
                                    <h5>` + type + `</h5>
                                </div>
                                <input type="checkbox" id="` + newid + `" class="myChk" name="Files" value="` + newid + `" />
                                <label for="` + newid + `" class="lblImage">
                                    <img class="card-img-top" src="` + filePath + `" alt="file" style="height: 250px; ">
                                </label>
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

$(document).on('click', '.btnDownload', function () {
    var id = $(this).attr('data-id');
    var datahref = '/pegawai/files/download/single/?fileID=' + id;

    $.ajax({
        url: datahref,
        type: 'GET',
        success: function () {
            window.location.href = datahref
        }
    });
});

$(document).on('click', '#ChooseAll', function () {
    $('.myChk').prop('checked', true);
});

$(document).on('click', '#DownSelected', function () {
    var files = [];

    $('.myChk:checked').each(function () {
        var value = $(this).val();
        files.push(value);             
    });

    if (files.length < 1) {
        alert("Pilih file terlebih dahulu");
    } else {
        DownloadASelected(files);
    }
});

$(document).on('click', '#DownAll', function () {
    var id = $(this).attr('data-id');

    DownloadAllFile(id);
});

function DownloadAllFile(list) {
    $.ajax({
        url: '/pegawai/files/download/all',
        type: "post",
        data: {
            myID: list
        },
        xhrFields: {
            responseType: 'blob'
        },
        beforeSend: function (xhr) {
            xhr.setRequestHeader("RequestVerificationToken",
                $('input:hidden[name="__RequestVerificationToken"]').val());

        },
        success: function (data, textStatus, xhr) {
            // check for a filename
            var filename = "";
            var disposition = xhr.getResponseHeader('Content-Disposition');
            if (disposition && disposition.indexOf('attachment') !== -1) {
                var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                var matches = filenameRegex.exec(disposition);
                if (matches != null && matches[1]) filename = matches[1].replace(/['"]/g, '');
                var a = document.createElement('a');
                var url = window.URL.createObjectURL(data);
                a.href = url;
                a.download = filename;
                document.body.append(a);
                a.click();
                a.remove();
                window.URL.revokeObjectURL(url);
            }
            else {
                getErrorToastMessage("Production file for order line " + list[i].orderLineId + " does not exist");
            }
            i = i + 1;
            if (i < max) {
                DownloadAllFile(list);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        },
        complete: function () {
            if (i === max)
                hidePreloader();
        }
    });
}

function DownloadASelected(list) {
    $.ajax({
        url: '/pegawai/files/download/selected',
        type: "post",
        data: {
            Files : list
        },
        xhrFields: {
            responseType: 'blob'
        },
        beforeSend: function (xhr) {
            xhr.setRequestHeader("RequestVerificationToken",
                $('input:hidden[name="__RequestVerificationToken"]').val());

        },
        success: function (data, textStatus, xhr) {
            // check for a filename
            var filename = "";
            var disposition = xhr.getResponseHeader('Content-Disposition');
            if (disposition && disposition.indexOf('attachment') !== -1) {
                var filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
                var matches = filenameRegex.exec(disposition);
                if (matches != null && matches[1]) filename = matches[1].replace(/['"]/g, '');
                var a = document.createElement('a');
                var url = window.URL.createObjectURL(data);
                a.href = url;
                a.download = filename;
                document.body.append(a);
                a.click();
                a.remove();
                window.URL.revokeObjectURL(url);
            }
            else {
                getErrorToastMessage("Production file for order line " + list[i].orderLineId + " does not exist");
            }
            i = i + 1;
            if (i < max) {
                DownloadAllFile(list);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        },
        complete: function () {
            if (i === max)
                hidePreloader();
        }
    });
}