using Formulario.Class;
using Formulario.ComplexProperties;
using Formulario.DTO;
using Formulario.Enumeradores;
using Formulario.Formulario;
using Formulario.Leiaute;
using Formulario.Perguntas.Concicional;
using System.Collections.Generic;

namespace Formulario.Perguntas
{
    public abstract class Pergunta
    {
        public long PerguntaID { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }

        public long? PerguntaCondicionalID { get; set; }

        public virtual PerguntaCondicional PerguntaCondicional { get; set; }
        public bool Obrigatorio { get; set; }
        public ICollection<LeiautePergunta> LeiautePerguntas { get; set; } = new HashSet<LeiautePergunta>();

        public long ModeloDeFormularioID { get; set; }
        public virtual ModeloDeFormulario ModeloDeFormulario { get; set; }
                
        public ControleUsuario ControleAtualizacao { get; set; }
        public byte TipoEntradaID { get; set; }
        public virtual TipoEntrada TipoEntrada { get; set; }

        public eTipoValidador? TipoValidadorID { get; set; }

        public virtual void AtribuirPergunta(PerguntaDTO perguntaDTO)
        {
            this.PerguntaID = perguntaDTO.PerguntaID;
            this.Titulo = perguntaDTO.Titulo;
            this.Descricao = perguntaDTO.Descricao;
            this.PerguntaCondicionalID = perguntaDTO.PerguntaCondicionalID;
            this.Obrigatorio = perguntaDTO.Obrigatorio;
            this.TipoEntradaID = perguntaDTO.TipoEntrada;
            this.TipoValidadorID = (eTipoValidador?)perguntaDTO.Validador;
        }

        public void RemoverCondicional()
        {
            this.PerguntaCondicional = null;
            this.PerguntaCondicionalID = null;
        }

    }
}
