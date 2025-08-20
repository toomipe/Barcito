import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { HeaderService } from '../../core/services/headerService';
import { CommonModule } from '@angular/common';
import { CategoriaService } from '../../core/services/categoriaService';
import { CategoriaC } from '../categoria/categoria';
import { NombreUsuario } from '../../core/components/nombre-usuario/nombre-usuario';
import { MatDialog } from '@angular/material/dialog';

@Component({
  standalone: true,
  selector: 'app-home',
  imports: [RouterModule, CommonModule],
  templateUrl: './home.html',
  styleUrls: ['./home.scss']
})
export class Home implements OnInit{

  
  headerService = inject(HeaderService);

  ngOnInit(): void {
    this.headerService.settitulo("Tu barcito");
    if (!this.headerService.getnombre()) {
      this.abrirModal();
      
      console.log(this.headerService.getnombre());
    }
    
  }
  
  constructor(private router: Router, private dialog: MatDialog) {}

  irAlDashboard() {
    this.router.navigate(['/dashboard']);
  }

  abrirModal() {
    this.dialog.open(NombreUsuario);
  }

    nombreBar = 'Tu barcito';
  slogan = 'Tu lugar favorito para disfrutar';

  imagenes = [
    { url: 'https://images.unsplash.com/photo-1509042239860-f550ce710b93?auto=format&fit=crop&w=600&q=80', alt: 'Cócteles' }
  ];

  contacto = {
    direccion: 'Av. Siempre Viva 742, Ciudad',
    telefono: '+54 9 11 1234 5678',
    email: 'contacto@labuenaonda.com'
  };

  mediosPago = [
    'Tarjetas de crédito: Visa, MasterCard, American Express',
    'Débito',
    'Efectivo',
    'MercadoPago'
  ];
}
  