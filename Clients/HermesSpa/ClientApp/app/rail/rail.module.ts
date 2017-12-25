import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { RailRoutingModule, routedComponents } from './rail.routing';

@NgModule({
    imports: [
        CommonModule,
        RailRoutingModule
    ],
    declarations: [
        ...routedComponents
    ],
    providers: [

    ],
    exports: [
        
    ],
})
export class RailModule { }
