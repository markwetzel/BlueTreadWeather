import { useState, useEffect } from "react";
import { WeatherForecast } from "../interfaces/WeatherForecast";

interface WeatherData {
  data: WeatherForecast[];
  isLoading: boolean;
  error: string | null;
}

const useWeatherData = (forecastUri: string): WeatherData => {
  const [data, setData] = useState<WeatherForecast[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      setIsLoading(true);
      setError(null);
      try {
        const response = await fetch(forecastUri);
        const forecasts: WeatherForecast[] = await response.json();
        setData(forecasts);
      } catch (error) {
        setError((error as Error).message);
      }
      setIsLoading(false);
    };
    fetchData();
  }, [forecastUri]);

  return { data, isLoading, error };
};

export default useWeatherData;
