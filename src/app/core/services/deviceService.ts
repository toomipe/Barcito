import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {
  getDeviceId() {
    let deviceId = localStorage.getItem('deviceId');
    if (!deviceId) {
      deviceId = crypto.randomUUID(); // generar un UUID
      localStorage.setItem('deviceId', deviceId);
    }
    return deviceId;
  }
}