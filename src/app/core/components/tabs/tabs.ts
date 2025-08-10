import { Component } from '@angular/core';
import { NavigationEnd, Router, RouterModule } from '@angular/router';

@Component({
  standalone: true,
  selector: 'app-tabs',
  imports: [RouterModule],
  templateUrl: './tabs.html',
  styleUrls: ['./tabs.scss']
})
export class Tabs {

  seleccionado = [false, false, false];

  constructor(private router: Router){
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd){
        console.log("evento", event)
        switch(event.urlAfterRedirects){
          case "":
            this.seleccionado = [false, true, false];
            break;
          case "/carrito":
            this.seleccionado = [false, false, true];
            break;
          case "/buscar":
            this.seleccionado = [true, false, false];
            break;
          default:
            this.seleccionado = [false, false, false];
            break;
        }
      }
    })
  }


  navegar(direccion:string){
    // cambiar de pagina
    console.log(direccion);
    this.router.navigate([direccion]);

  }
}
