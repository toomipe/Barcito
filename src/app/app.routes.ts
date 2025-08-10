import { Routes } from '@angular/router';
import { Carrito } from './pages/carrito/carrito';
import { CategoriaC } from './pages/categoria/categoria';
import { Articulo } from './pages/articulo/articulo';
import { Buscar } from './pages/buscar/buscar';
import { Pedido } from './pages/pedido/pedido';
import { Home } from './pages/home/home';

export const routes: Routes = [
    {
        path: "carrito",
        component: Carrito
    },
    {
        path: "categoria",
        component: CategoriaC
    },
    {
        path: "articulo",
        component: Articulo
    },
    {
        path: "buscar",
        component: Buscar
    },
    {
        path: "pedido",
        component: Pedido
    },
    {
        path: "",
        component: Home
    },
    {
        path: "categoria/:categoriaID",
        component: CategoriaC
    }
];

