import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: signalR.HubConnection;

  private newOrderSubject = new Subject<string>();
  private orderStatusChangeSubject = new Subject<string>();

  newOrder$ = this.newOrderSubject.asObservable();
  orderStatusChange$ = this.orderStatusChangeSubject.asObservable();

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7249/orderHub')
      .build();
  }

  public startConnection(): void {
    this.hubConnection
      .start()
      .then(() => console.log('SignalR connection started'))
      .catch(err => console.log('Error while starting SignalR connection: ' + err));

    this.addListeners();
  }

  private addListeners(): void {
    this.hubConnection.on('NewOrder', (message: string) => {
      this.newOrderSubject.next(message);
    });

    this.hubConnection.on('OrderStatusChange', (message: string) => {
      this.orderStatusChangeSubject.next(message);
    });
  }

  public joinGroup(groupName: string): void {
    this.hubConnection.invoke('JoinGroup', groupName)
      .catch(err => console.error(`Error joining group: ${err}`));
  }

  public leaveGroup(groupName: string): void {
    this.hubConnection.invoke('LeaveGroup', groupName)
      .catch(err => console.error(`Error leaving group: ${err}`));
  }
}


