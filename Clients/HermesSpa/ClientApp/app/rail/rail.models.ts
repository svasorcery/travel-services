export class TrainsListRequest {
    constructor(
        public from: string,
        public to: string,
        public departDate: string,
        public hourFrom?: number,
        public hourTo?: number
    ) { }
}

export class TrainsListResult {
    origin: string;
    originCode: string;
    destination: string;
    destinationCode: string;
    departureDate: Date;
    timeType: number;
    trains: Train[];
}

class Train {
    optionRef: number;
    name: string;
    number: string;
    displayNumber: string;
    routeStart: TripEvent;
    routeEndStation: string;
    depart: TripEvent;
    arrive: TripEvent;
    arriveLocal: TripEvent;
    tripDuration: number;
    timezoneDifference: string;
    carrier: string;
    brand: string;
    bEntire: boolean;
    isFirm: boolean;
    isComponent: boolean;
    hasElectronicRegistration: boolean;
    hasDynamicPricing: boolean;
    tripDistance?: number;
    cars: CarInfo[];
}

class CarInfo {
    type: string;
    serviceClass: string;
    freeSeats: number;
    minPrice: number;
    bonusPoints?: number;
}

class TripEvent {
    dateAndTime: Date;
    timeType: number;
    station: { code: string, name: string };
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
