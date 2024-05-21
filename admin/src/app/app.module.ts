import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ProductModule } from './modules/product/product.module';
import { CategoryModule } from './modules/category/category.module';
import { OrdersModule } from './modules/orders/orders.module';
import { FormsModule } from '@angular/forms';
import { SharedModule } from './modules/shared/shared.module'; 
import { HttpClientModule } from '@angular/common/http';
import { RoleListComponent } from './admin/role-list/role-list.component';
import { RoleCreateComponent } from './admin/role-create/role-create.component';
import { RoleAssignComponent } from './admin/role-assign/role-assign.component';
import { LoginComponent } from './login/login.component';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './services/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    RoleListComponent,
    RoleCreateComponent,
    RoleAssignComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ProductModule,
    CategoryModule,
    OrdersModule,
    FormsModule,
    SharedModule,
    HttpClientModule

  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
