import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { ErrorRoutingModule, errorsComponents } from './errors.routing';
import { ErrorsHandler } from './errors.handler';
import { NotificationService } from './notification.service';
import { ServerErrorsInterceptor } from './server-errors.interceptor';

@NgModule({
    imports: [
        CommonModule,
        ErrorRoutingModule
    ],
    declarations: [
        ...errorsComponents
    ],
    providers: [
        {
          provide: ErrorHandler,
          useClass: ErrorsHandler,
        },
        {
          provide: HTTP_INTERCEPTORS,
          useClass: ServerErrorsInterceptor,
          multi: true
        },
        NotificationService
    ],
    exports: [

    ],
})
export class ErrorsModule {

}
