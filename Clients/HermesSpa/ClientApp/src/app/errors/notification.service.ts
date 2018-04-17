import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import 'rxjs/add/operator/publish';


@Injectable()
export class NotificationService {
    private _notification: BehaviorSubject<string>;
    readonly notification$: Observable<any>;

    constructor() {
        this._notification = new BehaviorSubject(null);
        this.notification$ = this._notification.asObservable().publish().refCount();
    }

    public notify = (message) => {
        console.error(message);
        this._notification.next(message);
    }
}
