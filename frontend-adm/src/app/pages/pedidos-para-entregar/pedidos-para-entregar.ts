import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { interval, Subscription } from 'rxjs';

import { DetalleService } from '../../core/services/detalleService';
import { DetalleConCuenta } from '../../core/interfaces/detallecompleto-concuenta';

@Component({
  selector: 'app-pedidos-en-preparacion',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pedidos-para-entregar.html',
  styleUrls: ['./pedidos-para-entregar.css']
})
export class PedidosParaEntregar implements OnInit, OnDestroy {

  pedidos: DetalleConCuenta[] = [];
  loading = false;

  private refreshSub?: Subscription;

  constructor(
    private detalleService: DetalleService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.loadPedidos();

    // refresco automático cada 1 minuto
    this.refreshSub = interval(60000).subscribe(() => {
      this.loadPedidos();
    });
  }

  ngOnDestroy(): void {
    // corta el intervalo al salir del componente
    this.refreshSub?.unsubscribe();
  }

  loadPedidos(): void {
    // muestra loading solo la primera vez
    if (this.pedidos.length === 0) {
      this.loading = true;
    }

    this.detalleService.obtenerDetallesParaEntrega().subscribe({
      next: data => {
        this.pedidos = data || [];
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: err => {
        console.error('Error cargando pedidos', err);
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  marcarListo(pedido: DetalleConCuenta): void {
    this.detalleService.marcarComoEntregado(pedido.idDetalleCuenta).subscribe({
      next: () => {
        this.pedidos = this.pedidos.filter(
          p => p.idDetalleCuenta !== pedido.idDetalleCuenta
        );
        this.loadPedidos();

        this.cdr.markForCheck();
      },
      error: err => console.error('Error marcando como listo', err)
    });
  }

  marcarCocina(pedido: DetalleConCuenta): void {
    this.detalleService.marcarDevueltoCocina(pedido.idDetalleCuenta).subscribe({
      next: () => {
        this.pedidos = this.pedidos.filter(
          p => p.idDetalleCuenta !== pedido.idDetalleCuenta
        );
        this.loadPedidos();

        this.cdr.markForCheck();
      },
      error: err => console.error('Error marcando como listo', err)
    });
  }
}
