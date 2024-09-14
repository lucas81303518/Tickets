unit UF_ConsultaBase;

interface

uses
  System.SysUtils, System.Types, System.UITypes, System.Classes, System.Variants,
  FMX.Types, FMX.Controls, FMX.Forms, FMX.Graphics, FMX.Dialogs,
  FMX.Controls.Presentation, FMX.StdCtrls, FMX.Objects, FMX.Layouts, FMX.ListBox;

type
  TExecutarAposInserirOuAtualizar = procedure(Sender: TObject) of object;

  TFrmConsultaBase = class(TForm)
    RectHeader: TRectangle;
    lblTitulo: TLabel;
    lbRegistros: TListBox;
    rectIncluir: TRectangle;
    ImageIncluir: TImage;
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  FrmConsultaBase: TFrmConsultaBase;

implementation

{$R *.fmx}

end.