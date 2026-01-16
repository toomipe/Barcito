import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { Articulo } from '../../core/interfaces/articulo';
import { ArticuloService } from '../../core/services/articuloService';
import { Categoria } from '../../core/interfaces/categoria';
import { CategoriaService } from '../../core/services/categoriaService';


@Component({
  selector: 'app-articulos',
  templateUrl: './articulos.html',
  styleUrls: ['./articulos.css'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class Articulos implements OnInit {

  categorias: Categoria[] = [];
  articulos: Articulo[] = [];
  articulosFiltered: Articulo[] = [];
  filter = '';

  selectedArticulo: Articulo | null = null;
  toDeleteArticulo: Articulo | null = null;
  isEditing = false;
  loading = false;

  errorMessage: string | null = null;

  constructor(
    private articulosService: ArticuloService,
    private categoriasService: CategoriaService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.applyFilter();
    this.loadCategorias();
    this.loadArticulos();
  }

  loadArticulos(): void {
    this.loading = true;
    this.articulosService.getAll().subscribe({
      next: list => {
        this.articulos = list || [];
        this.applyFilter();
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: err => {
        console.error('Error cargando artículos', err);
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  loadCategorias(): void {
    this.categoriasService.getAll().subscribe({
      next: list => {
        this.categorias = list || [];
        this.cdr.markForCheck();
      },
      error: err => console.error('Error cargando categorías', err)
    });
  }


  applyFilter(): void {
    const q = (this.filter || '').toLowerCase().trim();
    if (!q) {
      this.articulosFiltered = [...this.articulos];
      return;
    }

    this.articulosFiltered = this.articulos.filter(a =>
      (a.nombre || '').toLowerCase().includes(q) ||
      (a.descripcion || '').toLowerCase().includes(q) ||
      (a.categoria?.nombre || '').toLowerCase().includes(q)
    );
  }

  newArticulo(): void {
    this.selectedArticulo = {
      articuloID: 0,
      nombre: '',
      urlImagen: '',
      descripcion: '',
      precio: 0,
      categoriaID: 0,
      categoria: null as any
    };
    this.isEditing = false;
    this.errorMessage = null;

    setTimeout(() => {
      window.scrollTo({ top: document.body.scrollHeight, behavior: 'smooth' });
    }, 100);
  }

  editArticulo(a: Articulo): void {
    this.selectedArticulo = {
      ...a,
      categoriaID: a.categoria?.categoriaID ?? a.categoriaID
    };
    this.isEditing = true;
    this.errorMessage = null;

    setTimeout(() => {
      window.scrollTo({ top: document.body.scrollHeight, behavior: 'smooth' });
    }, 100);
  }


  saveArticulo(): void {
    if (!this.selectedArticulo) return;

    this.errorMessage = null;

    if (this.isEditing) {
      this.articulosService.update(this.selectedArticulo).subscribe({
        next: () => { this.loadArticulos(); this.cancel(); },
        error: err => {
          this.errorMessage = err.error?.detail || 'Error actualizando artículo';
          this.cdr.markForCheck();
        }
      });
    } else {
      this.articulosService.create(this.selectedArticulo).subscribe({
        next: () => { this.loadArticulos(); this.cancel(); },
        error: err => {
          this.errorMessage = err.error?.detail || 'Error creando artículo';
          this.cdr.markForCheck();
        }
      });
    }
  }

  confirmDelete(a: Articulo): void {
    this.toDeleteArticulo = a;
  }

  deleteArticulo(a?: Articulo): void {
    const target = a ?? this.toDeleteArticulo;
    if (!target) return;

    this.articulosService.delete(target.articuloID).subscribe({
      next: () => {
        this.toDeleteArticulo = null;
        this.loadArticulos();
      },
      error: err => console.error('Error eliminando artículo', err)
    });
  }

  cancel(): void {
    this.selectedArticulo = null;
    this.toDeleteArticulo = null;
    this.isEditing = false;
    this.errorMessage = null;
  }
}
