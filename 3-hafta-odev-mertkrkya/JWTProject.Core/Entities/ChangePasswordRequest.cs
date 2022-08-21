using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Core.Entities
{
    public class ChangePasswordRequest
    {
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string confirmPassword { get; set; }

    }
}
