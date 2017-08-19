using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Users.Infrastructure
{
    /// <summary>
    /// 模拟一个非应用程序内的 Claim 来源
    /// </summary>
    public class LocationClaimsProvider
    {
        public static IEnumerable<Claim> GetClaims(ClaimsIdentity user)
        {
            var claims = new List<Claim>();
            if (user.Name.ToUpper() == "ROB")
            {
                claims.Add(CreateClaim(ClaimTypes.PostalCode, "BJ 100000"));
                claims.Add(CreateClaim(ClaimTypes.StateOrProvince, "BJ"));
            }
            else
            {
                claims.Add(CreateClaim(ClaimTypes.PostalCode, "NY 470000"));
                claims.Add(CreateClaim(ClaimTypes.StateOrProvince, "NY"));
            }
            return claims;
        }

        private static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String, "RemoteClaims");
        }
    }
}