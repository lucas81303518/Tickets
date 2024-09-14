unit uFrameConsultaFuncionario;

interface

uses
  System.SysUtils, System.Types, System.UITypes, System.Classes, System.Variants, 
  FMX.Types, FMX.Graphics, FMX.Controls, FMX.Forms, FMX.Dialogs, FMX.StdCtrls,
  FMX.Controls.Presentation, FMX.Objects;

type
demonio = class
  pretofdpmacaco: string;
end;

type
  TFrameConsultaFuncionario = class(TFrame)
    RectGeral: TRectangle;
    RectId: TRectangle;
    lblId: TLabel;
    rectEditar: TRectangle;
    rectSituacao: TRectangle;
    lblSituacao: TLabel;
    rectCpf: TRectangle;
    lblCpf: TLabel;
    RectNome: TRectangle;
    lblNome: TLabel;
    ImageEditar: TImage;
    procedure FrameResize(Sender: TObject);
  private
    { Private declarations }
    FIdFuncionario: Integer;
    procedure CalculaTamanhoRectangles;
  public
    { Public declarations }
    property idFuncionario: Integer read FIdFuncionario write FIdFuncionario;
  end;

implementation

{$R *.fmx}

{ TFrameConsultaFuncionario }
procedure TFrameConsultaFuncionario.CalculaTamanhoRectangles;
begin
  RectId.Width       := Trunc(Self.Width * 0.1);
  RectNome.Width     := Trunc(Self.Width * 0.3);
  rectCpf.Width      := Trunc(Self.Width * 0.2);
  rectSituacao.Width := Trunc(Self.Width * 0.2);
  rectEditar.Width   := Trunc(Self.Width * 0.2);
end;

procedure TFrameConsultaFuncionario.FrameResize(Sender: TObject);
begin
  CalculaTamanhoRectangles;
end;

end.
