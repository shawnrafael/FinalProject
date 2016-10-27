
$(document).ready(function () {

    setInterval(function () {
        $("#postContainer").load(getPostUrl);
    }, 60000);

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

    $('#postBtnHome').on('click', function () {

        var data = {
            content: $('#postContent').val(),
            currentProfile: 0
        }
        if (data.content == "") {
            $('#errorPost').text('Please add something to your post.');
        } else if (data.content.length > 1000) {
            $('#errorPost').text('Post can only have 1000 characters.');
        } else {
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
                    alert();
                }
            });
        }
    });

});

