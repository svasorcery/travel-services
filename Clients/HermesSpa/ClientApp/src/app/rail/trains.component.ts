import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';

import { TrainsListRequest, TrainsListResult } from './rail.models';
import { RailService } from './rail.service';

@Component({
    templateUrl: 'trains.component.html'
})
export class TrainsListComponent implements OnInit {
    result: TrainsListResult;

    constructor(
        private _rail: RailService,
        private _route: ActivatedRoute
    ) { }

    ngOnInit() {
        this._route.queryParams.pipe(switchMap(params => {
            const request = new TrainsListRequest(params['from'], params['to'], params['date']);
            return this._rail.queryTrains(request);
        }))
        .subscribe(
            result => this.result = result,
            error => console.log(error)
        );
    }

    public select = (optionRef: number): void =>
        this._rail.gotoCars(this.result.sessionId, optionRef);
}
