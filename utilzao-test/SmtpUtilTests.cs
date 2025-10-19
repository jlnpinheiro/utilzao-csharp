using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace JNogueira.Utilzao.Test;

public class SmtpUtilTests
{
    private SmtpClient _smtp;

    private string _emailRemetente;

    private string[] _emailDestinatarios;

    [SetUp]
    public void SetupTest()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("testSettings.json")
            .Build();

        _smtp = new(configuration["SmtpHost"], Convert.ToInt32(configuration["SmtpPort"]))
        {
            EnableSsl             = true,
            DeliveryMethod        = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials           = new NetworkCredential(configuration["SmtpUsername"], configuration["SmtpPassword"])
        };

        _emailRemetente = configuration["SmtpEmail"];

        _emailDestinatarios = [configuration["SmtpEmail"]];
    }
    
    [Test]
    public void Deve_Enviar_Email_Por_Smtp()
    {
        var email = new SmtpUtil(_emailRemetente, _emailDestinatarios, "Você recebeu uma mensagem enviada pelo teste <b>Deve_Enviar_Email_Por_Smtp</b>.", _smtp)
        {
            NomeRemetente = "Utilzão Teste",
            Assunto = "Mensagem enviada pelo teste Deve_Enviar_Email_Por_Smtp",
            MensagemEmHtml = true
        };

        email.Enviar();

        Assert.Pass();
    }

    [Test]
    public void Deve_Enviar_Email_Por_Smtp_Com_Copia()
    {
        var email = new SmtpUtil(_emailRemetente, _emailDestinatarios, "Você recebeu uma mensagem enviada pelo teste <b>Deve_Enviar_Email_Por_Smtp_Com_Copia</b>.", _smtp)
        {
            EmailsDestinatariosEmCopia = _emailDestinatarios,
            NomeRemetente = "Utilzão Teste",
            Assunto = "Mensagem enviada pelo teste Deve_Enviar_Email_Por_Smtp_Com_Copia",
            MensagemEmHtml = true
        };

        email.Enviar();

        Assert.Pass();
    }

    [Test]
    public void Deve_Enviar_Email_Por_Smtp_Com_Anexo()
    {
        using MemoryStream memoryStream = new MemoryStream();
        byte[] contentAsBytes = Encoding.UTF8.GetBytes("Olá, sou um anexo!");
        memoryStream.Write(contentAsBytes, 0, contentAsBytes.Length);
        memoryStream.Seek(0, SeekOrigin.Begin);

        var contentType = new ContentType
        {
            MediaType = MediaTypeNames.Text.Plain,
            Name = "AnexoEmail.txt"
        };

        var anexo = new Attachment(memoryStream, contentType);

        var email = new SmtpUtil(_emailRemetente, _emailDestinatarios, "Você recebeu uma mensagem enviada pelo teste <b>Deve_Enviar_Email_Por_Smtp_Com_Anexo</b>.", _smtp)
        {
            Anexos = new List<Attachment> { anexo },
            NomeRemetente = "Utilzão Teste",
            Assunto = "Mensagem enviada pelo teste Deve_Enviar_Email_Por_Smtp_Com_Anexo",
            MensagemEmHtml = true
        };

        email.Enviar();

        Assert.Pass();
    }

    [OneTimeTearDown]
    public void DisposeTest()
    {
        _smtp.Dispose();
    }
}
