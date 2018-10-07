using Formulario.ComplexProperties;
using Formulario;
using Formulario.Business.Perguntas;
using System.Collections.Generic;

namespace Formulario.Business.Leiaute
{            
    public class LeiautePergunta
    {
        public long LeiautePerguntaID { get; set; }

        public virtual ICollection<LeiautePerguntaItem> LeiauteItem { get; set; } = new HashSet<LeiautePerguntaItem>();
        public long PerguntaID { get; set; }
        public virtual Pergunta Pergunta { get; set; }
        public ControleUsuario ControleAtualizacao { get; set; }
        /// <summary>
        /// Cria leiaute padrão mobile 12
        /// </summary>
        /// <returns></returns>
        public static LeiautePergunta LeiautePadrao(Pergunta p)
        {

            return new LeiautePergunta
            {
                Pergunta = p,
                PerguntaID = p.PerguntaID,
                LeiauteItem = new HashSet<LeiautePerguntaItem>{
                      new LeiautePerguntaItem
                      {
                            Responsivo = eTamanhoTela.Mobile,
                             Tamanho = eColunas.T12
                      } }
            };
        }
    }
}
