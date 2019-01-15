import { Injectable, ErrorHandler } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, empty } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ServerErrorsInterceptor implements HttpInterceptor {
    constructor(
        private _handler: ErrorHandler
    ) { }

    intercept = (request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> =>
        next.handle(request).pipe(
            catchError((error: HttpErrorResponse) => {
                this._handler.handleError(error);
                return empty();
        }))
}
