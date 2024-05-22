import { Component, OnInit } from '@angular/core';
import { OrderService } from 'src/app/services/order.service';
import { Order } from 'src/app/models/order.model';
import { OrderStatus } from 'src/app/enums/orderStatus.model';

@Component({
  selector: 'app-orders-list',
  templateUrl: './orders-list.component.html',
  styleUrls: ['./orders-list.component.css']
})
export class OrdersListComponent implements OnInit {
  orders: Order[] = [];

  constructor( private orderService: OrderService) { }

  ngOnInit(): void {
    this.loadOrders();

  }

 
  loadOrders(): void {
    this.orderService.getAllOrders().subscribe(
      orders => this.orders = orders,
      error => console.error('Error loading orders:', error)
    );
  }
  getStatusName(status: number): string {
    return OrderStatus[status];
  }
}
