using Formulario.Business.DTO;
using Formulario.Business.Leiaute;
using Formulario.Business.Perguntas;
using Formulario.Business.Perguntas.Concicional;
using Formulario.Business.Perguntas.Misc;
using Formulario.Business.RepositoryPattern;
using Formulario.Business.Respostas;
using Formulario.ComplexProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Formulario.Business
{
    public class FormularioService : IFormularioService
    {
        private readonly IUnitOfWOrkGeneric<IDatabaseFactory> unitOfWork;
        private readonly IDatabaseFactory factory;

        public FormularioService(IDatabaseFactory factory, IUnitOfWOrkGeneric<IDatabaseFactory> unitOfWork)
        {
            this.factory = factory;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<ModeloDeFormulario> BuscarModeloDeFormulario(bool trackingChanges = true)
        {
            using (IRepository<ModeloDeFormulario> repo = factory.Get().GetRepository<ModeloDeFormulario>())
            {
                return repo.GetQuery(trackingChanges);
            }
        }

        public IQueryable<RespostaModeloDeFormulario> BuscarRespostaModeloDeFormulario(bool trackingChanges = true)
        {
            using (IRepository<RespostaModeloDeFormulario> repo = factory.Get().GetRepository<RespostaModeloDeFormulario>())
            {
                return repo.GetQuery(trackingChanges);
            }
        }

        public IQueryable<Resposta> BuscarResposta(bool trackingChanges = true)
        {
            using (IRepository<Resposta> repo = factory.Get().GetRepository<Resposta>())
            {
                return repo.GetQuery(trackingChanges);
            }
        }

        public ModeloFormularioDTO AbrirFormulario(long modeloFormularioID)
        {
            ModeloDeFormulario modelo = BuscarModeloDeFormulario().Single(c => c.ModeloFormularioID == modeloFormularioID);

            return new ModeloFormularioDTO
            {
                Descricao = modelo.Descricao,
                Html = modelo.Html,
                ModeloFormularioID = modeloFormularioID,
                Perguntas = modelo.Perguntas.Select(c => CriarPerguntaDTO(c)).ToList(),
            };
        }
        public RespostaModeloFormularioDTO AbrirRespostaModeloFormulario(long respostaModeloFormularioID)
        {
            RespostaModeloDeFormulario respostaModeloFormulario = BuscarRespostaModeloDeFormulario().Single(c => c.RespostaModeloFormularioID == respostaModeloFormularioID);

            RespostaModeloFormularioDTO respostaModelo = new DTO.RespostaModeloFormularioDTO
            {
                ModeloFormulario = AbrirFormulario(respostaModeloFormulario.ModeloDeFormularioID),
                RespostaModeloFormularioID = respostaModeloFormulario.RespostaModeloFormularioID,
                Respostas = respostaModeloFormulario.Respostas.Select(c => CriarRespostaDTO(c)).ToList()
            };

            return respostaModelo;
        }

        public RespostaModeloFormularioDTO ResponderFormulario(long modeloFormularioID)
        {
            ModeloDeFormulario modelo = BuscarModeloDeFormulario().Single(c => c.ModeloFormularioID == modeloFormularioID);

            RespostaModeloFormularioDTO respostaModelo = new DTO.RespostaModeloFormularioDTO
            {
                Respostas = modelo.Perguntas.Select(c => new RespostaDTO
                {
                    RespostaGrade = (c is PerguntaGradeDeOpcoes) ? (c as PerguntaGradeDeOpcoes).Linhas
                                                                    .Select(d => new RespostaGradeDTO { LinhaPerguntaGradeID = d.LinhaPerguntaGradeID, OpcaoRespondidaID = new long?() }).ToList() :
                                                                    null,
                    Valor = null,
                    Extensao = null,
                    NomeArquivo = null,
                    OpcaoID = null,
                    Opcoes = null,
                    UsuarioID = null,
                    PerguntaID = c.PerguntaID,
                    RespostaID = 0,
                }).ToList(),
                ModeloFormulario = AbrirFormulario(modeloFormularioID),
            };

            return respostaModelo;
        }

        public void ExcluirPergunta(long PerguntaID)
        {
            using (IRepository<Pergunta> repo = factory.Get().GetRepository<Pergunta>())
            {
                Pergunta p = repo.GetQuery().Single(c => c.PerguntaID == PerguntaID);
                repo.Delete(p);
            }
        }

        public void ExcluirPerguntaCondicional(long PerguntaCondicionalID)
        {
            using (IRepository<PerguntaCondicional> repo = factory.Get().GetRepository<PerguntaCondicional>())
            {
                PerguntaCondicional p = repo.GetQuery().Single(c => c.PerguntaID == PerguntaCondicionalID);
                repo.Delete(p);
            }
        }

        public PerguntaCondicional GravarPerguntaCondicional(PerguntaCondicionalDTO condicional, Pergunta pergunta, string UsuarioID)
        {
            using (IRepository<PerguntaCondicional> repo = factory.Get().GetRepository<PerguntaCondicional>())
            {
                PerguntaCondicional perguntaCondicional;
                if (condicional.PerguntaCondicionalID > 0)
                {
                    perguntaCondicional = repo.GetQuery().SingleOrDefault(c => c.PerguntaID == condicional.PerguntaCondicionalID).AtribuirCondicional(condicional, pergunta);
                }
                else
                {
                    perguntaCondicional = repo.Insert(CriarPerguntaCondicional(condicional, pergunta));
                }

                perguntaCondicional.ControleAtualizacao = ControleUsuario.Criar(UsuarioID);

                return perguntaCondicional;
            }
        }

        public IQueryable<Opcao> BuscarOpcao(bool trackingChanges = true)
        {
            using (IRepository<Opcao> repo = factory.Get().GetRepository<Opcao>())
            {
                return repo.GetQuery(trackingChanges);
            }
        }

        public Pergunta GravarPergunta(PerguntaDTO perguntaDTO, string UsuarioID)
        {
            try
            {
                using (IRepository<Pergunta> repo = factory.Get().GetRepository<Pergunta>())
                {
                    Pergunta pergunta;
                    if (perguntaDTO.PerguntaID > 0)
                    {
                        pergunta = repo.GetQuery().Single(c => c.PerguntaID == perguntaDTO.PerguntaID).AtribuirPergunta(perguntaDTO);
                    }
                    else
                    {
                        pergunta = repo.Insert(CriarPergunta(perguntaDTO));
                    }

                    pergunta.ControleAtualizacao = ControleUsuario.Criar(UsuarioID);

                    return pergunta;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ModeloDeFormulario GravarModeloFormulario(ModeloFormularioDTO modeloDTO, string UsuarioID)
        {
            ModeloDeFormulario modelo;
            using (IRepository<ModeloDeFormulario> repo = factory.Get().GetRepository<ModeloDeFormulario>())
            {
                if (modeloDTO.ModeloFormularioID > 0)
                {
                    modelo = BuscarModeloDeFormulario().Single(c => c.ModeloFormularioID == modeloDTO.ModeloFormularioID);
                }
                else
                {
                    modelo = repo.Insert(CreateInstance<ModeloDeFormulario>());
                }

                modelo.Descricao = modeloDTO.Descricao;
                modelo.Html = modeloDTO.Html;

                modelo.ControleAtualizacao = ControleUsuario.Criar(UsuarioID);

                modeloDTO.Perguntas.AtribuirUsuarioID(UsuarioID);

                //Perguntas novas
                foreach (PerguntaDTO perguntaDTO in modeloDTO.Perguntas.Where(c => c.PerguntaID <= 0 || !modelo.Perguntas.Any(d => d.PerguntaID == c.PerguntaID)))
                {
                    Pergunta pergunta = GravarPergunta(perguntaDTO, UsuarioID);
                    pergunta.ModeloDeFormulario = modelo;

                    modelo.Perguntas.Add(pergunta);

                    if (perguntaDTO.PerguntaCondicional != null)
                    {
                        Pergunta perguntaOrigem = modelo.Perguntas.Single(c => c.PerguntaID == perguntaDTO.PerguntaCondicional.PerguntaID);
                        PerguntaCondicional pCondicional = GravarPerguntaCondicional(perguntaDTO.PerguntaCondicional, perguntaOrigem, UsuarioID);
                        pergunta.PerguntaCondicional = pCondicional;
                    }

                    foreach (LeiautePerguntaDTO leiautePerguntaDTO in perguntaDTO.LeiautesPergunta.ToList())
                    {
                        LeiautePergunta leiautePergunta = CriarLeiautePergunta(leiautePerguntaDTO);
                        //grava o leiaute vinculado à pergunta
                        GravarLeiautePergunta(leiautePergunta, UsuarioID);
                    }

                    if (perguntaDTO.LeiautesPergunta.Any() == false)
                    {
                        GravarLeiautePergunta(LeiautePergunta.LeiautePadrao(pergunta), UsuarioID);
                    }
                }

                //Perguntas atualizadas
                foreach (Pergunta pergunta in modelo.Perguntas.ToList())
                {
                    pergunta.ModeloDeFormulario = modelo;
                    //tracking changes
                    PerguntaDTO perguntaDTO = modeloDTO.Perguntas.SingleOrDefault(d => d.PerguntaID == pergunta.PerguntaID);
                    if (perguntaDTO.Deleted)
                    {
                        List<Pergunta> perguntasCondicionais = modelo.Perguntas.Where(c => c.PerguntaCondicionalID == perguntaDTO.PerguntaID).ToList();

                        foreach (Pergunta pCondicional in perguntasCondicionais)
                        {
                            //remove o vínculo, operações e valor de ativação
                            pCondicional.RemoverCondicional();
                        }

                        foreach (LeiautePergunta leiautePergunta in pergunta.LeiautePerguntas.ToList())
                        {
                            //remove o leiaute vinculado à pergunta
                            ExcluirLeiautePergunta(leiautePergunta.LeiautePerguntaID);
                        }

                        //Remove a pergunta do modelo (Caso não tenha nenhuma resposta)
                        ExcluirPergunta(perguntaDTO.PerguntaID);
                    }
                    else
                    {
                        //Atualização
                        pergunta.AtribuirPergunta(perguntaDTO);
                        //Pergunta condicinoal removida
                        if (pergunta.PerguntaCondicionalID.HasValue && perguntaDTO.PerguntaCondicionalID.HasValue == false)
                        {
                            pergunta.RemoverCondicional();
                            ExcluirPerguntaCondicional(pergunta.PerguntaCondicionalID.Value);
                        }
                        else if (perguntaDTO.PerguntaCondicionalID.HasValue)
                        {
                            PerguntaCondicional condicional = GravarPerguntaCondicional(perguntaDTO.PerguntaCondicional, pergunta, UsuarioID);
                        }

                        foreach (LeiautePergunta leiautePergunta in pergunta.LeiautePerguntas.ToList())
                        {
                            //grava o leiaute vinculado à pergunta
                            GravarLeiautePergunta(leiautePergunta, UsuarioID);
                        }
                    }
                }
            }
            return modelo;
        }

        public RespostaModeloDeFormulario GravarRespostaModeloFormulario(RespostaModeloFormularioDTO respostaModeloDTO, string UsuarioID)
        {

            try
            {
                using (IRepository<Resposta> repo = factory.Get().GetRepository<Resposta>())
                using (IRepository<RespostaModeloDeFormulario> repoRespostaModelo = factory.Get().GetRepository<RespostaModeloDeFormulario>())
                using (IRepository<Pergunta> repoPergunta = factory.Get().GetRepository<Pergunta>())
                {
                    RespostaModeloDeFormulario respostaModeloFormulario;
                    if (respostaModeloDTO.RespostaModeloFormularioID > 0)
                    {
                        respostaModeloFormulario = BuscarRespostaModeloDeFormulario().Single(c => c.RespostaModeloFormularioID == respostaModeloDTO.RespostaModeloFormularioID);
                    }
                    else
                    {
                        respostaModeloFormulario = repoRespostaModelo.Insert(CreateInstance<RespostaModeloDeFormulario>());
                    }

                    respostaModeloFormulario.ModeloDeFormularioID = respostaModeloDTO.ModeloFormulario.ModeloFormularioID;

                    respostaModeloFormulario.ControleAtualizacao = ControleUsuario.Criar(UsuarioID);

                    foreach (RespostaDTO respostaDTO in respostaModeloDTO.Respostas.OrderBy(c => c.PerguntaID).ToList())
                    {
                        PerguntaDTO pergunta = respostaModeloDTO.ModeloFormulario.Perguntas.Single(c => c.PerguntaID == respostaDTO.PerguntaID);

                        Resposta resposta;
                        if (respostaDTO.RespostaID > 0)
                        {
                            resposta = BuscarResposta().Single(c => c.RespostaID == respostaDTO.RespostaID).AtribuirResposta(respostaDTO);
                        }
                        else
                        {
                            resposta = repo.Insert(CriarResposta(respostaDTO, pergunta.TipoPergunta));
                        }

                        resposta.Pergunta = repoPergunta.GetQuery().Single(d => d.PerguntaID == resposta.PerguntaID);
                        resposta.RespostaModeloDeFormulario = respostaModeloFormulario;
                        resposta.ControleAtualizacao = ControleUsuario.Criar(UsuarioID);

                        respostaModeloFormulario.Respostas.Add(resposta);
                    }

                    foreach (Resposta resposta in respostaModeloFormulario.Respostas)
                    {
                        bool validarResposta = true;

                        if (resposta.Pergunta.PerguntaCondicionalID.HasValue)
                        {
                            Resposta respostaOrigem = respostaModeloFormulario.Respostas.Single(d => d.PerguntaID == resposta.Pergunta.PerguntaCondicional.PerguntaID);

                            validarResposta = resposta.Pergunta.PerguntaCondicional.VerificarAtivacaoCondicional(respostaOrigem);
                        }

                        if (validarResposta && resposta.Validar() == false)
                        {
                            throw new ApplicationException($"Verifique a resposta da pergunta {resposta.Pergunta.PerguntaID} {resposta.Pergunta.Titulo}");
                        }
                    }

                    return respostaModeloFormulario;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GravarLeiautePergunta(LeiautePergunta leiautePergunta, string UsuarioID)
        {
            LeiautePergunta result = BuscarLeiautePergunta().SingleOrDefault(d => d.LeiautePerguntaID == leiautePergunta.LeiautePerguntaID);

            if (result == null)
            {
                using (IRepository<LeiautePergunta> repo = factory.Get().GetRepository<LeiautePergunta>())
                {
                    result = repo.Insert(leiautePergunta);
                }
            }
            else
            {
                result.PerguntaID = leiautePergunta.LeiautePerguntaID;
            }

            result.ControleAtualizacao = ControleUsuario.Criar(UsuarioID);

            foreach (LeiautePerguntaItem item in leiautePergunta.LeiauteItem.ToList())
            {
                GravarLeiautePerguntaItem(item, UsuarioID);
            }
        }

        private void GravarLeiautePerguntaItem(LeiautePerguntaItem leiautePerguntaItem, string UsuarioID)
        {
            LeiautePerguntaItem result = BuscarLeiautePerguntaItem().SingleOrDefault(d => d.LeiautePerguntaItemID == leiautePerguntaItem.LeiautePerguntaID);
            if (result == null)
            {
                using (IRepository<LeiautePerguntaItem> repo = factory.Get().GetRepository<LeiautePerguntaItem>())
                {
                    result = repo.Insert(leiautePerguntaItem);
                }
            }
            else
            {
                result.LeiautePerguntaID = leiautePerguntaItem.LeiautePerguntaID;
                result.Responsivo = leiautePerguntaItem.Responsivo;
                result.Tamanho = leiautePerguntaItem.Tamanho;
            }

            result.ControleAtualizacao = ControleUsuario.Criar(UsuarioID);
        }

        public IQueryable<LeiautePergunta> BuscarLeiautePergunta(bool trackingChanges = true)
        {
            using (IRepository<LeiautePergunta> repo = factory.Get().GetRepository<LeiautePergunta>())
            {
                return repo.GetQuery();
            }
        }

        public IQueryable<LeiautePerguntaItem> BuscarLeiautePerguntaItem(bool trackingChanges = true)
        {
            using (IRepository<LeiautePerguntaItem> repo = factory.Get().GetRepository<LeiautePerguntaItem>())
            {
                return repo.GetQuery();
            }
        }

        public void ExcluirLeiautePergunta(long leiautePerguntaID)
        {
            LeiautePergunta leiaute = BuscarLeiautePergunta().Single(c => c.LeiautePerguntaID == leiautePerguntaID);

            foreach (LeiautePerguntaItem item in leiaute.LeiauteItem.ToList())
            {
                ExcluirLeiautePerguntaItem(item.LeiautePerguntaItemID);
            }

            using (IRepository<LeiautePergunta> repo = factory.Get().GetRepository<LeiautePergunta>())
            {
                repo.Delete(leiaute);
            }
        }

        public void ExcluirLeiautePerguntaItem(long leiautePerguntaItemID)
        {
            LeiautePerguntaItem leiautePerguntaItem = BuscarLeiautePerguntaItem().Single(d => d.LeiautePerguntaItemID == leiautePerguntaItemID);
            using (IRepository<LeiautePerguntaItem> repo = factory.Get().GetRepository<LeiautePerguntaItem>())
            {
                repo.Delete(leiautePerguntaItem);
            }
        }


        #region Implementation methods
        public int Commit(string UserID)
        {
            int result = unitOfWork.Commit(UserID);
            return result;
        }

        public async Task<int> CommitAsync(string UserID)
        {
            return await unitOfWork.CommitAsync(UserID);
        }

        public void Rollback()
        {
            unitOfWork.Rollback();
        }
        #endregion


        #region  Creational Methods
        protected Pergunta CriarPergunta(PerguntaDTO perguntaDTO)
        {
            Pergunta pergunta = null;
            switch (perguntaDTO.TipoPergunta)
            {
                case eTipoPergunta.Texto:
                    pergunta = CreateInstance<PerguntaTexto>();
                    break;
                case eTipoPergunta.Anexo:
                    pergunta = CreateInstance<PerguntaAnexo>();
                    break;
                case eTipoPergunta.EscolhaUnica:
                    pergunta = CreateInstance<PerguntaEscolhaUnica>();
                    break;
                case eTipoPergunta.Numero:
                    pergunta = CreateInstance<PerguntaNumero>();
                    break;
                case eTipoPergunta.Data:
                    pergunta = CreateInstance<PerguntaData>();
                    break;
                case eTipoPergunta.Grade:
                    pergunta = CreateInstance<PerguntaGradeDeOpcoes>();
                    break;
                case eTipoPergunta.MultiplaEscolha:
                    pergunta = CreateInstance<PerguntaMultiplaEscolha>();
                    break;
                default:
                    throw new NotImplementedException();
            }

            pergunta.AtribuirPergunta(perguntaDTO);

            foreach (LeiautePerguntaDTO item in perguntaDTO.LeiautesPergunta)
            {
                pergunta.LeiautePerguntas.Add(CriarLeiautePergunta(item));
            }

            return pergunta;
        }

        protected eTipoPergunta CreateTipoPergunta(Pergunta p)
        {
            eTipoPergunta tipoPergunta = (
                    p is PerguntaTexto ? eTipoPergunta.Texto :
                    p is PerguntaAnexo ? eTipoPergunta.Anexo :
                    p is PerguntaEscolhaUnica ? eTipoPergunta.EscolhaUnica :
                    p is PerguntaMultiplaEscolha ? eTipoPergunta.MultiplaEscolha :
                    p is PerguntaNumero ? eTipoPergunta.Numero :
                    p is PerguntaGradeDeOpcoes ? eTipoPergunta.Grade :
                    p is PerguntaData ? eTipoPergunta.Data : new eTipoPergunta?()).Value;

            return tipoPergunta;
        }

        protected PerguntaDTO CriarPerguntaDTO(Perguntas.Pergunta p)
        {
            try
            {
                PerguntaCondicional pCondicional = p.PerguntaCondicional;

                eTipoPergunta tipoPergunta = CreateTipoPergunta(p);

                PerguntaCondicionalDTO perguntaCondicionalDTO = CriarPerguntaCondicionalDTO(p.PerguntaCondicional);

                PerguntaDTO pergunta = new PerguntaDTO
                {
                    Descricao = p.Descricao,
                    TipoPergunta = tipoPergunta,
                    Titulo = p.Titulo,
                    PerguntaID = p.PerguntaID,
                    PerguntaCondicionalID = p.PerguntaCondicionalID,
                    PerguntaCondicional = perguntaCondicionalDTO,
                    Obrigatorio = p.Obrigatorio,
                    Deleted = false,
                    TamanhoMaximo = null,
                    //PerguntasGrade = null,
                    //GradeOpcoes = null,
                    Opcoes = null,
                    ValidadorID = null,
                    PatternRegex = null,
                    TipoEntrada = p.TipoEntradaID,
                    LeiautesPergunta = p.LeiautePerguntas.Select(c => new LeiautePerguntaDTO
                    {
                        LeiautePerguntaID = c.LeiautePerguntaID,
                        PerguntaID = c.PerguntaID,
                        LeiautesPerguntaItem = c.LeiauteItem.Select(d => new LeiautePerguntaItemDTO
                        {
                            LeiautePerguntaID = d.LeiautePerguntaID,
                            LeiautePerguntaItemID = d.LeiautePerguntaItemID,
                            TamanhoTela = d.Responsivo,
                            Coluna = d.Tamanho,
                        }).ToList()
                    }).ToList()
                };

                switch (tipoPergunta)
                {
                    case eTipoPergunta.Texto:
                        PerguntaTexto pTexto = p as PerguntaTexto;
                        pergunta.PatternRegex = pTexto.PatternRegex;
                        pergunta.TamanhoMaximo = pTexto.TamanhoMaximo;
                        pergunta.ValidadorID = (short?)pTexto.TipoValidadorID;
                        pergunta.TipoEntrada = pTexto.TipoEntradaID;
                        break;
                    case eTipoPergunta.EscolhaUnica:
                    case eTipoPergunta.MultiplaEscolha:
                        pergunta.Opcoes = (p as Perguntas.PerguntaComOpcoes).Opcoes.Select(c => new OpcaoDTO { OpcaoID = c.OpcaoID, Descricao = c.Descricao }).ToList();
                        break;
                    case eTipoPergunta.Grade:

                        PerguntaGradeDeOpcoes pGrade = (p as Perguntas.PerguntaGradeDeOpcoes);
                        pergunta.Opcoes = (p as Perguntas.PerguntaGradeDeOpcoes).Opcoes
                            .Select(c => new OpcaoDTO { OpcaoID = c.OpcaoID, Descricao = c.Descricao }).ToList();
                        pergunta.LinhasGrade = (p as Perguntas.PerguntaGradeDeOpcoes).Linhas
                            .Select(c => new LinhasGradeDTO { LinhaID = c.LinhaPerguntaGradeID, Descricao = c.Titulo }).ToList();
                        break;
                    case eTipoPergunta.Anexo:
                        PerguntaAnexo pAnexo = p as PerguntaAnexo;
                        pergunta.TipoEntrada = pAnexo.TipoEntradaID;
                        break;
                    case eTipoPergunta.Numero:
                        PerguntaNumero pNumero = p as PerguntaNumero;
                        pergunta.TipoEntrada = pNumero.TipoEntradaID;
                        pergunta.CasasDecimais = pNumero.CasasDecimais;
                        pergunta.Prefixo = pNumero.Prefixo;
                        pergunta.Sufixo = pNumero.Sufixo;
                        break;
                    case eTipoPergunta.Data:
                        PerguntaData pData = p as PerguntaData;
                        pergunta.TipoEntrada = pData.TipoEntradaID;
                        break;
                    default:
                        throw new NotImplementedException();
                }

                pergunta.TipoEntrada = p.TipoEntradaID;
                pergunta.ValidadorID = (short?)p.TipoValidadorID;

                return pergunta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected PerguntaCondicionalDTO CriarPerguntaCondicionalDTO(PerguntaCondicional pCondicional)
        {
            if (pCondicional == null)
            {
                return null;
            }

            eTipoPergunta tipoPerguntaCondicional = (
               pCondicional is PerguntaCondicionalData ? eTipoPergunta.Texto :
               pCondicional is PerguntaCondicionalAnexo ? eTipoPergunta.Anexo :
               pCondicional is PerguntaCondicionalUnica ? eTipoPergunta.EscolhaUnica :
               pCondicional is PerguntaCondicionalMultipla ? eTipoPergunta.MultiplaEscolha :
               pCondicional is PerguntaCondicionalNumero ? eTipoPergunta.Numero :
               pCondicional is PerguntaCondicionalData ? eTipoPergunta.Data : new eTipoPergunta?()).Value;

            PerguntaCondicionalDTO perguntaCondicionalDTO = new PerguntaCondicionalDTO();

            switch (tipoPerguntaCondicional)
            {
                case eTipoPergunta.Texto:
                    perguntaCondicionalDTO.ValorAtivacao = (pCondicional as PerguntaCondicionalTexto).ValorAtivacao;
                    perguntaCondicionalDTO.OperacaoCondicional = (short)(pCondicional as PerguntaCondicionalTexto).Operacao;
                    break;
                case eTipoPergunta.Numero:
                    perguntaCondicionalDTO.ValorAtivacao = (pCondicional as PerguntaCondicionalNumero).ValorAtivacao;
                    perguntaCondicionalDTO.OperacaoCondicional = (short)(pCondicional as PerguntaCondicionalNumero).Operacao;
                    break;
                case eTipoPergunta.Data:
                    perguntaCondicionalDTO.ValorAtivacao = (pCondicional as PerguntaCondicionalData).ValorAtivacao;
                    perguntaCondicionalDTO.OperacaoCondicional = (short)(pCondicional as PerguntaCondicionalData).Operacao;
                    break;
                case eTipoPergunta.Anexo:
                    perguntaCondicionalDTO.ValorAtivacao = (pCondicional as PerguntaCondicionalAnexo).ValorAtivacao;
                    perguntaCondicionalDTO.OperacaoCondicional = (short)(pCondicional as PerguntaCondicionalAnexo).Operacao;
                    break;
                case eTipoPergunta.EscolhaUnica:
                    perguntaCondicionalDTO.ValorAtivacao = (pCondicional as PerguntaCondicionalUnica).OpcaoAtivacao.OpcaoID;
                    perguntaCondicionalDTO.OperacaoCondicional = (short)(pCondicional as PerguntaCondicionalUnica).Operacao;
                    break;
                case eTipoPergunta.MultiplaEscolha:
                    perguntaCondicionalDTO.OpcoesAtivacao = (pCondicional as PerguntaCondicionalMultipla).OpcoesAtivacao.Select(c => c.OpcaoID).ToList();
                    perguntaCondicionalDTO.OperacaoCondicional = (short)(pCondicional as PerguntaCondicionalMultipla).Operacao;
                    break;
                case eTipoPergunta.Grade:
                    perguntaCondicionalDTO.OpcoesAtivacao = (pCondicional as PerguntaCondicionalGrade).OpcoesAtivacao.Select(c => c.OpcaoID).ToList();
                    perguntaCondicionalDTO.OperacaoCondicional = (short)(pCondicional as PerguntaCondicionalGrade).Operacao;
                    perguntaCondicionalDTO.PerguntasGrade = ((pCondicional as PerguntaCondicionalGrade).Pergunta as PerguntaGradeDeOpcoes).Opcoes.ToDictionary(c => c.OpcaoID, d => d.Descricao);
                    break;
            }
            perguntaCondicionalDTO.PerguntaID = pCondicional.PerguntaID;
            perguntaCondicionalDTO.TipoPergunta = tipoPerguntaCondicional;
            return perguntaCondicionalDTO;
        }

        protected PerguntaCondicional CriarPerguntaCondicional(PerguntaCondicionalDTO pCondicional, Pergunta p)
        {
            PerguntaCondicional pergunta = null;
            switch (pCondicional.TipoPergunta)
            {
                case eTipoPergunta.Texto:
                    pergunta = CreateInstance<PerguntaCondicionalTexto>();
                    break;
                case eTipoPergunta.Anexo:
                    pergunta = CreateInstance<PerguntaCondicionalAnexo>();
                    break;
                case eTipoPergunta.EscolhaUnica:
                    pergunta = CreateInstance<PerguntaCondicionalUnica>();
                    break;
                case eTipoPergunta.Numero:
                    pergunta = CreateInstance<PerguntaCondicionalNumero>();
                    break;
                case eTipoPergunta.Data:
                    pergunta = CreateInstance<PerguntaCondicionalData>();
                    break;
                case eTipoPergunta.Grade:
                    pergunta = CreateInstance<PerguntaCondicionalGrade>();
                    break;
                case eTipoPergunta.MultiplaEscolha:
                    pergunta = CreateInstance<PerguntaCondicionalMultipla>();
                    break;
                default:
                    throw new NotImplementedException();
            }

            pergunta.AtribuirCondicional(pCondicional, p);

            return pergunta;
        }

        protected RespostaDTO CriarRespostaDTO(Resposta r)
        {
            try
            {
                RespostaDTO resposta = new RespostaDTO
                {
                    OpcaoID = null,
                    Opcoes = null,
                    PerguntaID = r.PerguntaID,
                    RespostaGrade = null,
                    RespostaID = r.RespostaID,
                    UsuarioID = r.ControleAtualizacao.UsuarioID,
                    Valor = null
                };

                switch (CreateTipoPergunta(r.Pergunta))
                {
                    case eTipoPergunta.Texto:
                        RespostaTexto rTexto = r as RespostaTexto;
                        resposta.Valor = rTexto.Valor;
                        break;
                    case eTipoPergunta.EscolhaUnica:
                        RespostaUnica rUnica = r as RespostaUnica;
                        resposta.OpcaoID = rUnica.OpcaoEscolhidaID;
                        break;
                    case eTipoPergunta.MultiplaEscolha:
                        RespostaMultipla rOpcoes = r as RespostaMultipla;
                        resposta.Opcoes = rOpcoes.OpcoesEscolhida.Select(c => c.OpcaoID).ToList();
                        break;
                    case eTipoPergunta.Grade:
                        RespostaGrade rGrade = r as RespostaGrade;
                        resposta.RespostaGrade = rGrade.Respostas.Select(c => new RespostaGradeDTO
                        {
                            LinhaPerguntaGradeID = c.LinhaPerguntaGradeID,
                            OpcaoRespondidaID = c.OpcaoRespondidaID
                        }).ToList();
                        break;
                    case eTipoPergunta.Anexo:
                        RespostaAnexo rAnexo = r as RespostaAnexo;
                        //Lazy to performance
                        resposta.Valor = new { rAnexo.Valor.AnexoID, rAnexo.Valor.Nome, rAnexo.Valor.Extensao };
                        break;
                    case eTipoPergunta.Numero:
                        RespostaNumero rNumero = r as RespostaNumero;
                        resposta.Valor = rNumero.Valor;
                        break;
                    case eTipoPergunta.Data:
                        RespostaData rData = r as RespostaData;
                        resposta.Valor = rData.Valor;
                        break;
                    default:
                        throw new NotImplementedException();
                }

                return resposta;
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected Resposta CriarResposta(RespostaDTO respostaDTO, eTipoPergunta tipoPergunta)
        {
            Resposta resposta = null;
            switch (tipoPergunta)
            {
                case eTipoPergunta.Texto:
                    resposta = CreateInstance<RespostaTexto>();
                    break;
                case eTipoPergunta.Anexo:
                    resposta = CreateInstance<RespostaAnexo>();
                    break;
                case eTipoPergunta.EscolhaUnica:
                    resposta = CreateInstance<RespostaUnica>();
                    break;
                case eTipoPergunta.Numero:
                    resposta = CreateInstance<RespostaNumero>();
                    break;
                case eTipoPergunta.Data:
                    resposta = CreateInstance<RespostaData>();
                    break;
                case eTipoPergunta.Grade:
                    resposta = CreateInstance<RespostaGrade>();
                    break;
                case eTipoPergunta.MultiplaEscolha:
                    resposta = CreateInstance<RespostaMultipla>();
                    break;
                default:
                    throw new NotImplementedException();
            }

            try
            {
                resposta.AtribuirResposta(respostaDTO);
            }
            catch (Exception)
            {

                throw;
            }

            return resposta;
        }

        protected LeiautePergunta CriarLeiautePergunta(LeiautePerguntaDTO leiautePergunta)
        {
            LeiautePergunta result = new LeiautePergunta
            {
                LeiautePerguntaID = leiautePergunta.LeiautePerguntaID,
                PerguntaID = leiautePergunta.PerguntaID,
                LeiauteItem = leiautePergunta.LeiautesPerguntaItem.Select(d =>
                new LeiautePerguntaItem
                {
                    LeiautePerguntaID = d.LeiautePerguntaID,
                    LeiautePerguntaItemID = d.LeiautePerguntaItemID,
                    Responsivo = d.TamanhoTela,
                    Tamanho = d.Coluna,
                }).ToList()
            };

            return result;
        }

        public T CreateInstance<T>() where T : class
        {
            using (IRepository<T> repo = factory.Get().GetRepository<T>())
            {
                return repo.Create();
            }
        }

        public ModeloDeFormulario ExcluirModeloFormulario(long id)
        {
            using (IRepository<ModeloDeFormulario> repo = factory.Get().GetRepository<ModeloDeFormulario>())
            {
                ModeloDeFormulario modelo = repo.GetQuery().Single(d => d.ModeloFormularioID == id);
                repo.Delete(modelo);
                return modelo;
            }
        }

        public RespostaModeloDeFormulario ExcluirRespostaModeloFormulario(long id)
        {
            using (IRepository<RespostaModeloDeFormulario> repo = factory.Get().GetRepository<RespostaModeloDeFormulario>())
            {
                RespostaModeloDeFormulario resposta = repo.GetQuery().Single(d => d.RespostaModeloFormularioID == id);
                repo.Delete(resposta);
                return resposta;
            }
        }

        public void Dispose()
        {

        }


        #endregion

    }
}
