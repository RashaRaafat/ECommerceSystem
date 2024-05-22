const connection = new signalR.HubConnectionBuilder()
    .withUrl("/orderHub")
    .build();

connection.on("ReceiveNotification", function (message) {
    // Display the notification for admins
    alert("lol:" + message);
});


connection.start().catch(function (err) {
    return console.error(err.toString());
});
