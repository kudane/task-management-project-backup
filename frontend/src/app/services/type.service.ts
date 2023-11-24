import { HttpClient } from "@angular/common/http";
import { Injectable, inject } from "@angular/core";
import { Observable, map } from "rxjs";
import { apiEndpoint } from "../app.config";

@Injectable({ providedIn: "root" })
export class TypeService {
    http = inject(HttpClient);

    list(): Observable<any> {
        return this.http.get<{ items: [] }>(`${apiEndpoint}/list/type`).pipe(map(a => a.items));
    }
}