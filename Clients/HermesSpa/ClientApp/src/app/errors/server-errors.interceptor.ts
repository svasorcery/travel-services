import { Injectable, ErrorHandler } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/retry';
import 'rxjs/add/operator/catch';

@Injectable()
export class ServerErrorsInterceptor implements HttpInterceptor {
    constructor(
        private _handler: ErrorHandler
    ) { }

    intercept = (request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> =>
        next.handle(request)
            .catch((error: HttpErrorResponse) => {
                this._handler.handleError(error);
                return Observable.empty<HttpEvent<any>>();
            })
}
