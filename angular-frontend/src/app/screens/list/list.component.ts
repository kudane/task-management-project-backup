import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable, switchMap } from 'rxjs';
import { TaskService } from '../../services/task.service';
import { PriorityService } from '../../services/priority.service';
import { TypeService } from '../../services/type.service';

@Component({
    selector: 'app-list',
    standalone: true,
    imports: [CommonModule, FormsModule, RouterLink, ReactiveFormsModule],
    templateUrl: './list.component.html'
})
export class ListComponent {
    type: string | undefined = "";
    priority: string | undefined = "";

    http = inject(HttpClient);
    route = inject(ActivatedRoute);
    router = inject(Router);
    taskService = inject(TaskService);
    typeService = inject(TypeService);
    priorityService = inject(PriorityService);

    type$: Observable<any[]> = this.typeService.list();
    priority$: Observable<any[]> = this.priorityService.list();
    tasks$: Observable<any[]> = this.route.queryParams.pipe(
        switchMap(() => this.taskService.list(this.type, this.priority))
    );

    onSearchChange() {
        this.router.navigate([], { queryParams: { type: this.type, priority: this.priority } })
    }

    onTaskDelete(id: any) {
        if (confirm("Confirm to delete ?")) {
            this.tasks$ = this.taskService.delete(id).pipe(
                switchMap(() => this.taskService.list(this.type, this.priority))
            );
        }
    }
}
