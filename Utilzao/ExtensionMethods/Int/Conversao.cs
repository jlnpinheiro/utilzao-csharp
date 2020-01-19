using System;
using System.Globalization;

namespace JNogueira.Utilzao
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Converte uma string em uma data, a partir do formato exato informado
        /// </summary>
        /// <param name="input">String que deverá ser convertida para uma data.</param>
        /// <param name="formato">Formato exato utilizado para obter a data (por exemplo DDMMYYY).</param>
        public static DateTime? ConverterDataPorFormato(this string input, string formato)
        {
            if (string.IsNullOrEmpty(formato))
                return null;

            if (string.IsNullOrEmpty(input))
                return null;

            DateTime.TryParseExact(input, formato, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime dataConvertida);

            return dataConvertida == DateTime.MinValue
                ? (DateTime?)null
                : dataConvertida;
        }
    }
}
