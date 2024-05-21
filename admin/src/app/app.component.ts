import { Component, OnInit } from '@angular/core';
import { SignalRService } from './services/signalr.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'admin';
  constructor(private signalRService: SignalRService) { }

  ngOnInit() {
    this.signalRService.startConnection();
    //this.signalRService.addTransferOrderDataListener((order) => {
    //  console.log('Order updated:', order);
    //  alert(`Order status changed: ${order}`);
    //});
    //this.signalRService.addNewOrderListener((order) => {
    //  console.log('New order placed:', order);
    //  alert(`New order placed: ${order}`);
    //});
  }
}
