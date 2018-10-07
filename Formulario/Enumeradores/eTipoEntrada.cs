using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario.Enumeradores
{
    public enum eTipoEntrada
    {
        [TipoPergunta(eTipoPergunta.Anexo)]
        Anexo_Dragable = 10,
        [TipoPergunta(eTipoPergunta.Anexo)]
        Anexo_FileInput = 11,

        [TipoPergunta(eTipoPergunta.Data)]
        Data_CaixaDeTexto = 20,
        [TipoPergunta(eTipoPergunta.Data)]
        Data_Calendario = 21,

        [TipoPergunta(eTipoPergunta.EscolhaUnica)]
        EscolhaUnica_Radio = 30,
        [TipoPergunta(eTipoPergunta.EscolhaUnica)]
        EscolhaUnica_Dropdown = 31,
        [TipoPergunta(eTipoPergunta.EscolhaUnica)]
        EscolhaUnica_Star = 32,

        [TipoPergunta(eTipoPergunta.Grade)]
        Grade_Radio = 40,
        [TipoPergunta(eTipoPergunta.Grade)]
        Grade_Estrela = 41,

        [TipoPergunta(eTipoPergunta.MultiplaEscolha)]
        MultiplaEscolha_CaixaDeSelecao = 50,
        [TipoPergunta(eTipoPergunta.MultiplaEscolha)]
        MultiplaEscolha_CaixaSuspensaMultiSelecao = 51,

        [TipoPergunta(eTipoPergunta.Numero)]
        Numero_CaixaDeTexto = 60,
        [TipoPergunta(eTipoPergunta.Numero)]
        Numero_ComBotoes = 61,

        [TipoPergunta(eTipoPergunta.Texto)]
        Texto_CaixaDeTexto = 71,
        [TipoPergunta(eTipoPergunta.Texto)]
        Texto_AreaDeTexto = 72,
    }
}
