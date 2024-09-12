using System.Text.RegularExpressions;
using Tickets.Data.DTO;
using static Tickets.Models.ErrorMessages;

namespace Tickets.Models.ValidationsAttribute
{
    public static class CpfValidator
    {
        public static ResponseClient<object> Validate(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return new ResponseClient<object>(false, ErrorCode.CpfNuloOuVazio);
            }            

            if (!cpf.All(char.IsDigit))
            {
                return new ResponseClient<object>(false, ErrorCode.CpfContemCaracteresInvalidos);
            }

            if (new string(cpf[0], 11) == cpf)
            {
                return new ResponseClient<object>(false, ErrorCode.CpfDigitosIguais);
            }

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
            {
                int.TryParse(tempCpf[i].ToString(), out int digit);
                soma += digit * multiplicador1[i];
            }

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;

            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                int.TryParse(tempCpf[i].ToString(), out int digit);
                soma += digit * multiplicador2[i];
            }

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            if (!cpf.EndsWith(digito))
            {
                return new ResponseClient<object>(false, ErrorCode.CpfInvalido);
            }

            return new ResponseClient<object>(true, ErrorCode.OperacaoBemSucedida);
        }

    }
}
