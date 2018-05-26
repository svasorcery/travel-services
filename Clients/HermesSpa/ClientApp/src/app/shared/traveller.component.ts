import { Component, OnInit, Inject, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Validator, NG_VALIDATORS, AbstractControl, ValidationErrors } from '@angular/forms';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { IAutoCompleteListSource } from './autocomplete.component';

export class Traveller {
    ref: number;
    firstName: string;
    middleName: string;
    lastName: string;
    gender: string;
    birthDate: Date;
    passport: Passport = new Passport();
}

export class Passport {
    type: string;
    series: string;
    number: string;
    citizenship: Country = new Country();
    expire?: Date;
}

export class Country {
    code: string;
    name: string;
}

export class CountriesListSource implements IAutoCompleteListSource {
    constructor(private _http: HttpClient, private baseUrl: string) { }
    public search = (term: string): Observable<{ name: string }[]> =>
        this._http.get<Country[]>(`${this.baseUrl}/api/countries?term=${term}`)
}


@Component({
    selector: 'traveller',
    template: `
        <form #f="ngForm" novalidate>
            <div class="row">
                <div class="form-group col-md-3">
                    <label class="control-label">Пол</label>
                    <select [(ngModel)]="value.gender" name="gender" #gender="ngModel" required class="form-control">
                        <option disabled selected value="">-- Пол --</option>
                        <option value="MALE">Мужской</option>
                        <option value="FEMALE">Женский</option>
                    </select>
                </div>
                <div class="form-group col-md-3">
                    <label class="control-label">Имя</label>
                    <input type="text"
                        [(ngModel)]="value.firstName"
                        placeholder="Имя"
                        name="firstName"
                        #firstName="ngModel"
                        autocomplete="off"
                        class="form-control">
                </div>
                <div class="form-group col-md-3">
                    <label class="control-label">Отчество</label>
                    <input type="text"
                        [(ngModel)]="value.middleName"
                        placeholder="Отчество"
                        name="middleName"
                        #middleName="ngModel"
                        autocomplete="off"
                        class="form-control">
                </div>
                <div class="form-group col-md-3">
                    <label class="control-label">Фамилия</label>
                    <input type="text"
                        [(ngModel)]="value.lastName"
                        placeholder="Фамилия"
                        name="lastName"
                        #lastName="ngModel"
                        autocomplete="off"
                        class="form-control">
                </div>
            </div>
            <div class="row">
                <div class="form-group col-md-3">
                    <label class="control-label">Дата рождения</label>
                    <input type="datetime"
                        [(ngModel)]="value.birthDate"
                        placeholder="Дата рождения"
                        name="birthDate"
                        #birthDate="ngModel"
                        class="form-control">
                </div>
                <div class="form-group col-md-3">
                    <label class="control-label">Документ</label>
                    <select
                        [(ngModel)]="value.passport.type"
                        required
                        name="type"
                        #type="ngModel"
                        class="form-control">
                        <option value="PS">Общегражданский паспорт</option>
                        <option value="PSP">Общегражданский заграничный паспорт</option>
                        <option value="SR">Свидетельство о рождении</option>
                    </select>
                </div>
                <div class="form-group col-md-3">
                    <label class="control-label">Номер документа</label>
                    <input type="text"
                        [(ngModel)]="value.passport.number"
                        placeholder="Номер"
                        name="number"
                        #number="ngModel"
                        autocomplete="off"
                        class="form-control">
                </div>
            </div>
        </form>
    `,
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => TravellerComponent),
            multi: true
        },
        {
            provide: NG_VALIDATORS,
            useExisting: forwardRef(() => TravellerComponent),
            multi: true
        }
    ]
})
export class TravellerComponent implements OnInit, ControlValueAccessor, Validator {
    value: Traveller;
    valid: boolean;
    countriesSource: IAutoCompleteListSource;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.countriesSource = new CountriesListSource(http, baseUrl);
    }

    ngOnInit() {
        if (!this.value) {
            this.value = new Traveller();
            this.value.passport = new Passport();
            this.value.passport.citizenship = new Country();
        }
        this.validate(null);
        this.onChange(this.value);
    }

    /* --- ControlValueAccessor -- */
    private onChange = (_: any) => { };
    private onTouched = (_: any) => { };
    writeValue = (value: Traveller): void => { if (value) this.value = value; };
    registerOnChange = (fn: any) => this.onChange = fn;
    registerOnTouched = (fn: any) => this.onTouched = fn;
    setDisabledState = (isDisabled: boolean) => { };

    /* --- Validator --- */
    validate(c: AbstractControl): ValidationErrors {
        return this.valid ? null : {
            traveller: false,
            message: 'Traveller is not valid'
        };
    }
}
