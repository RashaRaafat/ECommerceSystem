import { Order } from 'src/app/models/order.model'
import { Product } from 'src/app/models/product.model'

export interface OrderItem {
  id: number;
  orderId: number;
  productId: number;
  quantity: number;
  price: number;
  order?: Order;
  product?: Product;
}
