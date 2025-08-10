import { Categoria } from "./categoria";

export interface Articulo {
  articuloID: number;
  nombre: string;
  urlImagen: string;
  descripcion: string;
  precio: number;
  categoriaID: number;
  categoria: Categoria;
}