import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppRoutingModule, routedComponents } from './app.routing';
import { RailModule } from './rail/rail.module';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        ...routedComponents
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RailModule,
        AppRoutingModule
    ]
})
export class AppModuleShared {
}
