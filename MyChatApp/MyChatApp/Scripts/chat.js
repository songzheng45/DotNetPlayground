$(function () {
    toastr.success('Welcome!');

    var chat = $.connection.chatHub;

    $.connection.hub.connectionSlow(function () {
        toastr.warning('connectionSlow');
    });

    $.connection.hub.reconnecting(function () {
        toastr.warning('reconnecting');
    });

    $.connection.hub.reconnected(function () {
        toastr.success('reconnected');
    });

    $.connection.hub.disconnected(function () {
        //if (tryingToReconnect) {
        //    notifyUserOfDisconnect(); // Your function to notify user.
        //}
        toastr.success('disconnected');
    });

    chat.client.showAllUsers = function (allUsers) {
        $('.userlist').html('');
        allUsers.forEach(function (item) {
            $('.userlist').append('<li>' + item.Nickname + '</li>');
        });
    }

    chat.client.addNewMessageToPage = function (user, message) {
        // Add the message to the page.
        $('.conversation').append('<li><strong>' + htmlEncode(user.Nickname)
            + '</strong>: ' + htmlEncode(message) + '</li>');
    };

    $.connection.hub.start().done(function () {
        chat.server.addUser({ Nickname: '@ViewBag.Nickname' });

        $('#sendmessage').click(function () {
            // Call the Send method on the hub.
            chat.server.send(
                { nickname: '@ViewBag.Nickname' },
                $('#message').val()
            );
            // Clear text box and reset focus for next comment.
            $('#message').val('').focus();
        });
    });

    function htmlEncode(value) {
        var encodedValue = $('<div />').text(value).html();
        return encodedValue;
    }

});