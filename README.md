# BlueTreadWeather

## Prerequisites

### Front-end

    Node.js version 14 or higher

    NPM version 6 or higher

### Back-end

    .NET 6 SDK or higher

## Installation

To run BlueTreadWeather, please follow these steps:

Clone the repository using the following command:

```
git clone https://github.com/markwetzel/BlueTreadWeather
```

### Back-end

After cloning the repository, navigate into the `BlueTreadWeather` directory and proceed with the following steps:

1. Restore the back-end packages by running the following command:

```
dotnet restore
```

2. Add your WeatherAPI key by initializing the user secret store by running:

```
dotnet user-secrets init --project .\API\
```

3. Set the WeatherAPI key in the user secret store by running the following command, making sure to replace the example key with your own (or the one provided in the email):

```
dotnet user-secrets set "Keys:WeatherApiKey" "<your-weather-api-key>" --project .\API\
```

4. Run the back-end API using the following command:

```
dotnet run --project .\API\
```

### Front-end

To run the front-end, navigate to the `client` directory using a new terminal window:

```
cd client
```

Run the following command to restore the packages:

```
npm install
```

Then, run the front-end using the following command:

```
npm start
```

Finally, navigate to http://localhost:3000/ on your web browser to access the app.
