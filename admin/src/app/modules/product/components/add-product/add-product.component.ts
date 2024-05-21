import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CategoryService, ProductService } from 'src/app/services';
import { Product } from 'src/app/models/product.model';
import { Category } from 'src/app/models/category.model';
import { ActivatedRoute, Router } from '@angular/router';



@Component({
  selector: 'app-add-product',
  templateUrl: './add-product.component.html',
  styleUrls: ['./add-product.component.css']
})
export class AddProductComponent implements OnInit {
  productForm!: FormGroup;
  categories!: Category[];
  product: Product = {} as Product;
  selectedCategory: number = -1;
  imageUrl!: string;
  productId: number | null = null;
  constructor(private formBuilder: FormBuilder, private productService: ProductService, private categoryService: CategoryService,  private router: Router,private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.loadCategories();
    this.route.params.subscribe(params => {
      if (params['id']) {
        this.productId = +params['id'];
        this.fetchProductData(this.productId);
      }
    });
    this.productForm = this.formBuilder.group({
      name: ['', Validators.required],
      category: ['', Validators.required],
      price: ['', [Validators.required, Validators.min(0)]],
      quantity: ['', [Validators.required, Validators.min(1)]],
      description: [''],
      image: ['']
    });
  }



  fetchProductData(productId: number): void {
    this.productService.getProduct(productId).subscribe(
      product => {
        console.log('Product data:', product);
        this.productForm.patchValue({
          name: product.name,
          productId: product.id,
          price: product.price,
          quantity: product.quantity,
          description: product.description,
          category: product.categoryId
        });
        this.imageUrl = product?.imagePath?.replace("~", "https://localhost:7057/") as string;

      });
  }

  loadCategories(): void {
    this.categoryService.getCategories().subscribe(categories => {
      this.categories = categories;
      console.log('Categories:', categories);
    });
  }

  // Method to handle file selection event
  onFileSelected(event: any): void {
    const file: File = event.target.files[0];
    if (file) {
      this.productForm.get('image')?.setValue(file);
      this.previewImage(file);
    }
  }

  // Method to preview the selected image
  previewImage(file: File): void {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => {
      this.imageUrl = reader.result as string;
    };
  }






  onSubmit(): void {
    if (this.productForm.valid) {
      const formData = new FormData();
      formData.append('name', this.productForm.get('name')?.value);
      formData.append('categoryId', this.productForm.get('category')?.value);
      formData.append('price', this.productForm.get('price')?.value);
      formData.append('description', this.productForm.get('description')?.value);
      formData.append('quantity', this.productForm.get('quantity')?.value);
      formData.append('image', this.productForm.get('image')?.value);
      if (this.productId) {
        formData.append('ImagePath', this.imageUrl);
        formData.append('id', this.productId.toString());
        this.productService.updateProduct(formData).subscribe(
          response => {
            console.log('Product updated successfully:', response);
            this.router.navigate(['/products']);

            this.productForm.reset();
            this.imageUrl = '';
          },
          error => {
            console.error('Error updated product:', error);
          });
      }
      else {
        this.productService.addProduct(formData).subscribe(
          response => {
            console.log('Product added successfully:', response);
            this.router.navigate(['/products']);
            this.productForm.reset();
            this.imageUrl = '';
          },
          error => {
            console.error('Error adding product:', error);
          }

        );

      }
    }
  }
}
