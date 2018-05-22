import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { SeatsListResult } from './rail.models';
import { RailService } from './rail.service';

@Component({
    template: `<traveller></traveller>`
    // templateUrl: 'order.component.html'
})
export class RailOrderComponent implements OnInit {
    result: SeatsListResult;

    constructor(
        private _rail: RailService,
        private _route: ActivatedRoute) { }

    ngOnInit() {
        this._route
        .queryParams
        .switchMap(params => {
            return this._rail.querySeats(params['sessionId'], +params['trainOption'], +params['optionRef']);
        })
        .subscribe(
            result => console.log(result), //this.result = result,
            error => console.log(error)
        );
    }
}
