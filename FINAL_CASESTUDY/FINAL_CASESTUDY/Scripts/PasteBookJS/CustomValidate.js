//$(document).ready(function () {

//    $('#registrationForm').validate({
//        rules: {
//            USER_NAME: {
//                alreadyExist: true
//            }
//        }
//    });

//    var response;
//    $.validator.addMethod("alreadyExist",
//        function(value, element){
//            var data = {
//                username: $('#registerUsername').val()
//            }
//            alert(data.username);
//            $.ajax({
//                url: checkUsernameUrl,
//                data: data,
//                success: function (data) {
//                    response = (data.userExist == 'true') ? false : true;
//                    //if (data.userExist == true) {
//                    //    $('#errorUsername').text('Username already exist.');
//                    //}
//                },
//                error: function () {
//                    window.location.href(errorPageUrl);
//                }
//            });
//        return response;
//           },
//   "Username already exist."
//   );

//});