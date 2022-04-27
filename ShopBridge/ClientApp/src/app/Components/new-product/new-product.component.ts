import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ProductService } from 'src/app/Services/product.service';

@Component({
  selector: 'app-new-product',
  templateUrl: './new-product.component.html',
  styleUrls: ['./new-product.component.css']
})
export class NewProductComponent implements OnInit {
  addProductForm:FormGroup;
  categories:category[];
  constructor(private service:ProductService, private fb:FormBuilder,private router:Router) { }

  ngOnInit() {
    this.service.getAllCatefories().subscribe(data=>{
      console.log(data);
      this.categories = data;
    });
    this.addProductForm = this.fb.group({
      id:[Math.floor(Math.random()*100)],
      name:[null,Validators.required],
      description:[null,Validators.required],
      price:[null,Validators.required],
      category:[null,Validators.required]
    });
  }

  onSubmit(){
    this.service.addProduct(this.addProductForm.value).subscribe(data=>{
      this.router.navigate(["/products"]);
    })
  }
}
