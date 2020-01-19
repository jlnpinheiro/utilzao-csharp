namespace JNogueira.Utilzao
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Valida se uma determinada string é o número válido de um CNPJ
        /// </summary>
        /// <param name="cnpj">Número do CNPJ que será validado.</param>
        public static bool ValidarCnpj(this string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj)) return false;

            cnpj = cnpj.ExtrairNumeros().Trim();

            if (cnpj.Length != 14)
                return false;

            var multiplicador1 = new[] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            var tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;
            digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            resto = resto < 2 ? 0 : 11 - resto;
            digito += resto;
            return cnpj.EndsWith(digito);
        }

        /// <summary>
        /// Valida se uma determinada string é o número válido de um CPF
        /// </summary>
        /// <param name="cpf">Número do CPF que será validado.</param>
        public static bool ValidarCpf(this string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            cpf = cpf.ExtrairNumeros().Trim();

            if (cpf.Length != 11)
                return false;

            var multiplicador1 = new[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var tempCpf = cpf.Substring(0, 9);
            var soma = 0;

            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            var resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            var digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto;
            return cpf.EndsWith(digito);
        }

        /// <summary>
        /// Valida se uma determinada string é um número válido de CPF ou CNPJ
        /// </summary>
        /// <param name="input">Número do CPF ou CNPJ que será validado.</param>
        public static bool ValidarCpfCnpj(this string input)
        {
            return !string.IsNullOrEmpty(input)
                   && (input.RemoverCaracter(".", ",", "-", "/", "(", ")").Trim().Length == 11
                       ? input.ValidarCpf()
                       : input.ValidarCnpj());
        }
    }
}
