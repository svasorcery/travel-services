import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RailComponent } from './rail.component';
import { RailSearchComponent } from './search.component';
import { TrainsListComponent } from './trains.component';
import { CarsListComponent } from './cars.component';
import { RailOrderComponent } from './order.component';
import { CarSchemeRzdComponent } from './scheme-rzd.component';


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
            },
            {
                path: 'cars',
                component: CarsListComponent
            },
            {
                path: 'order',
                component: RailOrderComponent
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class RailRoutingModule {

}


export const routedComponents = [
    RailComponent,
    RailSearchComponent,
    TrainsListComponent,
    CarsListComponent,
    RailOrderComponent,
    CarSchemeRzdComponent
];
