unit ReadTicket;

interface

type
  TReadTicket = class
  private
    FId: Integer;
    FQuantidade: Integer;
    FSituacao: Char;
    FDataEntrega: TDateTime;
  public
    property Id: Integer read FId write FId;
    property Quantidade: Integer read FQuantidade write FQuantidade;
    property Situacao: Char read FSituacao write FSituacao;
    property DataEntrega: TDateTime read FDataEntrega write FDataEntrega;
  end;


implementation

end.
