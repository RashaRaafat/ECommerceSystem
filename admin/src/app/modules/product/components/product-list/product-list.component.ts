import { Component, OnInit } from '@angular/core';
import { ProductService, CategoryService } from 'src/app/services';
import { Product } from 'src/app/models/product.model';
import { Category } from 'src/app/models/category.model';
import { NavigationStart, Router } from '@angular/router';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  filteredProducts: Product[] = [];
  categories: Category[] = [];
  selectedCategory: number=-1;


  constructor(private productService: ProductService, private categoryService: CategoryService, private router: Router) { }

  ngOnInit(): void {
    this.loadProducts();
    this.loadCategories();
  }

  loadProducts(): void {
    this.productService.getAllProducts().subscribe(products => {
      this.products = products;
    });
  }
  filterProducts(): void {
    if (this.selectedCategory != -1) {
      this.productService.filterProducts(this.selectedCategory).subscribe(products => {
        this.products = products;
      });
    }
    else {
      this.loadProducts();
    }
  }


  loadCategories(): void {
    this.categoryService.getCategories().subscribe(categories => {
      this.categories = categories;
    });
  }

  editProduct(product: Product): void {
    this.router.navigate(['/edit-product', product.id]);

  }

  confirmDelete(product: Product): void {
    const confirmation = confirm(`Are you sure you want to delete ${product.name}?`);
    if (confirmation) {
      this.productService.deleteProduct(product.id ?? 0).subscribe((response) => {
        console.log(response);
        this.loadProducts();
      });
    }
  }
  navigateToAddProduct(): void {
    this.router.navigate(['/add-product']);
  }
}
