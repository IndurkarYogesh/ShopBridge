import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  _baseUrl:string = "api/Products";
  constructor(private http:HttpClient) { }

  getAllProducts(){
    return this.http.get<product[]>(this._baseUrl+"/GetAllProducts");
  }

  getAllCatefories(){
    return this.http.get<category[]>(this._baseUrl+"/GetAllCategories");
  }
  addProduct(product:product){
    return this.http.post(this._baseUrl+"/addProduct", product);
  }
  getAllProductById(id:number){
    return this.http.get<product>(this._baseUrl+"/GetProductById/"+id);
  }
  updateProductById(product:product){
    return this.http.put(this._baseUrl+"/UpdateProduct/"+product.id,product);
  }
  deleteProductById(id:number){
    return this.http.delete(this._baseUrl+"/DeleteProduct/"+id);
  }
}
