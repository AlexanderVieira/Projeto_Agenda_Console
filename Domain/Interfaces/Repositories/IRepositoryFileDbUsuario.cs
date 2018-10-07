using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IRepositoryFileDbUsuario
    {
        void WriteInFile(List<Usuario> amigos, String path);
        List<Usuario> GetDataInFile(String path);
    }
}
