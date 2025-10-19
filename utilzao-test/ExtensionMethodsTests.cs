namespace JNogueira.Utilzao.Test;

public class ExtensionMethodsTests
{
    Guid _chave;
    
    [SetUp]
    public void SetupTest()
    {
        _chave = new Guid("8aba82e7-c209-46fc-b2b1-4f36d32a7377");
    }

    [Test]
    public void Deve_Formatar_Uma_String_Pelo_Padrao()
    {
        Assert.That("123.456-789" == "123456789".Formatar("###.###-###"));
    }

    [Test]
    public void Deve_Nao_Formatar_Uma_String_Pelo_Padrao()
    {
        Assert.That("123.456-789" != "1234567".Formatar("###.###-###"));
    }

    [Test]
    public void Deve_Formatar_Um_Cpf()
    {
        Assert.That("425.802.840-10" == "42580284010".FormatarCpf());
    }

    [Test]
    public void Deve_Formatar_Um_Cnpj()
    {
        Assert.That("84.552.945/0001-06" == "84552945000106".FormatarCnpj());
    }

    [Test]
    public void Deve_Formatar_Um_Cpf_Ou_Cnpj()
    {
        Assert.That("84.552.945/0001-06" == "84552945000106".FormatarCpfCnpj());
    }

    [Test]
    public void Deve_Remover_Caracteres()
    {
        Assert.That("84552945000106" == "84.552.945/0001-06".RemoverCaracter(".", "/", "-"));
    }

    [Test]
    public void Deve_Extrair_Numeros()
    {
        Assert.That("84552945000106" == "84.552.945/0001-06".ExtrairNumeros());
    }

    [Test]
    public void Deve_Validar_Cpf()
    {
        Assert.That("425.802.840-10".ValidarCpf());
    }

    [Test]
    public void Deve_Validar_Cpf_Invalido()
    {
        Assert.That(!"425.802.840-11".ValidarCpf());
    }

    [Test]
    public void Deve_Validar_Cnpj()
    {
        Assert.That("84.552.945/0001-06".ValidarCnpj());
    }

    [Test]
    public void Deve_Validar_Cnpj_Invalido()
    {
        Assert.That(!"84.552.945/0001-07".ValidarCnpj());
    }

    [Test]
    public void Deve_Validar_Cpf_Ou_Cnpj()
    {
        Assert.That("84.552.945/0001-06".ValidarCpfCnpj());
    }

    [Test]
    public void Deve_Validar_Cpf_Ou_Cnpj_Invalido()
    {
        Assert.That(!"84.552.945/0001-02".ValidarCpfCnpj());
    }

    [Test]
    public void Deve_Obter_Nome_Uf_Por_Sigla()
    {
        Assert.That("São Paulo" == "SP".ObterNomeUfPorSiglaUf());
    }

    [Test]
    public void Deve_Remover_Acentuacao()
    {
        Assert.That("Aoce" == "Ãóçê".RemoverAcentuacao());
    }

    [Test]
    public void Deve_Criptografar_Uma_String()
    {
        var chave = "Essa é a minha chave secreta";

        Assert.That(chave != chave.Criptografar(_chave));
    }

    [Test]
    public void Deve_Descriptografar_Uma_String()
    {
        Assert.That("Essa é a minha chave secreta" == "T5GMYervItZXmHjsNCcd0llen1nxzTTIxB6MSdfiO8bLvOJeLark5bn4qF9Q8vMAPO09CKVR4cPyoangq2C53A==".Descriptografar(_chave));
    }

    [Test]
    public void Deve_Converter_Data_Para_Formato()
    {
        var data = "13/05/2018 23:12:55".ConverterDataPorFormato("dd/MM/yyyy HH:mm:ss");

        Assert.That(data == new DateTime(2018, 5, 13, 23, 12, 55));
    }

    [Test]
    public void Deve_Converter_Data_Horario_Oficial_Brasil()
    {
        var utcNow = DateTime.Now;
        
        var dataOficialBrasil = utcNow.ConverterDataUtcHorarioOficialBrasil();

        Assert.That(DateTime.Now.Day == dataOficialBrasil.Day && DateTime.Now.Month == dataOficialBrasil.Month && DateTime.Now.Year == dataOficialBrasil.Year && DateTime.Now.Hour == dataOficialBrasil.Hour && DateTime.Now.Minute == dataOficialBrasil.Minute);
    }

    [Test]
    public void Deve_Nao_Converter_Data_Para_Formato()
    {
        var data = "13/05/2018".ConverterDataPorFormato("dd/MM/yyyy HH:mm:ss");

        Assert.That(data == null);
    }

    [Test]
    public void Deve_Obter_Descricao_Item_Enum()
    {
        Assert.That(TimeFutebol.Flamengo.ObterDescricao(), Is.EqualTo("Clube de Regatas do Flamengo"));
    }

    [Test]
    public void Deve_Converter_String_Enum()
    {
        Assert.That("Flamengo".ConverterParaEnum<TimeFutebol>(), Is.EqualTo(TimeFutebol.Flamengo));
    }

    [Test]
    public void Deve_Converter_Int_Enum()
    {
        Assert.That(1.ConverterParaEnum<TimeFutebol>(), Is.EqualTo(TimeFutebol.Vasco));
    }

    [Test]
    public void Deve_Nao_Converter_Int_Enum()
    {
        Assert.That(3.ConverterParaEnum<TimeFutebol>(), Is.Null);
    }

    [Test]
    public void Deve_Nao_Converter_String_Enum()
    {
        Assert.That("Palmeiras".ConverterParaEnum<TimeFutebol>(), Is.Null);
    }
}

public enum TimeFutebol
{
    [System.ComponentModel.Description("Clube de Regatas do Flamengo")]
    Flamengo,
    [System.ComponentModel.Description("Clube de Regatas Vasco da Gama")]
    Vasco
}
