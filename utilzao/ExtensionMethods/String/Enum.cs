using JNogueira.Utilzao.Atributos;
using System;
using System.Reflection;

namespace JNogueira.Utilzao
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Obtém a descrição de um elemento de um enum a partir do atributo <see cref="EnumDescricaoAttribute"/>
        /// </summary>
        /// <param name="input">Enum sobre a qual será obtida a descrição do item</param>
        /// <returns>Descrição do item da Enum</returns>
        public static string ObterDescricao(this Enum input)
        {
            FieldInfo fieldInfo = input?.GetType().GetField(input.ToString());

            if (input == null || fieldInfo == null)
            {
                return null;
            }

            var attributes = fieldInfo.GetCustomAttributes(typeof(EnumDescricaoAttribute), false);

            return attributes.Length > 0 ? ((EnumDescricaoAttribute)attributes[0]).Descricao : input.ToString();
        }

        /// <summary>
        /// Obtém o valor de um elemento de um enum a partir do atributo <see cref="EnumValorAttribute"/>
        /// </summary>
        /// <param name="input">Enum sobre a qual será obtido o valor do item</param>
        /// <returns>Valor do item da Enum</returns>
        public static TValor ObterValor<TValor>(this Enum input)
        {
            FieldInfo fieldInfo = input?.GetType().GetField(input.ToString());

            if (input == null || fieldInfo == null)
            {
                return default;
            }

            var attributes = fieldInfo.GetCustomAttributes(typeof(EnumValorAttribute), false);

            return attributes.Length > 0
                ? Convert.ChangeType(((EnumValorAttribute)attributes[0]).Valor, typeof(TValor)) is TValor valor
                    ? valor
                    : default
                : default;
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
