export class TrainsListRequest {
    from: string;
    to: string;
    departDate: string;
    hourFrom?: number;
    hourTo?: number;
}

export class RailStation {
    id: string;
    name: string;
    express: number;
    esr?: number;
    location?: Location;
}

class Location {
    country: string;
    region: string;
    railway: string;
    latitude?: number;
    longitude?: number;
}
