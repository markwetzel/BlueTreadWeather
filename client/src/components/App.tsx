import { useState } from "react";
import WeatherForecasts from "./WeatherForecasts";
import { getUserTemperatureUnit } from "../utils/temperature";
import TemperatureUnitContext from "../contexts/TemperatureUnitContext";

const App = () => {
  const [temperatureUnit, setTemperatureUnit] = useState(
    getUserTemperatureUnit()
  );
  return (
    <>
      <TemperatureUnitContext.Provider
        value={{ temperatureUnit, setTemperatureUnit }}
      >
        <WeatherForecasts />
      </TemperatureUnitContext.Provider>
    </>
  );
};

export default App;
