export interface DetalleCuenta {
  idDetalleCuenta: number;
  idCuenta: number;
  idArticulo: number;
  pagado: boolean;
  cantidad: number;
  detalle: string;
  precio: number;
  estado: string;
}
