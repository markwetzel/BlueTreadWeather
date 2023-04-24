# Set the current directory to the directory where the script is located
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $scriptDir

# Stop the containers
docker-compose down

# Build the images
docker-compose build

# Start the containers
docker-compose up
