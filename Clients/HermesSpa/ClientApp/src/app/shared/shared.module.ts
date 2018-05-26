import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AutoCompleteComponent } from './autocomplete.component';
import { SpinnerComponent } from './spinner.component';
import { ErrorComponent } from './error.component';
import { TravellerComponent, TravellersArrayComponent } from './traveller.component';

@NgModule({
    imports: [
        FormsModule,
        CommonModule
    ],
    declarations: [
        AutoCompleteComponent,
        SpinnerComponent,
        ErrorComponent,
        TravellerComponent,
        TravellersArrayComponent
    ],
    providers: [

    ],
    exports: [
        CommonModule,
        AutoCompleteComponent,
        SpinnerComponent,
        ErrorComponent,
        TravellerComponent,
        TravellersArrayComponent
    ],
})
export class SharedModule {

}
