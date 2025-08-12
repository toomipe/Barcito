import { Component, inject } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HeaderService } from '../../services/headerService';

@Component({
  standalone: true,
  selector: 'app-header',
  imports: [RouterModule, CommonModule],
  templateUrl: './header.html',
  styleUrls: ['./header.scss']
})
export class Header {
  private headerService = inject(HeaderService);
  nombre$ = this.headerService.nombre$;
  titulo$ = this.headerService.titulo$;
}
