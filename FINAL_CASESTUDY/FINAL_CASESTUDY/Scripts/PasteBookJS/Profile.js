


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

    var user = $('#itemID').val();
    var currentProfile = $('#currentProfile').val();    

    $(document).on('click', '.btnComment', function () {
        var postID = this.id;
        var content = $('#addComment_'.concat(this.id)).val();
        content = content.replace(/</g, "&lt");
        content = content.replace(/>/g, "&gt");
        content = content.replace(/\s+/g, " ");
        content = $.trim(content);

        if ($.trim(content) == "") {
            $('#invalidComment_'.concat(postID)).text('Please add something to your comment.');
        } else if (content.length > 1000) {
            $('#invalidComment_'.concat(postID)).text('Comment can only have 1000 characters.');
        } else {            
            var data = {
                currentPost: postID,
                commentContent: content
            }
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
                    window.location.href(errorPageUrl);
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
                window.location.href(errorPageUrl);
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
                window.location.href(errorPageUrl);
            }
        });
    });

    $('#postBtnProfile').on('click', function () {        
        var postContent = $('#postContent').val();
        postContent = postContent.replace(/</g, "&lt");
        postContent = postContent.replace(/>/g, "&gt");
        postContent = postContent.replace(/\s+/g, " ");
        postContent = $.trim(postContent);
        
        if (postContent == "") {
            $('#errorPost').text('Please add something to your post.');
        } else if (postContent > 1000) {
            $('#errorPost').text('Post can only have 1000 characters.');
        } else {
            var data = {
                content: postContent,
                currentProfile: $('#currentProfile').val()
            }

            $.ajax({
                url: addPostUrl,
                data: data,
                type: 'GET',
                success: function (data) {
                    if (data.post == true) {
                        $('#errorPost').text('');
                        $('#postContent').val('');
                        $('#postContainer').load(getPostUrl);
                    } 
                },
                error: function () {
                    window.location.href(errorPageUrl);
                }
            });
        }
    });

    $('#submitAboutMe').on('click', function () {
        var aboutMeContent = $('#txtAboutMe').val();
        aboutMeContent = aboutMeContent.replace(/</g, "&lt");
        aboutMeContent = aboutMeContent.replace(/>/g, "&gt");
        aboutMeContent = aboutMeContent.replace(/\s+/g, " ");
        aboutMeContent = $.trim(aboutMeContent);

        if (aboutMeContent > 2000) {
            $('#errorAboutMe').text('About me content can only have 2000 characters.');
        } else {
            var data = {
                aboutMe: aboutMeContent
            }
            $.ajax({
                url: addAboutMeUrl,
                data: data,
                type: 'POST',
                success: function (data) {
                    location.reload();
                },
                error: function () {
                    window.location.href(errorPageUrl);
                }
            });
        }

        
    });

    //source: http://stackoverflow.com/questions/651700/how-to-have-jquery-restrict-file-types-on-upload
    $('#uploadImage').on('click', function () {
        var ext = $('#imageFile').val().split('.').pop().toLowerCase();
        if ($.inArray(ext, ['png', 'jpg', 'jpeg']) == -1) {
            $('#errorPic').text('invalid file extension');
        } else {
            $('#uploadImageForm').submit();
        }

        var fsize = $('#fileButton')[0].files[0].size;

        if (fsize > 2097152) {
            $('#errorPic').text("File size is too big");
        }
    });

    


    

});

function AddAsFriend() {
    $.ajax({
        url: addAsFriendUrl,
        success: function (data) {
            location.reload();
        },
        error: function () {
            window.location.href(errorPageUrl);
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
            window.location.href(errorPageUrl);
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
            window.location.href(errorPageUrl);
        }

    })
}

