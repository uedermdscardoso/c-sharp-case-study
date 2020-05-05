using Empresa.MinhaApi.Api.HATEOAS.ResourceBuildersInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Empresa.MinhaApi.Api.DTOs;
using System.Web.Http.Routing;

namespace Empresa.MinhaApi.Api.HATEOAS.ResourceBuilders.Impl
{
    //Irá lidar somente com AlunoDTO
    public class AlunoDTOResourceBuilder : IResourceBuilder
    {
        public void BuildResouce(object resource, HttpRequestMessage request)
        {
            AlunoDTO alunoDTO = resource as AlunoDTO;
            if (alunoDTO == null) { //Tenta fazer o cash - Não consegue lidar com esse DTO 
                throw new ArgumentException($"Era esperado um AlunoDTO, foi enviado um {resource.GetType().Name}");
            }

            UrlHelper urlHelper = new UrlHelper(request);
            string alunoDTORoute = urlHelper.Link("DefaultApi", new { Controller = "Alunos", id = alunoDTO.id }); //Basear na rota nomeada como 'DefaultApi'
            alunoDTO.Links.Add(new RestLink {
                Rel = "self", //Recuperar o próprio DTO
                Href = alunoDTORoute
            });
            alunoDTO.Links.Add(new RestLink {
                Rel = "edit",
                Href = alunoDTORoute
            });
            alunoDTO.Links.Add(new RestLink {
                Rel = "delete",
                Href = alunoDTORoute
            });
        }
    }
}