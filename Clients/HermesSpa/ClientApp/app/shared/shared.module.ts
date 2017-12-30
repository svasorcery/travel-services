import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AutoCompleteComponent } from './autocomplete.component';
import { SpinnerComponent } from './spinner.component';

@NgModule({
    imports: [
        FormsModule,
        CommonModule
    ],
    declarations: [
        AutoCompleteComponent,
        SpinnerComponent
    ],
    providers: [

    ],
    exports: [
        CommonModule,
        AutoCompleteComponent,
        SpinnerComponent
    ],
})
export class SharedModule {
    
 }
