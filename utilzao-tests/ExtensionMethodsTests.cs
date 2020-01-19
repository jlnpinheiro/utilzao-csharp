using JNogueira.Utilzao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Utilzao.Tests
{
    public enum EnumTeste
    {
        Valor1 = 1,
        Valor2 = 2,
        Valor3 = 3
    }

    [TestClass]
    [TestCategory("ExtensionMethods-String")]
    public class ExtensionMethodsTests
    {
        [TestMethod]
        public void Deve_Formatar_Uma_String_Pelo_Padrao()
        {
            Assert.IsTrue("123.456-789" == "123456789".Formatar("###.###-###"));
        }

        [TestMethod]
        public void Deve_Nao_Formatar_Uma_String_Pelo_Padrao()
        {
            Assert.IsFalse("123.456-789" == "1234567".Formatar("###.###-###"));
        }

        [TestMethod]
        public void Deve_Formatar_Um_Cpf()
        {
            Assert.IsTrue("425.802.840-10" == "42580284010".FormatarCpf());
        }

        [TestMethod]
        public void Deve_Formatar_Um_Cnpj()
        {
            Assert.IsTrue("84.552.945/0001-06" == "84552945000106".FormatarCnpj());
        }

        [TestMethod]
        public void Deve_Formatar_Um_Cpf_Ou_Cnpj()
        {
            Assert.IsTrue("84.552.945/0001-06" == "84552945000106".FormatarCpfCnpj());
        }

        [TestMethod]
        public void Deve_Remover_Caracteres()
        {
            Assert.IsTrue("84552945000106" == "84.552.945/0001-06".RemoverCaracter(".", "/", "-"));
        }

        [TestMethod]
        public void Deve_Extrair_Numeros()
        {
            Assert.IsTrue("84552945000106" == "84.552.945/0001-06".ExtrairNumeros());
        }

        [TestMethod]
        public void Deve_Validar_Cpf()
        {
            Assert.IsTrue("425.802.840-10".ValidarCpf());
        }

        [TestMethod]
        public void Deve_Validar_Cpf_Invalido()
        {
            Assert.IsFalse("425.802.840-11".ValidarCpf());
        }

        [TestMethod]
        public void Deve_Validar_Cnpj()
        {
            Assert.IsTrue("84.552.945/0001-06".ValidarCnpj());
        }

        [TestMethod]
        public void Deve_Validar_Cnpj_Invalido()
        {
            Assert.IsFalse("84.552.945/0001-07".ValidarCnpj());
        }

        [TestMethod]
        public void Deve_Validar_Cpf_Ou_Cnpj()
        {
            Assert.IsTrue("84.552.945/0001-06".ValidarCpfCnpj());
        }

        [TestMethod]
        public void Deve_Validar_Cpf_Ou_Cnpj_Invalido()
        {
            Assert.IsFalse("84.552.945/0001-02".ValidarCpfCnpj());
        }

        [TestMethod]
        public void Deve_Obter_Nome_Uf_Por_Sigla()
        {
            Assert.IsTrue("São Paulo" == "SP".ObterNomeUfPorSiglaUf());
        }

        [TestMethod]
        public void Deve_Remover_Acentuacao()
        {
            Assert.IsTrue("Aoce" == "Ãóçê".RemoverAcentuacao());
        }

        [TestMethod]
        public void Deve_Criptografar_Uma_String()
        {
            var chave = "Essa é a minha chave secreta";

            Assert.IsTrue(chave != chave.Criptografar());
        }

        [TestMethod]
        public void Deve_Descriptografar_Uma_String()
        {
            Assert.IsTrue("Essa é a minha chave secreta" == "rmtE8KPZNPIDH4SzUj6MtFLpdM2LMegEybHdTEP5ahI=".Descriptografar());
        }

        [TestMethod]
        public void Deve_Converter_Data_Para_Formato()
        {
            var data = "13/05/2018 23:12:55".ConverterDataPorFormato("dd/MM/yyyy HH:mm:ss");

            Assert.IsTrue(data.HasValue && data.Value == new DateTime(2018, 5, 13, 23, 12, 55));
        }

        [TestMethod]
        public void Deve_Nao_Converter_Data_Para_Formato()
        {
            var data = "13/05/2018".ConverterDataPorFormato("dd/MM/yyyy HH:mm:ss");

            Assert.IsTrue(data == null);
        }

    [TestClass]
    [TestCategory("ExtensionMethods-Int")]
    public class ExtensionMethodsIntTests
    {
        [TestMethod]
        public void Deve_Converter_Para_Um_Enum()
        {
            var enumTeste = 1.ConverterParaEnum<EnumTeste>();

            Assert.IsTrue(enumTeste == EnumTeste.Valor1);
        }

        [TestMethod]
        public void Deve_Converter_Para_Um_Enum_Default()
        {
            var enumTeste = 5.ConverterParaEnum<EnumTeste>(null);

            Assert.IsTrue(enumTeste == null);
        }

        [TestMethod]
        public void Deve_Realizar_Um_Substring_Com_Tamanho_Menor_Input()
        {
            var input = "Hello World!";

            Assert.IsTrue(input.SubstringSafe(0, 5) == "Hello");
        }

        [TestMethod]
        public void Deve_Realizar_Um_Substring_Com_Tamanho_Maior_Input()
        {
            var input = "Hello World!";

            Assert.IsTrue(input.SubstringSafe(6, 1000) == "World!");
        }
    }
}
