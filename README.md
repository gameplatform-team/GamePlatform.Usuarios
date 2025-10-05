# GamePlatform

## 📋 Sobre o Projeto
GamePlatform.Usuarios é um microserviço .NET 8.0 desenvolvida seguindo os princípios da Clean Architecture, oferecendo uma plataforma robusta e escalável para gerenciamento de usuários.

## 🏗️ Arquitetura

O projeto está estruturado em camadas:

- **GamePlatform.Usuarios.Api**: Camada de apresentação que expõe as APIs RESTful
- **GamePlatform.Usuarios.Application**: Contém a lógica de aplicação e casos de uso
- **GamePlatform.Usuarios.Domain**: Define as entidades de domínio e regras de negócio
- **GamePlatform.Usuarios.Infrastructure**: Implementa o acesso a dados e serviços externos
- **GamePlatform.Usuarios.Tests**: Projeto de testes unitários

## 🚀 Como Executar

### Pré-requisitos
- .NET SDK 8.0 ou superior
- Uma IDE compatível (recomendado: Visual Studio, JetBrains Rider ou VS Code)

### Passos para Execução

1. Clone o repositório:

```bash
https://github.com/gameplatform-team/GamePlatform.Usuarios.git
```

2. Navegue até a pasta do projeto:
```bash
cd GamePlatform.Usuarios
``` 

3. Restaure as dependências:
```bash
dotnet restore
``` 

4. Execute a aplicação:
```bash
cd GamePlatform.Usuarios.Api
``` 
```bash
dotnet run
``` 

A API estará disponível em `http://localhost:5232`.

Você pode executar as requisições através do Swagger: `http://localhost:5232/swagger/index.html`.

## 🧪 Executando os Testes

Para executar os testes unitários:
```bash
dotnet test
```

## 🛠️ Tecnologias Utilizadas

- ASP.NET Core 8.0
- C# 12.0
- Clean Architecture
- Testes Unitários

## 📦 Estrutura da Solução

```plaintext
GamePlatform/
├── GamePlatform.Usuarios.Api/            # API endpoints e configurações
├── GamePlatform.Usuarios.Application/    # Casos de uso e lógica de aplicação
├── GamePlatform.Usuarios.Domain/         # Entidades e regras de negócio
├── GamePlatform.Usuarios.Infrastructure/ # Implementações de repositórios e serviços
└── GamePlatform.Usuarios.Tests/          # Testes unitários
```

## 🔄 CI/CD

O projeto utiliza GitHub Actions para automação de CI/CD, incluindo:
- Build e testes automatizados
- Build e push de imagem Docker
- Deploy automático para Azure Web App
