import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { switchMap } from 'rxjs/operators';

import { RailService } from './rail.service';
import { SeatsListResult, SeatOptionParams, Traveller } from './rail.models';

@Component({
    templateUrl: 'order.component.html'
})
export class RailOrderComponent implements OnInit {
    result: SeatsListResult;
    params: SeatOptionParams;
    passengers: Traveller[] = [];

    constructor(
        private _rail: RailService,
        private _route: ActivatedRoute
    ) { }

    ngOnInit() {
        this._route.queryParams.pipe(switchMap(params => {
            const sessionId = params['sessionId'];
            const trainRef = +params['trainRef'];
            const optionRef = +params['optionRef'];
            this.params = new SeatOptionParams(sessionId, trainRef, optionRef);
            return this._rail.querySeats(sessionId, trainRef, optionRef);
        }))
        .subscribe(
            result => this.result = result,
            error => console.log(error)
        );
    }

    submit() {
        console.log(this.params);
        console.log(this.passengers);
        this._rail.reserveCreate(this.params, this.passengers)
            .subscribe(
                result => console.log(result),
                error => console.log(error)
            );
    }
}
