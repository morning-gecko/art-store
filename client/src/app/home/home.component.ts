import { Component } from '@angular/core';
import { homeContent } from 'src/content/home';
import { Product } from '../shared/models/product';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  homeContent = homeContent;
  
}
