import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiUrl = 'https://localhost:7057/api/ProductsApi'

  constructor(private http: HttpClient) { }

  getAllProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(this.apiUrl +"/GetAllProducts");
  }
  filterProducts(id: number): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/FilterProductBycategory/${id}`);
  }

  addProduct(formData: FormData) {
    return this.http.post<any>(`${this.apiUrl}/AddProduct`, formData);
  }
  getProduct(id: number): Observable<Product> {
    const url = `${this.apiUrl}/GetProduct/${id}`;
    return this.http.get<Product>(url);
  }
  updateProduct(formData: FormData) {
    const url = `${this.apiUrl}/updateProduct`;
    return this.http.post<any>(url, formData);
  }

  deleteProduct(id: number): Observable<void> {
    const url = `${this.apiUrl}/${id}`;
    return this.http.delete<void>(url);
  }
}
