import { Component, inject } from '@angular/core';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { Console } from 'console';
import { HeaderService } from '../../services/headerService';

@Component({
  selector: 'app-nombre-usuario',
  imports: [MatDialogModule, MatButtonModule, FormsModule],
  templateUrl: './nombre-usuario.html',
  styleUrl: './nombre-usuario.scss'
})
export class NombreUsuario {
  constructor(private dialogRef: MatDialogRef<NombreUsuario>) {}
  nombreUsuario: string = 'tomi';
  headerService = inject(HeaderService);
  cerrar() {
    // aca enviamos el nombre al header
    this.headerService.setnombre(this.nombreUsuario);
    this.dialogRef.close();
  }
}
