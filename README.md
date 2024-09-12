# Tickets - Principal
# API Rest - BackEnd

*Utilizo como arquitetura de software o MVC, pela facilidade de manutenção e escalabilidade.*

## Diretórios do Projeto

- **Pasta Models**
  - Contém os modelos de negócio com as regras.
  - `Models/Validations` contém minhas classes de validações de atributos.

- **Pasta Services**
  - Contém os serviços da aplicação, consultas no banco de dados.

- **Pasta Controllers**
  - Contém os endpoints que o cliente irá consumir.
  - Nos Controllers, utilizo os services com injeção de dependência para facilitar a reutilização futura das lógicas aplicadas.

- **Pasta Data**
  - Contém minha classe de contexto `TicketsContext` (Banco de dados).
  - String de conexão na raiz do projeto: `appsettings.json`.
  - `Data/DTO` contém meus objetos de transferência de dados.

- **Pasta Profiles**
  - Contém minhas classes de mapeamento de dados (DTO para Model, Model para DTO e vice-versa).
  - Utilizo **AutoMapper**.

## Tecnologias e Configurações

- **Migrations**
  - Utilizo Migrations no projeto, pois o EntityFramework facilita o trabalho sem precisar "reescrever a roda".

- **Injeção de Dependência**
  - Configurada para facilitar a reutilização e manutenção do código.

- **AutoMapper**
  - Configurado para mapeamento de dados.
  - Utilizo na pasta DTO onde estão meus objetos de transferência de dados.
