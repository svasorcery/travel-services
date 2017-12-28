import { Component, OnInit } from '@angular/core';

import { TrainsListRequest } from './rail.models';
import { RailService } from './rail.service';

@Component({
    templateUrl: 'search.component.html'
})
export class RailSearchComponent implements OnInit {
    request: TrainsListRequest;

    constructor(private _rail: RailService) { }

    ngOnInit() {
        this.request = new TrainsListRequest('', '', '');
     }

    public search(): void {
        if (!this.request) return;

        this._rail.setSearch(this.request);
        this._rail.gotoTrains();
    }
}
