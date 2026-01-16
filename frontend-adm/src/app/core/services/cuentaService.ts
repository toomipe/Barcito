import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CuentaConTotales } from '../interfaces/cuenta';

@Injectable({
  providedIn: 'root'
})
export class CuentaService {

  private baseUrl = 'https://4134zb58-5196.brs.devtunnels.ms/api/Cuenta';

  constructor(private http: HttpClient) {}

  getCuentasYTotales(): Observable<CuentaConTotales[]> {
    return this.http.get<CuentaConTotales[]>(`${this.baseUrl}/cuentaYtotales`);
  }

  marcarComoPagado(idCuenta: number): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/marcarPagado/${idCuenta}`, {});
  }
}
