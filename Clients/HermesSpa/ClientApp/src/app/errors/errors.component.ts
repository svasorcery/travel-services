import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    template: `
        <h1>ERROR {{ routeParams?.error }}</h1>
        <a [routerLink]="'/'">
            <h4>Go to Home</h4>
        </a>
    `
})
export class ErrorsComponent implements OnInit {
    routeParams: any;

    constructor(private activatedRoute: ActivatedRoute) { }

    ngOnInit() {
        this.routeParams = this.activatedRoute.snapshot.queryParams;
    }
}
