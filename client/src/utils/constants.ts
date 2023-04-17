const apiBaseUrl =
  process.env.REACT_APP_API_BASE_URL || "http://localhost:5000/api/v1";

export const forecastEndpoint = (cities: string[]) =>
  `${apiBaseUrl}/WeatherForecast?city=${cities.join("&city=")}`;
