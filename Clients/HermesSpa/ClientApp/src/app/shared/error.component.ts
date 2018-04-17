import { Component, OnInit, Input } from '@angular/core';

import { ApiError } from '../errors/errors.models';

@Component({
    selector: 'error',
    template: `
        <div *ngIf="active" class="panel panel-danger">
            <div class="alert alert-danger panel-body" [class.alert-dismissible]="dismissible" role="alert" style="margin-bottom:0">
                <div class="col-md-1 pull-right">
                    <button *ngIf="dismissible" (click)="active=!active" type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <i class="fa fa-close" style="padding:0 20px"></i>
                    </button>
                </div>
                <div class="col-md-1 text-center">
                    <i class="fa fa-warning fa-5x" style="margin-top:20px 0"></i>
                </div>
                <div class="col-md-10 text-center">
                    <h3 style="margin-top:0">{{ error.code }}: {{ error.message }}</h3>
                    <h4>{{ error.detail }}</h4>
                    <div *ngIf="refreshable">
                        <a (click)="refresh()" style="cursor:pointer">
                            <h4>
                                <span class="fa fa-refresh"></span>
                                Обновить страницу
                            </h4>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    `
})
export class ErrorComponent {
    @Input() active: boolean;
    @Input() dismissible: boolean = false;
    @Input() refreshable: boolean = true;
    @Input() error: ApiError;

    constructor() { }

    refresh() {
        window.location.reload();  
    }
}
