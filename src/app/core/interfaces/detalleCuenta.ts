import { Cuenta } from "./cuenta";
import { Articulo } from "./articulo";

export interface DetalleCuenta {
    idDetalleCuenta: number,
    idCuenta: number,
    cuenta: Cuenta,
    idArticulo: number,
    articulo: Articulo,
    cantidad: number,
    detalle: string,
    descuento: number
}