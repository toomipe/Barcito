import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Cuenta } from '../interfaces/cuenta';

@Injectable({
  providedIn: 'root'
})
export class CuentaService {
  constructor(private http: HttpClient) {}
  
  async nuevaCuenta(nombre: string, id: string): Promise<boolean> {
  const cuenta: Cuenta = { nombre: nombre, idDevice: id };
  console.log(JSON.stringify(cuenta));

  try {
    await this.http.post<void>("https://localhost:7031/api/Cuenta", cuenta).toPromise();
    console.log('Cuenta creada con éxito');
    return true;
  } catch (err) {
    console.error('Error al crear cuenta', err);
    return false;
  }
}

}
