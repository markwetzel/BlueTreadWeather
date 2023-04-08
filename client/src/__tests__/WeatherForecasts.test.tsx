import { render, screen } from "@testing-library/react";
import WeatherForecasts from "../components/WeatherForecasts";

jest.mock("../hooks/useWeatherData", () => {
  return () => ({
    data: [],
    isLoading: false,
    error: null,
  });
});

test("renders WeatherForecasts component without error", () => {
  render(<WeatherForecasts />);
  const weatherContainer = screen.getByTestId("weather-container");
  expect(weatherContainer).toBeInTheDocument();
});
