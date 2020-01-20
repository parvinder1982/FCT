import { Component } from '@angular/core';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ProductService, AuthenticationService } from '@app/_services';
import { Product } from '@app/_models/product';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent {
    loading = false;
    products: Product[];
    userName: string;
    constructor(private productService: ProductService,
        private authService: AuthenticationService
        ) { }

    ngOnInit() {
        this.loading = true;
        this.userName = this.authService.currentUserValue.username;
        this.productService.getAll().pipe(first()).subscribe(products => {
            this.loading = false;
            this.products = products;
        });
    }
}