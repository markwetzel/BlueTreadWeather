import { createContext } from "react";

const TemperatureUnitContext = createContext({
  temperatureUnit: "C",
  setTemperatureUnit: (_unit: "C" | "F") => {},
});

export default TemperatureUnitContext;
