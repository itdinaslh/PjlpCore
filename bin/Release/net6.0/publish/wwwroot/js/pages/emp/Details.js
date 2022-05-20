$(document).ready(function () {
    $('#MaritalStatus').select2({});
    $('#GenderID').select2();
    $('.datepicker').datepicker({
        format: 'yyyy-mm-dd',
        orientation: 'bottom'
    });
});

$('#PersonalData').submit(function (e) {
    e.preventDefault();
    $.ajax({
        url: this.action,
        type: this.method,
        data: $(this).serialize(),
        success: function (result) {
            if (result.success) {
                showSuccessMessage();
            } else {
                showFailedMessage();
            }
        }
    });
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

function showFailedMessage() {
    Swal.fire({
        position: 'top-end',
        type: 'danger',
        title: 'Data gagal disimpan!',
        showConfirmButton: false,
        timer: 1000
    }).then(function () {
         loadContent();
     });
}