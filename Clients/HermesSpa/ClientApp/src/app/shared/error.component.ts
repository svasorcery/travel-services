import { Component, OnInit, Input } from '@angular/core';

@Component({
    selector: 'error',
    template: `
        <div *ngIf="active">
            <div class="alert alert-danger" [class.alert-dismissible]="dismissible" role="alert">
                <button *ngIf="dismissible" type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <i class="fa fa-warning fa-3x pull-left"></i>
                <div class="text-center">
                    <h4>{{ message }}</h4>
                    <h5>{{ details }}</h5>
                </div>
            </div>
            <div *ngIf="refreshable" class="text-center">
                <a (click)="refresh()">
                    <h4>
                        <span class="fa fa-refresh"></span>
                        Refresh page
                    </h4>
                </a>
            </div>
        </div>
    `
})
export class ErrorComponent {
    @Input() active: boolean;
    @Input() dismissible: boolean = false;
    @Input() refreshable: boolean = true;

    @Input() message: string = `There was a problem processing your request.`;
    @Input() details: string = `Try again in a few minutes.`;

    constructor() { }

    refresh() {
        window.location.reload();
    }
}
