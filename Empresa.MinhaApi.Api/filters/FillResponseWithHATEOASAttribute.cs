using Empresa.MinhaApi.Api.HATEOAS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Empresa.MinhaApi.Api.filters
{
    //Trafegar estados (representações) e serviços (links)
    //Trafega as informações e o que pode ser feito com cada elemento dentro do response

    public class FillResponseWithHATEOASAttribute : ActionFilterAttribute
    {
        //Depois que foi executada
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //Só aplicar hateoas se o meu accept for 'application/hal+json'
            if (actionExecutedContext.Response.IsSuccessStatusCode && 
                actionExecutedContext.Request.Headers.SelectMany(s => s.Value).Any(a => a.Contains("hal"))) { //Verifica se deu sucesso ou não e contiver hal

                ObjectContent responseContent = actionExecutedContext.Response.Content as ObjectContent; //No formato de um ObjectContent
                object responseValue = responseContent.Value;
                RestResourceBuilder.BuildResource(responseValue, actionExecutedContext.Request);
            }
        }
    }
}