
# Utilzão
[![NuGet](https://img.shields.io/nuget/dt/Utilzao.svg?style=flat-square)](https://www.nuget.org/packages/Utilzao) [![NuGet](https://img.shields.io/nuget/v/Utilzao.svg?style=flat-square)](https://www.nuget.org/packages/Utilzao)

Coleção de classes e métodos úteis em C# para manipulação de strings, datas, envio de e-mail, etc.

A ideia desse projeto é agrupar várias soluções e recursos "úteis" que podem ser utilizados em projetos com C#. Coisas simples como aplicar uma máscara a um string, validar um CPF, remover caracteres de uma string, etc. O objetivo é fazer um componente "utilzão" agrupando todos esses recursos úteis em um componente só!

A solução é dividida nos seguintes projetos:

* **utilzao**: Projeto utilizando .NET Standard 2.1. Informações sobre versões suportadas em https://docs.microsoft.com/pt-br/dotnet/standard/net-standard
* **utilzao-tests**: Projeto de testes.

## Classes úteis
### SmtpUtil 
Classe que permite enviar e-mail via SMTP.
```csharp
// Configuração do SmtpClient para utilização do G-mail
var smtpGmail = new SmtpClient
{
    Host                  = "smtp.gmail.com",
    Port                  = 587,
    EnableSsl             = true,
    DeliveryMethod        = SmtpDeliveryMethod.Network,
    UseDefaultCredentials = true,
    Credentials           = new NetworkCredential("seu_email@gmail.com", "sua_senha")
};

// Informando um anexo (por exemplo, um arquivo TXT)
using MemoryStream memoryStream = new();
byte[] contentAsBytes = Encoding.UTF8.GetBytes("Olá, sou um anexo!");
memoryStream.Write(contentAsBytes, 0, contentAsBytes.Length);
memoryStream.Seek(0, SeekOrigin.Begin);

var contentType = new ContentType
{
    MediaType = MediaTypeNames.Text.Plain,
    Name = "AnexoEmail.txt"
};

var anexo = new Attachment(memoryStream, contentType);

var email = new SmtpUtil("emailRemetente@seudominio.com", new[] { "email_destinatario_1@seudominio.com" }, "<b>Você recebeu uma mensagem.</b>", smtpGmail)
{
    Anexos = new List<Attachment> { anexo },
    EmailsDestinatariosEmCopia = new[] { "email_destinatario_2@seudominio.com", "email_destinatario_3@seudominio.com" },
    NomeRemetente = "Utilzão",
    Assunto = "Você recebeu um e-mail.",
    MensagemEmHtml = true
};

// Enviando a mensagem
email.Enviar();
```

## Extension methods
Coleção de [extension methods](https://docs.microsoft.com/pt-br/dotnet/csharp/programming-guide/classes-and-structs/extension-methods), com algumas funcionalidades e facilitades úteis que utilizo com frequência:

### Conversões
**ConverterParaEnum** - Converte um valor em um elemento de um enum. Caso o valor não seja encontrado, o valor default é retornado.
```csharp
public enum EnumTeste
{
    Valor1 = 1,
    Valor2 = 2,
    Valor3 = 3
}

var enumTeste = 1.ConverterParaEnum(EnumTeste.Valor2);
// enumTeste == EnumTeste.Valor1

var enumTeste2 = 5.ConverterParaEnum(EnumTeste.Valor2);
// enumTeste2 == EnumTeste.Valor2, pois o item do enum com o valor 5 não existe.
```
**ConverterDataPorFormato** - Converte uma string em uma data, a partir do formato exato informado.
```csharp
var data = "13/05/2018 23:12:55".ConverterDataPorFormato("dd/MM/yyyy HH:mm:ss");
// data == new DateTime(2018, 5, 13, 23, 12, 55)

var data = "13/05/2018".ConverterDataPorFormato("dd/MM/yyyy HH:mm:ss");
// data == null, pois a string "13/05/2018" não possui o formato "dd/MM/yyyy HH:mm:ss".
```
**ConverterDataUtcHorarioOficialBrasil** - Converte uma data UTC em uma data com o horário oficial brasileiro (Brasília).
```csharp
var dataOficialBrasil = DateTime.UtcNow.ConverterDataUtcHorarioOficialBrasil();

var data = DateTime.Now.ConverterDataUtcHorarioOficialBrasil();
// Exception, pois DateTime.Now não é uma data UTC.
```

### Formatações
**Formatar** - Formata uma string a partir de um padrão.
```csharp
var aux = "123456789".Formatar("###.###-###");
// aux == 123.456-789
```
**FormatarCpf** - Formata uma string aplicando a máscara para CPF.
```csharp
var aux = "42580284010".FormatarCpf();
// aux == "425.802.840-10"
```
**FormatarCnpj** - Formata uma string aplicando a máscara para CNPJ.
```csharp
var aux = "84552945000106".FormatarCnpj();
// aux == "84.552.945/0001-06"
```
**RemoverCaracter** - Remove de uma string alguns caracteres desejados.
```csharp
var aux = "84.552.945/0001-06".RemoverCaracter(".", "/", "-");
// aux == "84552945000106"
```
**ExtrairNumeros** - Extrai somente os caracteres numéricos de uma string.
```csharp
var aux = "84.552.945/0001-06".ExtrairNumeros();
// aux == "84552945000106"
```
**RemoverAcentuacao** - Remove todos os caracteres acentuados de uma string.
```csharp
var aux = "Ãóçê".RemoverAcentuacao();
// aux == "Aoce"
```
**RemoverHtml** - Remove todas as tags HTML de uma string
```csharp
var aux = "<span style=\"border: 1px solid red;\"><b>Removeu toda formatação HTML</b></span></br>".RemoverHtml();
// aux == "Removeu toda formatação HTML"
```

### Validações
**ValidarCnpj** - Valida se uma determinada string é o número válido de um CNPJ.
```csharp
var aux = "84.552.945/0001-06".ValidarCnpj();
// aux == true
var aux = "123".ValidarCnpj();
// aux == false
```
**ValidarCpf** - Valida se uma determinada string é o número válido de um CPF.
```csharp
var aux = "425.802.840-10".ValidarCpf();
// aux == true
var aux = "123".ValidarCpf();
// aux == false
```

### Critptografia
**Criptografar** - Criptografa uma string, a partir de uma chave.
```csharp
var chave = "Essa é a minha chave secreta".Criptografar(chave: "8aba82e7-c209-46fc-b2b1-4f36d32a7377");
// chave == "T5GMYervItZXmHjsNCcd0llen1nxzTTIxB6MSdfiO8bLvOJeLark5bn4qF9Q8vMAPO09CKVR4cPyoangq2C53A=="
```
**Descriptografar** - Descriptografa uma string, a partir de uma chave.
```csharp
var chave = "T5GMYervItZXmHjsNCcd0llen1nxzTTIxB6MSdfiO8bLvOJeLark5bn4qF9Q8vMAPO09CKVR4cPyoangq2C53A==".Descriptografar(chave: "8aba82e7-c209-46fc-b2b1-4f36d32a7377");
// chave == "Essa é a minha chave secreta"
```

### Outros
**SubstringSafe** - Ao executar o método "Substring" em uma string, caso o valor do parâmentro "length" seja maior que a quantidade de caracteres da string, uma exception "ArgumentOutOfRangeException" é disparada.
```csharp
var substring = "Hello World".Substring(0, 1000); // O tamanho (parâmetro length) é maior que a quantida de caracteres da string.
\\ Uma exception "ArgumentOutOfRangeException" é disparada
```
O método "SubstringSafe" impede que essa exception seja disparada, pois caso o valor do parâmetro "length" seja maior que a quantidade de caracteres da string, será retornado os caracteres até o término da string. Veja o exemplo:
```csharp
var substring = "Hello World".SubstringSafe(0, 1000);
\\ substring = "Hello World"
```

## Dependências
* **Utilzao**: .NET Standard 2.1+

Informações sobre versões suportadas em https://docs.microsoft.com/pt-br/dotnet/standard/net-standard

## Instalação

### NuGet
```
Install-Package Utilzao
```
### .NET CLI
```
dotnet add package Utilzao
```
