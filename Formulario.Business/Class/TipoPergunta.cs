using Formulario;
using Formulario.Business.Perguntas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Business.Class
{
    public class TipoPergunta
    {
        public TipoPergunta(eTipoPergunta tipo)
        {
            this.TipoPerguntaID = (byte)tipo;
            this.Descricao = tipo.GetEnumDescription();
        }

        public TipoPergunta()
        {

        }

        public byte TipoPerguntaID { get; set; }
        public string Descricao { get; set; }
               
        
    }
}
