import { DayForecast } from "./DayForecast";

export interface WeatherForecast {
  city: string;
  state: string;
  dayForecasts: DayForecast[];
}
