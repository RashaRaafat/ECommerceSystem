import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { AuthService } from './auth.service'; 
@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;

  constructor(private authService: AuthService) {
    const token = this.authService.getToken(); // Method to get the stored JWT token

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7057/orderHub', {
        accessTokenFactory: () => token
      })
      .build();

    this.hubConnection.start().catch(err => console.error(err.toString()));

    this.hubConnection.on('ReceiveNotification', (message: string) => {
      console.log(message);
      // Display notification to the user
    });
  }
}
