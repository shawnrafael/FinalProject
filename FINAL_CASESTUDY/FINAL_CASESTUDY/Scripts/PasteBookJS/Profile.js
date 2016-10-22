var likePostUrl = '/Post/LikePost';

$(document).ready(function () {
    //#addAsFriend,#requestAdd,#confirmRequest
    $('#postBtnProfile').css('display', 'inline');
    CheckRequest();

    var currentProfile = $('#currentProfile').val();
    var user = $('#userID').val();

    
    $(document).on('click', '#confirmRequest', function () {
        ConfirmRequest();
    });
    $(document).on('click', '#addAsFriend', function () {
        AddAsFriend();
    });    

    function CheckRequest() {
        $.ajax({
            url: checkFriendRequestUrl,
            type: 'GET',
            success: function (data) {
                ProfileFriendStatus(data);
               
            },
            error: function () {
                alert("Oops");
            }

        })
    }

    function ProfileFriendStatus(data) {
        switch (data.friend) {
            case "profile owner":                
                break;
            case "friends":
                $('#friendsTrue').css('display', 'inline');
                break;
            case "no request":
                $('#addAsFriend').css('display', 'inline');
                break;
            case "user request":
                $('#requestAdd').css('display', 'inline');
                break;
            case "friend confirm":
                $('#dropdownRequest').css('display', 'inline');
                break;
            default:
                break;
        }
    }

    function AddAsFriend() {
        $.ajax({
            url: addAsFriendUrl,
            type: 'POST',
            success: function (data) {
                location.reload();
            },
            error: function () {
                alert("Oops");
            }

        })
    }

    function ConfirmRequest() {
        $.ajax({
            url: confirmRequestUrl,
            type: 'POST',
            success: function (data) {
                location.reload();
            },
            error: function () {
                alert("Oops");
            }

        })
    }

    $(document).on('click', '.btnComment', function () {
        var data = {
            currentPost: this.id,
            commentContent: $('#addComment_'.concat(this.id)).val()
        }
        $.ajax({
            url: addCommentUrl,
            data: data,
            type: 'POST',
            success: function (data) {
                $("#postContainer").load(getPostUrl);
            },
            error: function () {
                alert("Something went wrong");
            }
        });
    });

    $(document).on('click', '.btnLike', function () {
        var data = {
            currentPost: this.id
        }
        alert(data.currentPost);
        $.ajax({
            url: likePostUrl,
            data: data,
            type: 'GET',
            success: function (data) {
                $("#postContainer").load(getPostUrl);
            },
            error: function () {
                alert("Something went wrong");
            }
        });
    });

    $('#postBtnProfile').on('click', function () {        

        var data = {
            content: $('#postContent').val(),
            currentProfile: $('#currentProfile').val()
        }
        if (data.content == "") {
            $('#errorPost').css('display', 'block');
        } else {
            $.ajax({
                url: addPostUrl,
                data: data,
                type: 'GET',
                success: function (data) {
                    if (data.post == false) {
                        $('#errorPost').css('display', 'block');
                    } else {
                        $('#postContent').val("");
                        $('#postContainer').load(getPostUrl);
                    }

                },
                error: function () {
                    alert();
                }
            });
        }
    });

});