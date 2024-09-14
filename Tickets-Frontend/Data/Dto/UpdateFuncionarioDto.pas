unit UpdateFuncionarioDto;

interface

type
  TUpdateFuncionarioDto = class
    private
      FNome: string;
      FCpf: string;
      FSituacao: char;
    public
      property Nome: string read FNome write FNome;
      property Cpf: string read FCpf write FCpf;
      property Situacao: char read FSituacao write FSituacao;
  end;

implementation

end.
