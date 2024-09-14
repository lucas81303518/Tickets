unit TicketModel;

interface

uses
  FuncionarioModel;

type
  TTicketModel = class
    private
    FId: Integer;
    FQuantidade: Integer;
    FSituacao: Char;
    FDataEntrega: TDateTime;
    FFuncionario: TFuncionarioModel;
  public
    property Id: Integer read FId write FId;
    property Quantidade: Integer read FQuantidade write FQuantidade;
    property Situacao: Char read FSituacao write FSituacao;
    property DataEntrega: TDateTime read FDataEntrega write FDataEntrega;
    property Funcionario: TFuncionarioModel read FFuncionario write FFuncionario;
  end;

implementation

end.
