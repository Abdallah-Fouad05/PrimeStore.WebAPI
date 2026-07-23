using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PrimeStore.data.Helper
{
    public static class ClaimsStore
    {
        public static List<Claim> claims = new()
        {
            new Claim("CreateProduct","false"),
            new Claim("UpdateProduct","false"),
            new Claim("DeleteProduct","false")
        };
    }
}
