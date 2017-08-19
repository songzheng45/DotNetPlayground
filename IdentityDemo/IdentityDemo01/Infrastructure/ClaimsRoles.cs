using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Users.Infrastructure
{
    public class ClaimsRoles
    {
        /// <summary>
        /// 创建一个基于Claim的角色"BJStaff", 代表地址在北京的员工
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static IEnumerable<Claim> CreateRolesFromClaims(ClaimsIdentity user)
        {
            var claims = new List<Claim>();
            if (user.HasClaim(x => x.Issuer == "RemoteClaims" && x.Type == ClaimTypes.StateOrProvince && x.Value == "BJ")
                && user.HasClaim(x => x.Type == ClaimTypes.Role && x.Value == "Employees"))
            {
                claims.Add(new Claim(ClaimTypes.Role, "BJStaff"));
            }
            return claims;
        }
    }
}