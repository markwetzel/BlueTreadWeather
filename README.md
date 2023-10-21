# SkyStatus

This is a weather forecasting application that consists of a .NET 6 backend API and a React frontend. The application can be run locally using Docker and Docker Compose.

## Prerequisites

1. Docker Desktop: Download and install Docker Desktop for Windows from [here](https://www.docker.com/products/docker-desktop). Make sure to have WSL 2 installed and set up as the default, as described in the [official documentation](https://docs.docker.com/desktop/windows/wsl/).

2. Git: Download and install Git for Windows from [here](https://git-scm.com/download/win).

## Installation

To run SkyStatus, please follow these steps:

1. Clone the repository using the following command:

```
git clone https://github.com/markwetzel/SkyStatus
```

2. Navigate to the `SkyStatus` directory:

```
cd SkyStatus
```

3. Create the `.env` file in the `backend` directory and add the following environment variable to it and substitute the value with your own API key from [WeatherApi](https://www.weatherapi.com/):

```
WeatherApiKey=xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
```

4. Build and run the API and client using the included Docker Compose script:

```
.\run.ps1
```

5. Open a web browser and navigate to http://localhost:3000/ to access the app.

To stop and remove the running containers, execute the following command from the `SkyStatus` directory:

```
docker-compose down
```
