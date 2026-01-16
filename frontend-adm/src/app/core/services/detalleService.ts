import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { DetalleCuenta } from '../interfaces/detalle-cuenta';
import { DetalleCuentaCompleta } from '../interfaces/detalle-cuenta-completa';
import { DetalleConCuenta } from '../interfaces/detallecompleto-concuenta';

@Injectable({
  providedIn: 'root'
})
export class DetalleService {

  private baseUrl = 'https://4134zb58-5196.brs.devtunnels.ms/api/Detalle';

  constructor(private http: HttpClient) {}

  getByCuenta(idCuenta: number): Observable<DetalleCuentaCompleta[]> {
    return this.http.get<DetalleCuentaCompleta[]>(`${this.baseUrl}/consultaCuenta/${idCuenta}`);
  }

  marcarDetalleComoPagado(idDetalleCuenta: number): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/marcarPagado/${idDetalleCuenta}`, {});
  }

  obtenerDetallesEnPrepación(): Observable<DetalleCuentaCompleta[]> {
    return this.http.get<DetalleCuentaCompleta[]>(`${this.baseUrl}/detallesEnPreparación`);
  } 

  marcarComoPreparado(idDetalleCuenta: number): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/marcarPreparado/${idDetalleCuenta}`, {});
  } 

  obtenerDetallesParaEntrega(): Observable<DetalleConCuenta[]> {
    return this.http.get<DetalleConCuenta[]>(`${this.baseUrl}/DetallesParaEntregar`);
  }   

  marcarComoEntregado(idDetalleCuenta: number): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/marcarEntregado/${idDetalleCuenta}`, {});
  }

  marcarDevueltoCocina(idDetalleCuenta: number): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/marcarDevueltoCocina/${idDetalleCuenta}`, {});
  }

  obtenerDetallesPorMesa(): Observable<DetalleConCuenta[]> {
    return this.http.get<DetalleConCuenta[]>(`${this.baseUrl}/DetallesPorMesa`);
  }
}
