import {throwError} from "rxjs/index";
import {HttpErrorResponse} from "@angular/common/http";
import {MatSnackBar} from "@angular/material";
import {Injectable} from "@angular/core";
import {Message} from "../Infrastructure/Message";

const durationSnackBarOnResponse = 1000;

@Injectable({
  providedIn: 'root'
})
export class Util {

  public static _snackBar: MatSnackBar;

  constructor(private snackBar: MatSnackBar){
    Util._snackBar = snackBar;
  }

  public static handleError(error: HttpErrorResponse) {
    Util._snackBar.open('Error ' + error.statusText + ' with code ' + error.status, 'Close', {
      duration: durationSnackBarOnResponse,
      verticalPosition: 'top',
      horizontalPosition: 'right'
    });

    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      connsole.error(
        `Backend returned code ${error.status}`);
    }

    return throwError(
      'Something bad happened; please try again later.');
  }

  public static showResultMessage(response: Message) {
    if (response.isSuccess) {
      Util._snackBar.open('Success', 'Close', {
        duration: durationSnackBarOnResponse,
        verticalPosition: 'top',
        horizontalPosition: 'right'
      });
    } else {
      Util._snackBar.open('Error message from server ' + response.errorMessage, 'Close', {
        duration: durationSnackBarOnResponse,
        verticalPosition: 'top',
        horizontalPosition: 'right'
      });
    }
  }
}
