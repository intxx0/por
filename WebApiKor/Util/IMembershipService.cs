using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiKor.Util
{
    public interface IMembershipService
    {
        MembershipContext ValidateUser(string email, string senha, int grupo);
        usuario CreateUser(string nome, string email, string senha, int perfilId, bool flgMaster);
        usuario GetUser(int userId);
    }
}