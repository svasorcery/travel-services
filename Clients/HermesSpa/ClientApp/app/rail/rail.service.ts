import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { Http, Response, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/switchMap';

import { TrainsListRequest, TrainsListResult, CarsListResult, RailStation } from './rail.models';

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
        return this._search ? this._search : new TrainsListRequest('', '', '');
    }
    
    public queryTrains(request: TrainsListRequest): Observable<TrainsListResult> {
        var params = new URLSearchParams();
        params.set('from', request.fromCity);
        params.set('to', request.toCity);
        params.set('departDate', request.departDate);
        //params.set('fromHour', request.hourFrom.toString());
        //params.set('toHour', request.hourTo.toString());        

        return this.http.get(this._url, { search: params })
            .map((response: Response) => response.json() as TrainsListResult);
    }

    public queryCars(sessionId: string, optionRef: number): Observable<CarsListResult> {
        var params = new URLSearchParams();
        params.set('sessionId', sessionId);
        params.set('optionRef', optionRef.toString());

        return this.http.get(`${this._url}/cars`, { search: params })
            .map((response: Response) => response.json() as CarsListResult)
    }


    public gotoTrains(): void {
        this.router.navigate(['rail', 'trains'], {
            queryParams: { 
                from: this._search.fromCity,
                to: this._search.toCity,
                date: this._search.departDate,
                //t0: this._search.hourFrom,
                //t1: this._search.hourTo
            }
        });
    }

    public gotoCars(sessionId: string, optionRef: number): void {
        this.router.navigate(['rail', 'cars'], {
            queryParams: { 
                sessionId: sessionId,
                optionRef: optionRef.toString()
            }
        });
    }
}
