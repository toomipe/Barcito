import { Injectable, WritableSignal, signal, ɵunwrapWritableSignal } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Header } from '../components/header/header';

@Injectable({
  providedIn: 'root'
})
export class HeaderService {
  
  private _titulo = new BehaviorSubject<string>('Título');
  titulo$ = this._titulo.asObservable();

  

  constructor(){

  }

  settitulo(titulo: string) {
    this._titulo.next(titulo);
  }
}
