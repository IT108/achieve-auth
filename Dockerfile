FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://*:80


COPY . ./
RUN dotnet publish AuthServer -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS final
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "achieve-auth.dll"]