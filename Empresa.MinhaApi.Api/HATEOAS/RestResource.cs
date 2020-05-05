using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Empresa.MinhaApi.Api.HATEOAS
{
    public abstract class RestResource
    {
        //prop  tab tab  --comando para criar propriedade 
        public List<RestLink> Links { get; set; } = new List<RestLink>(); //As linkagens vinculadas ao recurso retornado pela api

    }
}