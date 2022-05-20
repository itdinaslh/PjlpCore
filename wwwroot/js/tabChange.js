function bindForm(dialog) {
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {
                if (result.success) {
                    $('#myModal').modal('hide');
                    showSuccessMessage();
                    $('#tabNum').text(result.NewTab);
                } else if (result.invalid) {
                    showInvalidMessage();
                } else {
                    $('#myModalContent').html(result);
                    bindForm();
                }
            }
        });
        return false;
    });
}

function showSuccessMessage() {
    Swal.fire(
    {
        type: "success",
        title: "Sukses",
        text: "Data berhasil disimpan!"
    }).then(function() {
        // reloadInfo();
    });
}

function showInvalidMessage() {
    $('.mod-warning').css("visibility", "visible");
}

function reloadInfo() {
    $('#DetailsHeader').load (' #DetailsHeader');
}

$(document).on('click', '.showMe', function() {
    $('#myModalContent').load($(this).attr('data-href'), function () {

        $('#myModal').modal();

        bindForm(this);
    });

    return false;
});
