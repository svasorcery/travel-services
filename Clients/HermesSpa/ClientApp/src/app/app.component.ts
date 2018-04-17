import { Component, OnInit } from '@angular/core';

import { ApiError } from './errors/errors.models';
import { NotificationService } from './errors/notification.service';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
    error: ApiError;

    constructor(private _notification: NotificationService) { }

    ngOnInit() {
        this._notification
            .notification$
            .subscribe(error => this.error = error);
      }
}
