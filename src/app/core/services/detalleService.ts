import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DetalleCuenta } from '../interfaces/detalleCuenta';

@Injectable({
  providedIn: 'root'
})
export class DetalleService {
  constructor(private http: HttpClient) {}

  async nuevosDetalles(idCuenta: number, detalles: Record<number, number>): Promise<boolean> {
    try {
      for (const idArticuloStr in detalles) {
        if (detalles.hasOwnProperty(idArticuloStr)) {
          const idArticulo = Number(idArticuloStr);
          const cantidad = detalles[idArticulo];

          const detalleCuenta: DetalleCuenta = {
            idCuenta: idCuenta,
            idArticulo: idArticulo,
            cantidad: cantidad
          };

          console.log(detalleCuenta);
          await this.http.post<void>("https://localhost:7031/api/Detalle", detalleCuenta).toPromise();
        }
      }

      return true;
    } catch (err) {
      console.error('Error al crear detalles', err);
      return false;
    }
  }
}
