# Estágio de Compilação
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
# Copia o arquivo de projeto e restaura as dependências
COPY ["ProjetoAgendamento.csproj", "./"]
RUN dotnet restore
# Copia o resto dos arquivos e compila
COPY . .
RUN dotnet publish -c Release -o /app

# Estágio de Execução
FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "ProjetoAgendamento.dll"]