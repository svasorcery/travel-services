import { Component, Input } from '@angular/core';
@Component({
    selector: 'spinner',
    template: `
        <div *ngIf="active" style="position:fixed; top:50%; left:50%;">
            <i style="margin-top:-0.55em; margin-left:-0.66em;" class="fa fa-spinner fa-pulse fa-5x fa-fw"></i>
        </div>
    `
})
export class SpinnerComponent {
    @Input() active: boolean;
}
