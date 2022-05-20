$(document).on('shown.bs.modal', function () {
    $(this).find('[autofocus]').focus();
});

$(document).on('click', '.empProfileImage', function () {
    $('#empImage').click();
});

$(function () {
    Test = {
        UpdatePreview: function (obj) {
            // if IE < 10 doesn't support FileReader
            if (!window.FileReader) {
                // don't know how to proceed to assign src to image tag
            } else {
                var reader = new FileReader();
                var target = null;

                reader.onload = function (e) {
                    target = e.target || e.srcElement;
                    $("#PreviewImage").prop("src", target.result);
                };
                reader.readAsDataURL(obj.files[0]);
            }
        }
    };
});
