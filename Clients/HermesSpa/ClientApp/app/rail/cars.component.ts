import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { CarsListResult } from './rail.models';
import { RailService } from './rail.service';

@Component({
    templateUrl: 'cars.component.html'
})
export class CarsListComponent implements OnInit {
    result: CarsListResult;
    error: boolean = false;
    
    constructor(
        private _rail: RailService,
        private _route: ActivatedRoute
    ) { }

    ngOnInit() {
        this._route
        .queryParams
        .switchMap(params => {
            return this._rail.queryCars(params['sessionId'], +params['optionRef']); 
        })
        .subscribe(
            result => this.result = result,
            error => this.error = true
        );
    }
}
