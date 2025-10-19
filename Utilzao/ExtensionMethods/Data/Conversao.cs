using System;
using System.Globalization;
using System.Runtime.InteropServices;

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

        /// <summary>
        /// Converte uma data UTC para o horário oficial do Brasil (Brasília)
        /// </summary>
        /// <param name="dataUtc">Data UTC que deverá ser convertida</param>
        public static DateTime ConverterDataUtcHorarioOficialBrasil(this DateTime dataUtc)
        {
            if (dataUtc.Kind != DateTimeKind.Utc)
                throw new ArgumentException("A data informada não é uma data UTC.", nameof(dataUtc));

            return RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? TimeZoneInfo.ConvertTime(dataUtc, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"))
                : TimeZoneInfo.ConvertTime(dataUtc, TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"));
        }
    }
}
