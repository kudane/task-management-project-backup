import { Routes } from '@angular/router';
import { ListComponent } from './screens/list/list.component';
import { NewComponent } from './screens/new/new.component';
import { EditComponent } from './screens/edit/edit.component';

export const routes: Routes = [
    {
        path: "",
        redirectTo: "list",
        pathMatch: "full"
    },
    {
        path: "new",
        component: NewComponent
    },
    {
        path: "edit/:id",
        component: EditComponent
    },
    {
        path: "list",
        component: ListComponent
    }
];
