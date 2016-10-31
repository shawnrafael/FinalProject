$(document).ready(function () {

    GetNotifCount();
    
    $(document).on('click', '#notificationList', function () {
        $('#notificationBadge').hide();
        SeeAllNotification();
        GetNotifCount();
    });

});

function GetNotifCount() {
    var data = {
        userID: $('#currentUserID').val()
    }
    $.ajax({
        url: getBadgeCountUrl,
        data: data,
        success: function (data) {
            if (data.notifCount != 0) {
                $('#notificationBadge').text(data.notifCount)
            } else {
                $('#notificationBadge').hide();
            }
        },
        error: function () {
            window.location.href(errorPageUrl);
        }
    })
}

function SeeAllNotification() {
    var data = {
        userID: $('#currentUserID').val()
    }
    $.ajax({
        url: updateNotificationsUrl,
        data: data,
        success: function (data) {
            if (data.notifUpdated) {
                $('#notificationDropdown').load(reloadNotificationUrl)
            }
        },
        error: function () {
            window.location.href(errorPageUrl);
        }
    })
}