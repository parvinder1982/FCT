
import { Product } from '@app/_models/product';
import { Component, OnInit, ViewChild, AfterContentInit, OnDestroy} from '@angular/core';
import { MatPaginator, MatSort } from '@angular/material';
import { ProductService, AuthenticationService } from '@app/_services';
import { Subscription } from 'rxjs';
import { CustomerProductDatasource } from './customer-product-datasource';
@Component({
  selector: 'app-customer-product-list',
  templateUrl: './customer-product-list.component.html',
  styleUrls: ['./customer-product-list.component.css']
})
export class CustomerProductListComponent implements OnInit, OnDestroy, AfterContentInit  {
  @ViewChild(MatPaginator, {
    static: true
  }) paginator: MatPaginator;
  @ViewChild(MatSort, {
    static: true
  }) sort: MatSort;

  showList: boolean = false;
  totalNumberOfProductsToDisplay: number;

  dataSource: CustomerProductDatasource;
  displayedColumns: string[];
  productsSubscription: Subscription;
 
  constructor(
    private productService: ProductService,
    private authenticationService: AuthenticationService
   ) { }

  ngOnInit() {

    this.totalNumberOfProductsToDisplay = 5; //TODO- can get from environments

    this.dataSource = new CustomerProductDatasource(this.paginator, this.sort, this.totalNumberOfProductsToDisplay);

    this.displayedColumns = ['id', 'name', 'price', 'action'];  }


  ngAfterContentInit() {
     this.productsSubscription = this.productService.getUserProducts(this.authenticationService.currentUserValue.userId)
    .subscribe(
      results => {
        if (results.length > 0 ) {
          this.showList = true;
          this.dataSource.setProducts(results);
        }
      }
    );
  }

  cancelOrder(product: Product): void {    
    let userId = this.authenticationService.currentUserValue.userId;
    this.productService.cancelOrder( userId, product.id)
    .subscribe(
      cancelOrder => {
        if (cancelOrder) {     
          alert('Order cancelled, Product Name - ' + product.name);
          this.productService.getUserProducts(userId)
          .subscribe(
            results => {
                this.dataSource.setProducts(results);
            }
          );
        }
        else{
          alert('Unable to cancelled your Order, Product Name - ' + product.name);
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