import { Component, signal } from '@angular/core';
import { RouterModule, RouterOutlet } from '@angular/router';
import { Tabs } from "./core/components/tabs/tabs";
import { Header } from "./core/components/header/header";
import { HttpClientModule } from '@angular/common/http';

@Component({
  standalone: true,
  selector: 'app-root',
  imports: [RouterModule, Tabs, Header, HttpClientModule],
  templateUrl: './app.html',
  styleUrls: ['./app.scss']
})
export class App {
  protected readonly title = signal('barcito');
}
