using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Empresa.MinhaApi.Api.Providers
{
    public class SimpleAuthServerProvider : OAuthAuthorizationServerProvider
    {
        //Verificar se aquele cliente tem permissão para acessar a api
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); //Qualquer um tem permissão de acessar a api
        }

        //Ocorre o processo de autenticação e validação (verificar se usuário e senha são válidos)
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin",new string[] {"*"}); 
            if(context.UserName != "usuario" || context.Password != "senha")
            {
                context.SetError("invalid_user_or_password","Usuário e/ou senha inválidos.");
                return;
            }
            ClaimsIdentity identity = new ClaimsIdentity(context.Options.AuthenticationType);
            context.Validated(identity);
        }
    }
}