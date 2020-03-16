import {EventEmitter, Injectable, Output} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CustomerResponseModel} from '../ResponseModels/CustomerResponseModels';
import {CreateCustomerRequestModel, EditCustomerRequestModel} from '../RequestModels/CustomerRequestModels';
import {Message} from '../Infrastructure/Message';
import {Environment, Local, Production} from "../../environments/environment";
import {Util} from "../Utils/Util";
import {catchError} from "rxjs/operators";

const httpOptions = {
  headers: new HttpHeaders({
  })};

let customerServiceUrl = Local.CUSTOMER_SERVICE_URL;

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  @Output() createdCustomer: EventEmitter<CustomerResponseModel> = new EventEmitter<CustomerResponseModel>();
  @Output() editedCustomer: EventEmitter<CustomerResponseModel> = new EventEmitter<CustomerResponseModel>();
  @Output() deletedCustomer: EventEmitter<number> = new EventEmitter<number>();

  constructor(private http: HttpClient,
              util: Util) {
    if (Environment.IsProduction) {
      customerServiceUrl = Production.CUSTOMER_SERVICE_URL;
    }
  }

  getAll(): Observable<Message> {
    return this.http.get<Message>(customerServiceUrl + '/Customers', httpOptions)
      .pipe(
        catchError(Util.handleError)
      );
  }

  createCustomer(model: CreateCustomerRequestModel) {
    return this.http.post<Message>(customerServiceUrl + '/Customers', model, httpOptions)
      .pipe(
        catchError(Util.handleError)
      )
      .subscribe(res => {
        Util.showResultMessage(res);
        if (res.isSuccess) {
          const data = JSON.parse(res.data);
          this.createdCustomer.emit(data);
        }
      });
  }

  editCustomer(model: EditCustomerRequestModel) {
    return this.http.put<Message>(customerServiceUrl + '/Customers', model, httpOptions)
      .pipe(
        catchError(Util.handleError)
      )
      .subscribe(res => {
        Util.showResultMessage(res);
        if (res.isSuccess) {
          const data = JSON.parse(res.data);
          this.editedCustomer.emit(data);
        }
      });
  }

  deleteCustomer(id: number) {
    return this.http.delete<Message>(customerServiceUrl + '/Customers' + id, httpOptions)
      .pipe(
        catchError(Util.handleError)
      )
      .subscribe(res => {
        Util.showResultMessage(res);
        if (res.isSuccess) {
          this.deletedCustomer.emit(id);
        }
      });
  }
}
