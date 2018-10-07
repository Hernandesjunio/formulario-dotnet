using Formulario.Perguntas.Misc;
using System.Collections.Generic;
using Formulario.DTO;
using System.Linq;
using Formulario.ComplexProperties;

namespace Formulario.Perguntas
{
    public abstract class PerguntaComOpcoes : Pergunta
    {
        public ICollection<Opcao> Opcoes { get; set; } = new HashSet<Opcao>();
        

        public override void AtribuirPergunta(PerguntaDTO perguntaDTO)
        {
            base.AtribuirPergunta(perguntaDTO);

            foreach (var item in Opcoes.ToList())
            {
                //update
                if (perguntaDTO.Opcoes.ContainsKey(item.OpcaoID))
                {
                    var descricao = perguntaDTO.Opcoes.Find(item.OpcaoID).Descricao;
                    item.Descricao = descricao;
                    item.ControleAtualizacao = ControleUsuario.Criar(perguntaDTO.UsuarioID);
                }
                //delete
                else
                {
                    Opcoes.Remove(item);
                }
            }

            var novasOpcoes = perguntaDTO.Opcoes.Where(d => Opcoes.Any(c => c.OpcaoID == d.OpcaoID) == false).ToList();

            foreach (var item in novasOpcoes)
            {
                //insert
                Opcoes.Add(new Opcao
                {
                    Descricao = item.Descricao,
                    OpcaoID = item.OpcaoID,
                    PerguntaComOpcoes = this,
                    PerguntaID = this.PerguntaID,
                    ControleAtualizacao = ControleUsuario.Criar(perguntaDTO.UsuarioID)
                });
            }
        }
    }
}
