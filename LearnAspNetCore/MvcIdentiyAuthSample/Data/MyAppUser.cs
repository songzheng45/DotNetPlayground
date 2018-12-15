using System;
using Microsoft.AspNetCore.Identity;

namespace MvcIdentiyAuthSample.Data
{
    public class MyAppUser : IdentityUser<int>
    {
        public MyAppUser()
        {
        }
    }

    public class MyAppRole : IdentityRole<int>
    {
        public MyAppRole()
        {
        }
    }
}
