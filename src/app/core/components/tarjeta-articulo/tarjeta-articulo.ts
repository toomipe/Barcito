import { Component, Input } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { Articulo } from '../../interfaces/articulo';
import { BehaviorSubject } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  standalone: true,
  selector: 'app-tarjeta-articulo',
  imports: [RouterModule, CommonModule],
  templateUrl: './tarjeta-articulo.html',
  styleUrls: ['./tarjeta-articulo.scss']
})
export class TarjetaArticulo {
  @Input() articulo!: Articulo;
  private _n = new BehaviorSubject<number>(0);
  n$ = this._n.asObservable();

  constructor(private router: Router){ }

  navegar(articuloID: number) {
    // cambiar de pagina
    // this.router.navigate(["categoria/",categoriaID]);
  }
  

  cambiar(delta: number) {
    console.log("cambiar llamado con delta =", delta);
    var val = this._n.getValue() + delta;
    if (val > -1 && val < 26) {
      const nuevoValor = val;
      this._n.next(nuevoValor);
    }
  }
}