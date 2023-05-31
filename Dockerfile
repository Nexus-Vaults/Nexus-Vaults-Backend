FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

RUN apt update && apt upgrade -y && apt install npm -y

RUN curl -fsSL https://deb.nodesource.com/setup_lts.x | bash - \
    && apt install -y nodejs

RUN wget https://packages.microsoft.com/config/debian/11/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && rm packages-microsoft-prod.deb

RUN apt update && apt install dotnet-runtime-5.0 -y

RUN echo "Cloning code"
RUN git clone https://github.com/Nexus-Vaults/Nexus-Vaults-Backend.git Code --recurse-submodules \
    && cd Code \
    && npm i \
    && cd contracts \
    && npm i

ENV PATH="${PATH}:/root/.dotnet/tools"

RUN cd Code \
    && node compile-contracts.js \
    && dotnet tool install -g Nethereum.Generator.Console \
    && cd src/Infrastructure \
    && Nethereum.Generator.Console generate from-project

RUN cd Code \
    && dotnet publish -c release -o /app


FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
COPY appsettings.json /app/appsettings.json
ENTRYPOINT ["dotnet", "WebAPI.dll"]