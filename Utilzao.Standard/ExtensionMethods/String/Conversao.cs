using System;

namespace JNogueira.Infraestrutura.Utilzao
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Converte um valor em um elemento de um enum. Caso o valor não seja encontrado, o valor default é retornado
        /// </summary>
        /// <typeparam name="T">Tipo do enum</typeparam>
        /// <param name="input">Valor que será convertido para um elemento do enum</param>
        /// <param name="defaultValue">Valor default retornado caso o elemento não seja encontrado no enum.</param>
        public static T ConverterParaEnum<T>(this int input, T defaultValue) where T : struct
        {
            return Enum.TryParse(input.ToString(), true, out T result) && Enum.IsDefined(typeof(T), result)
                    ? result
                    : defaultValue;
        }

    }
}
