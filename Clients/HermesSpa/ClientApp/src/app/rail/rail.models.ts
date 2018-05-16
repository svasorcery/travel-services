export class RailStation {
    constructor(
        public name: string,
        public express: string,
        public esr?: number,
        public location?: Location
    ) { }
}

class Location {
    country: string;
    region: string;
    railway: string;
    latitude?: number;
    longitude?: number;
}

export class TrainsListRequest {
    private _fromCity: string;
    private _toCity: string;

    constructor(
        public from: RailStation | String,
        public to: RailStation | String,
        public departDate: string,
        public hourFrom?: number,
        public hourTo?: number
    ) {
        if (typeof from === 'string') {
            this.from = new RailStation('', from);
            this._fromCity = from !== '' ? from : null;
        } else {
            this.from = from;
            this._fromCity = (<RailStation>from).express;
        }

        if (typeof to === 'string') {
            this.to = new RailStation('', to);
            this._toCity = to !== '' ? to : null;
        } else {
            this.to = to;
            this._toCity = (<RailStation>to).express;
        }
    }

    public get fromCity(): string { return this._fromCity ? this._fromCity : (<RailStation>this.from).express; }

    public get toCity(): string { return this._toCity ? this._toCity : (<RailStation>this.to).express; }
}

export class TrainsListResult {
    sessionId: string;
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

export class CarsListResult {
    sessionId: string;
    trainOption: number;
    cars: Car[];
    insuranceProviders: InsuranceProvider[] = [];
    ageLimits: AgeRestrictions;
}

class InsuranceProvider {
    id: number;
    fullName: string;
    shortName: string;
    offerUrl: string;
    insuranceCost: number;
    insuranceBenefit: number;
}

class AgeRestrictions {
    infantWithoutPlace: number;
    childWithPlace: number;
}

export class Car {
    optionRef: number;
    number: string;
    type: CarType;
    serviceClass: string;
    serviceClassInternational: string;
    letter: string;
    categories: string;
    schemeId: string;
    freePlaceNumbers: number[];
    specialSeatTypes: string[];
    freeSeats: SeatGroup[];
    services: { name: string, description: string }[];
    servicesDescription: string;
    price: { min: Price, max: Price };
    carrier: string;
    owner: string;
    hasElectronicRegistration: boolean;
    hasDynamicPricing: boolean;
    isNoSmoking: boolean;
    canAddBedding: boolean;
    hasBeddingIncluded: boolean;
    isTwoStorey: boolean;
    isWebSalesForbidden: boolean;
}

class TripEvent {
    dateAndTime: Date;
    timeType: number;
    station: { code: string, name: string };
}

class Price {
    total: number;
}

class CarType {
    id: number;
    category: string;
    name: string;
    displayName: string;
}

class SeatGroup {
    type: string;
    label: string;
    price: Price;
    places: CarPlace[];
    count: number;
}

export class CarPlace {
  number: number;
  gender: string;
  price: Price;
  isFree: boolean;
}

export class CarSchemeRzd {
    id: number;
    rows: SchemeCell[][];
}

export class SchemeCell {
    type: string;
    place: CarPlace;
    content: string;
    styleClass: string;
    border: string;
}

export class PlacesRange {
    constructor (
        public range0: number = null,
        public range1: number = null
    ) { }
}
