using System;

namespace JNogueira.Utilzao.Atributos
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class EnumDescricaoAttribute : Attribute
    {
        public EnumDescricaoAttribute(string descricao)
        {
            Descricao = descricao;
        }

        public string Descricao { get; }
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class EnumValorAttribute : Attribute
    {
        public EnumValorAttribute(object valor)
        {
            Valor = valor;
        }

        public object Valor { get; }
    }
}
