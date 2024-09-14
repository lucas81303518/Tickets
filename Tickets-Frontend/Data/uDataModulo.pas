unit uDataModulo;

interface

uses
  System.SysUtils, System.Classes, REST.Types, REST.Client,
  Data.Bind.Components, Data.Bind.ObjectScope, System.JSON,
  System.SyncObjs;

type
  TExecutarAposValidarConexaoApi = procedure of object;

  TConfigREST = class
  private
    FCriticalSection: TCriticalSection;
    procedure ResetRequest;
  public
    constructor Create();
    destructor Destroy;
    function Get(const AEndpoint: string): TJSONValue; overload;
    function Get(const AEndpoint, AParametro: string): TJSONObject; overload;
    function Post(const AEndpoint: string; const ABody: TJSONObject): TRESTResponse;
    function Put(const AEndpoint: string; const ABody: TJSONObject): TRESTResponse;
    procedure ValidaConexaoAPI();
  end;

  TDataModulo = class(TDataModule)
    FClient: TRESTClient;
    FRequest: TRESTRequest;
    FResponse: TRESTResponse;
    procedure DataModuleCreate(Sender: TObject);
    procedure DataModuleDestroy(Sender: TObject);
  private
    { Private declarations }
    FRestConfig: TConfigREST;
  public
    { Public declarations }
    property RestConfig: TConfigREST read FRestConfig;
  end;

var
  DataModulo: TDataModulo;

implementation

uses
  ThreadingEx, System.Threading;

{%CLASSGROUP 'FMX.Controls.TControl'}

{$R *.dfm}

{ TConfigREST }

constructor TConfigREST.Create();
begin
  inherited Create;
  FCriticalSection := TCriticalSection.Create;
end;

destructor TConfigREST.Destroy;
begin
  inherited Destroy;
  FCriticalSection.Free;
end;

function TConfigREST.Get(const AEndpoint, AParametro: string): TJSONObject;
begin
  try
    FCriticalSection.Enter;
    with DataModulo do
    begin
      FRequest.Resource := AEndpoint + '/' + AParametro ;
      FRequest.Method := rmGET;
      FRequest.Params.Clear;
      FRequest.Execute;
      if FResponse.StatusCode <> 200 then
        raise Exception.Create(FResponse.Content);
      Result := TJSONObject.ParseJSONValue(FResponse.Content) as TJSONObject;
    end;
  finally
    ResetRequest;
    FCriticalSection.Leave;
  end;
end;

function TConfigREST.Get(const AEndpoint: string): TJSONValue;
begin
  FCriticalSection.Enter;
  try
    with DataModulo do
    begin
      FRequest.Resource := AEndpoint;
      FRequest.Method := rmGET;
      FRequest.Params.Clear;
      FRequest.Execute;
      if FResponse.StatusCode <> 200 then
        raise Exception.Create(FResponse.Content);
      Result := TJSONObject.ParseJSONValue(FResponse.Content);
    end;
  finally
    ResetRequest;
    FCriticalSection.Leave;
  end;
end;

function TConfigREST.Post(const AEndpoint: string; const ABody: TJSONObject): TRESTResponse;
begin
  FCriticalSection.Enter;
  try
    with DataModulo do
    begin
      FRequest.Resource := AEndpoint;
      FRequest.Method := rmPOST;
      FRequest.Body.ClearBody;
      FRequest.Params.Clear;
      FRequest.Body.Add(ABody.ToString, TRESTContentType.ctAPPLICATION_JSON);
      FRequest.Execute;
      if FResponse.StatusCode <> 200 then
        raise Exception.Create(FResponse.Content);
      Result := FResponse;
    end;
  finally
    ResetRequest;
    FCriticalSection.Leave;
  end;
end;

function TConfigREST.Put(const AEndpoint: string;
  const ABody: TJSONObject): TRESTResponse;
begin
  FCriticalSection.Enter;
  try
    with DataModulo do
    begin
      FRequest.Resource := AEndpoint;
      FRequest.Method := rmPUT;
      FRequest.Body.ClearBody;
      FRequest.Params.Clear;
      FRequest.Body.Add(ABody.ToString, TRESTContentType.ctAPPLICATION_JSON);
      FRequest.Execute;
      if FResponse.StatusCode <> 204 then
        raise Exception.Create(FResponse.Content);
      Result := FResponse;
    end;
  finally
    ResetRequest;
    FCriticalSection.Leave;
  end;
end;

procedure TConfigREST.ResetRequest;
begin
  with DataModulo do
  begin
    FRequest.Params.Clear;
    FRequest.Body.ClearBody;
    FRequest.Method := rmGET;
    FRequest.Resource := '';
  end;
end;

procedure TConfigREST.ValidaConexaoAPI();
begin
  try
    Get('testeConexao');
  except on Ex: Exception do
    begin
      Ex.Message := 'Erro com a conex�o da API...' + ex.Message;
      raise;
    end;
  end;
end;

procedure TDataModulo.DataModuleCreate(Sender: TObject);
begin
  FRestConfig := TConfigREST.Create;
end;

procedure TDataModulo.DataModuleDestroy(Sender: TObject);
begin
  FRestConfig.Destroy;
end;


end.