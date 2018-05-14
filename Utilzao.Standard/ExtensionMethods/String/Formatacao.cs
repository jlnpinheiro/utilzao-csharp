using System.Linq;

namespace JNogueira.Infraestrutura.Utilzao
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Formata uma string a partir de um padrão
        /// </summary>
        /// <param name="input">String de input.</param>
        /// <param name="padrao">Padrão utilizado para a formatação (exemplo de máscara para CPF: ###.###.###-##).</param>
        public static string Formatar(this string input, string padrao)
        {
            if (string.IsNullOrEmpty(input)) return input;

            var output = string.Empty;
            var index = 0;

            foreach (var m in padrao)
            {
                if (m == '#')
                {
                    if (index >= input.Length) continue;
                    output += input[index];
                    index++;
                }
                else
                {
                    output += m;
                }
            }
            return output;
        }

        /// <summary>
        /// Formata uma string aplicando a máscara para CPF
        /// </summary>
        /// <param name="input">String onde a máscara será aplicada.</param>
        public static string FormatarCpf(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            input = input.ExtrairNumeros().Trim();

            return input.Length < 11 ? input : Formatar(input, "###.###.###-##");
        }

        /// <summary>
        /// Formata uma string aplicando a máscara para CNPJ
        /// </summary>
        /// <param name="input">String onde a máscara será aplicada.</param>
        public static string FormatarCnpj(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            input = input.ExtrairNumeros().Trim();

            return input.Length < 14 ? input : Formatar(input, "##.###.###/####-##");
        }

        /// <summary>
        /// Formata uma string aplicando a máscara para CPF ou CNPJ a partir da quantidade de caracteres da string
        /// </summary>
        /// <param name="input">String onde a máscara será aplicada.</param>
        public static string FormatarCpfCnpj(this string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            input = input.ExtrairNumeros().Trim();

            return input.Length < 14 ? FormatarCpf(input) : FormatarCnpj(input);
        }

        /// <summary>
        /// Remove de uma string alguns caracteres desejados.
        /// </summary>
        /// <param name="input">String de onde os caracteres serão removidos.</param>
        /// <param name="caracteres"></param>
        public static string RemoverCaracter(this string input, params string[] caracteres)
        {
            if (string.IsNullOrEmpty(input)) return input;

            if (caracteres == null || caracteres.Length == 0) return input;

            foreach(string caracter in caracteres)
            {
                input = input.Replace(caracter, string.Empty);
            }

            return input;
        }

        /// <summary>
        /// Extrai somente os caracteres numéricos de uma string
        /// </summary>
        /// <param name="input">String de onde somente os caracteres numéricos serão removidos.</param>
        public static string ExtrairNumeros(this string input)
        {
            return (string.IsNullOrEmpty(input))
                ? input
                : new string(input.Where(char.IsDigit).ToArray());
        }
    }
}
