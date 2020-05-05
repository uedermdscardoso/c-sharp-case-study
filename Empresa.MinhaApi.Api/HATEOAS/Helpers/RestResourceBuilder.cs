using Empresa.MinhaApi.Api.HATEOAS.ResourceBuildersInterfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;

namespace Empresa.MinhaApi.Api.HATEOAS.Helpers
{
    public class RestResourceBuilder
    {
        public static void BuildResource(object resource, HttpRequestMessage request)
        {
            IEnumerable enumerable = resource as IEnumerable; //Com cast silencioso
            Type dtoType;
            if (enumerable == null)
            {//é um objeto quando se é nulo
                dtoType = resource.GetType();
            } else {
                dtoType = resource.GetType().GetGenericArguments()[0];
            }

            if (dtoType.BaseType != typeof(RestResource))
            { // Se não estiver herdando RestResource
                throw new ArgumentException($"Era esperado um RestResource, porém, foi informado {resource.GetType().FullName}");
            }

            Assembly currentAssembly = Assembly.GetExecutingAssembly(); //Pega o assembly atual
            //Criar instância de maneira dinâmica
            IResourceBuilder resourceBuilder = (IResourceBuilder) Activator.CreateInstance(currentAssembly.GetType($"Empresa.MinhaApi.Api.HATEOAS.ResourceBuilders.Impl.{dtoType.Name}ResourceBuilder")); //Todos os builders ficarão na mesma namespace e terminar com 'ResourceBuilder'
            if (enumerable == null)
            { // É um objeto
                resourceBuilder.BuildResouce(resource, request);
            }
            else { // é uma lista
                foreach (var item in enumerable)
                {
                    resourceBuilder.BuildResouce(item, request);
                }
            }
        }
    }
}