unit CreateTicket;

interface

uses
  System.Generics.Collections, System.JSON;

type
  TCreateTicket = class
    private
      FQuantidade: Integer;
      FidFuncionarios: TList<Integer>;
    public
      property Quantidade: Integer read FQuantidade write FQuantidade;
      property idFuncionarios: TList<Integer> read FidFuncionarios write FidFuncionarios;
      function ToJson: TJSONObject;
      constructor Create();
  end;

implementation

constructor TCreateTicket.Create();
begin
  inherited Create;
  FidFuncionarios := TList<integer>.create;
end;

function TCreateTicket.ToJson: TJSONObject;
var
  JsonObj: TJSONObject;
  JsonArray: TJSONArray;
  IdFuncionario: Integer;
begin
  JsonObj := TJSONObject.Create;
  JsonArray := TJSONArray.Create;
  try
    JsonObj.AddPair('quantidade', TJSONNumber.Create(FQuantidade));
    for IdFuncionario in FIdFuncionarios do
      JsonArray.Add(IdFuncionario);
    JsonObj.AddPair('idFuncionarios', JsonArray);
    Result := JsonObj;
  except
    JsonObj.Free;
    raise;
  end;
end;

end.