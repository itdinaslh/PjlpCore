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

$('#PindahinGue').click(function () {
    var id = $(this).attr('data-id');

    Swal.fire({
        title: 'Yakin Pindah?',
        showCancelButton: true,
        confirmButtonText: 'Oke!',
    }).then(function (result) {        
        if (result.value) {
            Pindah(id);
        }
    });
});

function Pindah(theID) {
    $.ajax({
        url: '/pelamar/jenis/pindah',
        type: 'POST',
        data: {
            myID: theID
        },
        success: function (result) {
            if (result.success) {
                showUpdateSuccess();
            }
        }
    })
}