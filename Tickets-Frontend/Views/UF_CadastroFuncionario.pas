unit UF_CadastroFuncionario;

interface

uses
  System.SysUtils, System.Types, System.UITypes, System.Classes, System.Variants,
  FMX.Types, FMX.Graphics, FMX.Controls, FMX.Forms, FMX.Dialogs, FMX.StdCtrls,
  UF_CadastroBase, FMX.Objects, FMX.Controls.Presentation, FMX.Edit, FMX.Layouts,
  FMX.DateTimeCtrls, uFuncionarioController, CreateFuncionarioDto, UpdateFuncionarioDto;

type
  TFrmCadastroFuncionario = class(TFrmCadastroBase)
    LayoutNome: TLayout;
    Label3: TLabel;
    edtNome: TEdit;
    LayoutCpfAndData: TLayout;
    LayoutCpf: TLayout;
    Label2: TLabel;
    EditCpf: TEdit;
    LayoutDataAlteracao: TLayout;
    Label1: TLabel;
    DateEditAlteracao: TDateEdit;
    Layout1: TLayout;
    Label4: TLabel;
    gbSituacao: TGroupBox;
    rbInativo: TRadioButton;
    rbAtivo: TRadioButton;
    timeEditHoraAlteracao: TTimeEdit;
    procedure EditCpfEnter(Sender: TObject);
    procedure EditCpfChange(Sender: TObject);
    procedure imageSalvarClick(Sender: TObject);
    procedure FormShow(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
  private
    { Private declarations }
    FuncionarioController: TFuncionarioController;
    function ValidarCampos: Boolean;
    procedure OnExecutarAposExecucao(Sender: TObject);
    procedure AjustarComponentesVisuais;
    procedure ConsultarTodosDadosFuncionario;
    procedure OnExecutarAposConsultarDadosFuncionario(Sender: TObject);
    procedure PopularCampos;
  public
    { Public declarations }
    constructor Create(AOwner: TComponent; Id: Integer = 0); override;
  end;

var
  FrmCadastroFuncionario: TFrmCadastroFuncionario;

implementation

uses
  Funcoes, uFormat, Loading;

{$R *.fmx}

procedure TFrmCadastroFuncionario.AjustarComponentesVisuais;
begin
  edtNome.SetFocus;
  gbSituacao.Enabled := FId > 0;
  lblTitulo.text := ifthen(Fid > 0, 'Editando funcionário: ' + Fid.ToString, 'Inserindo funcionário');
end;

procedure TFrmCadastroFuncionario.ConsultarTodosDadosFuncionario;
begin
  TLoading.Show('Carregando informações do funcionário...', FrmCadastroFuncionario);
  FuncionarioController.RecuperarFuncionario(FId, OnExecutarAposConsultarDadosFuncionario);
end;

constructor TFrmCadastroFuncionario.Create(AOwner: TComponent; Id: Integer = 0);
begin
  inherited Create(AOwner, Id);
  FuncionarioController := TFuncionarioController.Create;
  if FId > 0 then
    ConsultarTodosDadosFuncionario;
end;

procedure TFrmCadastroFuncionario.EditCpfChange(Sender: TObject);
begin
  inherited;
  Formatar(Sender, TFormato.CPF);
end;

procedure TFrmCadastroFuncionario.EditCpfEnter(Sender: TObject);
begin
  inherited;
  ResetFormat(Sender);
end;

procedure TFrmCadastroFuncionario.FormClose(Sender: TObject;
  var Action: TCloseAction);
begin
  inherited;
  FuncionarioController.Free;
  Action := TCloseAction.caFree;
  FrmCadastroFuncionario := nil;
end;

procedure TFrmCadastroFuncionario.FormShow(Sender: TObject);
begin
  inherited;
  AjustarComponentesVisuais;
end;

procedure TFrmCadastroFuncionario.imageSalvarClick(Sender: TObject);
begin
  inherited;
  if not ValidarCampos() then
    Exit;

  TLoading.Show('Inserindo/Atualizando funcionário...', FrmCadastroFuncionario);

  if FId = 0 then
  begin
    var funcionarioDto: TCreateFuncionarioDto;
    funcionarioDto := TCreateFuncionarioDto.Create;
    with funcionarioDto do
    begin
      Nome := edtNome.Text;
      Cpf  := ApenasNumeros(EditCpf.Text);
    end;
    FuncionarioController.AdicionarFuncionario(funcionarioDto, OnExecutarAposExecucao);
  end
  else
  begin
    var funcionarioDto: TUpdateFuncionarioDto;
    funcionarioDto := TUpdateFuncionarioDto.Create;
    with funcionarioDto do
    begin
      Nome := edtNome.Text;
      Cpf  := ApenasNumeros(EditCpf.Text);
      Situacao := IfThen(rbAtivo.IsChecked, 'A', 'I')[1];
    end;
    FuncionarioController.EditarFuncionario(FId, funcionarioDto, OnExecutarAposExecucao);
  end;
end;

procedure TFrmCadastroFuncionario.OnExecutarAposConsultarDadosFuncionario(
  Sender: TObject);
begin
  TLoading.Hide;
  if Sender is TThread then
  begin
    if Assigned(TThread(Sender).FatalException) then
    begin
      ShowMessage(Exception(TThread(sender).FatalException).Message);
      Exit;
    end;
  end;

  PopularCampos;
end;

procedure TFrmCadastroFuncionario.OnExecutarAposExecucao(Sender: TObject);
begin
  TLoading.Hide;
  if Sender is TThread then
  begin
    if Assigned(TThread(Sender).FatalException) then
    begin
      ShowMessage(Exception(TThread(sender).FatalException).Message);
      Exit;
    end;
  end;

  if Assigned(OnExecutarAposInserirOuAtualizar) then
    OnExecutarAposInserirOuAtualizar(Sender);

  ShowMessage('Funcionário Inserido/Atualizado com sucesso.');
  Close;
end;

procedure TFrmCadastroFuncionario.PopularCampos;
begin
  with FuncionarioController.funcionario do
  begin
    edtNome.Text := Nome;
    EditCpf.Text := FormatarCPF(Cpf);
    DateEditAlteracao.Date     := DataAlteracao;
    timeEditHoraAlteracao.Time := DataAlteracao;
    if Situacao = 'A' then
      rbAtivo.IsChecked   := True
    else
      rbInativo.IsChecked := True;
  end;
end;

function TFrmCadastroFuncionario.ValidarCampos: Boolean;
begin
  Result := False;

  if edtNome.Text = EmptyStr then
  begin
    ShowMessage('Nome deve ser preenchido!');
    edtNome.SetFocus;
    Exit;
  end;

  if not ValidarCPF(ApenasNumeros(EditCpf.Text)) then
  begin
    ShowMessage('Cpf inválido.');
    EditCpf.SetFocus;
    Exit;
  end;

  Result := True;
end;

end.
