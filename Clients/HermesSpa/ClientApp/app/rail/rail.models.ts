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
    sessionId: string;
    origin: string;
    originCode: string;
    destination: string;
    destinationCode: string;
    departureDate: Date;
    timeType: number;
    trains: Train[];
}

export class CarsListResult {
    cars: Car[];
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

class Car {
    optionRef: number;
    number: string;
    type: CarType;
    serviceClass: string;
    serviceClassInternational: string;
    letter: string;
    categories: string;
    schemeId: string;
    preePlaceNumbers: number[];
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

class Price  { 
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
    places: number[];
    count: number;    
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
