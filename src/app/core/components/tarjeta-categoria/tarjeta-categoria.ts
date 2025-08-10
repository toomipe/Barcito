import { Component, Input } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { Categoria } from '../../interfaces/categoria';

@Component({
  standalone: true,
  selector: 'app-tarjeta-categoria',
  imports: [RouterModule],
  templateUrl: './tarjeta-categoria.html',
  styleUrls: ['./tarjeta-categoria.scss']
})
export class TarjetaCategoria {
  @Input() categoria!: Categoria;

  constructor(private router: Router){ }
  
  navegar(categoriaID:number){
    // cambiar de pagina
    this.router.navigate(["categoria/",categoriaID]);
  }
}

