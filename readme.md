
# Utilzão
Coleção de classes e métodos úteis em C# para manipulação de strings, datas, envio de e-mail, etc.

A ideia desse projeto é agrupar várias soluções e recursos "úteis" utilizados durante o desenvolvimento. Coisas simples como aplicar uma máscara a um string, validar um CPF, remover caracteres de uma string, etc. O objetivo é fazer um componente "utilzão" agrupando todos esses recursos em uma coisa só!

A solução é dividida nos seguintes projetos:

* **Utilzao.Standard**: Projeto utilizando .NET Standard 1.2. Informações sobre versões suportadas em https://docs.microsoft.com/pt-br/dotnet/standard/net-standard
* **Utilzao**: Projeto utilizando .NET Full Framework 4.5.1
* **Utilzao.Tests**: Projeto de testes.

## Comunicação
Precisando se comunicar? Tá na mão...!

### Slack
Envie mensagens para o [Slack](https://slack.com/), de maneira simples! Informe sua [incoming webhook url](https://api.slack.com/incoming-webhooks), o canal desejado e foi...!

Para realizar o envio das mensagens para o Slack, utilizo o **Slack.Webhooks** disponível em https://github.com/nerdfury/Slack.Webhooks.

**SlackUtil** - Classe responsável por enviar mensagem para o incoming webhook do Slack.
Informe os dados necessários para enviar as mensagens...
```csharp
private readonly string _urlWebhook = "https://hooks.slack.com/services/xyz";
private readonly string _nomeCanal = "#nome-canal";
private readonly string _nomeUsuario = "bot";
```
Enviando as mensagens...
```csharp
var slackUtil = new SlackUtil(_urlWebhook);
var mensagem = new SlackMensagem(_nomeCanal, "Sorria, você recebeu uma mensagem!", _nomeUsuario, "Sorria!", TipoSlackEmoji.Smile);

var enviou = slackUtil.Postar(mensagem);
// enviou == true;
```
![Exemplo de mensagem](https://github.com/jlnpinheiro/utilzao-csharp/blob/master/_media/mensagem-slack-1.png)

As mensagens são enviadas utilizando os *"attachments*" do Slack. Para maiores informações acesse https://api.slack.com/docs/message-attachments.

Enviando mensagens com informações adicionais...
```csharp
var mensagem = new SlackMensagem(_nomeCanal, "Essa é uma mensagem enviada para o Slack com informações adicionais.", _nomeUsuario, "Você recebeu uma mensagem.", TipoSlackEmoji.RobotFace);

var enviou = slackUtil.Postar(mensagem, infoAdicionais: new List<KeyValuePair<string, string>>
{
    new KeyValuePair<string, string>("Data atual", DateTime.Now.ToString("dd/MM/yyyy")),
    new KeyValuePair<string, string>("Outra informação", "Qualquer informação aqui.")
});
// enviou == true;
```
![Exemplo de mensagem](https://github.com/jlnpinheiro/utilzao-csharp/blob/master/_media/mensagem-slack-2.png)

Enviando mensagens por tipo...

**Aviso**
```csharp
var mensagem = new SlackMensagem(_nomeCanal, "Essa é uma mensagem enviada para o Slack.", _nomeUsuario, "Você recebeu uma mensagem.");

mensagem.DefinirTipo(TipoSlackMensagem.Aviso);

var enviou = slackUtil.Postar(mensagem);
// enviou == true;
```
![Exemplo de mensagem](https://github.com/jlnpinheiro/utilzao-csharp/blob/master/_media/mensagem-slack-aviso.png)

**Info**
```csharp
mensagem.DefinirTipo(TipoSlackMensagem.Info);

var enviou = slackUtil.Postar(mensagem);
// enviou == true;
```
![Exemplo de mensagem](https://github.com/jlnpinheiro/utilzao-csharp/blob/master/_media/mensagem-slack-info.png)

**Erro**
```csharp
mensagem.DefinirTipo(TipoSlackMensagem.Erro);

var enviou = slackUtil.Postar(mensagem);
// enviou == true;
```
![Exemplo de mensagem](https://github.com/jlnpinheiro/utilzao-csharp/blob/master/_media/mensagem-slack-erro.png)

Enviando mensagens quando uma exception acontece...
```csharp
try
{
    var a = 0;
    var i = 5 / a;
}
catch (Exception ex)
{
    var mensagem = new SlackMensagem(_nomeCanal, "Esse é um exemplo de exception enviada para o Slack.", _nomeUsuario, "Você recebeu uma nova exception");
    var enviou = slackUtil.Postar(mensagem, ex);
    // enviou == true;
}
```
![Exemplo de mensagem](https://github.com/jlnpinheiro/utilzao-csharp/blob/master/_media/mensagem-slack-exception.png)

### SMTP
**SmtpUtil** - Classe responsável por enviar e-mail a partir das configurações de um servidor SMTP.
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

// Informando um anexo
var anexo = new Attachment("c:\\meu_anexo.pdf");

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
Configuração do SMTP existente em um arquivo de configuração (Web.Config ou App.Config)
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
```
```csharp
var email = new SmtpUtil("emailRemetente@seudominio.com", new[] { "email_destinatario_1@seudominio.com" }, " <b>Você recebeu uma mensagem.</b>")
{
    NomeRemetente = "Utilzão",
    Assunto = "Você recebeu um e-mail com anexo.",
    MensagemEmHtml = true
};

email.Enviar();
```

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

## Dependências
* **Utilzao**: .NET Full 4.5.1
* **Utilzao.Standard**: .NET Standard 1.2+

Informações sobre versões suportadas em https://docs.microsoft.com/pt-br/dotnet/standard/net-standard

## Instalação

### NuGet
```
Install-Package Utilzao
```
```
Install-Package Utilzao.Standard
```
### .NET CLI
```
dotnet add package Utilzao
```
```
dotnet add package Utilzao.Standard
```
