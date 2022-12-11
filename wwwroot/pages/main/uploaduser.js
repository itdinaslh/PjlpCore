$('#UploadUser').submit(function (e) {
    e.preventDefault();

    var formdata = new FormData($('#UploadUser')[0]);

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