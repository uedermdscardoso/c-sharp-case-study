using Empresa.MinhaApi.Api.filters;
using Empresa.MinhaApi.Api.Formatters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Empresa.MinhaApi.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //Configuração do json
            var jsonFormatter = config.Formatters.JsonFormatter;
            jsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(), //Faz com que as palavras venham no formado Camel Case (primeira letra da primeira palavra em minúcula)
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
            };

            config.Formatters.Add(new CsvMediaTypeFormatter()); //Registrando o formatador csv - filtro global

            //Configuração do xml
            // var xmlFormatter = config.Formatters.XmlFormatter;
            //config.Formatters.Remove(xmlFormatter); //Não sabe mais em responder no formato xml

            //Registrando FillResponseWithHATEOASAttribute
            config.Filters.Add(new FillResponseWithHATEOASAttribute()); //filtro global

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
