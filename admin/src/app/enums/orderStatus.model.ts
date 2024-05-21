export enum OrderStatus {
  New=0,
  Processing=1,
  Shipped=2,
  Delivered=3,
  Cancelled=4
}
//export enum OrderStatus {
//  New = 'New',
//  Approved = 'Approved',
//  Cancelled = 'Cancelled',
//  Shipped ='Shipped'
//}
export const OrderStatusNames = {
  [OrderStatus.New]: 'New',
  [OrderStatus.Processing]: 'Processing',
  [OrderStatus.Cancelled]: 'Cancelled',
};
