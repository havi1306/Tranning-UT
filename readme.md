
## Command:
#### dotnet run --launch-profile "NetCore"
#### dotnet run --environment "Staging"
#### dotnet NetCore.API.dll --environment "Staging" --server.urls "http://localhost:5101;http://*:5102"
## Docker:
#### docker build -f "./Dockerfile" -t audiobook-api
#### docker build -f "./Dockerfile" -t audiobook-api  --target base "E:\DEV\audiobook\audiobook-api\src"
#### docker run -td -p 8080:80 -p 5050:5000 --name testapp audiobook-api

## Bookmark:
https://andrewlock.net/how-to-set-the-hosting-environment-in-asp-net-core/
http://www.talkingdotnet.com/define-a-custom-environment-in-asp-net-core/
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?tabs=basicconfiguration&view=aspnetcore-2.1#setup-and-use-the-commandline-configuration-provider