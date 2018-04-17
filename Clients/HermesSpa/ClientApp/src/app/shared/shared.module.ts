import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AutoCompleteComponent } from './autocomplete.component';
import { SpinnerComponent } from './spinner.component';
import { ErrorComponent } from './error.component';

@NgModule({
    imports: [
        FormsModule,
        CommonModule
    ],
    declarations: [
        AutoCompleteComponent,
        SpinnerComponent,
        ErrorComponent
    ],
    providers: [

    ],
    exports: [
        CommonModule,
        AutoCompleteComponent,
        SpinnerComponent,
        ErrorComponent
    ],
})
export class SharedModule {
    
}
