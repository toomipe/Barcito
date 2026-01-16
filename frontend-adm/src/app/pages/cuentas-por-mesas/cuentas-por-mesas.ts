import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { interval, Subscription } from 'rxjs';

import { DetalleService } from '../../core/services/detalleService';
import { DetalleConCuenta } from '../../core/interfaces/detallecompleto-concuenta';
import { DetalleCuentaCompleta } from '../../core/interfaces/detalle-cuenta-completa';

@Component({
  selector: 'app-pedidos-en-preparacion',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cuentas-por-mesas.html',
  styleUrls: ['./cuentas-por-mesas.css']
})
export class CuentasPorMesas implements OnInit, OnDestroy {

  pedidos: DetalleConCuenta[][] = [];
  loading = false;

  private refreshSub?: Subscription;
  mesaSeleccionada: DetalleConCuenta[] | null = null;
  mesaSeleccionadaId: number | null = null;


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
    if (this.pedidos.length === 0) {
      this.loading = true;
    }

    this.detalleService.obtenerDetallesPorMesa().subscribe({
      next: data => {
        const detalles = data || [];
        const grupos: { [idCuenta: number]: DetalleConCuenta[] } = {};

        detalles.forEach(d => {
          if (!grupos[d.idCuenta]) {
            grupos[d.idCuenta] = [];
          }
          grupos[d.idCuenta].push(d);
        });

        this.pedidos = Object.values(grupos);

        // 🔁 refresca el modal si estaba abierto
        if (this.mesaSeleccionadaId !== null) {
          const actualizada = this.pedidos.find(
            p => p[0].idCuenta === this.mesaSeleccionadaId
          );

          this.mesaSeleccionada = actualizada ?? null;
        }

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
      next: () => this.loadPedidos(),
      error: err => console.error('Error marcando como listo', err)
    });
  }

  marcarCocina(pedido: DetalleConCuenta): void {
    this.detalleService.marcarDevueltoCocina(pedido.idDetalleCuenta).subscribe({
      next: () => this.loadPedidos(),
      error: err => console.error('Error marcando como cocina', err)
    });
  }

  marcarListoPreparado(pedido: DetalleConCuenta): void {
    this.detalleService.marcarComoPreparado(pedido.idDetalleCuenta).subscribe({
      next: () => this.loadPedidos(),
      error: err => console.error('Error marcando como preparado', err)
    });
  }


  getTotalCuenta(detalles: DetalleConCuenta[]): number {
    return detalles.reduce(
      (total, d) => total + (d.precio * d.cantidad),
      0
    );
  }

  tienePendientes(detalles: DetalleConCuenta[]): boolean {
    return detalles.some(d => d.estado !== 'E');
  }

  abrirDetalles(cuenta: DetalleConCuenta[]): void {
    this.mesaSeleccionada = cuenta;
    this.mesaSeleccionadaId = cuenta[0].idCuenta;
  }


  cerrarDetalles(): void {
    this.mesaSeleccionada = null;
  }

}
