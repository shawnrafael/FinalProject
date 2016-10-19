var addPostUrl = '/PasteBook/AddPost';
//var getPostUrl = '/PasteBook/GetPostOnProfile';

var likePostUrl = '/PasteBook/LikePost';

$(document).ready(function () {

    $('.linkLike').on('click', function () {
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




    $('#postBtn').on('click', function () {
        
        var data = {
            content: $('#postContent').val()
        }
        if (data.content == "") {
            $('#errorPost').css('display', 'block');
        }
        else {
            $.ajax({
                url: addPostUrl,
                data: data,
                type: 'POST',
                success: function (data) {
                    if (data.post == false) {
                        $('#errorPost').css('display', 'block');
                    } else {
                        $('#postContent').val("");
                        $("#postContainer").load(getPostUrl);
                    }

                },
                error: function () {
                    alert();
                }
            });
        }

    });
});