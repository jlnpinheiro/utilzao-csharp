using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace JNogueira.Utilzao
{
    /// <summary>
    /// Classe responsável por enviar e-mail a partir das configurações de um servidor SMTP
    /// </summary>
    public class SmtpUtil
    {
        /// <summary>
        /// E-mail do remetente (From)
        /// </summary>
        public string EmailRemetente { get; }

        /// <summary>
        /// Nome do remetente (display name)
        /// </summary>
        public string NomeRemetente { get; set; }

        /// <summary>
        /// Lista com os e-mails dos destinatários (To)
        /// </summary>
        public ICollection<string> EmailsDestinatarios { get; set; }

        /// <summary>
        /// Lista com os e-mails dos destinatários em cópia (Cc)
        /// </summary>
        public ICollection<string> EmailsDestinatariosEmCopia { get; set; }

        /// <summary>
        /// Lista com os e-mails dos destinatários em cópia oculta (Cc)
        /// </summary>
        public ICollection<string> EmailsDestinatariosEmCopiaOculta { get; set; }

        /// <summary>
        /// Descrição do assunto do e-mail
        /// </summary>
        public string Assunto { get; set; }

        /// <summary>
        /// Mensagem do assunto do e-mail
        /// </summary>
        public string Mensagem { get; }

        /// <summary>
        /// Indica se a mensagem deverá ser enviada no formato HTML
        /// </summary>
        public bool MensagemEmHtml { get; set; }

        /// <summary>
        /// Anexos do e-mail
        /// </summary>
        public ICollection<Attachment> Anexos { get; set; }

        /// <summary>
        /// Configurações de SMTP para envio do email
        /// </summary>
        public SmtpClient SmtpClient { get; set; }


        public SmtpUtil(string emailRemetente, ICollection<string> emailsDestinatarios, string mensagem, SmtpClient smtpClient = null)
        {
            this.Anexos = new List<Attachment>();
            this.EmailsDestinatariosEmCopia = new List<string>();
            this.EmailsDestinatariosEmCopiaOculta = new List<string>();

            this.EmailRemetente = emailRemetente;
            this.EmailsDestinatarios = emailsDestinatarios;
            this.Mensagem = mensagem;
            this.SmtpClient = smtpClient;
        }

        public SmtpUtil(string emailRemetente, string emailDestinatario, string mensagem, SmtpClient smtpClient = null)
            : this(emailRemetente, new List<string>() { emailDestinatario }, mensagem, smtpClient)
        {
            
        }

        /// <summary>
        /// Realiza o envio do e-mail
        /// </summary>
        public void Enviar()
        {
            if (string.IsNullOrEmpty(this.EmailRemetente))
                throw new ArgumentNullException(nameof(this.EmailRemetente), "O e-mail do remetente não foi informado.");

            if (!this.EmailsDestinatarios.Any() && !this.EmailsDestinatariosEmCopia.Any() && !this.EmailsDestinatariosEmCopiaOculta.Any())
                throw new ArgumentNullException(nameof(this.EmailsDestinatarios), "Nenhum destinatário foi informado para enviar o e-mail.");

            if (string.IsNullOrEmpty(this.Mensagem))
                throw new ArgumentNullException(nameof(this.Mensagem), "A mensagem do e-mail não pode ser vazia.");

            var remetente = !string.IsNullOrEmpty(this.NomeRemetente)
                ? new MailAddress(this.EmailRemetente, this.NomeRemetente)
                : new MailAddress(this.EmailRemetente);

            using (var mensagem = new MailMessage())
            {
                mensagem.Subject = this.Assunto;
                mensagem.Body = this.Mensagem;
                mensagem.IsBodyHtml = this.MensagemEmHtml;
                mensagem.SubjectEncoding = Encoding.UTF8;
                mensagem.BodyEncoding = Encoding.UTF8;

                mensagem.From = remetente;
                mensagem.Sender = remetente;

                foreach(var destinatario in this.EmailsDestinatarios)
                {
                    mensagem.To.Add(destinatario);
                }

                if (this.EmailsDestinatariosEmCopia.Any())
                {
                    foreach (var destinatarioEmCopia in this.EmailsDestinatariosEmCopia)
                    {
                        mensagem.CC.Add(destinatarioEmCopia);
                    }
                }

                if (this.EmailsDestinatariosEmCopiaOculta.Any())
                {
                    foreach (var destinatarioEmCopiaOculta in this.EmailsDestinatariosEmCopiaOculta)
                    {
                        mensagem.Bcc.Add(destinatarioEmCopiaOculta);
                    }
                }

                if (this.Anexos.Any())
                {
                    foreach (var anexo in this.Anexos)
                    {
                        mensagem.Attachments.Add(anexo);
                    }
                }

                if (this.SmtpClient == null)
                    this.SmtpClient = new SmtpClient();

                using (this.SmtpClient)
                {
                    this.SmtpClient.Send(mensagem);
                }
            }
        }
    }
}
