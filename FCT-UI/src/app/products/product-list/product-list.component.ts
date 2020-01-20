import { AuthenticationService } from './../../_services/authentication.service';
import { gt } from 'lodash';
import { Product } from '@app/_models/product';
import { Component, OnInit, ViewChild, AfterContentInit, OnDestroy} from '@angular/core';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '@app/_services';
import { first } from 'rxjs/operators';
import { ProductDatasource } from './product-datasource';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit, OnDestroy, AfterContentInit  {
  @ViewChild(MatPaginator, {
    static: true
  }) paginator: MatPaginator;
  @ViewChild(MatSort, {
    static: true
  }) sort: MatSort;

  
  totalNumberOfProductsToDisplay: number;

  dataSource: ProductDatasource;
  displayedColumns: string[];
  productsSubscription: Subscription;
 
  constructor(
    private router: Router,
    private productService: ProductService,
    private authenticationService: AuthenticationService
   ) { }

  ngOnInit() {

    this.totalNumberOfProductsToDisplay = 5; //TODO- can get from environments

    this.dataSource = new ProductDatasource(this.paginator, this.sort, this.totalNumberOfProductsToDisplay);

    this.displayedColumns = ['id', 'name', 'price', 'action'];  }


  ngAfterContentInit() {
    this.productsSubscription = this.productService.getAll()
    .subscribe(
      results => {
        if (results.length > 0 ) {
          this.dataSource.setProducts(results);
        }
      }
    );
  }

  buyProduct(product: Product): void {    
    let userId = this.authenticationService.currentUserValue.userId;
    this.productService.placeOrder( userId, product.id)
    .subscribe(
      placeOrder => {
        if (placeOrder) {
          alert('Item Purchased, Product Name - ' + product.name);
          this.productService.getUserProducts(userId)
          .subscribe(
            results => {
              if (results.length > 0 ) {
                this.dataSource.setProducts(results);
                this.router.navigate(['/']);
              }
            }
          );
        }
        else{
          alert('Unable to place your Order, Product Name - ' + product.name);
        }
      }
    );
  }
 /**
  *  destroy the productsSubscription
  */
  ngOnDestroy() {
    this.productsSubscription.unsubscribe();
  }
}