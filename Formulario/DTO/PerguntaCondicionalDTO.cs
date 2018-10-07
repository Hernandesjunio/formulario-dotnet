using Formulario.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.DTO
{
    public class PerguntaCondicionalDTO
    {
        /// <summary>
        /// Código da pergunta condicional
        /// </summary>
        public long PerguntaCondicionalID { get; set; }

        /// <summary>
        /// Código da pergunta à que se refere
        /// </summary>
        public long PerguntaID { get; set; }

        /// <summary>
        /// Valor de ativação da condicional, exceto de múltiplas respostas
        /// </summary>
        public object ValorAtivacao { get; set; }

        /// <summary>
        /// Opcões de ativação para múltiplas respostas
        /// </summary>
        public ICollection<long> OpcoesAtivacao { get; set; }

        /// <summary>
        /// Qual operação condicional a ser utilizada (igual, diferente ...)
        /// </summary>
        public short? OperacaoCondicional { get; set; }

        /// <summary>
        /// Perguntas de grade
        /// </summary>
        public Dictionary<long, string> PerguntasGrade { get; set; }

        /// <summary>
        /// Tipo da pergunta que será condicionada
        /// </summary>
        public eTipoPergunta TipoPergunta { get; set; }
    }
}
