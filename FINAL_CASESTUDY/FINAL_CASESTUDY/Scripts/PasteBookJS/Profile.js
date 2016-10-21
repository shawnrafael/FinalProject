$(document).ready(function () {
    //#addAsFriend,#requestAdd,#confirmRequest
    var currentProfile = $('#currentProfile').val();
    var user = $('#userID').val();

    ConfirmRequest
    $(document).on('click', '#ConfirmRequest', function () {
        AddAsFriend();
    });

    $(document).on('click', '#addAsFriend', function () {
        AddAsFriend();
    });

    if (user != currentProfile) {
        CheckRequest();
    } else {
        
    }
    

    function CheckRequest() {
        $.ajax({
            url: checkIfRequestExist,
            type: 'GET',
            success: function (data) {
                if (data.request) {                    
                    CheckFriendUserRequest();
                } else {
                    $('#addAsFriend').css('display', 'inline');
                }
            },
            error: function () {
                alert("Oops");
            }

        })
    }

    function CheckFriendUserRequest() {
        $.ajax({
            url: checkFriendUser,
            type: 'GET',
            success: function (data) {
                if (data.checkUser) {
                    CheckIfFriends();                    
                } else {
                    $('#confirmRequest').css('display', 'inline');                    
                }
            },
            error: function () {
                alert("Oops");
            }

        })
    }

    function CheckIfFriends() {
        $.ajax({
            url: checkIfFriends,
            type: 'GET',
            success: function (data) {
                if (data.users) {
                    $('#friendsTrue').css('display', 'inline');
                } else {
                    $('#requestAdd').css('display', 'inline');
                }
            },
            error: function () {
                alert("Oops");
            }

        })
    }

    function AddAsFriend() {
        $.ajax({
            url: addAsFriend,
            type: 'POST',
            success: function (data) {
                $("#headerContainer").load(profileHeaderContainer);
            },
            error: function () {
                alert("Oops");
            }

        })
    }

});