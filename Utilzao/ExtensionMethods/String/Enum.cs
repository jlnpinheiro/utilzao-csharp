using System;
using System.ComponentModel;

namespace JNogueira.Utilzao
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Obtém a descrição de um elemento de um enum a partir do atributo DescriptionAttribute
        /// </summary>
        /// <param name="input">Enum sobre a qual será obtida a descrição do item</param>
        /// <returns>Descrição do item da Enum</returns>
        public static string ObterDescricao(this Enum input)
        {
            if (input == null)
                return null;

            var fieldInfo = input.GetType().GetField(input.ToString());

            if (fieldInfo == null)
                return null;

            var attributes = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? ((DescriptionAttribute)attributes[0]).Description : input.ToString();
        }

        /// <summary>
        /// Converte um valor em um elemento de um enum. Caso o valor não seja encontrado, o valor default é retornado
        /// </summary>
        /// <typeparam name="T">Tipo do enum</typeparam>
        /// <param name="input">Valor que será convertido para um elemento do enum</param>
        /// <param name="defaultValue">Valor default retornado caso o elemento não seja encontrado no enum.</param>
        public static T? ConverterParaEnum<T>(this int? input, T? defaultValue = null) where T : struct
        {
            if (!input.HasValue)
                return defaultValue;

            return ConverterParaEnum(input.ToString(), defaultValue);
        }

        /// <summary>
        /// Converte um valor em um elemento de um enum. Caso o valor não seja encontrado, o valor default é retornado
        /// </summary>
        /// <typeparam name="T">Tipo do enum</typeparam>
        /// <param name="input">Valor que será convertido para um elemento do enum</param>
        /// <param name="defaultValue">Valor default retornado caso o elemento não seja encontrado no enum.</param>
        public static T? ConverterParaEnum<T>(this int input, T? defaultValue = null) where T : struct
        {
            return ConverterParaEnum(input.ToString(), defaultValue);
        }

        /// <summary>
        /// Converte um valor em um elemento de um enum. Caso o valor não seja encontrado, o valor default é retornado
        /// </summary>
        /// <typeparam name="T">Tipo do enum</typeparam>
        /// <param name="input">Valor que será convertido para um elemento do enum</param>
        /// <param name="defaultValue">Valor default retornado caso o elemento não seja encontrado no enum.</param>
        public static T? ConverterParaEnum<T>(this string input, T? defaultValue = null) where T : struct
        {
            if (string.IsNullOrEmpty(input))
                return defaultValue;

            return Enum.TryParse(input, true, out T result) && Enum.IsDefined(typeof(T), result) ? result : defaultValue;
        }
    }
}
