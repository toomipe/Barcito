import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { Categoria } from '../../core/interfaces/categoria';
import { CategoriaService } from '../../core/services/categoriaService';

@Component({
  selector: 'app-categorias',
  templateUrl: './categorias.html',
  styleUrls: ['./categorias.css'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class Categorias implements OnInit {

  categorias: Categoria[] = [];
  categoriasFiltered: Categoria[] = [];
  filter = '';

  selectedCategoria: Categoria | null = null;
  toDeleteCategoria: Categoria | null = null;
  isEditing = false;
  loading = false;

  errorMessage: string | null = null;

  constructor(
    private categoriasService: CategoriaService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.applyFilter();
    this.loadCategorias();
  }

  loadCategorias(): void {
    this.loading = true;
    this.categoriasService.getAll().subscribe({
      next: list => {
        this.categorias = list || [];
        this.applyFilter();
        this.loading = false;
        this.cdr.markForCheck();
      },
      error: err => {
        console.error('Error cargando categorías', err);
        this.loading = false;
        this.cdr.markForCheck();
      }
    });
  }

  applyFilter(): void {
    const q = (this.filter || '').toLowerCase().trim();
    if (!q) {
      this.categoriasFiltered = [...this.categorias];
      return;
    }

    this.categoriasFiltered = this.categorias.filter(c =>
      (c.nombre || '').toLowerCase().includes(q)
    );
  }

  newCategoria(): void {
    this.selectedCategoria = {
      categoriaID: 0,
      nombre: '',
      urlImagen: ''
    };
    this.isEditing = false;
    this.errorMessage = null;

    setTimeout(() => {
      window.scrollTo({ top: document.body.scrollHeight, behavior: 'smooth' });
    }, 100);
  }

  editCategoria(c: Categoria): void {
    this.selectedCategoria = { ...c };
    this.isEditing = true;
    this.errorMessage = null;

    setTimeout(() => {
      window.scrollTo({ top: document.body.scrollHeight, behavior: 'smooth' });
    }, 100);
  }

  saveCategoria(): void {
    if (!this.selectedCategoria) return;

    this.errorMessage = null;

    if (this.isEditing) {
      this.categoriasService.update(this.selectedCategoria).subscribe({
        next: () => { this.loadCategorias(); this.cancel(); },
        error: err => {
          this.errorMessage = err.error?.detail || 'Error actualizando categoría';
          this.cdr.markForCheck();
        }
      });
    } else {
      this.categoriasService.create(this.selectedCategoria).subscribe({
        next: () => { this.loadCategorias(); this.cancel(); },
        error: err => {
          this.errorMessage = err.error?.detail || 'Error creando categoría';
          this.cdr.markForCheck();
        }
      });
    }
  }

  confirmDelete(c: Categoria): void {
    this.toDeleteCategoria = c;
  }

  deleteCategoria(c?: Categoria): void {
    const target = c ?? this.toDeleteCategoria;
    if (!target) return;

    this.categoriasService.delete(target.categoriaID).subscribe({
      next: () => {
        this.toDeleteCategoria = null;
        this.loadCategorias();
      },
      error: err => console.error('Error eliminando categoría', err)
    });
  }

  cancel(): void {
    this.selectedCategoria = null;
    this.toDeleteCategoria = null;
    this.isEditing = false;
    this.errorMessage = null;
  }
}
