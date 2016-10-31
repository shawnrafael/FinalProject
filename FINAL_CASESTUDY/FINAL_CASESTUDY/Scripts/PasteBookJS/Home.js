
$(document).ready(function () {

    var user = $('#itemID').val();

    setInterval(function () {
        $("#postContainer").load(getPostUrl);
    }, 60000);

    $(document).on('click', '.btnComment', function () {
        var postID = this.id;
        var content = $('#addComment_'.concat(this.id)).val();
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

    $('#postBtnHome').on('click', function () {
        var postContent = $('#postContent').val();
        postContent = postContent.replace(/\s+/g, " ");
        postContent = $.trim(postContent);

        if (postContent == "") {
            $('#errorPost').text('Please add something to your post.');
        } else if (postContent.length > 1000) {
            $('#errorPost').text('Post can only have 1000 characters.');
        } else {
            var data = {
                content: postContent,
                currentProfile: 0
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

});

