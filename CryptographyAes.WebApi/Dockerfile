FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY /bin/release/net7.0/publish/ .
EXPOSE 7221
ENV ASPNETCORE_URLS=http://0.0.0.0:7221
RUN ln -fs /usr/share/zoneinfo/America/Bogota /etc/localtime
RUN dpkg-reconfigure -f noninteractive tzdata
ENTRYPOINT ["dotnet", "CryptographyAes.WebApi.dll"]


