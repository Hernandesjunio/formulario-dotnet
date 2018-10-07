using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Formulario.WebApi.Controllers
{
    public abstract class BaseAPIController : ApiController
    {
        protected string UserID => "ADMIN";
        protected const string Response = "ResponseObject";
    }
}