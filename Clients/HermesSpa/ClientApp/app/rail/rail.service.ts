import { Injectable, Inject } from '@angular/core';
import { Http, Response, URLSearchParams } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';

import { TrainsListRequest } from './rail.models';

@Injectable()
export class RailService {
    private _url: string;

    constructor(
        private http: Http,
        @Inject("BASE_URL") private baseUrl: string
    ) { 
        this._url = `${baseUrl}/api/rail/trains`
    }
    
    public queryTrains(request: TrainsListRequest): Observable<any> {
        var params = new URLSearchParams();
        params.set('from', request.from);
        params.set('to', request.to);
        params.set('departDate', request.departDate);
        //params.set('fromHour', request.hourFrom.toString());
        //params.set('toHour', request.hourTo.toString());        

        return this.http.get(this._url, {search: params })
            .map((response: Response) => response.json() as any);
    }
}
