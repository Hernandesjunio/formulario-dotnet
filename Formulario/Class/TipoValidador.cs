using Formulario.Enumeradores;
using Formulario.Perguntas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Class
{
    public class TipoValidador
    {        
        public TipoValidador()
        {

        }

        public byte TipoValidadorID { get; set; }
        public string Descricao { get; set; }

        public byte TipoPerguntaID { get; set; }
        public virtual TipoPergunta TipoPergunta { get; set; }
    }
}
