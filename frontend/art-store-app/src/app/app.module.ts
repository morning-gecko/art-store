import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { WeatherForecastComponent } from './weather-forecast/weather-forecast.component';
import { HomeComponent } from './home/home.component';
import { RouterModule } from '@angular/router';
import { WeatherService } from './weather.service';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HomeComponent,
    RouterModule,
    WeatherForecastComponent,
    HttpClientModule
  ],
  providers: [WeatherService],
  bootstrap: [AppComponent]
})
export class AppModule { }
