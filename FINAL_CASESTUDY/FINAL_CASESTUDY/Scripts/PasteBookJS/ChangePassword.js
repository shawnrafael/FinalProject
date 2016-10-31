$(document).ready(function () {
    $('#changePassword').on('click', function () {

        CheckConfirmPassword()
    });
});

function CheckConfirmPassword() {
    var oldPassword = $('#oldPassword').val();
    var newPassword = $('#newPassword').val();
    var confirmPassword = $('#confirmPassword').val();

    if (oldPassword == '') {
        $('#successChangePass').hide();
        $('#invalidPassword').text("Old password field is required.");
    }
    if (newPassword == '') {
        $('#successChangePass').hide();
    }
    if (confirmPassword == '') {
        $('#successChangePass').hide();
        $('#errorConfirm').text("Confirm password field is required.")
    }

    if (newPassword != confirmPassword) {
        $('#successChangePass').hide();
        $('#errorConfirm').text('Password does not match to confirm password')
    } else {
        $('#changePasswordForm').submit();
    }
}