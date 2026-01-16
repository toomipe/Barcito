import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { interval, Subscription } from 'rxjs';

import { DetalleService } from '../../core/services/detalleService';
import { DetalleCuentaCompleta } from '../../core/interfaces/detalle-cuenta-completa';

@Component({
  selector: 'app-pedidos-en-preparacion',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './pedidos-en-preparacion.html',
  styleUrls: ['./pedidos-en-preparacion.css']
})
export class PedidosEnPreparacion implements OnInit, OnDestroy {

  pedidos: DetalleCuentaCompleta[] = [];
  loading = false;

  private refreshSub?: Subscription;

  constructor(
    private detalleService: DetalleService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadPedidos();

    // refresca cada 1 minuto
    this.refreshSub = interval(60000).subscribe(() => {
      this.loadPedidos();
    });
  }

  ngOnDestroy(): void {
    this.refreshSub?.unsubscribe();
  }

  loadPedidos(): void {
    if (this.pedidos.length === 0) {
      this.loading = true;
    }

    this.detalleService.obtenerDetallesEnPrepación().subscribe({
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

  marcarListo(pedido: DetalleCuentaCompleta): void {
    this.detalleService.marcarComoPreparado(pedido.idDetalleCuenta).subscribe({
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
