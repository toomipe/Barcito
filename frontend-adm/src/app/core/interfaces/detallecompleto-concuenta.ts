export interface DetalleConCuenta {
  idDetalleCuenta: number;
  idCuenta: number;
  idArticulo: number;
  pagado: boolean;
  cantidad: number;
  detalle: string;
  precio: number;
  estado: string;
  nombreArticulo: string;
  detalleArticulo: string;
  descripcionArticulo: string;
  formaDePago: boolean;
  selected?: boolean;
  nombre?: string;
  mesa?: number;
}
