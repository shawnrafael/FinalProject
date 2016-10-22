var likePostUrl = '/Post/LikePost';

$(document).ready(function () {    

    $('#postBtnHome').css('display', 'inline');

    var profileOwner = $('#profileOwner').text();
    if (profileOwner != "none") {
        $('#userDivider').css('display', 'inline');
        $('#profileOwner').css('display', 'inline');
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

    $('#postBtnHome').on('click', function () {

        var data = {
            content: $('#postContent').val(),
            currentProfile: 0
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

