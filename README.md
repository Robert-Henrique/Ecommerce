# E-commerce API & Frontend

## Introdução
Este projeto consiste em uma API RESTful desenvolvida em .NET 8 e um frontend em Vue.js para gerenciar pedidos de uma loja online. A API segue a Clean Architecture, utiliza CQRS e DDD, e interage com um banco de dados SQL Server para armazenamento transacional e MongoDB para leitura.

## Tecnologias Utilizadas
### Backend
- .NET 8
- Entity Framework Core
- SQL Server
- MongoDB
- Clean Architecture
- CQRS (Command Query Responsibility Segregation)
- DDD (Domain-Driven Design)

### Frontend
- Vue.js
- Vue Router

## Estrutura de Pastas
```
Ecommerce/
  src/
    API/              # Projeto da API
    Application/      # Camada de Aplicação
    Domain/          # Camada de Domínio
    Infra/           # Infraestrutura (Banco de dados, ORM, etc.)
  Client/
    ecommerce-frontend/ # Projeto Frontend Vue.js
```

## Executando o Backend
### 1. Configurar o banco de dados
Certifique-se de que o SQL Server e o MongoDB estejam rodando.

#### Executar Migrations do SQL Server
Abra o terminal e execute o seguinte comando para aplicar as migrations no banco de dados:
```sh
dotnet ef database update --project C:\Projetos\Estudos\Ecommerce\src\Infra --startup-project C:\Projetos\Estudos\Ecommerce\src\API
```

### 2. Executar a API
Para rodar a API, utilize o seguinte comando:
```sh
dotnet run --project C:\Projetos\Estudos\Ecommerce\src\API
```
A API estará disponível em `http://localhost:5223/api/`.

### 3. Subir o MongoDB com Docker
Caso esteja utilizando Docker, rode o seguinte comando para iniciar o MongoDB:
```sh
docker run -it --rm mongo mongo --host mongo --port 27017
```

## Executando o Frontend
### 1. Instalar dependências
Navegue até o diretório do frontend e instale as dependências:
```sh
cd C:\Projetos\Estudos\Ecommerce\Client\ecommerce-frontend
npm install
```

### 2. Rodar o servidor de desenvolvimento
Para iniciar o frontend, execute:
```sh
npm run serve
```
A aplicação estará acessível em `http://localhost:8080`.
