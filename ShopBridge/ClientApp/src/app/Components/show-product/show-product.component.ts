import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from 'src/app/Services/product.service';

@Component({
  selector: 'app-show-product',
  templateUrl: './show-product.component.html',
  styleUrls: ['./show-product.component.css']
})
export class ShowProductComponent implements OnInit {
  product:product;
  constructor(private service:ProductService,private route:ActivatedRoute) { }

  ngOnInit() {
    this.service.getAllProductById(this.route.snapshot.params.id).subscribe(data=>{
      this.product=data;
    })
  }

}
