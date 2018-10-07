using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.SqlDataBase
{
    public class Db_Sql
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader DataReader;
        public String StrConn { get; set; }

        public bool Conectar()
        {
            conn = new SqlConnection();
            conn.ConnectionString = StrConn;
            conn.Open();
            return conn.State == System.Data.ConnectionState.Open;
        }

        public bool Desconectar()
        {
            cmd.Dispose();
            cmd = null;
            conn.Close();
            conn.Dispose();
            conn = null;
            return true;
        }

        public bool Executar(String sql, bool select)
        {
            cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = System.Data.CommandType.Text;

            if (select)
            {
                DataReader = cmd.ExecuteReader();
                return DataReader != null;
            }
            else
            {
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable GetData(String nomeTabela)
        {
            DataTable dt = new DataTable(nomeTabela);
            dt.Load(DataReader);
            return dt;
        }
    }
}
