import { Directive, OnInit, Input, forwardRef } from '@angular/core';
import { AbstractControl, ValidatorFn, Validator, NG_VALIDATORS, ValidationErrors } from '@angular/forms';

export class DateValidators {
    public static minValue(min: Date): ValidatorFn {
        min = new Date(min.getFullYear(), min.getMonth(), min.getDate());
        return (control: AbstractControl): { [key: string]: any } => {
            const value = <Date>control.value;
            return value && (value.getTime() < min.getTime()) ? { dateMinValue: true } : null;
        };
    }

    public static maxValue(max: Date): ValidatorFn {
        return (control: AbstractControl): { [key: string]: any } => {
            const value = <Date>control.value;
            return value && (value.getTime() > max.getTime()) ? { dateMaxValue: true } : null;
        };
    }
}

@Directive({
    selector: '[validateDateRange]',
    providers: [
        { provide: NG_VALIDATORS, useExisting: forwardRef(() => DateRangeValidatorDirective), multi: true }
    ]
})
export class DateRangeValidatorDirective implements Validator, OnInit {
    @Input('validateDateRange') options: any = {};
    required: boolean;
    min: Date;
    max: Date;
    
    constructor() { }

    ngOnInit() {
        this.required = this.options.required || false;
        this.min = this.options.min ? new Date(Date.parse(this.options.min)) : DateCalc.addYearsFromToday(-150);
        this.max = this.options.max ? new Date(Date.parse(this.options.max)) : DateCalc.addDaysFromToday(-1);
    }

    validate(c: AbstractControl): ValidationErrors|null {

        if (this.required && !c.value) {
            return {
                required: false,
                message: 'Выберите дату.'
            }
        }

        if (c.value && (c.value.getTime() < this.min.getTime())) {
            return { 
                dateMinValue: false,
                message: 'Дата недействительна.',
                min: this.min
            };
        }

        if (c.value && (c.value.getTime() > this.max.getTime())) {
            return { 
                dateMaxValue: false,
                message: 'Дата превышает максимальное значение.',
                max: this.max
            };
        }
    }
}

export class DateCalc {    
    public static addDaysFromToday(count: number): Date {
        var today = new Date();
        return new Date(today.setDate(today.getDate() + count));
    }

    public static addMonthsFromToday(count: number): Date {
        var today = new Date();
        return new Date(today.setMonth(today.getMonth() + count));
    }

    public static addYearsFromToday(count: number): Date {
        var today = new Date();
        return new Date(today.setFullYear(today.getFullYear() + count));
    }
} 
