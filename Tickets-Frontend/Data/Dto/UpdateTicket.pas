unit UpdateTicket;

interface

type
  TUpdateTicket = class
    private
      FQuantidade: Integer;
      FFuncionarioId: Integer;
      FSituacao: char;
    public
      property Quantidade: Integer read FQuantidade write FQuantidade;
      property FuncionarioId: Integer read FFuncionarioId write FFuncionarioId;
      property Situacao: char read FSituacao write FSituacao;
  end;

implementation

end.
