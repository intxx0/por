using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace WebApiKor.Util
{
    public class MembershipContext
    {
        public IPrincipal Principal { get; set; }
        public usuario Usuario { get; set; }
        public bool IsValid()
        {
            return Principal != null;
        }
    }
}