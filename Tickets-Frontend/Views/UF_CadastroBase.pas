unit UF_CadastroBase;

interface

uses
  System.SysUtils, System.Types, System.UITypes, System.Classes, System.Variants,
  FMX.Types, FMX.Controls, FMX.Forms, FMX.Graphics, FMX.Dialogs,
  FMX.Controls.Presentation, FMX.StdCtrls, FMX.Objects, UF_ConsultaBase;

type
  TFrmCadastroBase = class(TForm)
    RectHeader: TRectangle;
    lblTitulo: TLabel;
    rectSalvar: TRectangle;
    RectFechar: TRectangle;
    ImageCancelar: TImage;
    imageSalvar: TImage;
    procedure RectFecharClick(Sender: TObject);
  private
    { Private declarations }
    FExecutarAposInserirOuAtualizar: TExecutarAposInserirOuAtualizar;
  protected
    { Protected declarations }
    FId: Integer;
  public
    { Public declarations }
    property OnExecutarAposInserirOuAtualizar: TExecutarAposInserirOuAtualizar read FExecutarAposInserirOuAtualizar write FExecutarAposInserirOuAtualizar;
    constructor Create(AOwner: TComponent; Id: Integer = 0); virtual;
  end;

var
  FrmCadastroBase: TFrmCadastroBase;

implementation

{$R *.fmx}

{ TFrmCadastroBase }

constructor TFrmCadastroBase.Create(AOwner: TComponent; Id: Integer = 0);
begin
  inherited Create(AOwner);
  FId := Id;
end;

procedure TFrmCadastroBase.RectFecharClick(Sender: TObject);
begin
  Close;
end;

end.