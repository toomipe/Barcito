import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { CuentaService } from '../../core/services/cuentaService';
import { CuentaConTotales } from '../../core/interfaces/cuenta';

import { DetalleService } from '../../core/services/detalleService';
import { DetalleCuenta } from '../../core/interfaces/detalle-cuenta';
import { DetalleCuentaCompleta } from '../../core/interfaces/detalle-cuenta-completa';

@Component({
  selector: 'app-cuentas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cuentas.html',
  styleUrls: ['./cuentas.css']
})
export class Cuentas implements OnInit {

  cuentas: CuentaConTotales[] = [];
  cuentasFiltered: CuentaConTotales[] = [];
  filter = '';
  mostrarPagadas = false;

  detalles: DetalleCuentaCompleta[] = [];
  showDetalle = false;
  cuentaSeleccionada?: number;
  nombreCuenta? = '';
  loadingDetalle = false;

  loading = false;

  constructor(
    private cuentaService: CuentaService,
    private detalleService: DetalleService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.loadCuentas();
  }

  loadCuentas(): void {
    this.loading = true;
    this.cuentaService.getCuentasYTotales().subscribe({
      next: list => {
        this.cuentas = list || [];
        this.applyFilter();
        this.applyFilterPagadas();
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: err => {
        console.error('Error cargando cuentas', err);
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  verDetalles(cuenta: CuentaConTotales): void {
    this.loadingDetalle = true;
    this.showDetalle = true;
    this.cuentaSeleccionada = cuenta.cuenta.IdCuenta;
    this.nombreCuenta = cuenta.cuenta.Nombre;

    this.detalleService.getByCuenta(this.cuentaSeleccionada).subscribe({
      next: data => {
        this.detalles = (data || []).map(d => ({
          ...d,
          selected: false   // 👈 ACÁ está la clave
        }));

        this.checkAll = false;
        this.loadingDetalle = false;
        this.cdr.markForCheck();
      },
      error: err => {
        console.error('Error cargando detalles', err);
        this.loadingDetalle = false;
        this.cdr.markForCheck();
      }
    });
  }


  cerrarDetalle(): void {
    this.showDetalle = false;
    this.detalles = [];
  }

  applyFilter(): void {
    const q = (this.filter || '').toLowerCase().trim();
    if (!q) {
      this.cuentasFiltered = [...this.cuentas];
      return;
    }

    this.cuentasFiltered = this.cuentas.filter(c =>
      c.cuenta.Nombre.toLowerCase().includes(q)
    );
  }

  applyFilterPagadas(): void {
    if (this.mostrarPagadas) {
      this.cuentasFiltered = [...this.cuentas];
    } else {
      this.cuentasFiltered = this.cuentas.filter(c => !c.cuenta.Pagado);
    }
  }

  /*
  marcarPagado(cuenta: CuentaConTotales): void {
    if (cuenta.cuenta.pagado) return;

    this.cuentaService.marcarComoPagado(cuenta.cuenta.IdCuenta).subscribe({
      next: () => {
        cuenta.cuenta.pagado = true;
        this.cdr.markForCheck();
      },
      error: err => console.error('Error marcando como pagado', err)
    });
  }*/

  checkAll = false;

  get totalSeleccionado(): number {
    return this.detalles
      .filter(d => d.selected && !d.pagado)
      .reduce((acc, d) => acc + (d.cantidad * d.precio), 0);
  }

  get idsSeleccionados(): number[] {
    return this.detalles
      .filter(d => d.selected)
      .map(d => d.idDetalleCuenta);
  }


  toggleAll() {
    this.detalles.forEach(d => {
      if (!d.pagado) {
        d.selected = this.checkAll;
      }
    });
  }

  toggleItem() {
    this.checkAll = this.detalles
      .filter(d => !d.pagado)
      .every(d => d.selected);
  }

  marcarPagado() {
    const ids = this.idsSeleccionados;

    if (!ids.length) return;

    ids.forEach(idDetalle => {
      this.detalleService.marcarDetalleComoPagado(idDetalle).subscribe({
        next: () => {
          // Actualizar el estado localmente
          const detalle = this.detalles.find(d => d.idDetalleCuenta === idDetalle);
          if (detalle) {
            detalle.pagado = true;
            detalle.selected = false;
          }
          this.cdr.markForCheck();
        },
        error: err => console.error('Error marcando como pagado:', err)
      });
    });
  }

}
