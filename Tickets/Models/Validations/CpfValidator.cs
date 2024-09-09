using System.Text.RegularExpressions;

namespace Tickets.Models.ValidationsAttribute
{
    public static class CpfValidator
    {
        public static bool IsValid(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                throw new ArgumentNullException(nameof(cpf), "O CPF não pode ser nulo ou vazio.");

            if (cpf.Length != 11)
                throw new ArgumentException("O CPF deve ter exatamente 11 dígitos.");

            if (!cpf.All(char.IsDigit))
                throw new ArgumentException("O CPF deve conter apenas números e não pode ter caracteres especiais.");     

            if (new string(cpf[0], 11) == cpf)
                throw new ArgumentException("O CPF não pode conter todos os dígitos iguais.", nameof(cpf));

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
                throw new ArgumentException("O CPF informado é inválido.", nameof(cpf));

            return true;
        }

    }

}
