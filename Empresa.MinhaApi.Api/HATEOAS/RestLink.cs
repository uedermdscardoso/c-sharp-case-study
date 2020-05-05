using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Empresa.MinhaApi.Api.HATEOAS
{
    //Esta classe vai conter uma estrutura que representará o processo de linkagem descrito pelo hateoas
    public class RestLink
    {
        public string Rel { get; set; } //Significado do link - Link de atualização, de exclusão 
        public string Href { get; set; } //Link propriamente dito

    }
}