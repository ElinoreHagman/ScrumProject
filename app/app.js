﻿var hub = $.connection.ChatHub;

hub.client.message = function (msg) {
    $("#message").append("<li>" + msg + "</li >")
}

hub.client.user = function (msg) {
    $("#user").append("<li>" + msg + "</li >")
}
$.connection.hub.start(function () {

    $("#send").click(function () {
        hub.server.send($("#txt").val());
        $("#txt").val("");
    });
})
