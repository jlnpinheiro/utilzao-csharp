using System;

namespace JNogueira.Infraestrutura.Utilzao.Comunicacao.Slack
{
    /// <summary>
    /// Representa uma mensagem enviada pelo Slack
    /// </summary>
    public class SlackMensagem
    {
        /// <summary>
        /// Nome do canal
        /// </summary>
        public string Canal { get; set; }

        /// <summary>
        /// Título da mensagem
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Texto da mensagem
        /// </summary>
        public string Texto { get; set; }

        /// <summary>
        /// Username do usuário responsável pelo envio da mensagem
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Nome do remetente da mensagem
        /// </summary>
        public string NomeRemetente { get; set; }

        /// <summary>
        /// Cor da borda da mensagem
        /// </summary>
        public string Cor { get; set; }

        /// <summary>
        /// Tipo do emoji utilizando na mensagem
        /// </summary>
        public TipoSlackEmoji TipoEmoji { get; set; }

        /// <summary>
        /// Tipo de visibilidade da mensagem
        /// </summary>
        public TipoVisibilidadeSlackMensagem TipoVisibilidade { get; set; }


        public SlackMensagem(string canal, string texto, string userName, string titulo = null, TipoSlackEmoji tipoEmoji = TipoSlackEmoji.RobotFace, TipoVisibilidadeSlackMensagem tipoVisibilidade = TipoVisibilidadeSlackMensagem.NoCanal)
        {
            if (string.IsNullOrEmpty(canal))
                throw new ArgumentNullException(nameof(canal), "O nome do canal da mensagem não foi informado.");

            if (string.IsNullOrEmpty(texto))
                throw new ArgumentNullException(nameof(canal), "O texto da mensagem não foi informado.");

            if (string.IsNullOrEmpty(texto))
                throw new ArgumentNullException(nameof(canal), "O user name da mensagem não foi informado.");

            this.Canal = canal;
            this.Titulo = titulo;
            this.Texto = texto;
            this.UserName = userName;
            this.TipoEmoji = tipoEmoji;
            this.TipoVisibilidade = tipoVisibilidade;
        }

        public void DefinirTipo(TipoSlackMensagem tipo)
        {
            var cor = "#035BF1";
            var emoji = TipoSlackEmoji.InformationSource;

            switch (tipo)
            {
                case TipoSlackMensagem.Aviso:
                    cor = "warning";
                    emoji = TipoSlackEmoji.Warning;
                    break;
                case TipoSlackMensagem.Erro:
                    cor = "danger";
                    emoji = TipoSlackEmoji.Bomb;
                    break;
            }

            this.TipoEmoji = emoji;
            this.Cor = cor;
        }
    }
}
