import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Weather as WeatherService, WeatherForecast } from '../../services/weather';

@Component({
  selector: 'app-weather',
  imports: [CommonModule],
  templateUrl: './weather.html',
  styleUrl: './weather.css',
})
export class Weather implements OnInit {
  forecasts: WeatherForecast[] = [];
  loading = true;
  error: string | null = null;

  constructor(private weatherService: WeatherService) {}

  ngOnInit(): void {
    this.loadWeather();
  }

  loadWeather(): void {
    this.loading = true;
    this.error = null;
    
    this.weatherService.getWeatherForecast().subscribe({
      next: (data) => {
        this.forecasts = data;
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load weather data. Make sure the API is running.';
        this.loading = false;
        console.error('Error loading weather:', err);
      }
    });
  }
}
