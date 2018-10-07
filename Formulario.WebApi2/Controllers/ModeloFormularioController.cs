using Formulario.Business;
using Formulario.Business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Data.Entity;
using Formulario.Business.Class;

namespace Formulario.WebApi2.Controllers
{
    [RoutePrefix("api/modelodeformulario")]
    public class ModeloFormularioController : BaseAPIController
    {
        private readonly Lazy<IFormularioService> svc;

        public ModeloFormularioController(Lazy<IFormularioService> svc)
        {
            this.svc = svc;
        }

        //[Route("")]
        //public async Task<IHttpActionResult> Get()
        //{
        //    return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(new Filter<ModeloDeFormulario>() { PageSize = 10, PageIndex = 0, ColumnsFilter = null, SortBy = null, Descending = false }));
        //}

        [HttpGet]
        [Route("all")]
        public async Task<IHttpActionResult> GetAll([FromUri]Filter<ModeloDeFormulario> filter)
        {
            var queryResposta = svc.Value.BuscarRespostaModeloDeFormulario(false);
            var query = svc.Value.BuscarModeloDeFormulario(false);

            if (string.IsNullOrEmpty(filter.SortBy))
                filter.SortBy = "ModeloFormularioID";

            var result = await filter.ApplyFilter(query)
                .Select(c => new
                {
                    c.ModeloFormularioID,
                    c.Descricao,
                    c.ControleAtualizacao.UsuarioID,
                    c.ControleAtualizacao.Data,
                    PossiModeloRespondido = queryResposta.Any(d => d.ModeloDeFormularioID == c.ModeloFormularioID)
                }).ToListAsync();

            return Ok(result);

        }

        // GET api/<controller>/5
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> Get(long id)
        {
            var result = await Task.FromResult(svc.Value.AbrirFormulario(id));
            return Ok(result);
        }

        [HttpPost()]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody]ModeloFormularioDTO value)
        {
            var modelo = await Task.FromResult(svc.Value.GravarModeloFormulario(value, UserID));

            await svc.Value.CommitAsync(UserID);

            if (Request.Headers.TryGetValues("ResultObject", out IEnumerable<string> lst))
                return Ok(svc.Value.AbrirFormulario(modelo.ModeloFormularioID));

            return Ok(modelo.ModeloFormularioID);
        }

        [HttpPut()]
        [Route("")]
        public async Task<IHttpActionResult> Put([FromBody]ModeloFormularioDTO value)
        {
            var modelo = await Task.FromResult(svc.Value.GravarModeloFormulario(value, UserID));

            await svc.Value.CommitAsync(UserID);

            if (Request.Headers.TryGetValues("ResultObject", out IEnumerable<string> lst))
                return Ok(svc.Value.AbrirFormulario(modelo.ModeloFormularioID));
            
            return Ok(modelo.ModeloFormularioID);
        }

        [HttpDelete()]
        [Route("")]
        public async Task<IHttpActionResult> Delete(long id)
        {
            object result;

            if (Request.Headers.TryGetValues(Response, out IEnumerable<string> lst))
                result = svc.Value.AbrirFormulario(id);
            else
                result = id;

            await Task.FromResult(svc.Value.ExcluirModeloFormulario(id));

            await svc.Value.CommitAsync(UserID);

            return Ok(result);
        }
    }
}