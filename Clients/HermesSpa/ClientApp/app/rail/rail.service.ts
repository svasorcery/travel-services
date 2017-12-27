import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { Http, Response, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/switchMap';

import { TrainsListRequest } from './rail.models';

@Injectable()
export class RailService {
    private _url: string;    
    private _search: TrainsListRequest;

    constructor(
        private http: Http,
        @Inject("BASE_URL") private baseUrl: string,
        private router: Router
    ) { 
        this._url = `${baseUrl}/api/rail/trains`
    }

    public setSearch(params: TrainsListRequest): void {
        this._search = params;
    }

    public getSearch(): TrainsListRequest {
        return this._search;
    }
    
    public queryTrains(request: TrainsListRequest): Observable<any> {
        var params = new URLSearchParams();
        params.set('from', request.from);
        params.set('to', request.to);
        params.set('departDate', request.departDate);
        //params.set('fromHour', request.hourFrom.toString());
        //params.set('toHour', request.hourTo.toString());        

        return this.http.get(this._url, { search: params })
            .map((response: Response) => response.json() as any);
    }


    public gotoTrains(): void {
        this.router.navigate(['rail', 'trains'], {
            queryParams: { 
                from: this._search.from,
                to: this._search.to,
                date: this._search.departDate,
                //t0: this._search.hourFrom,
                //t1: this._search.hourTo
             }
        });
    }
}
