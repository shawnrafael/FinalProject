jQuery.extend(jQuery.validator.messages, {
    max: jQuery.validator.format("Birthday must not be a date later than today.")
});

$(document).ready(function () {

    $('#registerUsername').on('blur', function () {
        CheckUsernameAvailability();
        CheckIfNull($(this).val(), "Username", "errorUsername")
    });

    $('#registerEmail').on('blur', function () {
        CheckEmailAvailability();
        CheckIfNull($(this).val(), "Email", "errorEmail");
    });

    $('#registerFirstName').on('blur', function () {
        CheckIfNull($(this).val(), 'First name', 'errorFirstName');
    });

    $('#registerLastName').on('blur', function () {
        CheckIfNull($(this).val(), 'Last name', 'errorLastName');
    });

    $('#confirmPassword').on('blur', function () {
        PasswordNotMatch();
        CheckIfNull($(this).val(), 'Confirm password', 'errorConfirm');
    });

    $('#userPassword').on('blur', function () {
        PasswordNotMatch();
        CheckIfNull($(this).val(), 'Password', 'errorPassword');
    });

    $('#registerBday').on('blur', function () {
        CheckIfNull($(this).val(), 'Birthday', 'errorBday');
    });

    $('#registerUser').on('click', function () {

        CheckIfNull($('#confirmPassword').val(), 'Confirm password', 'errorConfirm');
        CheckUsernameAvailability();
        CheckEmailAvailability();
        PasswordNotMatch();
        $('#registrationForm').submit();     
    });
});

function CheckIfNull(textValue, fieldName, errorField) {
    if ($.trim(textValue) == '') {
        $('#'.concat(errorField)).text(fieldName + ' field is required.');
    }
}

function CheckUsernameAvailability() {
    var data = {
        username: $('#registerUsername').val()
    }
    $.ajax({
        url: checkUsernameUrl,
        data: data,
        success: function (data) {
            if (data.userExist == true) {
                $('#errorUsername').text('Username already exist.');
            }           
        },
        error: function () {
            window.location.href(errorPageUrl);
        }
    });
}

function CheckEmailAvailability() {
    var data = {
        email: $('#registerEmail').val()
    }
    $.ajax({
        url: checkEmailUrl,
        data: data,
        success: function (data) {
            if (data.emailExist == true) {
                $('#errorEmail').text('Email already exist.');
            }
        },
        error: function () {
            window.location.href(errorPageUrl);
        }
    });
}

function PasswordNotMatch() {
    if (($('#confirmPassword').val() != $('#userPassword').val()) && $('#confirmPassword').val() != '') {
        $('#errorConfirm').text('Password does not match.');
    } else if ($('#confirmPassword').val() == '') {
        
    } else {
        $('#errorConfirm').text('');
    }
        
}

function ChangePasswordScript() {
    var password = $('#userPassword').val();
    var confirmPassword = $('#confirmPassword').val();

    password = password.replace(/</g, "&lt");
    password = password.replace(/>/g, "&gt");

    confirmPassword = confirmPassword.replace(/</g, "&lt");
    confirmPassword = confirmPassword.replace(/>/g, "&gt");
    
    $('#userPassword').val(password);
    $('#confirmPassword').val(confirmPassword);
}