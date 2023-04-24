#!/bin/bash

echo "Deploying frontend..."
cd frontend
heroku container:login
heroku container:push web -a bt-weather-frontend --arg REACT_APP_API_URL=https://bt-weather-backend.herokuapp.com/api/v1
heroku container:release web -a bt-weather-frontend
cd ..

echo "Deploying backend..."
cd backend
heroku container:login
heroku container:push web -a bt-weather-backend
heroku container:release web -a bt-weather-backend
cd ..

echo "Deployment complete."
