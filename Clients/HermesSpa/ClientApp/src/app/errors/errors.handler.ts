import { ErrorHandler, Injectable, Injector } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

import { ApiError } from './errors.models';
import { NotificationService } from './notification.service';

@Injectable()
export class ErrorsHandler implements ErrorHandler {
    constructor(private injector: Injector) { }

    public handleError = (error: Error | HttpErrorResponse) => {
        const notification = this.injector.get(NotificationService);
        const router = this.injector.get(Router);

        if (error instanceof HttpErrorResponse) {
            // Server error happened
            if (!navigator.onLine) {
                // No Internet connection
                return notification.notify(new ApiError(503, 'Service Unavailable', 'No Internet Connection'));
            }
            // Http Error
            console.warn(`Backend returned code ${error.status}`);
            // Unauthorized Http Error
            if (error.status === 401) {
                router.navigate(['/account', 'login']);
            }
            // Forbidden Http Error
            if (error.status === 403) {
                router.navigate(['/forbidden']);
            }
            let errorData = error.error.code ? error.error : ApiError.fromHttpErrorResponse(error);
            return notification.notify(errorData);
        } else {
            // Client Error happend
            router.navigate(['/error'], { queryParams: { error: error } });
        }
        // Log the error anyway
        console.error(error);
    }
}
