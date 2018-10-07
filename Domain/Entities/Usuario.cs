using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario
    {
        public long Id { get; set; }
        public String Nome { get; set; }
        public String SobreNome { get; set; }
        public String Email { get; set; }
        public DateTime Nascimento { get; set; }
        public int ProxAniv { get; set; }
    }
}
