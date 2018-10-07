using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;

namespace SqlDataBase.Repositories
{
    public class RepositoryFileDbUsuario : IRepositoryFileDbUsuario
    {        
        public RepositoryFileDbUsuario()
        {

        }
        public List<Usuario> GetDataInFile(string path)
        {
            var amigos = new List<Usuario>();

            var arquivo = new System.IO.StreamReader(path);
            while (!arquivo.EndOfStream)
            {
                var amigo = new Usuario
                {
                    Id = long.Parse(arquivo.ReadLine()),
                    Nome = arquivo.ReadLine(),
                    Nascimento = DateTime.Parse(arquivo.ReadLine())
                };
                amigos.Add(amigo);
            }
            arquivo.Close();
            return amigos;
        }

        public void WriteInFile(List<Usuario> amigos, string path)
        {
            var file = new System.IO.StreamWriter(path);

            for (int i = 0; i < amigos.Count; i++)
            {
                file.WriteLine(amigos[i].Id);
                file.WriteLine(amigos[i].Nome);
                file.WriteLine(amigos[i].Nascimento);
            }
            file.Close();
        }
    }
}
