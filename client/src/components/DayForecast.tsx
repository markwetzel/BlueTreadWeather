import React, { useContext } from "react";
import TemperatureUnitContext from "../contexts/TemperatureUnitContext";
import { celsiusToFahrenheit } from "../utils/temperature";
import { DayForecast as DayForecastInterface } from "../interfaces/DayForecast";

interface DayForecastProps {
  day: DayForecastInterface;
}

const DayForecast: React.FC<DayForecastProps> = ({ day }) => {
  const { temperatureUnit } = useContext(TemperatureUnitContext);

  const minTemp = Math.round(
    temperatureUnit === "F"
      ? celsiusToFahrenheit(day.minimumTemperatureCelsius)
      : day.minimumTemperatureCelsius
  );
  const maxTemp = Math.round(
    temperatureUnit === "F"
      ? celsiusToFahrenheit(day.maximumTemperatureCelsius)
      : day.maximumTemperatureCelsius
  );

  return (
    <div>
      <h3>
        {new Date(day.date).toLocaleDateString("en-US", {
          weekday: "long",
          year: "numeric",
          month: "long",
          day: "numeric",
        })}
      </h3>
      <p>
        {minTemp}° - {maxTemp}° {temperatureUnit}
      </p>
      <p>{day.humidity}% humidity</p>
      <p>{day.text}</p>
      <img src={day.icon} alt={day.text} />
    </div>
  );
};

export default React.memo(DayForecast);
