import { Component, inject, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { HeaderService } from '../../core/services/headerService';
import { ArticuloService } from '../../core/services/articuloService';
import { CategoriaService } from '../../core/services/categoriaService';
import { Articulo } from '../../core/interfaces/articulo'; 
import { CommonModule } from '@angular/common';
import { TarjetaArticulo } from '../../core/components/tarjeta-articulo/tarjeta-articulo';

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
  route = inject(ActivatedRoute);
  articuloService = inject(ArticuloService);
  categoriaService = inject(CategoriaService);

  articulos:Articulo[] = [];
  categoriaID!: string | null;


  ngOnInit(): void {
    this.categoriaID = this.route.snapshot.paramMap.get('categoriaID');

    if (this.categoriaID) {
      const id = Number(this.categoriaID);

      // 1. Obtener categoría para el título
      this.categoriaService.getById(id).subscribe(categoria => {
        this.headerService.settitulo(categoria.nombre);
      });

      // 2. Obtener artículos de esa categoría
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
