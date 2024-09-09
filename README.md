# Tickets - Principal
API Rest - BackEnd
*Utilizo como arquitetura de software o MVC, pela facilidade de manutenção e escalabilidade.

Diretórios do Projeto:
-Pasta Models (Contém os modelos de negócio com as regras)
  - Models/Validations contém minhas classes de validações de atributos.
-Pasta Services (Contém os serviços da aplicação, consultas no banco de dados)
-Pasta Controllers (Obtém os endpoints aonde o Client irá consumir)
  - Nos Controllers eu utilizo os services com injeção de dependencia, para facilitar a reutilização futura
  das lógicas aplicadas.  
-Pasta Data (contém minha classe de contexto TicketsContext(Banco de dados)), String de conexão na raiz do projeto --> appsettings.json
  - Pasta Data/DTO contem meus objetos de transferência de dados.
-Pasta Profiles (contém minhas classes de Mapeamento de dados (Dto para Model, Model para Dto vice-versa) utilizo AutoMapper)

*Utilizo Migrations no Projeto, pois o EntityFramework facilita demais o trabalho sem precisar reescrever a roda.
*Injeção de dependência configurada
*AutoMapper configurado (Interface de mapeamento de dados, utilizo ela na pasta DTO aonde tenho meus objetos de transferencia de dados)
