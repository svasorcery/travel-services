import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppRoutingModule, routedComponents } from './app.routing';
import { SharedModule } from './shared/shared.module';
import { RailModule } from './rail/rail.module';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';

@NgModule({
    imports: [
        SharedModule,
        HttpModule,
        FormsModule,
        RailModule,
        AppRoutingModule
    ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        ...routedComponents
    ]
})
export class AppModuleShared {
    
}
