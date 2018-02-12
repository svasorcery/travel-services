# base image for ASP.NET Core with NodeJS
# automatically exposes PORT 80
FROM microsoft/aspnetcore

# set the working directory
WORKDIR /app

# set the environment
ENV ASPNETCORE_ENVIRONMENT Production

# copy the published folder created using
# dotnet publish -c Release -o publish
COPY publish ./

# set the entrypoint of the application
ENTRYPOINT ["dotnet", "Kaolin.Api.Rail.dll"]
