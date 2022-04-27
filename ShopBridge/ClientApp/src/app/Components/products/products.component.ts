import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProductService } from 'src/app/Services/product.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  public products: product[];
  constructor(private service:ProductService,private router:Router) { }

  ngOnInit() {
    this.service.getAllProducts().subscribe(data => {
      console.log(data);
      this.products = data;
    })
  }

  showProduct(id:number){
    this.router.navigate(["/show-product/"+id]);
  }
  deleteProduct(id:number){
    this.router.navigate(["/delete-product/"+id]);
  }
}
