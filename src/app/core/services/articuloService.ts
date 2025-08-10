import { Injectable } from "@angular/core";
import { Articulo } from '../interfaces/articulo';
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root'
})
export class ArticuloService {
    constructor(private http: HttpClient) {}

    getAll(): Observable<Articulo[]> { 
        const res$ = this.http.get<Articulo[]>("https://localhost:7031/api/Articulo");
        res$.subscribe(data => console.log('Datos recibidos:', data));
        return res$;
    }

    getByCategoriaID(categoriaID:number): Observable<Articulo[]> { 
        const res$ = this.http.get<Articulo[]>(`https://localhost:7031/api/Articulo/porCategoria/${categoriaID}`);
        res$.subscribe(data => console.log('Datos recibidos:', data));
        return res$;
    }
}