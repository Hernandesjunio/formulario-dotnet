using Formulario.ComplexProperties;
using Formulario.Business.Perguntas.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Business.Respostas
{
    public class OpcaoRespondida
    {
        public long RespostaID { get; set; }
        public virtual RespostaMultipla Resposta { get; set; }
        public long OpcaoID { get; set; }
        public virtual Opcao Opcao { get; set; }
        public ControleUsuario ControleAtualizacao { get; set; }
    }
}
