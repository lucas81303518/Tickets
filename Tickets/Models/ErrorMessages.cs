namespace Tickets.Models
{
    public static class ErrorMessages
    {
        public enum ErrorCode
        {
            FuncionarioNaoEncontrado,
            ErroAoSalvar,
            CpfJaExiste,
            OperacaoBemSucedida,
            CpfNuloOuVazio,            
            CpfContemCaracteresInvalidos,
            CpfDigitosIguais,
            CpfInvalido,
            TicketNaoEncontrado,
            ErroAoConsultar
        }

        private static readonly Dictionary<ErrorCode, string> _messages = new Dictionary<ErrorCode, string>
        {
            { ErrorCode.FuncionarioNaoEncontrado, "Funcionário não encontrado." },
            { ErrorCode.ErroAoSalvar, "Erro ao salvar as alterações." },
            { ErrorCode.CpfJaExiste, "CPF já existe no sistema." },
            { ErrorCode.OperacaoBemSucedida, "Operação realizada com sucesso." },
            { ErrorCode.CpfNuloOuVazio, "O CPF não pode ser nulo ou vazio." },            
            { ErrorCode.CpfContemCaracteresInvalidos, "O CPF deve conter apenas números e não pode ter caracteres especiais." },
            { ErrorCode.CpfDigitosIguais, "O CPF não pode conter todos os dígitos iguais." },
            { ErrorCode.CpfInvalido, "O CPF informado é inválido." },
            { ErrorCode.TicketNaoEncontrado, "Ticket não encontrado."},
            { ErrorCode.ErroAoConsultar, "Erro ao consultar dados." }
        };

        public static string GetErrorMessage(ErrorCode errorCode)
        {
            return _messages.ContainsKey(errorCode) ? _messages[errorCode] : "Erro desconhecido.";
        }
    }
}
