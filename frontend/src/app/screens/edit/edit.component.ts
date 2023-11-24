import { Component, Input, inject, numberAttribute } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { TaskService } from '../../services/task.service';
import { TypeService } from '../../services/type.service';
import { PriorityService } from '../../services/priority.service';

@Component({
    selector: 'app-edit-task',
    standalone: true,
    imports: [CommonModule, FormsModule, RouterLink, ReactiveFormsModule],
    templateUrl: './edit.component.html'
})
export class EditComponent {
    router = inject(Router);
    http = inject(HttpClient);
    taskService = inject(TaskService);
    typeService = inject(TypeService);
    priorityService = inject(PriorityService);
    type$: Observable<any[]> = this.typeService.list();
    priority$: Observable<any[]> = this.priorityService.list();
    form = new FormGroup({
        id: new FormControl('', Validators.required),
        name: new FormControl('', Validators.required),
        description: new FormControl('', Validators.required),
        startDate: new FormControl(),
        dueDate: new FormControl(),
        priority: new FormControl(Validators.required),
        type: new FormControl(Validators.required)
    });

    @Input({ transform: numberAttribute }) set id(value: string) {
        this.taskService.byId(value).subscribe((a: any) => {
            this.form.patchValue({
                id: value,
                name: a.name,
                description: a.description,
                startDate: a.startDate && a.startDate.slice(0, 10),
                dueDate: a.dueDate && a.dueDate.slice(0, 10),
                priority: a.fkPriorityId,
                type: a.fkTypeId
            });
        });
    }

    onSubmit() {
        if (this.form.invalid) {
            alert("please fill *");
            return;
        }

        this.taskService.edit(this.form.value)
            .pipe(
                catchError(err => {
                    alert('error in source. Details: ' + err.message);
                    throw err;
                })
            )
            .subscribe(a => {
                this.router.navigate(['/']);
            });
    }
}
