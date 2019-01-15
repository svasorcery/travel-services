import { Component, Input, OnInit, OnDestroy, forwardRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Observable, Subscription, Subject } from 'rxjs';
import { debounceTime, distinctUntilChanged, filter, switchMap } from 'rxjs/operators';

export interface IAutoCompleteListSource {
    search(term: string): Observable<{ name: string }[]>;
}


@Component({
    selector: 'autocomplete',
    template: `
        <div class="autocomplete" [ngClass]="{ 'open': items && items.length }">
            <input type="text"
                [(ngModel)]="label"
                [attr.placeholder]="placeholder"
                [disabled]="!source"
                (keyup)="search($event, term.value)"
                (keydown)="onKeyDown($event)"
                (blur)="leave()"
                class="form-control"
                #term />
            <span *ngIf="loading" class="fa fa-spinner fa-pulse text-muted"></span>
            <span *ngIf="(!hasError) && (!loading) && (data == null)" class="fa fa-question text-muted"></span>
            <span *ngIf="(!loading) && (data != null)" class="fa fa-check text-muted"></span>
            <span *ngIf="hasError" class="fa fa-exclamation-triangle text-danger"></span>
            <ul *ngIf="items && items.length" class="list-group">
                <li *ngFor="let item of items" (click)="select(item)" [ngClass]="{ 'list-group-item': true, 'hover': item == hover }">
                    {{item.name}}
                </li>
            </ul>
        </div>
    `,
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => AutoCompleteComponent),
            multi: true,
        }
    ],
    styles: [ `
        input::-ms-clear {
            display: none;
        }

        .autocomplete {
            position: relative;
        }

        .open input {
            border-color: white;
            z-index: 2001;
        }

        .open input:focus {
            border-color: white;
            box-shadow: none;
        }

        .list-group {
            top: 0px;
            position:absolute;
            width: 100%;
            z-index: 1000;
            border: 1px solid #4189c7;
            box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(red(#4189c7), green(#4189c7), blue(#4189c7), .6);
        }

        .list-group-item:first-child {
            margin-top: 34px;
            border-top-left-radius: 0;
            border-top-right-radius: 0;
        }

        .list-group-item {
            cursor:pointer;
            padding-left: 12px;
            border-color: transparent;
            border-top-color: gray;
        }

        .fa {
            position: absolute;
            right: 6px;
            top: 6px;
            font-size: 150%;
        }

        .list-group-item:hover, .list-group-item.hover {
            border-color: #4189c7;
            background: #4189c7;
            color: white;
        }`
    ]
})
export class AutoCompleteComponent implements OnInit, OnDestroy, ControlValueAccessor {
    @Input() source: IAutoCompleteListSource;
    @Input() placeholder: string;
    @Input() searchOnInit: string;

    public data: any = null;
    public items: { name: string }[];
    public hover: { name: string } = null;
    public hoverIndex = -1;

    public debounceTime: number = 300;
    public minTermLength: number = 2;
    public label: string = null;
    public loading: boolean = false;
    public hasError: boolean = false;

    private sub: Subscription;
    private searchTermStream: Subject<string> = new Subject<string>();
    private nonInteractiveSearch: boolean = false;


    constructor() { }

    ngOnInit(): void {
        this.sub = this.searchTermStream.pipe(
            debounceTime(this.debounceTime),
            distinctUntilChanged(),
            filter((term: string) => term.length >= this.minTermLength),
            switchMap((term: string) => {
                this.loading = true;
                this.hasError = false;
                return this.source.search(term);
            })
        )
        .subscribe(
            items => {
                if (this.nonInteractiveSearch) {
                    this.nonInteractiveSearch = false;
                    if (items.length > 0) {
                        this.select(items[0]);
                    }
                } else {
                    this.items = items;
                }
                this.loading = false;
            },
            error => {
                this.loading = false;
                this.hasError = true;
            });

        if (this.searchOnInit) {
            this.nonInteractiveSearch = true;
            this.searchTermStream.next(this.searchOnInit);
        }
    }

    ngOnDestroy(): void {
        this.sub.unsubscribe();
    }


    public search(event: any, term: string): void {
        if (event.key === 'Escape' || event.key === 'Enter' || event.key === 'Tab') {
            return;
        }

        if (this.data && this.data['name'] && this.data.name !== term) {
            this.data = null;
            this.propagateChange(null);
        }

        if (this.source != null) {
            this.searchTermStream.next(term);
        }
    }

    public select(item: {name: string}): void {
        this.items = [];
        this.data = item;
        this.hover = null;
        this.hoverIndex = -1;
        this.label = item.name;
        this.propagateChange(item);
    }

    public leave(): void {
        if ( this.data && this.data['name']) {
            this.label = this.data['name'];
        } else {
            this.label = '';
        }

        if (this.items) {
            setTimeout(x => this.items = null, 100);
        }
    }

    public onKeyDown(event: any): boolean {
        if (event.key === 'Tab') {
            if (this.items && this.items.length === 1) {
                this.select(this.items[0]);
                return false;
            }

            if (this.items && this.items.length > 0) {
                if (this.hover == null) {
                    this.hoverIndex = 0;
                } else {
                    this.hoverIndex ++;
                }

                if (this.hoverIndex >= this.items.length) {
                    this.hoverIndex = 0;
                }
                this.hover = this.items[this.hoverIndex];
            }

            if ((this.items == null || !this.items.length) && !this.loading) {
                return true;
            }

            return false;
        } else if (event.key === 'Escape') {
            this.items = [];
            return false;
        } else if (event.key === 'Enter') {
            if (this.hover != null) {
                this.select(this.hover);
                return false;
            }
        } else if (event.key === 'ArrowDown') {
            if (this.items && this.items.length > 0) {
                this.hoverIndex ++;
                if (this.hoverIndex >= this.items.length) {
                    this.hoverIndex = 0;
                }
                this.hover = this.items[this.hoverIndex];
            }
            return false;
        } else if (event.key === 'ArrowUp') {
            if (this.items && this.items.length > 0) {
                this.hoverIndex --;
                if (this.hoverIndex < 0) {
                    this.hoverIndex = this.items.length - 1;
                }
                this.hover = this.items[this.hoverIndex];
            }
            return false;
        }

        return true;
    }


    /* --- ControlValueAccessor -- */
    writeValue = (obj: any) => {
        this.data = obj;
        if (obj && obj['name']) {
            this.label = obj.name;
        } else {
            this.label = '';
        }
    }
    private propagateChange = (_: any) => { };
    registerOnChange = (fn: any) => this.propagateChange = fn;
    registerOnTouched = (fn: any) => { };
    setDisabledState = (isDisabled: boolean) => { };
}
