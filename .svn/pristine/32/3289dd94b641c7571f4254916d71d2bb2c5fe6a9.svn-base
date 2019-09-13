using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Owin.Security.OAuth;
using System.Data.Entity;
using System.Web;
using System.Web.SessionState;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;



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

                SHA1Managed sha1 = new SHA1Managed();

                var senha = Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(context.Password)));

                using (ModeloBancoEntities banco = new ModeloBancoEntities())
                {
                    var usuarios = banco.usuario.Where(user => user.email == username && user.senha == senha).ToList();

                    if (usuarios.Count <= 0)
                    {
                        context.SetError("invalid_grant", "USER_NOT_FOUND");
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

                    usuario row = new usuario
                    {
                        usuario_id = usuarios[0].usuario_id,
                        qtd_acessos = (Convert.ToInt32(usuarios[0].qtd_acessos) + 1)
                    };

                    ModeloBancoEntities db = new ModeloBancoEntities();

                    db.usuario.Attach(row);
                    db.Entry(row).Property(x => x.qtd_acessos).IsModified = true;
                    db.SaveChanges();

                }

            }
            catch(Exception ex)
            {

                context.SetError("invalid_grant", "Falha ao se autenticar!" + ex.Message);
            }

        }

    }
}