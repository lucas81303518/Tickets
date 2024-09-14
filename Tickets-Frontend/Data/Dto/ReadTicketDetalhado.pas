unit ReadTicketDetalhado;

interface

uses
  FuncionarioModel, System.Generics.Collections, ReadTicket;

type
  TReadTicketDetalhado = class
  private
    FFuncionario: TFuncionarioModel;
    FTotalTicketsPorFuncionario: Integer;
    FTickets: TObjectList<TReadTicket>;
  public
    property Funcionario: TFuncionarioModel read FFuncionario write FFuncionario;
    property TotalTicketsPorFuncionario: Integer read FTotalTicketsPorFuncionario write FTotalTicketsPorFuncionario;
    property Tickets: TObjectList<TReadTicket> read FTickets write FTickets;
  end;


implementation

end.
