<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="chat.aspx.cs" Inherits="chat.ChatPage" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <title>Basic chat</title>
    <style type="text/css">
        body {
            font-family: "Segoe UI";
            font-size: 12px;
        }
        form {
            margin-bottom: 10px;
            display: block;
        }
        label {
            display: block;
        }
    </style>
    <script src="Scripts/jquery-1.6.3.min.js"></script>
    <script src="Scripts/jquery.cookie.js" type="text/javascript"></script>
    <script src="Scripts/json2.min.js" type="text/javascript"></script>
    <script>
        $(function () {
            "use strict";

            var username = prompt("Please enter a username:");
            $.cookie('username', username);

            //HostUrl resolves to:  localhost:80/chat/chat-sockets.ashx
            //As a result, var host = 'ws://localhost:80/chat/chat-sockets.ashx'
            var host = "ws://<%: HostUrl %>";

            $("#join").click(function () {
                
                var connection = new WebSocket(host);

                connection.onmessage = function (message) {
                    var data = window.JSON.parse(message.data);
                    $("<li/>").html("[" + data.from + "]:&nbsp;" + data.value).appendTo($("#messages"));
                };

                $("#broadcast").click(function () {
                    connection.send(window.JSON.stringify({ type: 1, value: $("#msg").val() }));
                });

            });

        });
</script>
</head>
<body>
    <h2>Chat</h2>
    <form>
        <input type="text" id="msg" size="50" />
        <br />
        <input type="button" id="broadcast" value="Broadcast Message" />
        <input type="button" id="join" value="Join chat" />
    </form>

    <ul id="messages">
    </ul>
    
</body>
</html>
