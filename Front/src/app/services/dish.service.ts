import {EventEmitter, Injectable, Output} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Message} from '../Infrastructure/Message';
import {Observable} from 'rxjs';
import {CreateDishRequestModel, UpdateDishRequestModel} from '../RequestModels/DishRequestModels';
import {DishResponseModel} from '../ResponseModels/DishResponseModels';
import {catchError} from 'rxjs/operators';
import {Environment, Local, Production} from "../../environments/environment";
import {Util} from "../Utils/Util";

const httpOptions = {
  headers: new HttpHeaders({
  })};

let orderServiceUrl = Local.ORDERS_SERVICE_URL;

@Injectable({
  providedIn: 'root'
})
export class DishService {
  @Output() createdDish: EventEmitter<DishResponseModel> = new EventEmitter();
  @Output() editedDish: EventEmitter<DishResponseModel> = new EventEmitter();
  @Output() deletedDish: EventEmitter<number> = new EventEmitter();

  constructor(private http: HttpClient,
              util: Util) {
    if (Environment.IsProduction) {
      orderServiceUrl = Production.ORDERS_SERVICE_URL;
    }
  }

  getAll(): Observable<Message> {
    return this.http.get<Message>(orderServiceUrl + '/Dishes', httpOptions)
      .pipe(
        catchError(Util.handleError)
      );
  }

  createDish(model: CreateDishRequestModel) {
    return this.http.post<Message>(orderServiceUrl + '/Dishes', model, httpOptions)
      .pipe(
        catchError(Util.handleError)
      )
      .subscribe(res => {
        Util.showResultMessage(res);
        if (res.isSuccess) {
          const data = JSON.parse(res.data);
          this.createdDish.emit(data);
        }
      });
  }

  editDish(model: UpdateDishRequestModel) {
    return this.http.put<Message>(orderServiceUrl + '/Dishes', model, httpOptions)
      .pipe(
        catchError(Util.handleError)
      )
      .subscribe(res => {
        Util.showResultMessage(res);
        if (res.isSuccess) {
          const data = JSON.parse(res.data);
          this.editedDish.emit(data);
        }
      });
  }

  deleteDish(id: number) {
    return this.http.delete<Message>(orderServiceUrl + '/Dishes' + id, httpOptions)
      .pipe(
        catchError(Util.handleError)
      )
      .subscribe(res => {
        Util.showResultMessage(res);
        if (res.isSuccess) {
          this.deletedDish.emit(id);
        }
      });
  }
}
