unit uFrameColunas;

interface

uses
  System.SysUtils, System.Types, System.UITypes, System.Classes, System.Variants, 
  FMX.Types, FMX.Graphics, FMX.Controls, FMX.Forms, FMX.Dialogs, FMX.StdCtrls,
  FMX.Objects, Generics.Collections;

type
  TColumnsFrame = class
  private

  public
    Nome: string;
    tamanhoPorcent: Single;
end;

type
  TFrameColunas = class(TFrame)
    rectGeral: TRectangle;
    procedure FrameResized(Sender: TObject);
  private
    FColunas: TObjectList<TColumnsFrame>;
    procedure CriarSubRectangles(Pai: TRectangle;
      Colunas: TObjectList<TColumnsFrame>);
    { Private declarations }
  public
    { Public declarations }
    constructor Create(AOwner: TComponent; AColumnsFrames: TObjectList<TColumnsFrame>);

  end;

implementation

{$R *.fmx}

constructor TFrameColunas.Create(AOwner: TComponent;
  AColumnsFrames: TObjectList<TColumnsFrame>);
begin
  inherited Create(AOwner);
  FColunas := AColumnsFrames;
end;

procedure TFrameColunas.CriarSubRectangles(Pai: TRectangle; Colunas: TObjectList<TColumnsFrame>);
var
  i: Integer;
  NovoRetangulo: TRectangle;
  NovoLabel: TLabel;
  PosicaoX: Single;
begin
  for i := Pai.ChildrenCount - 1 downto 0 do
  begin
    if Pai.Children[i] is TRectangle then
      Pai.Children[i].Free;
  end;
  PosicaoX := 0;
  for i := 0 to Colunas.Count - 1 do
  begin
    NovoRetangulo := TRectangle.Create(Pai);
    NovoRetangulo.Parent := Pai;
    NovoRetangulo.Stroke.Kind := TBrushKind.None;
    NovoRetangulo.Fill.Color := Pai.Fill.Color;
    NovoRetangulo.Width := Pai.Width * Colunas[i].TamanhoPorcent / 100;
    NovoRetangulo.Height := Pai.Height - 1;
    NovoRetangulo.Position.X := PosicaoX;

    NovoLabel := TLabel.Create(NovoRetangulo);
    NovoLabel.StyledSettings := [];
    NovoLabel.Parent := NovoRetangulo;
    NovoLabel.Align := TAlignLayout.Client;
    NovoLabel.Text := Colunas[i].Nome;
    NovoLabel.TextAlign := TTextAlign.Center;
    NovoLabel.VertTextAlign := TTextAlign.Center;
    NovoLabel.Font.Size := 14;
    PosicaoX := PosicaoX + NovoRetangulo.Width;
  end;
end;

procedure TFrameColunas.FrameResized(Sender: TObject);
begin
  CriarSubRectangles(rectGeral, FColunas);
end;

end.