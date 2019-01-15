import { Component } from '@angular/core';

import { TrainsListRequest } from './rail.models';
import { RailService, RailStationsListSource } from './rail.service';

@Component({
    templateUrl: 'search.component.html'
})
export class RailSearchComponent {
    searchParams: TrainsListRequest;
    railStationsSource: RailStationsListSource;

    constructor(private _rail: RailService) {
        this.searchParams = this._rail.getSearch();
        this.railStationsSource = this._rail.getRailStationsSource();
    }

    public search = (): void => {
        if (!this.searchParams) return;
        this._rail.setSearch(this.searchParams);
        this._rail.gotoTrains();
    }
}
