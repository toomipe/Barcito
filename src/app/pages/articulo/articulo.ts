import { Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HeaderService } from '../../core/services/headerService';

@Component({
  selector: 'app-articulo',
  imports: [RouterModule],
  templateUrl: './articulo.html',
  styleUrl: './articulo.scss'
})
export class Articulo {
  headerService = inject(HeaderService);

  ngOnInit(): void {
    this.headerService.settitulo("Articulos");
  }
}
