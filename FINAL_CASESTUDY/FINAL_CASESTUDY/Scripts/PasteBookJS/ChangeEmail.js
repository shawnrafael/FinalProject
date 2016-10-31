$(document).ready(function() {
    $('#changeUserEmail').on('click', function () {
        CheckUserEmailExist()
    });

    $('#newInputEmail').bind('blur keydown', function () {
        $('#invalidEmail').hide();
    });
});

function CheckUserEmailExist() {
    var data = {
        email: $('#newInputEmail').val()
    }
    $.ajax({
        url: checkInputEmailUrl,
        data: data,
        type: 'POST',
        success: function (data) {
            if (data.emailExist == true) {
                $('#successChangeEmail').hide();
                $('#invalidEmail').text('Email already exist.')
                $('#invalidEmail').css('display', 'inline');
            } else {
                $('#invalidEmail').css('display', 'none');
                $('#editEmailForm').submit();
            }

        },
        error: function () {
            window.location.href(errorPageUrl);
        }
    });
}