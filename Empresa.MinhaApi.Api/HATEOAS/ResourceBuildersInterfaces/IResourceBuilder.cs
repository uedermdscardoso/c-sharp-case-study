using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Empresa.MinhaApi.Api.HATEOAS.ResourceBuildersInterfaces
{
    //Construtor de Resources
    interface IResourceBuilder
    {
        void BuildResouce(object resource, HttpRequestMessage request); // resource a ser construído, request para poder construir os links
    }
}
