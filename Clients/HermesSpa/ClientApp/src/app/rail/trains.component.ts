import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { TrainsListRequest, TrainsListResult } from './rail.models';
import { RailService } from './rail.service';

@Component({
    templateUrl: 'trains.component.html'
})
export class TrainsListComponent implements OnInit {
    result: TrainsListResult;
    error: boolean = false;
    
    constructor(
        private _rail: RailService,
        private _route: ActivatedRoute
    ) { }

    ngOnInit() {
        this._route
            .queryParams
            .switchMap(params => {
                var request = new TrainsListRequest(params['from'], params['to'], params['date']);
                return this._rail.queryTrains(request);
            })
            .subscribe(
                result => this.result = result,
                error => this.error = true
            );
    }

    public select = (optionRef: number): void =>
        this._rail.gotoCars(this.result.sessionId, optionRef);
}
