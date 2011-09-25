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
    <script src="Scripts/jquery-1.6.4.min.js"></script>
    <script src="Scripts/jquery.cookie.js" type="text/javascript"></script>
    <script src="Scripts/json2.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.signalR.js" type="text/javascript"></script>
    <script src="signalr/hubs" type="text/javascript"></script>
    <script>
        $(function () {
            "use strict";

            var username = prompt("Please enter a username:");
            $.cookie('username', username);

            $("#join").click(function () {

                var connection = $.connection.chatHub;

                connection.messageFromServer = function (username, message) {
                    $("<li/>").html("[" + username + "]:&nbsp;" + message).appendTo($("#messages"));
                };

                $("#broadcast").click(function () {
                    connection.send($("#msg").val());
                });

                $.connection.hub.start(function () {
                    connection.join();
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