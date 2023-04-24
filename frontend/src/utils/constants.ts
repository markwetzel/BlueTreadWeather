const apiBaseUrl = process.env.REACT_APP_API_URL;

export const forecastEndpoint = (cities: string[]) =>
  `${apiBaseUrl}/WeatherForecast?city=${cities.join("&city=")}`;
