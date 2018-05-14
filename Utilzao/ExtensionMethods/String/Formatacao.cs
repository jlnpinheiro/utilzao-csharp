using System.Globalization;
using System.Text;

namespace JNogueira.Infraestrutura.Utilzao
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Remove todos os caracteres acentuados de uma string
        /// </summary>
        /// <param name="input">String onde os acentos serão removidos</param>
        public static string RemoverAcentuacao(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            var normalizedString = input.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
