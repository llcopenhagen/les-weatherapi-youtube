FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-env
WORKDIR /app

#Copy the csproj file and restore any dependensies (via nuget)
COPY *.csproj ./
RUN dotnet restore

#copy the project files and build our release
COPY . ./
RUN dotnet publish -c Release -o out

# generate runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "weatherapi.dll"]