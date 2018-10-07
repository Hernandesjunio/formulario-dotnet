using Formulario.Perguntas;
using Formulario.Respostas;
using System.Collections.Generic;
using Formulario.Leiaute;
using System;
using Formulario.ComplexProperties;

namespace Formulario.Formulario
{
    public class ModeloDeFormulario
    {
        public long ModeloFormularioID { get; set; }
        public string Descricao { get; set; }
        public string Html { get; set; }
        public ControleUsuario ControleAtualizacao { get; set; }
        public ICollection<Pergunta> Perguntas { get; set; } = new HashSet<Pergunta>();
        
        public void CriarLeiautePerguntasPadrao()
        {            
            foreach (var item in Perguntas)
            {
                item.LeiautePerguntas.Add(LeiautePergunta.LeiautePadrao(item));
            }
        }
    }
}
