import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductListComponent } from './modules/product/components/product-list/product-list.component';
import { AddProductComponent } from './modules/product/components/add-product/add-product.component';
import { OrdersListComponent } from './modules/orders/components/orders-list/orders-list.component';
import { ChangeOrderStatusComponent } from './modules/orders/components/change-order-status/change-order-status.component';
import { CategoryListComponent } from './modules/category/components/category-list/category-list.component';
import { AddCategoryComponent } from './modules/category/components/add-category/add-category.component';
import { CategoryEditComponent } from './modules/category/components/category-edit/category-edit.component';
import { RoleListComponent } from './admin/role-list/role-list.component';
import { RoleCreateComponent } from './admin/role-create/role-create.component';
import { RoleAssignComponent } from './admin/role-assign/role-assign.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './services/auth.guard';


const routes: Routes = [
  { path: 'login', component: LoginComponent },
  //{path: '', component: ProductListComponent, canActivate: [AuthGuard]},
  { path: '', redirectTo: '/products', pathMatch: 'full' },
  {
    path: 'products', component: ProductListComponent, canActivate: [AuthGuard] 
  },
  { path: 'add-product', component: AddProductComponent, canActivate: [AuthGuard] } ,
  { path: 'edit-product/:id', component: AddProductComponent, canActivate: [AuthGuard] },
  { path: 'orders/list', component: OrdersListComponent, canActivate: [AuthGuard] },
  { path: 'orders/change-order-status/:id', component: ChangeOrderStatusComponent, canActivate: [AuthGuard] },
  { path: 'Categories', component: CategoryListComponent, canActivate: [AuthGuard] },
  { path: 'categories/add', component: AddCategoryComponent, canActivate: [AuthGuard] },
  { path: 'categories/edit/:id', component: CategoryEditComponent, canActivate: [AuthGuard] },
  { path: 'admin/role-list', component: RoleListComponent, canActivate: [AuthGuard] },
  { path: 'admin/role-create', component: RoleCreateComponent, canActivate: [AuthGuard] },
  { path: 'admin/role-assign', component: RoleAssignComponent, canActivate: [AuthGuard] }



];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
