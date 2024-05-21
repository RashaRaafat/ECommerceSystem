import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from 'src/app/services/category.service';
import { Category } from 'src/app/models/category.model';
@Component({
  selector: 'app-category-edit',
  templateUrl: './category-edit.component.html',
  styleUrls: ['./category-edit.component.css']
})
export class CategoryEditComponent implements OnInit {

  categoryForm: FormGroup;
  categoryId!: number;

  constructor(
    private formBuilder: FormBuilder,
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.categoryForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['']
    });
  }

  ngOnInit(): void {
    this.categoryId = +this.route.snapshot.paramMap.get('id')!;
    this.categoryService.getCategoryById(this.categoryId).subscribe(category => {
      this.categoryForm.patchValue(category);
    });
  }

  onSubmit(): void {
    if (this.categoryForm.valid) {
      const updatedCategory: Category = this.categoryForm.value;
      this.categoryService.updateCategory(this.categoryId, updatedCategory).subscribe((res) => {
        console.log('Category updated successfully:', res);
        this.router.navigate(['/Categories']);
      }, error => {
        console.error('Error updated product:', error);
      });
    }
  }
}

