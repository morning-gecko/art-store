import { Injectable } from '@angular/core';
import { WeatherForecast } from './weather-forecast';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class WeatherService {
  private url = 'https://localhost:7280/WeatherForecast';
  
  constructor(private http: HttpClient) { }
  
  getWeatherForecast(): Observable<WeatherForecast[]> {
    return this.http.get<WeatherForecast[]>(this.url);
  }
}
