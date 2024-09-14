# Tickets - Principal
# API Rest - BackEnd

*Utilizo como arquitetura de software o MVC, pela facilidade de manutenção e escalabilidade.*
*Projeto posui documentação com Swagger, ao rodar o projeto .NET ele abre um navegador contendo todos Endpoints*
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

# Tickets - FrontEnd
## Aplicação desenvolvida em Delphi 11 FMX (Firemonkey)
## Diretórios do Projeto     
- **Pasta DAO **
  - Ficam as reais requisições nos EndPoints da API (Tickets-Backend).
    
- **Pasta Controllers **
  - Ficam as Requisições que são solitadas pela VIEW, nos Controller é utilizado Threads para manter a responsividade
  - Para enviar as Requisições o Controller utiliza uma classe DAO para fazer as reais requisições.

- **Pasta Data **
  - Fica o DataModulo que é uma classe responsável pelo acesso aos meus objetos de Requisições TREST (TRESTClient, TRESTRequest, TRESTResponse)
  - Possuo uma classe TConfigREST dentro do DataModulo aonde possuir os metodos Get, Post, Put para o DAO poder utilizar.
  - **Pasta Data/Dto**
  - Fica meus objetos de transferência de dados, são utilizados como um modelo para inserir registros, atualizar e consultar.

- **Pasta Frames **
  - Ficam meus frames que utilizo nas minhas Views.

- **Pasta Models **
  - Possui meus objetos modelo das Tabelas (Funcionario e Tickets)

- **Pasta Relatorio **
  - Criei um novo projeto VCL somente para Relatório, para conseguir utilizar a ferramenta FastReport que na minha opnião para Delphi é uma das melhores.
  - Possui meu projeto VCL para emissão do relatório utilizando FastReport.
  - O executavel do projeto Relatorio é utilizado na tela Principal do projeto Tickets-Frontend botao Relatorio
    
- **Pasta Utils **
  - Possui algumas funções gerais que sao utilizadas no Projeto.

- **Pasta Views **
  - Contém todas telas do sistema.

      
