import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from 'src/app/Services/product.service';

@Component({
  selector: 'app-delete-product',
  templateUrl: './delete-product.component.html',
  styleUrls: ['./delete-product.component.css']
})
export class DeleteProductComponent implements OnInit {
  product:product;
  constructor(private service:ProductService,private route:ActivatedRoute, private router:Router) { }

  ngOnInit() {
    this.service.getAllProductById(this.route.snapshot.params.id).subscribe(data=>{
      this.product=data;
    })
  }

  deleteProduct(){
    this.service.deleteProductById(this.product.id).subscribe(data=>{
      this.router.navigate(['/products']);
    })
  }
}
