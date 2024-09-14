program Relatorio;

uses
  Vcl.Forms,
  FrmRelatorio in 'FrmRelatorio.pas' {FrmRelatorios},
  uFuncionarioController in '..\Controllers\uFuncionarioController.pas',
  uTicketController in '..\Controllers\uTicketController.pas',
  uFuncionarioDAO in '..\DAO\uFuncionarioDAO.pas',
  uTicketDAO in '..\DAO\uTicketDAO.pas',
  FuncionarioModel in '..\Models\FuncionarioModel.pas',
  TicketModel in '..\Models\TicketModel.pas',
  CreateFuncionarioDto in '..\Data\Dto\CreateFuncionarioDto.pas',
  CreateTicket in '..\Data\Dto\CreateTicket.pas',
  ReadTicket in '..\Data\Dto\ReadTicket.pas',
  ReadTicketDetalhado in '..\Data\Dto\ReadTicketDetalhado.pas',
  TicketsResumo in '..\Data\Dto\TicketsResumo.pas',
  UpdateFuncionarioDto in '..\Data\Dto\UpdateFuncionarioDto.pas',
  UpdateTicket in '..\Data\Dto\UpdateTicket.pas',
  uDataModulo in '..\Data\uDataModulo.pas' {DataModulo: TDataModule},
  ThreadingEx in '..\Utils\ThreadingEx.pas',
  Funcoes in '..\Utils\Funcoes.pas';

{$R *.res}

begin
  Application.Initialize;
  Application.MainFormOnTaskbar := True;
  Application.CreateForm(TDataModulo, DataModulo);
  Application.CreateForm(TFrmRelatorios, FrmRelatorios);
  Application.Run;
end.
