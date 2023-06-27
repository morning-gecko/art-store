import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { WeatherForecastComponent } from './weather-forecast/weather-forecast.component';

export const routes: Routes = [
  { path: '', component: HomeComponent, title: 'Home page' },
  { path: 'weather-forecast', component: WeatherForecastComponent }
];
