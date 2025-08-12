import { Injectable, WritableSignal, signal, ɵunwrapWritableSignal } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { Header } from '../components/header/header';

@Injectable({
  providedIn: 'root'
})
export class HeaderService {
  
  private _titulo = new BehaviorSubject<string>('Título');
  titulo$ = this._titulo.asObservable();
  private _nombre = new BehaviorSubject<string>('a');
  nombre$ = this._nombre.pipe(
  map(nombre => `A cuenta de: ${nombre}`)
);
  

  constructor(){

  }

  settitulo(titulo: string) {
    this._titulo.next(titulo);
  }

  setnombre(nombre: string) {
    this._nombre.next(nombre);
  }
}
