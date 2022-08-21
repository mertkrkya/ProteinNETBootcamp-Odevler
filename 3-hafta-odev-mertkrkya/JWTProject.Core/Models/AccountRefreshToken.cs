using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Core.Models
{
    public class AccountRefreshToken
    {
        public int AccountId { get; set; }
        public string Code { get; set; }
        public DateTime Expiration { get; set; }
    }
}
