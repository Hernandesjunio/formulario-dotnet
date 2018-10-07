using Formulario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Business.DTO
{
    public class LeiautePerguntaDTO
    {
        /// <summary>
        /// Código leiaute da pergunta
        /// </summary>
        public long LeiautePerguntaID { get; set; }

        /// <summary>
        /// Código da pergunta
        /// </summary>
        public long PerguntaID { get; set; }

        /// <summary>
        /// Itens de leiaute da pergunta (tamanho e tamanho de coluna)
        /// </summary>
        public List<LeiautePerguntaItemDTO> LeiauteItem { get; set; }

        public static LeiautePerguntaDTO LeiautePadrao(PerguntaDTO p)
        {

            return new LeiautePerguntaDTO
            {
                LeiautePerguntaID = 0,
                PerguntaID = p.PerguntaID,
                LeiauteItem = new List<LeiautePerguntaItemDTO>{
                      new LeiautePerguntaItemDTO
                      {
                            Responsivo = eTamanhoTela.Mobile,
                             Tamanho = eColunas.T12
                      } }
            };
        }
    }
}
