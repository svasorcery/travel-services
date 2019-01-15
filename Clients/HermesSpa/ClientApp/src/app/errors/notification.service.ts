import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';


@Injectable()
export class NotificationService {
    private _notification: BehaviorSubject<string>;
    readonly notification$: Observable<any>;

    constructor() {
        this._notification = new BehaviorSubject(null);
        this.notification$ = this._notification.asObservable();
    }

    public notify = (message: any) => {
        console.error(message);
        this._notification.next(message);
    }
}
