import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { TaskService } from '../../services/task.service';
import { PriorityService } from '../../services/priority.service';
import { TypeService } from '../../services/type.service';

@Component({
    selector: 'app-new-task',
    standalone: true,
    imports: [CommonModule, FormsModule, RouterLink, ReactiveFormsModule],
    templateUrl: './new.component.html'
})
export class NewComponent {
    router = inject(Router);
    http = inject(HttpClient);
    taskService = inject(TaskService);
    typeService = inject(TypeService);
    priorityService = inject(PriorityService);
    type$: Observable<any[]> = this.typeService.list();
    priority$: Observable<any[]> = this.priorityService.list();
    form = new FormGroup({
        name: new FormControl('', Validators.required),
        description: new FormControl('', Validators.required),
        startDate: new FormControl(),
        dueDate: new FormControl(),
        priority: new FormControl(Validators.required),
        type: new FormControl(Validators.required)
    });

    onSubmit() {
        if (this.form.invalid) {
            alert("please fill *");
            return;
        }

        this.taskService.new(this.form.value)
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
