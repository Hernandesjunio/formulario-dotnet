using Formulario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Business.DTO
{
    public class LeiautePerguntaItemDTO
    {
        /// <summary>
        /// Código do leiaute da pergunta
        /// </summary>
        public long LeiautePerguntaID { get; set; }

        /// <summary>
        /// Código do item do leiaute
        /// </summary>
        public long LeiautePerguntaItemID { get; set; }

        /// <summary>
        /// Tamanho da tela a ser renderizado
        /// </summary>
        public eTamanhoTela TamanhoTela { get; set; }

        /// <summary>
        /// Quantas colunas a pergunta ocupará
        /// </summary>
        public eColunas Coluna { get; set; }
    }
}
