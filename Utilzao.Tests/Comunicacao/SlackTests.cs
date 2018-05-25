using JNogueira.Infraestrutura.Utilzao.Comunicacao.Slack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Utilzao.Tests
{
    [TestClass]
    [TestCategory("Slack")]
    public class SlackTests
    {
        private readonly string _urlWebhook = ConfigurationManager.AppSettings["Slack.Webhook"];

        private readonly string _nomeCanal = ConfigurationManager.AppSettings["Slack.Channel"];

        private readonly string _nomeUsuario = ConfigurationManager.AppSettings["Slack.Username"];

        [TestMethod]
        public void Deve_Enviar_Mensagem_Para_Slack()
        {
            var slackUtil = new SlackUtil(_urlWebhook);

            try
            {
                var mensagem = new SlackMensagem(_nomeCanal, "Essa é uma mensagem enviada para o Slack", _nomeUsuario, "Você recebeu uma mensagem.");

                Assert.IsTrue(slackUtil.Postar(mensagem));
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [TestMethod]
        public void Deve_Enviar_Mensagem_Para_Slack_Com_Informacoes_Adicionais()
        {
            var slackUtil = new SlackUtil(_urlWebhook);

            var mensagem = new SlackMensagem(_nomeCanal, "Essa é uma mensagem enviada para o Slack com informações adicionais.", _nomeUsuario, "Você recebeu uma mensagem.", TipoSlackEmoji.RobotFace);

            Assert.IsTrue(slackUtil.Postar(mensagem, infoAdicionais: new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Data atual", DateTime.Now.ToString("dd/MM/yyyy")),
                new KeyValuePair<string, string>("Outra informação", "Qualquer informação aqui.")
            }));
        }

        [TestMethod]
        public void Deve_Enviar_Mensagem_Para_Slack_Tipo_Aviso()
        {
            var slackUtil = new SlackUtil(_urlWebhook);

            var mensagem = new SlackMensagem(_nomeCanal, "Essa é uma mensagem enviada para o Slack", _nomeUsuario, "Você recebeu uma mensagem.");

            mensagem.DefinirTipo(TipoSlackMensagem.Aviso);

            Assert.IsTrue(slackUtil.Postar(mensagem));
        }

        [TestMethod]
        public void Deve_Enviar_Mensagem_Para_Slack_Tipo_Info()
        {
            var slackUtil = new SlackUtil(_urlWebhook);

            var mensagem = new SlackMensagem(_nomeCanal, "Essa é uma mensagem enviada para o Slack", _nomeUsuario, "Você recebeu uma mensagem.");

            mensagem.DefinirTipo(TipoSlackMensagem.Info);

            Assert.IsTrue(slackUtil.Postar(mensagem));
        }

        [TestMethod]
        public void Deve_Enviar_Mensagem_Para_Slack_Tipo_Erro()
        {
            var slackUtil = new SlackUtil(_urlWebhook);

            var mensagem = new SlackMensagem(_nomeCanal, "Essa é uma mensagem enviada para o Slack", _nomeUsuario, "Você recebeu uma mensagem.");

            mensagem.DefinirTipo(TipoSlackMensagem.Erro);

            Assert.IsTrue(slackUtil.Postar(mensagem));
        }

        [TestMethod]
        public void Deve_Enviar_Mensagem_Para_Slack_Por_Exception()
        {
            var slackUtil = new SlackUtil(_urlWebhook);

            try
            {
                var a = 0;
                var i = 5 / a;
            }
            catch (Exception ex)
            {
                var mensagem = new SlackMensagem(_nomeCanal, "Esse é um exemplo de exception enviada para o Slack.", _nomeUsuario, "Você recebeu uma nova exception");

                Assert.IsTrue(slackUtil.Postar(mensagem, ex));
            }
        }
    }
}
