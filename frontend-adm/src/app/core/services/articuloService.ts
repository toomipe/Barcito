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
        const res$ = this.http.get<Articulo[]>("https://4134zb58-5196.brs.devtunnels.ms/api/Articulo");
        // res$.subscribe(data => console.log('Datos recibidos:', data));
        return res$;
    }

    getByCategoriaID(categoriaID:number): Observable<Articulo[]> { 
        const res$ = this.http.get<Articulo[]>(`https://4134zb58-5196.brs.devtunnels.ms/api/Articulo/porCategoria/${categoriaID}`);
        // res$.subscribe(data => console.log('Datos recibidos:', data));
        return res$;
    }

    getByID(articuloID:number): Observable<Articulo> { 
        const res$ = this.http.get<Articulo>(`https://4134zb58-5196.brs.devtunnels.ms/api/Articulo/${articuloID}`);
        // res$.subscribe(data => console.log('Datos recibidos:', data));
        return res$;
    }

    create(articulo: Articulo): Observable<Articulo> {
        console.log('Creando artículo:', articulo);
        const res$ = this.http.post<Articulo>("https://4134zb58-5196.brs.devtunnels.ms/api/Articulo", articulo);
        // res$.subscribe(data => console.log('Artículo creado:', data));
        return res$;
    }

    update(articulo: Articulo): Observable<Articulo> {
        const res$ = this.http.put<Articulo>(`https://4134zb58-5196.brs.devtunnels.ms/api/Articulo/${articulo.articuloID}`, articulo);
        // res$.subscribe(data => console.log('Artículo actualizado:', data));
        return res$;
    }

    delete(articuloID: number): Observable<void> {
        const res$ = this.http.delete<void>(`https://4134zb58-5196.brs.devtunnels.ms/api/Articulo/${articuloID}`);  
        // res$.subscribe(() => console.log('Artículo eliminado'));
        return res$;
    }
}