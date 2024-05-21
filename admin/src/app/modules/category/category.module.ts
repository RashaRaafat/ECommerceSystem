import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { CategoryListComponent } from './components/category-list/category-list.component';
import { AddCategoryComponent } from './components/add-category/add-category.component';
import { CategoryEditComponent } from './components/category-edit/category-edit.component';
@NgModule({
  declarations: [
    CategoryListComponent,
    AddCategoryComponent,
    CategoryEditComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule
  
  ]
})
export class CategoryModule { }
