using Slack.Webhooks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JNogueira.Infraestrutura.Utilzao.Comunicacao.Slack
{
    /// <summary>
    /// Classe responsável por enviar mensagem para o webhook do Slack
    /// </summary>
    public class SlackUtil
    {
        private readonly SlackClient _slackClient;

        public SlackUtil(string webHookUrl)
        {
            if (string.IsNullOrEmpty(webHookUrl))
                throw new ArgumentNullException(nameof(webHookUrl), "A URL do webhook precisa obrigatoriamente ser informada para enviar a mensagem.");

            _slackClient = new SlackClient(webHookUrl);
        }

        /// <summary>
        /// Realiza a postagem de uma mensagem no Slack
        /// </summary>
        /// <param name="mensagem">Mensagem que será postada.</param>
        /// <param name="tipo">Tipo de mensagem postada.</param>
        /// <param name="infoAdicionais">Informações adicionais da mensagem.</param>
        public bool Postar(SlackMensagem mensagem, TipoSlackMensagem? tipo = null, List<KeyValuePair<string, string>> infoAdicionais = null)
        {
            if (mensagem == null)
                return false;

            if (tipo.HasValue)
                mensagem.DefinirTipo(tipo.Value);

            var attachment = new SlackAttachment
            {
                Color = mensagem.Cor,
                Title = mensagem.Titulo,
                Text = mensagem.Texto,
                Fields = new List<SlackField>()
            };

            if (infoAdicionais?.Any() == true)
            {
                foreach (var item in infoAdicionais)
                {
                    attachment.Fields.Add(new SlackField { Title = item.Key, Value = item.Value });
                }
            }

            if (!string.IsNullOrEmpty(mensagem.NomeRemetente))
            {
                attachment.Fields.Add(new SlackField
                {
                    Title = "Remetente",
                    Value = mensagem.NomeRemetente
                });
            }

            return this.Postar(mensagem, new List<SlackAttachment> { attachment });
        }

        /// <summary>
        /// Realiza a postagem de uma mensagem no Slack, referente a um erro ocorrido.
        /// </summary>
        /// <param name="mensagem">Mensagem que será postada.</param>
        /// <param name="ex">Informações do erro.</param>
        /// <param name="infoAdicionais">Informações adicionais da mensagem.</param>
        public bool Postar(SlackMensagem mensagem, Exception ex, List<KeyValuePair<string, string>> infoAdicionais = null)
        {
            if (mensagem == null)
                return false;

            if (ex == null)
                return false;

            mensagem.DefinirTipo(TipoSlackMensagem.Erro);

            var attachment = new SlackAttachment
            {
                Pretext = !string.IsNullOrEmpty(mensagem.Texto) && mensagem.Texto != ex?.Message ? mensagem.Texto : null,
                Color = mensagem.Cor,
                Title = ex?.Message,
                Text = !string.IsNullOrEmpty(ex?.StackTrace) ? "```" + ex.StackTrace + "```" : null,
                Fields = new List<SlackField>()
            };

            if (infoAdicionais?.Any() == true)
            {
                foreach (var item in infoAdicionais)
                {
                    attachment.Fields.Add(new SlackField { Title = item.Key, Value = item.Value });
                }
            }

            attachment.Fields.AddRange(new[]
            {
                new SlackField
                {
                    Title = "Base exception",
                    Value = ex.GetBaseException().Message
                },
                new SlackField
                {
                    Title = "Source",
                    Value = ex.Source,
                    Short = true
                }
            });

            if (!string.IsNullOrEmpty(mensagem.NomeRemetente))
            {
                attachment.Fields.Add(new SlackField
                {
                    Title = "Remetente",
                    Value = mensagem.NomeRemetente
                });
            }

            return this.Postar(mensagem, new List<SlackAttachment> { attachment });
        }

        /// <summary>
        /// Realiza a postagem de uma mensagem no Slack
        /// </summary>
        /// <param name="mensagem">Mensagem que será postada.</param>
        /// <param name="anexos">Anexos da mensagem</param>
        private bool Postar(SlackMensagem mensagem, List<SlackAttachment> anexos = null)
        {
            if (_slackClient == null)
                return false;

            var slackMessage = new SlackMessage
            {
                Channel = !mensagem.Canal.StartsWith("#") ? "#" + mensagem.Canal : mensagem.Canal,
                IconEmoji = (Emoji)mensagem.TipoEmoji,
                Username = mensagem.UserName,
                Markdown = true,
                ResponseType = mensagem.TipoVisibilidade == TipoVisibilidadeSlackMensagem.NoCanal ? "in-channel" : "ephemeral"
            };

            if (anexos?.Any() == true)
                slackMessage.Attachments = anexos;

            try
            {
                return _slackClient.Post(slackMessage);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
