import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { AutoCompleteComponent } from './autocomplete.component';

@NgModule({
    imports: [
        FormsModule,
        CommonModule
    ],
    declarations: [
        AutoCompleteComponent
    ],
    providers: [

    ],
    exports: [
        AutoCompleteComponent,
        CommonModule
    ],
})
export class SharedModule {
    
 }
