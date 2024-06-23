import * as signalR from "@microsoft/signalr";

export const connection = new signalR.HubConnectionBuilder()
  .withUrl("http://localhost:5000/appointmenthub")
  .withAutomaticReconnect()
  .build();

export const notificationConnection = new signalR.HubConnectionBuilder()
  .withUrl("http://localhost:5000/notificationHub")
  .withAutomaticReconnect()
  .build();

connection.start().catch(function (err) {
    return console.error("Error from hub: ",err.toString());
});

notificationConnection.start().catch(function (err) {
    return console.error("Error from notification hub: ", err.toString());
});

export function registerAppointmentUpdates(callback) {
    connection.on("ReceiveMessage", message => {
        callback(message);
    });
    connection.on("AppointmentDeleted", message => {
        callback({type: "delete", message});
    });
    connection.on("AppointmentUpdated", updatedAppointment => {
        callback({ type: "update", message: updatedAppointment });
    });
}

export function registerNotificationUpdates(callback) {
    notificationConnection.on("ReceiveNotification", notification => {
        console.log("Received Notification:", notification);
        callback(notification);
    });
}
