using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Empresa.MinhaApi.Api.Providers;

[assembly: OwinStartup(typeof(Empresa.MinhaApi.Api.Startup))]

namespace Empresa.MinhaApi.Api
{
    /*
        Dependências necessárias: 
            WebApi.Owin 5.2.3
            Owin.Host.SystemWeb 3.1.0
            Owin.Security.OAuth 3.1.0 
            Owin.Cors 3.1.0
    */

   //grant_type especifica de quem fazerá a verificação se aquele usuário é válido ou não (ex.: password)

    //Criada a classe OwinStartup
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //Configurar OAuth
            ConfigureOAuth(app);

            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll); //Permitir de todos os locais/origens
            app.UseWebApi(config);

        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions oauthOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true, //Permitindo ocorra sem criptografia (http)
                TokenEndpointPath = new PathString("/token"), //Irá informar o token
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(50), //Expiração do token
                Provider = new SimpleAuthServerProvider() //Configuração do Provider
            };
            app.UseOAuthAuthorizationServer(oauthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()); //Configuração padrão
        }
    }
}
