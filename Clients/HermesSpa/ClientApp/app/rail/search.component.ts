import { Component, OnInit, Inject } from '@angular/core';
import { Http, Response, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import { TrainsListRequest, RailStation } from './rail.models';
import { RailService } from './rail.service';

import { IAutoCompleteListSource } from '../shared/autocomplete.component';

class RailStationsListSource implements IAutoCompleteListSource {

    constructor(private http: Http, private baseUrl: string) { }

    search(term: string): Observable<{ name: string }[]> {
        let params = new URLSearchParams();
        params.set('term', term);
        return this.http.get(this.baseUrl + '/api/rail/stations', { search: params })
            .map((response: Response) => response.json() as RailStation[]);
    }
}


@Component({
    templateUrl: 'search.component.html'
})
export class RailSearchComponent implements OnInit {
    searchParams: TrainsListRequest;
    railStationsSource: RailStationsListSource;

    constructor(
        private http: Http,
        @Inject('BASE_URL') baseUrl: string,
        private _rail: RailService
    ) {
        this.searchParams = this._rail.getSearch();
        this.railStationsSource = new RailStationsListSource(http, baseUrl);
    }

    ngOnInit() {
        
    }

    public search(): void {
        if (!this.searchParams) return;
        this._rail.setSearch(this.searchParams);
        this._rail.gotoTrains();
    }
}
