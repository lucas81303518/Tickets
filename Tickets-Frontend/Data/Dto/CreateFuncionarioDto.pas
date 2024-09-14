unit CreateFuncionarioDto;

interface

type
  TCreateFuncionarioDto = class
    private
      FNome: string;
      FCpf: string;
    public
      property Nome: string read FNome write FNome;
      property Cpf: string read FCpf write FCpf;
  end;

implementation

end.
