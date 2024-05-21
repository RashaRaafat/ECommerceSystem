import { Category } from "./category.model";

export interface Product {
  id?: number;
  name: string;
  category: Category;
  price: number;
  quantity: number;
  description?: string;
  imagePath?: string;
  categoryId: number;
}
