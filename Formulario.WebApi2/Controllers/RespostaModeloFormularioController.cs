using Formulario.Business;
using Formulario.Business.Class;
using Formulario.Business.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Formulario.WebApi2.Controllers
{
    [RoutePrefix("api/respostamodelodeformulario")]
    public class RespostaModeloFormularioController : BaseAPIController
    {
        private readonly Lazy<IFormularioService> svc;

        public RespostaModeloFormularioController(Lazy<IFormularioService> svc)
        {
            this.svc = svc;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IHttpActionResult> GetAll([FromUri]Filter<RespostaModeloDeFormulario> filter)
        {
            var queryResposta = svc.Value.BuscarRespostaModeloDeFormulario(false);
            var query = svc.Value.BuscarRespostaModeloDeFormulario(false);
            var result = await filter.ApplyFilter(query)
                .Select(c => new
                {
                    c.RespostaModeloFormularioID,
                    QuantidadeRespostas = c.Respostas.Count,
                    c.ControleAtualizacao.UsuarioID,
                    c.ControleAtualizacao.Data,
                }).ToListAsync();

            return Ok(result);

        }
                
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(long id)
        {
            var result = await Task.FromResult(svc.Value.AbrirRespostaModeloFormulario(id));
            return Ok(result);
        }

        [HttpPost()]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]RespostaModeloFormularioDTO value)
        {
            var modelo = await Task.FromResult(svc.Value.GravarRespostaModeloFormulario(value, UserID));

            await svc.Value.CommitAsync(UserID);

            if (Request.Headers.TryGetValues("ResultObject", out IEnumerable<string> lst))
                return Ok(svc.Value.AbrirRespostaModeloFormulario(modelo.RespostaModeloFormularioID));

            return Ok(modelo.RespostaModeloFormularioID);
        }

        [HttpPut()]
        [Route("")]
        public async Task<IHttpActionResult> Put([FromBody]RespostaModeloFormularioDTO value)
        {
            var modelo = await Task.FromResult(svc.Value.GravarRespostaModeloFormulario(value, UserID));

            await svc.Value.CommitAsync(UserID);

            if (Request.Headers.TryGetValues("ResultObject", out IEnumerable<string> lst))
                return Ok(svc.Value.AbrirRespostaModeloFormulario(modelo.RespostaModeloFormularioID));

            return Ok(modelo.RespostaModeloFormularioID);
        }

        [HttpDelete()]
        [Route("")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            object result;

            if (Request.Headers.TryGetValues(Response, out IEnumerable<string> lst))
                result = svc.Value.AbrirRespostaModeloFormulario(id);
            else
                result = id;

            await Task.FromResult(svc.Value.ExcluirRespostaModeloFormulario(id));

            await svc.Value.CommitAsync(UserID);

            return Ok(result);
        }
    }
}