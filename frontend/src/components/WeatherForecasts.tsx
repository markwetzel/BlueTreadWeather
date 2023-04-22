import { useContext } from "react";
import TemperatureUnitContext from "../contexts/TemperatureUnitContext";
import { WeatherForecast } from "../interfaces/WeatherForecast";
import CityWeather from "./CityWeather";
import useWeatherData from "../hooks/useWeatherData";
import "./styles/WeatherForecasts.css";
import ErrorMessage from "./ErrorMessage";
import { forecastEndpoint } from "../utils/constants";
import LoadingSpinner from "./LoadingSpinner";

const WeatherForecasts = () => {
  const cities = ["Tampa, FL", "Austin, TX", "Reno, NV"];
  const forecastUri = forecastEndpoint(cities);

  const { temperatureUnit, setTemperatureUnit } = useContext(
    TemperatureUnitContext
  );

  const toggleUnit = () => {
    setTemperatureUnit(temperatureUnit === "F" ? "C" : "F");
  };

  const { data, isLoading, error } = useWeatherData(forecastUri);

  if (isLoading) {
    return <LoadingSpinner />;
  }

  if (error) {
    return <ErrorMessage message={error} />;
  }

  return (
    <div className='weather-container' data-testid='weather-container'>
      <button className='toggle-button' onClick={toggleUnit}>
        Toggle Unit ({temperatureUnit === "F" ? "Celsius" : "Fahrenheit"})
      </button>
      <div className='forecasts-container'>
        {data.map((city: WeatherForecast) => (
          <CityWeather key={city.city} cityWeather={city} />
        ))}
      </div>
    </div>
  );
};

export default WeatherForecasts;
