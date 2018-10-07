using Formulario.Enumeradores;
using Formulario.Perguntas.Misc;
using Formulario.Respostas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.DTO
{
    public class PerguntaDTO
    {
        /// <summary>
        /// Controla o estado de exclusão de uma pergunta na interface do usuário
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// Código da pergunta
        /// </summary>
        public long PerguntaID { get; set; }

        /// <summary>
        /// Título da pergunta
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Informações adicionais
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Tipo da pergunta
        /// </summary>
        public eTipoPergunta TipoPergunta { get; set; }

        /// <summary>
        /// Obriga ou não a resposta
        /// </summary>
        public bool Obrigatorio { get; set; }

        /// <summary>
        /// Opções de pergunta única e múltipla e grade
        /// </summary>
        public List<OpcaoDTO> Opcoes { get; set; } 

        /// <summary>
        /// Grade de opcoes
        /// </summary>
        public List<LinhasGradeDTO> LinhasGrade { get; set; }
        
        /// <summary>
        /// Pergunta condicional
        /// </summary>
        public PerguntaCondicionalDTO PerguntaCondicional { get; set; }

        /// <summary>
        /// Código da pergunta condicional
        /// </summary>
        public long? PerguntaCondicionalID { get; set; }

        /// <summary>
        /// Tipo de entrada do dado
        /// </summary>
        public byte TipoEntrada { get; set; }

        /// <summary>
        /// Pattern para validação de expressões regulares
        /// </summary>
        public string PatternRegex { get; set; }

        /// <summary>
        /// Tamanho máximo de caracteres
        /// </summary>
        public short? TamanhoMaximo { get; set; }

        /// <summary>
        /// Tamanho máximo de caracteres
        /// </summary>
        public int? TamanhoMaximoBytes { get; set; }

        /// <summary>
        /// Tipo do validador da entrada do dado
        /// </summary>
        public short? Validador { get; set; }

        /// <summary>
        /// Leiaute da pergunta para responsividade
        /// </summary>
        public List<LeiautePerguntaDTO> LeiautePergunta { get; set; } = new List<LeiautePerguntaDTO>();

        /// <summary>
        /// Prefixo para número
        /// </summary>
        public string Prefixo { get; set; }

        /// <summary>
        /// Casas decimais para número
        /// </summary>
        public byte? CasasDecimais { get; set; }

        /// <summary>
        /// Sufixo para número
        /// </summary>
        public string Sufixo { get; set; }

        /// <summary>
        /// Código do usuário
        /// </summary>
        public string UsuarioID { get; set; }
    }
}
