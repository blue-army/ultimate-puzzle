# ultimate-puzzle

solver for [the ultimate puzzle](http://www.theultimatepuzzle.com).

# running the application

1. clone this repo and navigate to the root folder.
2. install `dotnet`.
3. from the command-line execute: `dotnet run`.

alternatively run within a container (ensure docker is installed):

1. clone the repo and navigate to the root folder.
2. from the command-line execute: `docker run --rm -v $(pwd):/app -w /app mcr.microsoft.com/dotnet/sdk:9.0 dotnet run`


