import { Component, inject, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { HeaderService } from '../../core/services/headerService';
import { ArticuloService } from '../../core/services/articuloService';
import { CategoriaService } from '../../core/services/categoriaService';
import { Articulo } from '../../core/interfaces/articulo'; 
import { CommonModule } from '@angular/common';
import { TarjetaArticulo } from '../../core/components/tarjeta-articulo/tarjeta-articulo';
import { DetalleService } from '../../core/services/detalleService';
import { CuentaService } from '../../core/services/cuentaService';
import { Console } from 'console';

@Component({
  standalone: true,
  selector: 'app-categoria',
  imports: [RouterModule, CommonModule, TarjetaArticulo],
  templateUrl: './categoria.html',
  styleUrls: ['./categoria.scss']
})
export class CategoriaC {
  
  cdr = inject(ChangeDetectorRef);
  headerService = inject(HeaderService);
  detalleService = inject(DetalleService);
  route = inject(ActivatedRoute);
  articuloService = inject(ArticuloService);
  categoriaService = inject(CategoriaService);
  cuentaService = inject(CuentaService);

  articulos:Articulo[] = [];
  categoriaID!: string | null;
  pedido: Record<number, number> = {};

  actualizarPedido(event: { id: number, cantidad: number }) {
  this.pedido[event.id] = event.cantidad;
  // console.log('Pedido actualizado:', this.pedido);
  }
  
  enviarCarrito() {
    // console.log('lo que anoto para el carrito', this.pedido);
    console.log('-->', this.cuentaService.getIdCuenta(), this.pedido);
    this.detalleService.nuevosDetalles(this.cuentaService.getIdCuenta(), this.pedido);
  }

  ngOnInit(): void {
    this.categoriaID = this.route.snapshot.paramMap.get('categoriaID');

    if (this.categoriaID) {
      const id = Number(this.categoriaID);

      this.categoriaService.getById(id).subscribe(categoria => {
        this.headerService.settitulo(categoria.nombre);
      });

      this.articuloService.getByCategoriaID(id).subscribe(res => {
        this.articulos = res;
        console.log('Artículos cargados:', this.articulos);
        this.cdr.detectChanges(); 
      });

    } else {
      console.error('No se recibió categoriaID en la ruta');
    } 
  }
}
