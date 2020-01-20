import { Product } from './../_models/product';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '@environments/environment';
import { map } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class ProductService {
    constructor(private http: HttpClient) { }

    getAll() {
        return this.http.get<Product[]>(`${environment.apiUrl}/fct/products`);
    }

    getUserProducts(userId: number) {
        return this.http.post<Product[]>(`${environment.apiUrl}/fct/userproducts`,  userId )
            .pipe(map(products => {
                return products;
            }));
    }

    placeOrder(userId: number, productId: number) {
        return this.http.post<boolean>(`${environment.apiUrl}/fct/purchase`, {userId, productId} )
            .pipe(map(isProductPurchased => {
                return isProductPurchased;
            }));
    }

    cancelOrder(userId: number, productId: number) {
        return this.http.post<boolean>(`${environment.apiUrl}/fct/cancelproduct`, {userId, productId} )
            .pipe(map(isProductPurchased => {
                return isProductPurchased;
            }));
    }
}