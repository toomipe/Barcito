import { Component, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { HeaderService } from '../../core/services/headerService';

@Component({
  standalone: true,
  selector: 'app-carrito',
  imports: [RouterModule], // ← ¡Esto es clave!
  templateUrl: './carrito.html',
  styleUrls: ['./carrito.scss']
})
export class Carrito {

  constructor(private router: Router) {}

  headerService = inject(HeaderService);

  ngOnInit(): void {
    this.headerService.settitulo("Carrito");
  }

  irAlDashboard() {
    this.router.navigate(['/dashboard']);
  }
}
