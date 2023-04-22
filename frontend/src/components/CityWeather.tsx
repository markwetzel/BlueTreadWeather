import React from "react";
import { DayForecast as DayForecastInterface } from "../interfaces/DayForecast";
import { WeatherForecast as WeatherForecastInterface } from "../interfaces/WeatherForecast";
import DayForecast from "./DayForecast";
import "./styles/CityWeather.css";

interface CityWeatherProps {
  cityWeather: WeatherForecastInterface;
}

const CityWeather: React.FC<CityWeatherProps> = ({ cityWeather }) => {
  return (
    <div className='city-weather'>
      <h2>{`${cityWeather.city}, ${cityWeather.state}`}</h2>
      {cityWeather.dayForecasts.map(
        (day: DayForecastInterface, index: number) => (
          <DayForecast key={index} day={day} />
        )
      )}
    </div>
  );
};

export default React.memo(CityWeather);
