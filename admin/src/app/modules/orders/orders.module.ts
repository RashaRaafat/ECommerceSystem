import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { OrdersListComponent } from './components/orders-list/orders-list.component';
import { ChangeOrderStatusComponent } from './components/change-order-status/change-order-status.component';
import { Router, RouterModule } from '@angular/router';

@NgModule({
  declarations: [
    OrdersListComponent,
    ChangeOrderStatusComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    CommonModule,
    RouterModule
  ]
})
export class OrdersModule { }
