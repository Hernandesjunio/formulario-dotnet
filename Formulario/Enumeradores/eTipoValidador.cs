using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Enumeradores
{
    public enum eTipoValidador:byte
    {
        [TipoPergunta(eTipoPergunta.Data)]
        Data_SomenteMenorIgualHoje = 11,
        [TipoPergunta(eTipoPergunta.Data)]
        Data_SomenteMenorHoje = 12,
        [TipoPergunta(eTipoPergunta.Data)]
        Data_SomenteMaiorIgualHoje = 13,
        [TipoPergunta(eTipoPergunta.Data)]
        Data_SomenteMaiorHoje = 14,
        [TipoPergunta(eTipoPergunta.Data)]
        Data_QualquerValor = 30,

        [TipoPergunta(eTipoPergunta.Numero)]
        Numero_MenorIgualZero = 31,
        [TipoPergunta(eTipoPergunta.Numero)]
        Numero_MenorZero = 32,
        [TipoPergunta(eTipoPergunta.Numero)]
        Numero_MaiorIgualZero = 33,
        [TipoPergunta(eTipoPergunta.Numero)]
        Numero_MaiorZero = 34,
        [TipoPergunta(eTipoPergunta.Numero)]
        Numero_NaoValidar = 20,

        [TipoPergunta(eTipoPergunta.Texto)]
        Texto_CPF = 22,
        [TipoPergunta(eTipoPergunta.Texto)]
        Texto_CNPJ = 23,
        [TipoPergunta(eTipoPergunta.Texto)]
        Texto_Email = 24,
        [TipoPergunta(eTipoPergunta.Texto)]
        Texto_Regex = 25,
    }
}
