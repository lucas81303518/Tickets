unit uFuncionarioDAO;

interface

uses
  System.Generics.Collections, FuncionarioModel, REST.Json,
  CreateFuncionarioDto, UpdateFuncionarioDto;

type
  TFuncionarioDAO = class
  private

  public
    function AdicionarFuncionario(createFuncionarioDto: TCreateFuncionarioDto): Boolean;
    function EditarFuncionario(idFuncionario: Integer; UpdateFuncionarioDto: TUpdateFuncionarioDto): Boolean;
    function RecuperarFuncionarios(SomenteAtivos: Boolean = false): TObjectList<TFuncionarioModel>;
    function RecuperarFuncionario(idFuncionario: Integer): TFuncionarioModel;
    function ExistemFuncionariosCadastrados(): Boolean;
end;

implementation

uses
  uDataModulo, System.JSON, System.Classes, System.SysUtils,
  funcoes;

{ TFuncionarioDAO }

function TFuncionarioDAO.AdicionarFuncionario(
  createFuncionarioDto: TCreateFuncionarioDto): Boolean;
var
  JSONObject: TJSONObject;
begin
  JSONObject := TJSON.ObjectToJsonObject(createFuncionarioDto);
  Result     := DataModulo.RestConfig.Post('Funcionario', JSONObject).StatusCode = 200;
end;

function TFuncionarioDAO.EditarFuncionario(idFuncionario: Integer;
  UpdateFuncionarioDto: TUpdateFuncionarioDto): Boolean;
var
  JSONObject: TJSONObject;
begin
  JSONObject := TJSON.ObjectToJsonObject(UpdateFuncionarioDto);
  Result := DataModulo.RestConfig.Put('Funcionario/' + idFuncionario.ToString, JSONObject).StatusCode = 204;
end;

function TFuncionarioDAO.ExistemFuncionariosCadastrados: Boolean;
var
  JSONObject: TJSONOBject;
begin
  JSONObject := DataModulo.RestConfig.Get('Funcionario/ExisteAlgumFuncionarioCadastrado') as TJSONObject;
  Result := TJSON.JsonToObject<TFuncionarioModel>(JSONObject) <> nil;
end;

function TFuncionarioDAO.RecuperarFuncionario(
  idFuncionario: Integer): TFuncionarioModel;
var
  JSONObject: TJSONOBject;
begin
  JSONObject := DataModulo.RestConfig.Get('Funcionario', idFuncionario.ToString);
  Result := TJSON.JsonToObject<TFuncionarioModel>(JSONObject);
end;

function TFuncionarioDAO.RecuperarFuncionarios(SomenteAtivos: Boolean = false): TObjectList<TFuncionarioModel>;
var
  JSONArray: TJSONArray;
begin
  Result := TObjectList<TFuncionarioModel>.create;

  JSONArray := DataModulo.RestConfig.Get('Funcionario?SomenteAtivos=' + ifThen(SomenteAtivos, 'True', 'False')) as TJSONArray;
  if (JSONArray <> nil) and (JSONArray.Count > 0) then
  begin
    for var JSONValue: TJSONValue in JSONArray do
    begin
      var JSONItem := TJSONObject(JSONValue);
      Result.Add(TJSON.JsonToObject<TFuncionarioModel>(JSONItem));
    end;
  end;
end;

end.
