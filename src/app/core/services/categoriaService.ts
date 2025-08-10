import { Injectable } from "@angular/core";
import { Categoria } from '../interfaces/categoria';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class CategoriaService {
    constructor(private http: HttpClient) {}

    getAll(): Observable<Categoria[]> { 
        const res$ = this.http.get<Categoria[]>("https://localhost:7031/api/Categoria");
        res$.subscribe(data => console.log('Datos recibidos:', data));
        return res$;
    }

    getById(idCategoria: Number): Observable<Categoria> { 
        const res$ = this.http.get<Categoria>(`https://localhost:7031/api/Categoria/${idCategoria}`);
        res$.subscribe(data => console.log('Datos recibidos:', data));
        return res$;
    }
}
