export const getUserTemperatureUnit = (): "C" | "F" => {
  const userLanguage = navigator.language;

  // Countries that use Fahrenheit
  // Irrelevant for this demo's purposes
  const fahrenheitCountries = ["US", "BS", "BZ", "KY", "PW"];

  const userCountry = userLanguage.split("-")[1];

  return fahrenheitCountries.includes(userCountry) ? "F" : "C";
};

export const celsiusToFahrenheit = (celsius: number) => (celsius * 9) / 5 + 32;
export const fahrenheitToCelsius = (fahrenheit: number) =>
  ((fahrenheit - 32) * 5) / 9;
