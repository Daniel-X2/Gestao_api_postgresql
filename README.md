# Sistema de Gestão com .NET

API REST assíncrona desenvolvida em ASP.NET Core para gestão de clientes, funcionários e produtos, utilizando PostgreSQL como banco de dados. O projeto segue uma arquitetura modular com separação  de responsabilidades entre as camadas de roteamento, serviços e repositórios.

## Tecnologias

- **Plataforma**: .NET / ASP.NET Core (Minimal APIs)
- **Banco de Dados**: PostgreSQL
- **Acesso a Dados**: ADO.NET com Npgsql
- **Configuração**: DotNetEnv (variáveis de ambiente via `.env`)
- **Testes**: xUnit, Moq, Bogus
- **Padrões**: Injeção de Dependência, Repository Pattern, Service Layer, Middleware de Exceções

## Estrutura do Projeto

```
├── Api/                        # Ponto de entrada, rotas, middleware
│   ├── Application/	
│   │   ├── Routers/            # Definição dos endpoints (Minimal APIs)
│   │   └── middleware/         # Tratamento centralizado de exceções
│   └── Program.cs
│
├── Api.Core/                   # Lógica de negócio e acesso a dados
│   └── Application/
│       ├── dto/                # DTOs (ClientDto, FuncionarioDto, ProdutoDto)
│       ├── repository/         # Repositórios com queries SQL
│       ├── service/            # Regras de negócio e validações
│       ├── utils/              # Conexão, DI (AddScope), Validation
│       └── CustomException/    # Exceções personalizadas
│
└── Api.Test/                   # Testes unitários
    ├── dados.cs                # Geração de dados com Bogus
    ├── TestServiceClient.cs
    ├── TestServiceFuncionario.cs
    ├── TestServiceProduct.cs
    └── TestUtils.cs
```

## Funcionalidades

### Clientes (`/client/`)
- Cadastro com validação de CPF (algoritmo próprio), conta e nome
- Atualização parcial — campos inválidos são mantidos com o valor anterior e retornados na resposta
- Listagem completa e busca por ID
- Remoção por ID
- Suporte a marcação de cliente VIP

### Funcionários (`/funcionario/`)
- Cadastro com validação de CPF, nome e ano de nascimento (18–85 anos)
- Controle de atestados e privilégios administrativos
- CRUD completo

### Produtos (`/product/`)
- Cadastro com validação de código, lote, quantidade e valor de revenda
- Consulta de estoque e valor bruto total
- Atualização parcial — campos inválidos mantêm o valor anterior
- CRUD completo

## Arquitetura de Tratamento de Erros

Middleware centralizado captura todas as exceções e retorna respostas padronizadas:

| Exceção | Status HTTP |
|---|---|
| `InvalidCpfException` | 422 Unprocessable Entity |
| `InvalidNameException` | 422 Unprocessable Entity |
| `InvalidAccountException` | 422 Unprocessable Entity |
| `InvalidCodeException` | 422 Unprocessable Entity |
| `NegativeNumericException` | 422 Unprocessable Entity |
| `ReturnDataIsEmpty` | 400 Bad Request |
| `InvalidIdException` | 400 Bad Request |
| `InvalidNascimentoException` | 400 Bad Request |
| `InvalidLoteException` | 400 Bad Request |
| `ErroAddToDatabaseException` | 400 Bad Request |
| `ErroUpdateToDatabaseException` | 400 Bad Request |
| `InvalidConnection` | 500 Internal Server Error |

## Testes

Os testes cobrem a camada de serviço com mocks dos repositórios, sem necessidade de banco de dados.

- **Moq** — mock das interfaces de repositório
- **Bogus** — geração de dados falsos  para os Testes
- **xUnit** — execução dos testes

```bash
dotnet test
```

## Configuração

### 1. Banco de Dados

Execute o script `sql.sql` (em `Api.Core/`) no seu servidor PostgreSQL para criar as tabelas `cliente`, `funcionario` e `produto`.

### 2. Variáveis de Ambiente

Crie um arquivo `.env` na raiz do projeto `Api/`:

```env
DB_CONNECTION=Host=localhost;Database=nome;Username=user;Password=senha
```

### 3. Execução

```bash
dotnet run --project Api
```

## Endpoints

| Método | Rota | Descrição |
|---|---|---|
| GET | `/client/get/` | Lista todos os clientes |
| POST | `/client/add/` | Adiciona um cliente |
| PUT | `/client/update/{id}/` | Atualiza um cliente |
| DELETE | `/client/delete/{id}` | Remove um cliente |
| GET | `/funcionario/get` | Lista todos os funcionários |
| GET | `/funcionario/get/{id}` | Busca funcionário por ID |
| POST | `/funcionario/add/` | Adiciona um funcionário |
| PUT | `/funcionario/update/{id}/` | Atualiza um funcionário |
| DELETE | `/funcionario/delete/{id}` | Remove um funcionário |
| GET | `/product/get` | Lista todos os produtos |
| GET | `/product/get/{id}` | Busca produto por ID |
| GET | `/estoque/get` | Consulta estoque (nome e quantidade) |
| GET | `/estoque/valorBruto` | Lista valores de revenda |
| POST | `/product/add` | Adiciona um produto |
| PUT | `/product/update/{id}/` | Atualiza um produto |
| DELETE | `/product/delete/{id}` | Remove um produto |
