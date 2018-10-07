using Domain.Entities;
using Domain.Interfaces.Repositories;
using Persistencia.SqlDataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.Repositories
{
    public class RepositoryUsuario : IRepositoryUsuario
    {
        private readonly String str_conn = @"Data Source=(LocalDB)\MSSQLLocalDB;
                                AttachDbFilename=C:\Users\Alexander\source\repos\AgendaConsole\SqlDataBase\DataSource\agenda_db.mdf;
                                Integrated Security=True";

        public RepositoryUsuario()
        {
        }

        public bool Save(Usuario usuario)
        {
            bool response = false;
            Db_Sql banco = new Db_Sql
            {
                StrConn = str_conn
            };

            if (banco.Conectar())
            {
                response = banco.Executar(String.Format("INSERT INTO Amigos "
                                          + "(nome, sobrenome, email, nascimento)"
                                          + "VALUES" + "('{0}','{1}','{2}','{3}')",
                                          usuario.Nome,
                                          usuario.SobreNome,
                                          usuario.Email,
                                          usuario.Nascimento.ToString("yyyy-MM-dd")), false);

                banco.Desconectar();
            }

            return response;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Usuario> GetAll(String nome)
        {
            List<Usuario> amigos = new List<Usuario>();
            DataTable dt = new DataTable();
            Db_Sql banco = new Db_Sql
            {
                StrConn = str_conn
            };

            if (banco.Conectar())
            {
                banco.Executar("SELECT * from Amigos WHERE nome like '%"
                                + nome + "%'", true);
                dt = banco.GetData("Amigos");
                banco.Desconectar();
            }

            amigos = dt.AsEnumerable().Select(row => new Usuario
            {
                Id = (int)row["id"],
                Nome = row["nome"].ToString(),
                SobreNome = row["sobrenome"].ToString(),
                Email = row["email"].ToString(),
                Nascimento = (DateTime)row["nascimento"]
            }).ToList();
            return amigos;
        }

        public Usuario GetById(long? id)
        {
            List<Usuario> amigos = new List<Usuario>();
            Usuario aux;
            DataTable dt = new DataTable();
            Db_Sql banco = new Db_Sql
            {
                StrConn = str_conn
            };

            if (banco.Conectar())
            {
                banco.Executar("SELECT * from Amigos WHERE id = "
                                + id.ToString(), true);
                dt = banco.GetData("Amigos");
                banco.Desconectar();
            }

            foreach (DataRow dr in dt.Rows)
            {
                aux = new Usuario
                {
                    Id = (int)dr["id"],
                    Nome = dr["nome"].ToString(),
                    SobreNome = dr["sobrenome"].ToString(),
                    Email = dr["email"].ToString(),
                    Nascimento = (DateTime)dr["nascimento"]
                };
                amigos.Add(aux);
            }
            return amigos.Count > 0 ? amigos[0] : null;
        }

        public bool Remove(long id)
        {
            bool response = false;
            Db_Sql banco = new Db_Sql
            {
                StrConn = str_conn
            };

            if (banco.Conectar())
            {
                response = banco.Executar(String.Format("DELETE Amigos "
                                                        + " WHERE id = {0} ",
                                                        id.ToString()), false);

                banco.Desconectar();
            }

            return response;
        }

        public bool Update(Usuario usuario)
        {
            bool response = false;
            Db_Sql banco = new Db_Sql
            {
                StrConn = str_conn
            };

            if (banco.Conectar())
            {
                response = banco.Executar(String.Format("UPDATE Amigos SET nome = '{0}', "
                                                        + "sobrenome = '{1}', "
                                                        + "email = '{2}', "
                                                        + "nascimento = '{3}'"
                                                        + " WHERE id = '{4}'",
                                                        usuario.Nome,
                                                        usuario.SobreNome,
                                                        usuario.Email,
                                                        usuario.Nascimento.ToString("yyyy-MM-dd"),
                                                        usuario.Id), false);

                banco.Desconectar();
            }

            return response;
        }
        
    }
}
