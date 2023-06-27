import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeatherForecastComponent } from '../weather-forecast/weather-forecast.component';
import { WeatherService } from '../weather.service';
import { WeatherForecast } from '../weather-forecast';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, WeatherForecastComponent],
  template: `
    <section>
      <h1>Home</h1>
    </section>
    <section class="results">
      <app-weather-forecast *ngFor="let forecast of weatherForecastList" [weatherForecast]="forecast"></app-weather-forecast>
      </section>
  `,
  styleUrls: ['./home.component.css']
})

export class HomeComponent {
  weatherForecastList: WeatherForecast[] = [];
  weatherService: WeatherService = inject(WeatherService);
  constructor() {
    this.weatherService.getWeatherForecast().subscribe((weatherForecastList: WeatherForecast[]) => {
      this.weatherForecastList = weatherForecastList;
    });
  }
}
