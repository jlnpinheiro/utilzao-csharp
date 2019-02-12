namespace JNogueira.Infraestrutura.Utilzao
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Obtém o nome por extenso de um estado brasileiro a partir da sua sigla
        /// </summary>
        /// <param name="sigla">Sigla do estado brasileiro (por exemplo, ES, SP, RJ, etc).</param>
        /// <returns>Nome por extenso do estado</returns>
        public static string ObterNomeUfPorSiglaUf(this string sigla)
        {
            if (string.IsNullOrEmpty(sigla))
                return string.Empty;

            switch (sigla.Trim().ToUpper())
            {
                case "AC":
                    return "Acre";
                case "AL":
                    return "Alagoas";
                case "AP":
                    return "Amapá";
                case "AM":
                    return "Amazonas";
                case "BA":
                    return "Bahia";
                case "CE":
                    return "Ceará";
                case "DF":
                    return "Distrito Federal";
                case "ES":
                    return "Espírito Santo";
                case "GO":
                    return "Goiás";
                case "MA":
                    return "Maranhão";
                case "MT":
                    return "Mato Grosso";
                case "MS":
                    return "Mato Grosso do Sul";
                case "MG":
                    return "Minas Gerais";
                case "PA":
                    return "Pará";
                case "PB":
                    return "Paraíba";
                case "PR":
                    return "Paraná";
                case "PE":
                    return "Pernambuco";
                case "PI":
                    return "Piauí";
                case "RJ":
                    return "Rio de Janeiro";
                case "RN":
                    return "Rio Grande do Norte";
                case "RS":
                    return "Rio Grande do Sul";
                case "RO":
                    return "Rondônia";
                case "RR":
                    return "Roraima";
                case "SC":
                    return "Santa Catarina";
                case "SP":
                    return "São Paulo";
                case "SE":
                    return "Sergipe";
                case "TO":
                    return "Tocantins";
                case "EX":
                    return "Exterior";
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Realiza um "Substring" mesmo que o tamanho informado seja maior que o tamanho do input
        /// </summary>
        /// <param name="input">Input onde será realizado o "Substring"</param>
        /// <param name="startIndex">Index onde o substring deverá ser realizado</param>
        /// <param name="tamanho">Quantidade de caracteres que deverá ser retornado. Caso seja maior que o tamanho do input, a quantidade máxima após o start index será retornado.</param>
        /// <returns>Uma nova string</returns>
        public static string SubstringSafe(this string input, int startIndex, int tamanho)
        {
            if (string.IsNullOrEmpty(input)) return input;

            return input.Length <= startIndex
                ? input
                : input.Length - startIndex <= tamanho
                    ? input.Substring(startIndex)
                    : input.Substring(startIndex, tamanho);
        }
    }
}
