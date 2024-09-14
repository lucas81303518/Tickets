unit uFrameConsultaTickets;

interface

uses
  System.SysUtils, System.Types, System.UITypes, System.Classes, System.Variants, 
  FMX.Types, FMX.Graphics, FMX.Controls, FMX.Forms, FMX.Dialogs, FMX.StdCtrls,
  FMX.Objects, FMX.Controls.Presentation, TicketModel;

type
  TFrameConsultaTickets = class(TFrame)
    RectGeral: TRectangle;
    RectId: TRectangle;
    lblId: TLabel;
    rectEditar: TRectangle;
    ImageEditar: TImage;
    rectSituacao: TRectangle;
    lblSituacao: TLabel;
    RectQuantidade: TRectangle;
    lblQuantidade: TLabel;
    rectFuncionario: TRectangle;
    lblFuncionario: TLabel;
    procedure FrameResized(Sender: TObject);
  private
    { Private declarations }
    FTicket: TTicketModel;
    procedure CalculaTamanhoRectangles;
  public
    { Public declarations }
    property Ticket: TTicketModel read FTicket write FTicket;
  end;

implementation

{$R *.fmx}

{ TFrameConsultaTickets }

procedure TFrameConsultaTickets.CalculaTamanhoRectangles;
begin
  RectId.Width          := Trunc(Self.Width * 0.1);
  RectQuantidade.Width  := Trunc(Self.Width * 0.2);
  rectSituacao.Width    := Trunc(Self.Width * 0.2);
  rectFuncionario.Width := Trunc(Self.Width * 0.3);
  rectEditar.Width      := Trunc(Self.Width * 0.2);
end;

procedure TFrameConsultaTickets.FrameResized(Sender: TObject);
begin
  CalculaTamanhoRectangles;
end;

end.