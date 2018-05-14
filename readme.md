
# Utilzão
Coleção de classes e métodos úteis em C# para manipulação de strings, datas, envio de e-mail, etc.

A ideia desse projeto é agrupar várias soluções e recursos "úteis" utilizados durante o desenvolvimento. Coisas simples como aplicar uma máscara a um string, validar um CPF, remover caracteres de uma string, etc. O objetivo é fazer um componente "utilzão" agrupando todos esses recursos em uma coisa só!

## Extension methods
Gosto muito [extension methods](https://docs.microsoft.com/pt-br/dotnet/csharp/programming-guide/classes-and-structs/extension-methods), por isso criei uma pequena coleção desses métodos que utilizo com frequência:

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
// aux == "Aoce
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
**Criptografar** - Criptografa uma string.
```csharp
var chave = "Essa é a minha chave secreta".Criptografar();
// chave == "rmtE8KPZNPIDH4SzUj6MtFLpdM2LMegEybHdTEP5ahI="
```
**Descriptografar** - Descriptografa uma string.
```csharp
var chave = "rmtE8KPZNPIDH4SzUj6MtFLpdM2LMegEybHdTEP5ahI=".Descriptografar();
// chave == "Essa é a minha chave secreta"
```
## Comunicação
Precisando se comunicar? Tá na mão...!

### SMTP - enviando e-mails
**SmtpUtil** - Classe responsável por enviar e-mail a partir das configurações de um servidor SMTP.
```csharp
var smtpGmail = new SmtpClient
{
    Host                  = "smtp.gmail.com",
    Port                  = 587,
    EnableSsl             = true,
    DeliveryMethod        = SmtpDeliveryMethod.Network,
    UseDefaultCredentials = true,
    Credentials           = new NetworkCredential("seu_email@gmail.com", "sua_senha")
};

var anexo = new Attachment("c:\\meu_anexo.pdf");

var email = new SmtpUtil("emailRemetente@seudominio.com", new[] { "email_destinatario_1@seudominio.com" }, "<b>Você recebeu uma mensagem.</b>", smtpGmail)
{
    Anexos = new List<Attachment> { anexo },
    EmailsDestinatariosEmCopia = new[] { "email_destinatario_2@seudominio.com", "email_destinatario_3@seudominio.com" },
    NomeRemetente = "Utilzão",
    Assunto = "Você recebeu um e-mail.",
    MensagemEmHtml = true
};

email.Enviar();
```
```xml
<configuration>
  <system.net>
    <mailSettings>
      <smtp from="seu_email@seudominio.com.br">
        <network host="smtp.seudominio.com.br" port="587" password="sua_senha" userName="username@seudominio.com.br" defaultCredentials="false" />
      </smtp>
    </mailSettings>
  </system.net>
</configuration>

var email = new SmtpUtil("emailRemetente@seudominio.com", new[] { "email_destinatario_1@seudominio.com" }, " <b>Você recebeu uma mensagem.</b>")
{
    NomeRemetente = "Utilzão",
    Assunto = "Você recebeu um e-mail com anexo.",
    MensagemEmHtml = true
};

email.Enviar();
```
