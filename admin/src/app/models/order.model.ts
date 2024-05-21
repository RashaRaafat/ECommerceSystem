import { OrderStatus } from 'src/app/enums/orderStatus.model'
import { OrderItem } from 'src/app/models/orderItems.model'
export interface Order {
  id: number;
  userId: string;
  totalPrice: number;
  orderDate: Date;
  status: OrderStatus;
  orderItems: OrderItem[];
}
