import { Product } from '@app/_models/product';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { BehaviorSubject } from 'rxjs';
import { toLower, deburr, trim } from 'lodash';

export class CustomerProductDatasource extends MatTableDataSource<Product> {
    private products: Product[];
    totalNumberOfProductsToDisplay: number;
  
    constructor(public paginator: MatPaginator, public sort: MatSort, public pageSize: number ) {
      super();
  
      this.paginator.pageSize = pageSize;
      this.totalNumberOfProductsToDisplay = pageSize;
  
      this.sortingDataAccessor = this.customSortingDataAccessor;
      this.sort.sortChange.subscribe((sortParams: MatSort) => {
        const orderedData = this.sortData(this.data, sortParams);
        this.data = orderedData;
      });
    }
  
    /**
     * Connect this data source to the table. The table will only update when
     * the returned stream emits new items.
     *
     * @returns A stream of the items to be rendered.
     * @memberof CustomerProductDatasource
     */
    connect(): BehaviorSubject<Product[]> {
      return super.connect();
    }
  
    /**
     * Called when the table is being destroyed. Use this function, to clean up
     * any open connections or free any held resources that were set up during connect.
     *
     * @memberof CustomerProductDatasource
     */
    disconnect(): void {
      super.disconnect();
      this.sort.sortChange.unsubscribe();
    }
  
    /**
     * Loads the next page of contacts
     *
     * @memberof CustomerProductDatasource
     */
    nextPage(): void {
      this.totalNumberOfProductsToDisplay += this.pageSize;
      this.paginator._changePageSize(this.totalNumberOfProductsToDisplay);
    }
  
    /**
     * Sets the contacts for the datasource
     * - Clears any filter and applies the current sort
     *
     * @param {Product[]} displayData Products to binded to the datatable
     * @memberof CustomerProductDatasource
     */
    setProducts(displayData: Product[]) {
      this.data = this.products = this.sortData(displayData, this.sort);
    }

  
    /**
     * Reconfigures the searching on the different properties of a product
     * @param {product} product the product that is going to be searched
     * @param {string} property the product property that is being searched
     * @returns {any} propVal the value of that property
     * @memberof CustomerProductDatasource
     */
    customSortingDataAccessor(product: Product, property: string) {
      let propVal: any;
      switch (property) {
        case 'id':
          propVal = this.toLowerDeburred(`${product.id}`);
          break;
        case 'name':
          propVal = this.toLowerDeburred(`${product.name}`);
          break;
        case 'price':
          propVal = this.toLowerDeburred(`${product.price}`);
          break;
        default:
          propVal = product[property];
      }
      return propVal;
    }

    /**
     * Funtion that lowers and deburrs the string for searching
     *
     * @param {string} value string to apply on
     * @returns a deburred and lowered string
     * @memberof CustomerProductDatasource
     */
    toLowerDeburred(value: string) {
      return toLower(deburr(trim(value)));
    }
  }