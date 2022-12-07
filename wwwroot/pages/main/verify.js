$('#StatusVerify').submit(function (e) {
    e.preventDefault();

    $.ajax({
        url: this.action,
        type: this.method,
        data: $(this).serialize(),
        success: function (result) {
            if (result.success == "yes") {
                showUpdateSuccess();
                $('#StatusGue').html(result.status);
            }
        }
    })
});