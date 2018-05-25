import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { SeatsListResult, SeatOptionParams } from './rail.models';
import { RailService } from './rail.service';

@Component({
    templateUrl: 'order.component.html'
})
export class RailOrderComponent implements OnInit {
    result: SeatsListResult;
    params: SeatOptionParams;

    constructor(
        private _rail: RailService,
        private _route: ActivatedRoute
    ) { }

    ngOnInit() {
        this._route.queryParams
            .switchMap(params => {
                this.params = new SeatOptionParams(+params['optionRef']);
                return this._rail.querySeats(params['sessionId'], +params['trainRef'], +params['optionRef']);
            })
            .subscribe(
                result => this.result = result,
                error => console.log(error)
            );
    }
}
