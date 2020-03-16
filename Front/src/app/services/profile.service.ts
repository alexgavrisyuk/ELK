import {EventEmitter, Injectable, Output} from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http';
import {Message} from '../Infrastructure/Message';
import {UserResponseModel} from '../ResponseModels/UserResponseModels';
import {EditProfileRequestModel} from '../RequestModels/AuthRequestModels';
import {Environment, Local, Production} from "../../environments/environment";
import {Util} from "../Utils/Util";
import {catchError} from "rxjs/operators";

const httpOptions = {
  headers: new HttpHeaders({
  })};

let profileServiceUrl = Local.AUTH_SERVICE_URL;

@Injectable({
  providedIn: 'root'
})
export class ProfileService {
  @Output() returnsProfile: EventEmitter<UserResponseModel> = new EventEmitter();
  @Output() editedProfile: EventEmitter<boolean> = new EventEmitter();

  constructor(private http: HttpClient,
              util: Util) {
    if (Environment.IsProduction) {
      profileServiceUrl = Production.AUTH_SERVICE_URL;
    }
  }

  get(email: string) {
    return this.http.get<Message>(profileServiceUrl + '/Profile' + '/?email=' + email, httpOptions)
      .pipe(
        catchError(Util.handleError)
      )
      .subscribe(res => {
        Util.showResultMessage(res);
        if (res.isSuccess) {
          const data = JSON.parse(res.data);
          this.returnsProfile.emit(data);
        }
      });
  }

  editProfile(model: EditProfileRequestModel) {
    return this.http.put<Message>(profileServiceUrl + '/Profile', model, httpOptions)
      .pipe(
        catchError(Util.handleError)
      )
      .subscribe(res => {
        Util.showResultMessage(res);
        if (res.isSuccess) {
          this.editedProfile.emit(true);
        }
      });
  }
}
