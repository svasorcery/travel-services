import { Component, Input, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { DomSanitizer, SafeStyle } from '@angular/platform-browser';

import { Car, CarScheme, PlacesRange, CarPlace, SchemeCell } from './rail.models';

@Component({
    selector: 'car-scheme-rzd',
    templateUrl: './scheme-rzd.component.html',
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => CarSchemeRzdComponent),
            multi: true,
        }
    ]
})
export class CarSchemeRzdComponent implements ControlValueAccessor {
    @Input() scheme: CarScheme;
    data: PlacesRange;

    constructor(private sanitizer: DomSanitizer) { }


    selectSeat(place: CarPlace) {
        if (!this.data) {
            this.data = new PlacesRange();
        }

        if (this.data.range0 && this.data.range1 && this.data.range0 === this.data.range1) {
            if (place.number > this.data.range0) {
                this.data.range1 = place.number;
            } else {
                this.data.range1 = this.data.range0;
                this.data.range0 = place.number;
            }
        } else {
            this.data.range0 = place.number;
            this.data.range1 = place.number;
        }
        this.propagateChange(this.data);
    }

    getCellStyle(cell: SchemeCell): SafeStyle {
        if (cell && cell.border) {
            const style = `border-${cell.border}: solid 1px #222`;
            return this.sanitizer.bypassSecurityTrustStyle(style);
        } else {
            return this.sanitizer.bypassSecurityTrustStyle('');
        }
    }


    /* ControlValueAccessor */
    private propagateChange = (_: any) => { };
    registerOnChange = (fn: any): void => this.propagateChange = fn;
    registerOnTouched = (fn: any): void => { };
    setDisabledState = (isDisabled: boolean): void => { };

    writeValue(obj: any): void {
        if (!obj) { return; }
        this.data = obj;
    }
}
