import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { OrderService } from 'src/app/services/order.service';
import { Order } from 'src/app/models/order.model';
import { OrderStatus } from 'src/app/enums/orderStatus.model';

@Component({
  selector: 'app-change-order-status',
  templateUrl: './change-order-status.component.html',
  styleUrls: ['./change-order-status.component.css']
})
export class ChangeOrderStatusComponent implements OnInit {
  orderForm!: FormGroup;
  orderId!: number;
  order!: Order;
  OrderStatus = OrderStatus;  // Expose the OrderStatus enum

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder,
    private orderService: OrderService
  ) {}

  ngOnInit(): void {
    this.orderId = +this.route.snapshot.paramMap.get('id')!;
    this.orderForm = this.formBuilder.group({
      status: ['', Validators.required]
    });
    this.loadOrder();
  }

  loadOrder(): void {
    this.orderService.getOrderById(this.orderId).subscribe(
      order => {
        this.order = order;
        this.orderForm.patchValue({
          status: order.status
        });
      },
      error => console.error('Error loading order:', error)
    );
  }

  getStatusKeys(): string[] {
    return Object.keys(OrderStatus).filter(key => isNaN(Number(key)));
  }

  getStatusName(status: number): string {
    return OrderStatus[status];
  }

  onSubmit(): void {
    if (this.orderForm.valid) {
      const updatedStatus = this.orderForm.value.status;
      this.orderService.updateOrderStatus(this.orderId, updatedStatus).subscribe(
        response => {
          console.log('Order status updated successfully:', response);
          this.router.navigate(['/orders/list']);
        },
        error => console.error('Error updating order status:', error)
      );
    }
  }
}
