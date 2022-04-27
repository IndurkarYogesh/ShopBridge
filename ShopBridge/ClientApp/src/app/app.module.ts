import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './Components/nav-menu/nav-menu.component';
import { HomeComponent } from './Components/home/home.component';
import { ProductsComponent } from './Components/products/products.component';
import { DeleteProductComponent } from './Components/delete-product/delete-product.component';
import { NewProductComponent } from './Components/new-product/new-product.component';
import { ShowProductComponent } from './Components/show-product/show-product.component';
import { UpdateProductComponent } from './Components/update-product/update-product.component';
import { ProductService } from './Services/product.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ProductsComponent,
    DeleteProductComponent,
    NewProductComponent,
    ShowProductComponent,
    UpdateProductComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'products', component: ProductsComponent },
      { path: 'new-product', component: NewProductComponent },
      { path: 'update-product/:id', component: UpdateProductComponent },
      { path: 'delete-product/:id', component: DeleteProductComponent },
      { path: 'show-product/:id', component: ShowProductComponent },
    ])
  ],
  providers: [ProductService],
  bootstrap: [AppComponent]
})
export class AppModule { }
