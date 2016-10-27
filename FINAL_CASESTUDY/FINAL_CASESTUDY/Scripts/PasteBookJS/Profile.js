


$(document).ready(function () {

    $('#confirmRequest').on('click', function () {
        ConfirmRequest();
    });

    $('#addAsFriend').on('click', function () {
        AddAsFriend();
    });

    $('#deleteRequest').on('click', function () {
        RejectRequest();
    });


    var currentProfile = $('#currentProfile').val();

    var user = $('#itemID').val();

    function AddAsFriend() {
        $.ajax({
            url: addAsFriendUrl,
            success: function (data) {
                location.reload();
            },
            error: function () {
                alert("Oops");
            }

        })
    }

    function ConfirmRequest() {        
        var data = {
            currentProfileID: $('#currentProfile').val()
        }
        $.ajax({
            url: confirmRequestUrl,
            data: data,
            success: function (data) {
                location.reload();
            },
            error: function () {
                alert("Oops");
            }

        })
    }

    function RejectRequest() {
        var data = {
            currentProfileID: $('#currentProfile').val()
        }
        $.ajax({
            url: deleteRequestUrl,
            data: data,
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
        if (data.commentContent == "") {
            $('#invalidComment_'.concat(user)).text('Please add sometthing to your comment.');
        } else if (data.commentContent.length > 1000) {
            $('#addComment_'.concat(user)).val('')
            $('#invalidComment_'.concat(user)).text('Comment can only have 1000 characters.');
        } else {
            $.ajax({
                url: addCommentUrl,
                data: data,
                type: 'POST',
                success: function (data) {
                    if (data.postAdded == true) {
                        $("#postContainer").load(getPostUrl);                        
                    } 
                    
                },
                error: function () {
                    alert("Something went wrong");
                }
            });
        }
        
    });

    $(document).on('click', '.btnLike', function () {
        var data = {
            currentPost: this.id
        }
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

    $(document).on('click', '.btnUnLike', function () {
        var data = {
            currentPost: this.id
        }
        $.ajax({
            url: unlikePostUrl,
            data: data,
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
            $('#errorPost').text('Please add something to your post.');
        } else if (data.content.length > 1000) {
            $('#errorPost').text('Post can only have 1000 characters.');
        }else {
            $.ajax({
                url: addPostUrl,
                data: data,
                type: 'GET',
                success: function (data) {
                    if (data.post == true) {
                        $('#postContent').val('');
                        $('#postContainer').load(getPostUrl);
                    } 
                },
                error: function () {
                    alert("Oops");
                }
            });
        }
    });

    $('#submitAboutMe').on('click', function () {
        var data = {
            aboutMe: $('#txtAboutMe').val()
        }
        $.ajax({
            url: addAboutMeUrl,
            data: data,
            type: 'POST',
            success: function (data) {
                location.reload();
            },
            error: function () {
                alert("Something went wrong");
            }
        });
    });

    //source: http://stackoverflow.com/questions/651700/how-to-have-jquery-restrict-file-types-on-upload
    $('#uploadImage').on('click', function () {
        var ext = $('#imageFile').val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['png', 'jpg', 'jpeg']) == -1) {
            alert('invalid extension!');
        } else {
            $('#uploadImageForm').submit();
        }
    });

    

});