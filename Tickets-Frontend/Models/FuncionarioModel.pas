unit FuncionarioModel;

interface

type
  TFuncionarioModel = class
  private
    FId: Integer;
    FNome: string;
    FCpf: string;
    FSituacao: char;
    FDataAlteracao: TDateTime;
  public
    property Id: Integer read FId write FId;
    property Nome: string read FNome write FNome;
    property Cpf: string read FCpf write FCpf;
    property Situacao: char read FSituacao write FSituacao;
    property DataAlteracao: TDateTime read FDataAlteracao write FDataAlteracao;
  end;

implementation

end.
