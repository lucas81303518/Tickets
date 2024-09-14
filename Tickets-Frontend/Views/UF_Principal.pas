unit UF_Principal;

interface

uses
  System.SysUtils, System.Types, System.UITypes, System.Classes, System.Variants,
  FMX.Types, FMX.Controls, FMX.Forms, FMX.Graphics, FMX.Dialogs, FMX.Objects,
  FMX.Controls.Presentation, FMX.StdCtrls;

type
  TFrmPrincipal = class(TForm)
    RectangleMenu: TRectangle;
    rectFuncionarios: TRectangle;
    Label1: TLabel;
    Image1: TImage;
    rectRelatorios: TRectangle;
    Label2: TLabel;
    rectTickets: TRectangle;
    Label3: TLabel;
    Image7: TImage;
    Image5: TImage;
    Rectangle4: TRectangle;
    Rectangle1: TRectangle;
    procedure FormShow(Sender: TObject);
    procedure rectFuncionariosClick(Sender: TObject);
    procedure rectTicketsClick(Sender: TObject);
    procedure rectRelatoriosClick(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
  private
    { Private declarations }
  public
    { Public declarations }

  end;

var
  FrmPrincipal: TFrmPrincipal;

implementation

uses
  uDataModulo, Loading, UF_ConsultaFuncionarios, UF_ConsultaTickets,
  funcoes;

{$R *.fmx}

procedure TFrmPrincipal.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  if ProcessoEstaEmExecucao(NOME_SERVICO_RELATORIO) then
    TerminarProcesso(NOME_SERVICO_RELATORIO);
end;

procedure TFrmPrincipal.FormShow(Sender: TObject);
begin
  TLoading.show('Verificando conex�o com API...', FrmPrincipal);
  try
    DataModulo.RestConfig.ValidaConexaoAPI();
  finally
    TLoading.Hide;
  end;
end;

procedure TFrmPrincipal.rectFuncionariosClick(Sender: TObject);
begin
  if not Assigned(FrmConsultaFuncionarios) then
    FrmConsultaFuncionarios := TFrmConsultaFuncionarios.Create(Self);
  FrmConsultaFuncionarios.Show;
end;

procedure TFrmPrincipal.rectRelatoriosClick(Sender: TObject);
begin
  ExecutarExecutavel(NOME_SERVICO_RELATORIO);
end;

procedure TFrmPrincipal.rectTicketsClick(Sender: TObject);
begin
  if not Assigned(FrmConsultaTicket) then
    FrmConsultaTicket := TFrmConsultaTicket.Create(Self);
  FrmConsultaTicket.Show;
end;

end.