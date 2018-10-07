using Formulario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Business.DTO
{    
    public class RespostaDTO
    {                
        /// <summary>
        /// Código da resposta
        /// </summary>
        public long RespostaID { get; set; }

        /// <summary>
        /// Código da pergunta
        /// </summary>
        public long PerguntaID { get; set; }

        /// <summary>
        /// Opções respondidas para múltipla escolha
        /// </summary>
        public List<long> Opcoes { get; set; } = new List<long>();

        /// <summary>
        /// Opção escolhida para escolha única
        /// </summary>
        public long? OpcaoID { get; set; }

        public ICollection<RespostaGradeDTO> RespostaGrade { get; set; } = new List<RespostaGradeDTO>();

        /// <summary>
        /// Valor respondido para perguntas exceto, escolha única e múltipla
        /// </summary>
        public object Valor { get; set; }

        //public long RespostaModeloFormularioID { get; set; }

        public string UsuarioID { get; set; }
        public string Extensao { get; set; }
        public string NomeArquivo { get; set; }
    }
}
