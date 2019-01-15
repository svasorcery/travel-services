import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';

import { SharedModule } from './shared/shared.module';
import { ErrorsModule } from './errors/errors.module';
import { RailModule } from './rail/rail.module';

@NgModule({
    imports: [
        BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
        HttpClientModule,
        FormsModule,
        SharedModule,
        ErrorsModule,
        RailModule,
        RouterModule.forRoot([
            { path: '', component: HomeComponent, pathMatch: 'full' },
            { path: '**', redirectTo: '' }
        ])
    ],
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
    ],
    providers: [

    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule {

}
