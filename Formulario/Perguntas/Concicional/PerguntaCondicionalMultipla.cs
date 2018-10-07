using System;
using Formulario.DTO;
using Formulario.Enumeradores;
using System.Linq;
using System.Collections.Generic;
using Formulario.Perguntas.Misc;

namespace Formulario.Perguntas.Concicional
{
    public class PerguntaCondicionalMultipla : PerguntaCondicionalOpcoesMultipla
    {
        //public ICollection<OpcaoAtivacao> OpcoesAtivacao { get; set; }

        public override void AtribuirCondicional(PerguntaCondicionalDTO pCondicional, Pergunta pergunta)
        {
            Pergunta = pergunta;
            
            PerguntaID = pCondicional.PerguntaID;
            OpcoesAtivacao = (Pergunta as PerguntaMultiplaEscolha).Opcoes
                .Where(c => pCondicional.OpcoesAtivacao.Contains(c.OpcaoID))
                .Select(c => new OpcaoAtivacao
                {
                    OpcaoID = c.OpcaoID,
                    Opcao = c,
                    PerguntaCondicionalID = pCondicional.PerguntaCondicionalID,
                    PerguntaCondicional = this
                })
                .ToList();
        }
    }
}
