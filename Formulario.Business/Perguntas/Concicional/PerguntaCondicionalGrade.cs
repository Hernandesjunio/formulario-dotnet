using System;
using Formulario.Business.DTO;
using Formulario;
using System.Linq;
using System.Collections.Generic;
using Formulario.Business.Perguntas.Misc;

namespace Formulario.Business.Perguntas.Concicional
{
    public class PerguntaCondicionalGrade : PerguntaCondicionalOpcoesMultipla
    {        
        public override PerguntaCondicional AtribuirCondicional(PerguntaCondicionalDTO pCondicional, Pergunta pergunta)
        {
            base.AtribuirCondicional(pCondicional, pergunta);

            Pergunta = pergunta;
            
            PerguntaID = pCondicional.PerguntaID;
            OpcoesAtivacao = (Pergunta as PerguntaGradeDeOpcoes).Opcoes
                .Where(c => pCondicional.OpcoesAtivacao.Contains(c.OpcaoID))
                .Select(c => new OpcaoAtivacao
                {
                    OpcaoID = c.OpcaoID,
                    Opcao = c,
                    PerguntaCondicionalID = pCondicional.PerguntaCondicionalID,
                    PerguntaCondicional = this
                })
                .ToList();
            return this;
        }                     
    }
}
