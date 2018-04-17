import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'forbidden',
    template: `
        <h1><i class="fa fa-exclamation-triangle"></i> Access denied</h1>
        <h3>You have not permission to access this page</h3>
    `
})
export class ForbiddenComponent implements OnInit {
    constructor() { }

    ngOnInit() { }
}
