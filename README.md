# ProjetoStefaniniAPI

Este projeto é uma API para gerenciar pedidos, produtos e itens de pedido, desenvolvida em .NET Core. A API segue os princípios de DDD, Clean Code e é configurada para trabalhar com o Entity Framework Core.

## Requisitos

- [.NET SDK 6.0 ou superior](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- [Visual Studio 2022 ou Visual Studio Code](https://visualstudio.microsoft.com/pt-br/)
- [Entity Framework Core CLI](https://docs.microsoft.com/pt-br/ef/core/cli/dotnet)

## Configuração do Projeto

### 1. Clonar o Repositório



```bash
    git clone https://github.com/igormieski27/ProjetoStefaniniAPI.git
```
```bash
    cd ProjetoStefaniniAPI
```

### 2. Configurar o Banco de Dados
No arquivo appsettings.json, configure a string de conexão com seu banco de dados SQL Server.

Configuração padrão:
```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=PedidosDB;Integrated Security=true;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}

```


### 3. Executar as Migrations

No terminal, execute os seguintes comandos para aplicar as migrations e criar o banco de dados:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Executar a API
Para executar a API e acessar a UI do Swagger para acessar e visualizar os  endpoints, execute usando o modo IIS Express:
![image](https://github.com/user-attachments/assets/96c551ef-32da-4fb7-a1b4-18d5134bb6b1)

### 5. Acessar o swagger ui
Caso não abra automaticamente, acesse o endereço [https://localhost:44347/](https://localhost:44347/swagger/index.html) e você terá acesso à interface do swagger.

![image](https://github.com/user-attachments/assets/3cd0ebae-f19d-472d-aede-1dc2a4694962)

