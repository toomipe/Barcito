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
        const res$ = this.http.get<Categoria[]>("https://4134zb58-5196.brs.devtunnels.ms/api/Categoria");
        // res$.subscribe(data => console.log('Datos recibidos:', data));
        return res$;
    }

    getById(idCategoria: Number): Observable<Categoria> { 
        const res$ = this.http.get<Categoria>(`https://4134zb58-5196.brs.devtunnels.ms/api/Categoria/${idCategoria}`);
        // res$.subscribe(data => console.log('Datos recibidos:', data));
        return res$;
    }

    create(categoria: Categoria): Observable<Categoria> {
        console.log('Creando categoría:', categoria);
        const res$ = this.http.post<Categoria>("https://4134zb58-5196.brs.devtunnels.ms/api/Categoria", categoria); 
        // res$.subscribe(data => console.log('Categoría creada:', data));
        return res$;
    }

    update(categoria: Categoria): Observable<Categoria> {
        const res$ = this.http.put<Categoria>(`https://4134zb58-5196.brs.devtunnels.ms/api/Categoria/${categoria.categoriaID}`, categoria);
        // res$.subscribe(data => console.log('Categoría actualizada:', data));
        return res$;
    }
    
    delete(categoriaID: number): Observable<void> {
        const res$ = this.http.delete<void>(`https://4134zb58-5196.brs.devtunnels.ms/api/Categoria/${categoriaID}`);
        // res$.subscribe(() => console.log('Categoría eliminada'));
        return res$;
    }
    
}
