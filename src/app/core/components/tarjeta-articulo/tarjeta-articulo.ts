import { Component, Input } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { Articulo } from '../../interfaces/articulo';

@Component({
  standalone: true,
  selector: 'app-tarjeta-articulo',
  imports: [RouterModule],
  templateUrl: './tarjeta-articulo.html',
  styleUrls: ['./tarjeta-articulo.scss']
})
export class TarjetaArticulo {
  @Input() articulo!: Articulo;

  constructor(private router: Router){ }

  navegar(articuloID: number) {
    // cambiar de pagina
    // this.router.navigate(["categoria/",categoriaID]);
  }
}