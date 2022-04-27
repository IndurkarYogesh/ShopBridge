import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from 'src/app/Services/product.service';

@Component({
  selector: 'app-update-product',
  templateUrl: './update-product.component.html',
  styleUrls: ['./update-product.component.css']
})
export class UpdateProductComponent implements OnInit {
  updateProductForm:FormGroup;
  constructor(private service:ProductService, private fb:FormBuilder,private router:Router, private route:ActivatedRoute) { }

  ngOnInit() {
    this.service.getAllProductById(this.route.snapshot.params.id).subscribe(data=>{
      console.log(data);
      this.updateProductForm = this.fb.group({
        id:[data.id],
        name:[data.name,Validators.required],
        description:[data.description,Validators.required],
        price:[data.price,Validators.required],
        category:[data.category,Validators.required]
      });
    });
  }

  onSubmit(){
    this.service.updateProductById(this.updateProductForm.value).subscribe(data=>{
      this.router.navigate(["/products"]);
    })
  }

}
