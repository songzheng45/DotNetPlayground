$(function () {
    toastr.success('Welcome!');

    var nickName = $('#nickName').text().trim() ;

    //$.connection.hub.url = '/signalr';

    var chat = $.connection.chatHub;


    $.connection.hub.logging = true;
    $.connection.hub.error(function (error) {
        console.log('SignalR Error: ' + error);
    });

    $.connection.hub.qs = {
        nickName:nickName
    };

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
            $('.userlist').append('<li>' + item + '</li>');
        });
    }

    chat.client.addNewMessageToPage = function (name, message) {
        // Add the message to the page.
        $('.conversation').append('<li><strong>' + name
            + '</strong>: ' + message + '</li>');
    };

    $.connection.hub.start().done(function () {
        
        //chat.server.addUser({ Nickname: nickName });

        $('#sendmessage').click(function () {
            // Call the Send method on the hub.
            chat.server.send($('#message').val());
            // Clear text box and reset focus for next comment.
            $('#message').val('').focus();
        });

        $("#message").keydown(function (e) {
            if ((e.which === 13 && e.ctrlKey)) {
                $(this).val(function (i, val) {
                    return val + "\n";
                });
            }
        }).keypress(function (e) {
            if (e.which === 13 && !e.ctrlKey) {
                $('#sendmessage').click();
                e.preventDefault();
            }
        });
     }).fail(function(error){
        toastr.error(error.message);
     });

     /*
    function htmlEncode(value) {
        var encodedValue = $('<div />').text(value).html();
        return encodedValue;
    }
    */
});