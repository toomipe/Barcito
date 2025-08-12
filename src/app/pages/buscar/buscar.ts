import { ChangeDetectorRef, Component, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { HeaderService } from '../../core/services/headerService';
import { TarjetaCategoria } from '../../core/components/tarjeta-categoria/tarjeta-categoria';    
import { CategoriaService } from '../../core/services/categoriaService';
import { Categoria } from '../../core/interfaces/categoria';
import { CommonModule } from '@angular/common';
import { DeviceService } from '../../core/services/deviceService';
import { CuentaService } from '../../core/services/cuentaService';


@Component({
  standalone: true,
  selector: 'app-buscar',
  imports: [RouterModule, TarjetaCategoria, CommonModule],
  templateUrl: './buscar.html',
  styleUrls: ['./buscar.scss']
})
export class Buscar {

  constructor(private router: Router) {}

  deviceService = inject(DeviceService)
  cdr = inject(ChangeDetectorRef);
  headerService = inject(HeaderService);
  categoriaService = inject(CategoriaService);
  cuentaService = inject(CuentaService);
  cuentaRegistrada:boolean = false;
  categorias:Categoria[] = [];
  nombre:string = "tomas";

  async ngOnInit(){
    const id = this.deviceService.getDeviceId();
    this.headerService.setnombre(this.nombre);
    console.log('ID del dispositivo:', id);

    // generacion de cuenta
    this.cuentaRegistrada = await this.cuentaService.nuevaCuenta(this.nombre, id);
    console.log(this.cuentaRegistrada, "<-------");

    this.headerService.settitulo("Realizar Pedido");
    this.categoriaService.getAll().subscribe(res => {
      this.categorias = res;
      this.cdr.detectChanges(); // forzar actualización
      })
  }

  irAlDashboard() {
    this.router.navigate(['/dashboard']);
  }

  
}

