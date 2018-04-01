import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { RailRoutingModule, routedComponents } from './rail.routing';
import { RailService } from './rail.service';
import { SharedModule } from '../shared/shared.module';

@NgModule({
    imports: [
        SharedModule,
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
export class RailModule { 
    
}
