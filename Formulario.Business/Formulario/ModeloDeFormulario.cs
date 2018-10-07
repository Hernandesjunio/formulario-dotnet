using Formulario.Business.Perguntas;
using Formulario.Business.Respostas;
using System.Collections.Generic;
using Formulario.Business.Leiaute;
using System;
using Formulario.ComplexProperties;

namespace Formulario.Business
{
    public class ModeloDeFormulario
    {
        public long ModeloFormularioID { get; set; }
        public string Descricao { get; set; }
        public string Html { get; set; }
        public ControleUsuario ControleAtualizacao { get; set; }
        public ICollection<Pergunta> Perguntas { get; set; } = new HashSet<Pergunta>();
        
        public void AtribuirLeiautePerguntasPadrao()
        {            
            foreach (var item in Perguntas)
            {
                item.LeiautePerguntas.Add(LeiautePergunta.LeiautePadrao(item));
            }
        }
    }
}
