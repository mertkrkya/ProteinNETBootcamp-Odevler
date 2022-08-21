using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTProject.Core.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModTime { get; set; }
    }
}
