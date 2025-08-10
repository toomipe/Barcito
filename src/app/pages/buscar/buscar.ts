import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { HeaderService } from '../../core/services/headerService';
import { TarjetaCategoria } from '../../core/components/tarjeta-categoria/tarjeta-categoria';    
import { CategoriaService } from '../../core/services/categoriaService';
import { Categoria } from '../../core/interfaces/categoria';
import { CommonModule } from '@angular/common';


@Component({
  standalone: true,
  selector: 'app-buscar',
  imports: [RouterModule, TarjetaCategoria, CommonModule],
  templateUrl: './buscar.html',
  styleUrls: ['./buscar.scss']
})
export class Buscar {

  constructor(private router: Router) {}
  
  cdr = inject(ChangeDetectorRef);
  headerService = inject(HeaderService);
  categoriaService = inject(CategoriaService);
  categorias:Categoria[] = [];

  ngOnInit(): void {
    console.log('ngOnInit');
    this.headerService.settitulo("Buscar");
    this.categoriaService.getAll().subscribe(res => {
      this.categorias = res;
      this.cdr.detectChanges(); // Forzar actualización
      })
  }

  irAlDashboard() {
    this.router.navigate(['/dashboard']);
  }

  
}

