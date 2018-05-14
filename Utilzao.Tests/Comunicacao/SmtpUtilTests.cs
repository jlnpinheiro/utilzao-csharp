using JNogueira.Infraestrutura.Utilzao.Comunicacao.Email;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace Utilzao.Tests
{
    [TestClass]
    [TestCategory("SMTP")]
    public class SmtpUtilTests
    {
        private readonly SmtpClient _smtpGmail;

        private readonly string _emailRemetente;

        private readonly string[] _emailDestinatarios;

        public SmtpUtilTests()
        {
            _smtpGmail = new SmtpClient
            {
                Host                  = "smtp.gmail.com",
                Port                  = 587,
                EnableSsl             = true,
                DeliveryMethod        = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials           = new NetworkCredential("seu_email@gmail.com", "sua_senha")
            };

            _emailRemetente = "teste@utilzao.com";

            _emailDestinatarios = new[] { "email_destinatario_1@email.com" };
        }
        
        /// <summary>
        /// Para realização desse teste habilitar na conta do G-mail a opção "Turn On Access for less secure apps" (mais informações em https://stackoverflow.com/questions/32260/sending-email-in-net-through-gmail)
        /// </summary>
        [TestMethod]
        public void Deve_Enviar_Email_Por_Smtp_Gmail()
        {
            try
            {
                var email = new SmtpUtil(_emailRemetente, _emailDestinatarios, "<b>Você recebeu uma mensagem teste.</b>", _smtpGmail)
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
        public void Deve_Enviar_Email_Por_Smtp_Gmail_Com_Copia()
        {
            try
            {
                var email = new SmtpUtil(_emailRemetente, _emailDestinatarios, "<b>Você recebeu uma mensagem teste.</b>", _smtpGmail)
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
        public void Deve_Enviar_Email_Por_Smtp_Gmail_Com_Anexo()
        {
            try
            {
                var anexo = new Attachment("c:\\declaracao.pdf");

                var email = new SmtpUtil(_emailRemetente, _emailDestinatarios, "<b>Você recebeu uma mensagem teste com anexo.</b>", _smtpGmail)
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

        [TestMethod]
        public void Deve_Enviar_Email_Por_Smtp_AppConfig()
        {
            try
            {
                // ATENÇÃO: certifique-se que as informações do servidor SMTP estão corretamente informadas no arquivo App.Config 

                var email = new SmtpUtil(_emailRemetente, _emailDestinatarios, " <b>Você recebeu uma mensagem teste.</b>")
                {
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
