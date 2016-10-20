var likePostUrl = '/PasteBook/LikePost';
var likeModalUrl = '/PasteBook/LikeModal';

$(document).ready(function () {

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

    $(document).on('click', '.btnComment', function () {
        var data = {
            currentPost: this.id,
            commentContent: $('#addComment').val()
        }
        alert(data.currentPost+data.commentContent)
        $.ajax({
            url: addCommentUrl,
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

    $('#postBtn').on('click', function () {

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