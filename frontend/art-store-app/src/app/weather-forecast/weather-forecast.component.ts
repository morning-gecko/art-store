import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WeatherForecast } from '../weather-forecast';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-weather-forecast',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <section class="listing">
      <h2 class="listing-heading">{{ weatherForecast.date }}</h2>
      <p class="listing-location">{{ weatherForecast.temperatureF }}, {{ weatherForecast.temperatureC }}</p>
      <p class="listing-price">{{ weatherForecast.summary }}</p>
    </section>
  `,
  styleUrls: ['./weather-forecast.component.css']
})
export class WeatherForecastComponent {
  @Input() weatherForecast!: WeatherForecast;
}
