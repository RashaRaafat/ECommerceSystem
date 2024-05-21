const connection = new signalR.HubConnectionBuilder()
    .withUrl("/orderHub")
    .build();

connection.on("NewOrder", function (message) {
    // Display the notification for admins
    alert(message);
});

connection.on("OrderStatusChanged", function (message) {
    // Display the notification for the user
    alert(message);
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
