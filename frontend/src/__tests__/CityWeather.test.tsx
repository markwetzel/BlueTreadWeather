// Add this test to a new file `CityWeather.test.tsx`
import { render, screen } from "@testing-library/react";
import CityWeather from "../components/CityWeather";
import { WeatherForecast } from "../interfaces/WeatherForecast";

const mockCityWeather: WeatherForecast = {
  city: "Tampa",
  state: "FL",
  dayForecasts: [],
};

test("renders CityWeather component with correct city and state", () => {
  render(<CityWeather cityWeather={mockCityWeather} />);
  const cityState = screen.getByText(/Tampa, FL/i);
  expect(cityState).toBeInTheDocument();
});
