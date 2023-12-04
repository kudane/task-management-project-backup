import { HttpClient } from "@angular/common/http";
import { Injectable, inject } from "@angular/core";
import { Observable, map } from "rxjs";
import { apiEndpoint } from "../app.config";

@Injectable({ providedIn: "root" })
export class TaskService {
    http = inject(HttpClient);

    list(type: any, priority: any): Observable<any> {
        return this.http.get<{ items: [] }>(`${apiEndpoint}/list/task?type=${type || ''}&priority=${priority || ''}`).pipe(map(a => a.items));
    }

    new(body: any): Observable<any> {
        return this.http.post(`${apiEndpoint}/new/task`, body);
    }

    byId(id: any): Observable<any> {
        return this.http.get(`${apiEndpoint}/get/task/${id}`);
    }

    edit(body: any): Observable<any> {
        return this.http.put(`${apiEndpoint}/edit/task`, body);
    }

    delete(id: any): Observable<any> {
        return this.http.delete(`${apiEndpoint}/delete/task/${id}`);
    }
}