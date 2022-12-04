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

function showSuccessMessage() {
    swal({
        position: 'top-end',
        type: 'success',
        title: 'Data berhasil disimpan',
        showConfirmButton: false,
        timer: 1000
    });
}