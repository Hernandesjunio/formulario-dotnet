using System;
using Formulario.DTO;
using Formulario.Enumeradores;
using System.Linq;
using System.Collections.Generic;
using Formulario.Perguntas.Misc;

namespace Formulario.Perguntas.Concicional
{
    public class PerguntaCondicionalGrade : PerguntaCondicionalOpcoesMultipla
    {        
        public override void AtribuirCondicional(PerguntaCondicionalDTO pCondicional, Pergunta pergunta)
        {
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
        }                     
    }
}
