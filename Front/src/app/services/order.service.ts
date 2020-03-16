import {EventEmitter, Injectable, Output} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Observable} from 'rxjs';
import {OrderResponseModel} from '../ResponseModels/OrderResponseModel';
import {Message} from '../Infrastructure/Message';
import {catchError} from 'rxjs/operators';
import {Environment, Local, Production} from "../../environments/environment";
import {EditOrderRequestModel} from "../RequestModels/OrderRequestModels";
import {Util} from "../Utils/Util";

const httpOptions = {
  headers: new HttpHeaders({
  })};

let orderServiceUrl = Local.ORDERS_SERVICE_URL;

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  @Output() createdOrder: EventEmitter<OrderResponseModel> = new EventEmitter();
  @Output() editedOrder: EventEmitter<OrderResponseModel> = new EventEmitter();
  @Output() deletedOrder: EventEmitter<number> = new EventEmitter();

  constructor(private http: HttpClient,
              public util: Util) {
    if (Environment.IsProduction) {
      orderServiceUrl = Production.ORDERS_SERVICE_URL;
    }
  }

  getAll(): Observable<Message> {
    return this.http.get<Message>(orderServiceUrl + '/Orders', httpOptions)
      .pipe(
        catchError(Util.handleError)
      );
  }

  createOrder(model) {
    return this.http.post<Message>(orderServiceUrl + '/Orders', model, httpOptions)
      .pipe(
        catchError(Util.handleError)
      )
      .subscribe(res => {
        Util.showResultMessage(res);
        if (res.isSuccess) {
            const data = JSON.parse(res.data);
            this.createdOrder.emit(data);
          }
      });
  }

  editOrder(model: EditOrderRequestModel) {
    return this.http.put<Message>(orderServiceUrl + '/Orders', model, httpOptions)
      .pipe(
        catchError(Util.handleError)
      )
      .subscribe(res => {
        Util.showResultMessage(res);
        if (res.isSuccess) {
          const data = JSON.parse(res.data);
          this.editedOrder.emit(data);
        }
      });
  }

  deleteOrder(id: number) {
    return this.http.delete<Message>(orderServiceUrl + '/Orders' + id, httpOptions)
      .pipe(
        catchError(Util.handleError)
      )
      .subscribe(res => {
        Util.showResultMessage(res);
        if (res.isSuccess) {
          this.deletedOrder.emit(id);
        }
      });
  }
}
