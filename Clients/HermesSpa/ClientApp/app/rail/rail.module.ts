import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { RailRoutingModule, routedComponents } from './rail.routing';
import { RailService } from './rail.service';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        RailRoutingModule
    ],
    declarations: [
        ...routedComponents
    ],
    providers: [
        RailService
    ],
    exports: [
        
    ],
})
export class RailModule { }
