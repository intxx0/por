using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Owin.Security.OAuth;

namespace WebApiKor
{

   
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            context.Validated();
        }


        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            //context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            try
            {
                var userId = new List<int>();
                var username = context.UserName;
                var senha = context.Password;

                using (ModeloBancoEntities banco = new ModeloBancoEntities())
                {
                    var usuarios = banco.usuario.Where(user => user.email == username && user.senha == senha).ToList();

                    if (usuarios.Count <= 0)
                    {
                        context.SetError("invalid_grant", "Usuário ou senha não encontrado!");
                        return;
                    }

                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                    identity.AddClaim(new Claim(ClaimTypes.Name, username));


                    var roles = new List<string>();

                    foreach (var usu in usuarios)
                    {
                        roles.Add(usu.grupo.nome);
                    }


                    foreach (var role in roles)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, role));
                    }

                    GenericPrincipal principal = new GenericPrincipal(identity, roles.ToArray());
                    Thread.CurrentPrincipal = principal;

                    context.Validated(identity);

                }

            }
            catch
            {

                context.SetError("invalid_grant", "Falha ao se autenticar!");
            }

        }

    }
}