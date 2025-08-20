import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Cuenta } from '../interfaces/cuenta';

@Injectable({
  providedIn: 'root'
})
export class CuentaService {
  constructor(private http: HttpClient) {}
  idCuenta?: number;

  async nuevaCuenta(nombre: string, id: string): Promise<boolean> {
    const cuenta: Cuenta = { nombre: nombre, idDevice: id };
    console.log(JSON.stringify(cuenta));

    try {
      console.log('andiamo por aca');
      this.idCuenta = await this.http.post<number>("https://localhost:7031/api/Cuenta", cuenta).toPromise();

      // es en donde se devuelve la consulta
      if (this.idCuenta === undefined) throw new Error("No se recibió idCuenta");
      console.log('Cuenta creada con éxito', cuenta);
      return true;
    } catch (err) {
      console.error('Error al crear cuenta', err);
      return false;
    }
  }

  getIdCuenta() {
    if (this.idCuenta === undefined) {
      console.log('fijate por aca', this.idCuenta);
      return -1;
    }
    return this.idCuenta;
  }

}
