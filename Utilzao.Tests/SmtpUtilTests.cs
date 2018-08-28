using JNogueira.Infraestrutura.Utilzao;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Utilzao.Tests
{
    [TestClass]
    [TestCategory("SMTP")]
    public class SmtpUtilTests
    {
        private readonly SmtpClient _smtp;

        private readonly string _emailRemetente;

        private readonly string[] _emailDestinatarios;

        public SmtpUtilTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _smtp = new SmtpClient
            {
                Host                  = config["Smtp:Host"],
                Port                  = Convert.ToInt32(config["Smtp:Port"]),
                EnableSsl             = false,
                DeliveryMethod        = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials           = new NetworkCredential(config["Smtp:UserName"], config["Smtp:Password"])
            };

            _emailRemetente = config["Smtp:Email"];

            _emailDestinatarios = new[] { config["Smtp:Email"] };
        }
        
        [TestMethod]
        public void Deve_Enviar_Email_Por_Smtp()
        {
            try
            {
                var email = new SmtpUtil(_emailRemetente, _emailDestinatarios, "Você recebeu uma mensagem enviada pelo teste <b>Deve_Enviar_Email_Por_Smtp</b>.", _smtp)
                {
                    NomeRemetente  = "Utilzão Teste",
                    Assunto        = "Mensagem enviada pelo teste Deve_Enviar_Email_Por_Smtp",
                    MensagemEmHtml = true
                };

                email.Enviar();

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetBaseException().Message);
            }
        }

        [TestMethod]
        public void Deve_Enviar_Email_Por_Smtp_Com_Copia()
        {
            try
            {
                var email = new SmtpUtil(_emailRemetente, _emailDestinatarios, "Você recebeu uma mensagem enviada pelo teste <b>Deve_Enviar_Email_Por_Smtp_Com_Copia</b>.", _smtp)
                {
                    EmailsDestinatariosEmCopia = _emailDestinatarios,
                    NomeRemetente = "Utilzão Teste",
                    Assunto = "Mensagem enviada pelo teste Deve_Enviar_Email_Por_Smtp_Com_Copia",
                    MensagemEmHtml = true
                };

                email.Enviar();

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetBaseException().Message);
            }
        }

        [TestMethod]
        public void Deve_Enviar_Email_Por_Smtp_Com_Anexo()
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
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

                    Assert.IsTrue(true);
                }

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.GetBaseException().Message);
            }
        }
    }
}
