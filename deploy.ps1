# Set the current directory to the directory where the script is located
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $scriptDir

# Deploy the frontend
Write-Host "Deploying frontend..."
Set-Location frontend
heroku container:login
heroku container:push web -a bt-weather-frontend --arg REACT_APP_API_URL=https://bt-weather-backend.herokuapp.com/api/v1
heroku container:release web -a bt-weather-frontend
Set-Location ..

# Deploy the backend
Write-Host "Deploying backend..."
Set-Location backend
heroku container:login
heroku container:push web -a bt-weather-backend
heroku container:release web -a bt-weather-backend
Set-Location ..

Write-Host "Deployment complete."
