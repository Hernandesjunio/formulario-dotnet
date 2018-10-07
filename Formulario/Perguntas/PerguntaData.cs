using Formulario.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formulario.DTO;

namespace Formulario.Perguntas
{
    public class PerguntaData : Pergunta
    {
        //public eValorRespostaData Validador { get; set; } = eValorRespostaData.NaoValidar;
        //public eTipoEntradaData TipoEntrada { get; set; } = eTipoEntradaData.CaixaDeTexto;

        public override void AtribuirPergunta(PerguntaDTO perguntaDTO)
        {
            base.AtribuirPergunta(perguntaDTO);

            //this.TipoEntrada = (eTipoEntradaData)perguntaDTO.TipoEntrada;
            
        }
    }
}
