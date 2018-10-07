using System.Linq;
using Formulario.Business.DTO;
using Formulario.Business.Interfaces;
using Formulario.Business.Leiaute;
using Formulario.Business.Perguntas;
using Formulario.Business.Perguntas.Concicional;
using Formulario.Business.Perguntas.Misc;
using Formulario.Business.Respostas;

namespace Formulario.Business
{
    public interface IFormularioService : IServicePattern
    {
        ModeloFormularioDTO AbrirFormulario(long modeloFormularioID);
        RespostaModeloFormularioDTO AbrirRespostaModeloFormulario(long respostaModeloFormularioID);
        //IQueryable<LeiautePergunta> BuscarLeiautePergunta(bool trackingChanges = true);
        //IQueryable<LeiautePerguntaItem> BuscarLeiautePerguntaItem(bool trackingChanges = true);
        IQueryable<ModeloDeFormulario> BuscarModeloDeFormulario(bool trackingChanges = true);
        //IQueryable<Opcao> BuscarOpcao(bool trackingChanges=true);
        //IQueryable<Resposta> BuscarResposta(bool trackingChanges = true);
        IQueryable<RespostaModeloDeFormulario> BuscarRespostaModeloDeFormulario(bool trackingChanges = true);
        //void ExcluirLeiautePergunta(long leiautePerguntaID);
        //void ExcluirLeiautePerguntaItem(long leiautePerguntaItemID);
        //void ExcluirPergunta(long PerguntaID);
        //void ExcluirPerguntaCondicional(long PerguntaCondicionalID);
        ModeloDeFormulario GravarModeloFormulario(ModeloFormularioDTO modeloDTO, string UsuarioID);
        //Pergunta GravarPergunta(PerguntaDTO perguntaDTO, string UsuarioID);
        //PerguntaCondicional GravarPerguntaCondicional(PerguntaCondicionalDTO condicional, Pergunta pergunta, string UsuarioID);
        RespostaModeloDeFormulario GravarRespostaModeloFormulario(RespostaModeloFormularioDTO respostaModeloDTO, string UsuarioID);
        RespostaModeloFormularioDTO ResponderFormulario(long modeloFormularioID);
        ModeloDeFormulario ExcluirModeloFormulario(long id);
        RespostaModeloDeFormulario ExcluirRespostaModeloFormulario(long id);
    }
}