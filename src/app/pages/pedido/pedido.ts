import { Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HeaderService } from '../../core/services/headerService';

@Component({
  standalone: true,
  selector: 'app-pedido',
  imports: [RouterModule],
  templateUrl: './pedido.html',
  styleUrls: ['./pedido.scss']
})
export class Pedido {
  headerService = inject(HeaderService);

  ngOnInit(): void {
    this.headerService.settitulo("Pedido");
  }
}
