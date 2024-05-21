import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CategoryService } from 'src/app/services/category.service';
import { Category } from 'src/app/models/category.model';
@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent {
  categoryForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private categoryService: CategoryService,
    private router: Router
  ) {
    this.categoryForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['']
    });
  }

  onSubmit(): void {
    if (this.categoryForm.valid) {
      const newCategory: Category = this.categoryForm.value;
      this.categoryService.addCategory(newCategory).subscribe((res) => {
        console.log('Category added successfully:', res);
        this.router.navigate(['/Categories']);
      }, error => {
        console.error('Error updated product:', error);
      });
    }
  }
}
