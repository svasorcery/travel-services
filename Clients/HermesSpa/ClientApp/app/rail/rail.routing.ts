import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RailComponent } from './rail.component';
import { RailSearchComponent } from './search.component';
import { TrainsListComponent } from './trains.component';

const routes: Routes = [
    { 
        path: 'rail', 
        component: RailComponent,
        children: [
            {
                path: '',
                component: RailSearchComponent
            },
            {
                path: 'trains',
                component: TrainsListComponent
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class RailRoutingModule { }

export const routedComponents = [
    RailComponent,
    RailSearchComponent,
    TrainsListComponent
];
