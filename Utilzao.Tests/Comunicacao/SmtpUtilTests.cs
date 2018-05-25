using JNogueira.Infraestrutura.Utilzao.Comunicacao.Email;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;

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
            _smtp = new SmtpClient
            {
                Host                  = ConfigurationManager.AppSettings["Smtp.Host"],
                Port                  = Convert.ToInt32(ConfigurationManager.AppSettings["Smtp.Port"]),
                EnableSsl             = false,
                DeliveryMethod        = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials           = new NetworkCredential(ConfigurationManager.AppSettings["Smtp.Username"], ConfigurationManager.AppSettings["Smtp.Password"])
            };

            _emailRemetente = ConfigurationManager.AppSettings["Smtp.Email"];

            _emailDestinatarios = new[] { ConfigurationManager.AppSettings["Smtp.Email"] };
        }
        
        [TestMethod]
        public void Deve_Enviar_Email_Por_Smtp()
        {
            try
            {
                var email = new SmtpUtil(_emailRemetente, _emailDestinatarios, "<b>Você recebeu uma mensagem teste.</b>", _smtp)
                {
                    NomeRemetente  = "Utilzão Teste",
                    Assunto        = "Você recebeu um e-mail teste.",
                    MensagemEmHtml = true
                };

                email.Enviar();

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Deve_Enviar_Email_Por_Smtp_Com_Copia()
        {
            try
            {
                var email = new SmtpUtil(_emailRemetente, _emailDestinatarios, "<b>Você recebeu uma mensagem teste.</b>", _smtp)
                {
                    EmailsDestinatariosEmCopia = _emailDestinatarios,
                    NomeRemetente = "Utilzão Teste",
                    Assunto = "Você recebeu um e-mail teste.",
                    MensagemEmHtml = true
                };

                email.Enviar();

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void Deve_Enviar_Email_Por_Smtp_Com_Anexo()
        {
            try
            {
                Attachment anexo;

                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (System.IO.StreamWriter writer = new System.IO.StreamWriter(ms))
                    {
                        writer.Write("Olá, sou um anexo!");
                        writer.Flush();
                    }

                    anexo = new Attachment(ms, new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Text.Plain));
                    anexo.ContentDisposition.FileName = "AnexoEmail.txt";
                }

                var email = new SmtpUtil(_emailRemetente, _emailDestinatarios, "<b>Você recebeu uma mensagem teste com anexo.</b>", _smtp)
                {
                    Anexos = new List<Attachment> { anexo },
                    NomeRemetente = "Utilzão Teste",
                    Assunto = "Você recebeu um e-mail teste com anexo.",
                    MensagemEmHtml = true
                };

                email.Enviar();

                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
