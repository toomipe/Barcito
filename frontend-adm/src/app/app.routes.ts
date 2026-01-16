import { Routes } from '@angular/router';
import { Login } from './core/components/login/login';
import { Home } from './core/components/home/home';
import { AuthGuard } from './core/services/authguard';
import { SeguridadComponent } from './pages/seguridad/seguridad';
import { Articulos } from './pages/articulos/articulos';
import { Categorias } from './pages/categorias/categorias';
import { Cuentas } from './pages/cuentas/cuentas';
import { CuentaForm } from './pages/cuenta-form/cuenta-form';
import { PedidosEnPreparacion } from './pages/pedidos-en-preparacion/pedidos-en-preparacion';
import { PedidosParaEntregar } from './pages/pedidos-para-entregar/pedidos-para-entregar';
import { CuentasPorMesas } from './pages/cuentas-por-mesas/cuentas-por-mesas';

export const routes: Routes = [
    { 
        path: '', 
        redirectTo: 'login', 
        pathMatch: 'full' },
    { 
        path: 'home', 
        component: Home, 
        canActivate: [AuthGuard] 
    },
    {
        path: 'articulos',
        component: Articulos,
        canActivate: [AuthGuard]
    },
    {
        path: 'categorias',
        component: Categorias,
        canActivate: [AuthGuard]
    },
    {
        path: 'cuentas',
        component: Cuentas,
        canActivate: [AuthGuard]
    },
    {
        path: 'cuentas/nuevo',
        component: CuentaForm,
        canActivate: [AuthGuard]
    },
    {
        path: 'cuentas/mesas',
        component: CuentasPorMesas,
        canActivate: [AuthGuard]
    },
    {
        path: 'pedidos/cocina',
        component: PedidosEnPreparacion,
        canActivate: [AuthGuard]
    },
    {
        path: 'pedidos/entrega',
        component: PedidosParaEntregar,
        canActivate: [AuthGuard]
    },
    {   
        path: 'seguridad', 
        component: SeguridadComponent, 
        canActivate: [AuthGuard], 
        data: { role: 'admin' }
    },
    { 
        path: 'login', 
        component: Login 
    },
    { 
        path: '**', 
        redirectTo: 'login', 
        pathMatch: 'full' 
    }
];


