using Formulario.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario
{
    public class TipoPerguntaAttribute : Attribute
    {
        
        public eTipoPergunta TipoPergunta { get;}

        public TipoPerguntaAttribute(eTipoPergunta tipo)
        {
            this.TipoPergunta = tipo;
        }
    }
}
