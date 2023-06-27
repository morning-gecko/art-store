import { Injectable } from '@angular/core';
import { WeatherForecast } from './weather-forecast';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class WeatherService {
  private url = 'http://localhost:5006/WeatherForecast';
  
  constructor(private http: HttpClient) { }
  
  getWeatherForecast(): Observable<WeatherForecast[]> {
    return this.http.get<WeatherForecast[]>(this.url);
  }
}
