import { render, screen } from "@testing-library/react";
import { DayForecast as DayForecastInterface } from "../interfaces/DayForecast";
import TemperatureUnitContext from "../contexts/TemperatureUnitContext";
import DayForecast from "../components/DayForecast";

const mockDayForecast: DayForecastInterface = {
  date: new Date(),
  minimumTemperatureCelsius: 20,
  maximumTemperatureCelsius: 30,
  humidity: 50,
  text: "Partly Cloudy",
  icon: "https://example.com/icon.png",
};

test("renders DayForecast component with correct temperature", () => {
  render(
    <TemperatureUnitContext.Provider
      value={{ temperatureUnit: "C", setTemperatureUnit: () => {} }}
    >
      <DayForecast day={mockDayForecast} />
    </TemperatureUnitContext.Provider>
  );
  const temperature = screen.getByText(/20° - 30° C/i);
  expect(temperature).toBeInTheDocument();
});
