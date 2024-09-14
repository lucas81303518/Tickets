program Tickets;

uses
  System.StartUpCopy,
  FMX.Forms,
  UF_Principal in 'Views\UF_Principal.pas' {FrmPrincipal},
  FuncionarioModel in 'Models\FuncionarioModel.pas',
  Enumerados in 'Models\Enums\Enumerados.pas',
  CreateFuncionarioDto in 'Data\Dto\CreateFuncionarioDto.pas',
  UpdateFuncionarioDto in 'Data\Dto\UpdateFuncionarioDto.pas',
  UF_ConsultaBase in 'Views\UF_ConsultaBase.pas' {FrmConsultaBase},
  uFrameConsultaFuncionario in 'Frames\uFrameConsultaFuncionario.pas' {FrameConsultaFuncionario: TFrame},
  uFrameColunas in 'Frames\uFrameColunas.pas' {FrameColunas: TFrame},
  UF_ConsultaFuncionarios in 'Views\UF_ConsultaFuncionarios.pas' {FrmConsultaFuncionarios},
  uDataModulo in 'Data\uDataModulo.pas' {DataModulo: TDataModule},
  Loading in 'Utils\Loading.pas',
  ThreadingEx in 'Utils\ThreadingEx.pas',
  uFuncionarioDAO in 'DAO\uFuncionarioDAO.pas',
  uFuncionarioController in 'Controllers\uFuncionarioController.pas',
  Funcoes in 'Utils\Funcoes.pas',
  UF_CadastroBase in 'Views\UF_CadastroBase.pas' {FrmCadastroBase},
  UF_CadastroFuncionario in 'Views\UF_CadastroFuncionario.pas' {FrmCadastroFuncionario},
  uFormat in 'Utils\uFormat.pas',
  UF_ConsultaTickets in 'Views\UF_ConsultaTickets.pas' {FrmConsultaTicket},
  CreateTicket in 'Data\Dto\CreateTicket.pas',
  UpdateTicket in 'Data\Dto\UpdateTicket.pas',
  ReadTicket in 'Data\Dto\ReadTicket.pas',
  ReadTicketDetalhado in 'Data\Dto\ReadTicketDetalhado.pas',
  TicketModel in 'Models\TicketModel.pas',
  uTicketDAO in 'DAO\uTicketDAO.pas',
  TicketsResumo in 'Data\Dto\TicketsResumo.pas',
  uTicketController in 'Controllers\uTicketController.pas',
  uFrameConsultaTickets in 'Frames\uFrameConsultaTickets.pas' {FrameConsultaTickets: TFrame},
  UF_CadastroTicket in 'Views\UF_CadastroTicket.pas' {FrmCadastroTicket};

{$R *.res}

begin
  Application.Initialize;
  Application.CreateForm(TDataModulo, DataModulo);
  Application.CreateForm(TFrmPrincipal, FrmPrincipal);
  Application.Run;
end.
