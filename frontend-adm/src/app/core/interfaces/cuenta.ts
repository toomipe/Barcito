export interface Cuenta {
  IdCuenta: number;
  Nombre: string;
  idDevice: string;
  Fecha: string;
  Pagado: boolean;
  Mesa: number;
}

export interface CuentaConTotales {
  cuenta: Cuenta;
  totales: {
    total: number;
  };
}
