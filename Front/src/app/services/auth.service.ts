import {EventEmitter, Injectable, Output} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {SignInRequestModel, SignOutRequestModel} from '../RequestModels/AuthRequestModels';
import {Message} from '../Infrastructure/Message';
import {Environment, Local, Production} from "../../environments/environment";
import {Util} from "../Utils/Util";
import {catchError} from "rxjs/operators";

const httpOptions = {
  headers: new HttpHeaders({
  })};

let authServiceUrl = Local.AUTH_SERVICE_URL;

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  @Output() signOutCompleted: EventEmitter<boolean> = new EventEmitter();
  @Output() signInCompleted: EventEmitter<boolean> = new EventEmitter();

  constructor(private http: HttpClient,
              util: Util) {
    if (Environment.IsProduction) {
      authServiceUrl = Production.AUTH_SERVICE_URL;
    }
  }

    signIn(model: SignInRequestModel) {
      return this.http.post<Message>(authServiceUrl + '/Auth' + '/SignIn', model, httpOptions)
        .pipe(
          catchError(Util.handleError)
        )
        .subscribe(res => {
          Util.showResultMessage(res);
          if (res.isSuccess) {
            const data = JSON.parse(res.data);
            localStorage.setItem('token', data);
            localStorage.setItem('login', model.login);
            this.signInCompleted.emit(true);
          }
        });
    }

    signOut(model: SignOutRequestModel) {
      return this.http.post<Message>(authServiceUrl + '/Auth' + '/SignOut', model, httpOptions)
        .pipe(
          catchError(Util.handleError)
        )
        .subscribe(res => {
          Util.showResultMessage(res);
          if (res.isSuccess) {
            this.signOutCompleted.emit(true);
          }
        });
    }
}
