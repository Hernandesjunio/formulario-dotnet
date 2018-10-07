using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Enumeradores
{
    public enum eTipoPergunta : byte
    {        
        Texto = 1,
        Anexo = 2,
        EscolhaUnica = 3,
        MultiplaEscolha = 4,
        Numero = 5,
        Data = 6,
        Grade = 7,        
    }
}
