using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CamadaDados
{
    public class DApresentacao
    {
        private int _Idapresentacao;
        private string _Nome;
        private string _Descricao;
        private string _TextoBuscar;

        public int Idapresentacao
        {
            get
            {
                return _Idapresentacao;
            }

            set
            {
                _Idapresentacao = value;
            }
        }

        public string Nome
        {
            get
            {
                return _Nome;
            }

            set
            {
                _Nome = value;
            }
        }

        public string Descricao
        {
            get
            {
                return _Descricao;
            }

            set
            {
                _Descricao = value;
            }
        }

        public string TextoBuscar
        {
            get
            {
                return _TextoBuscar;
            }

            set
            {
                _TextoBuscar = value;
            }
        }

        public DApresentacao() { }

        public DApresentacao(int idapresentacao, string nome, string descricao)
        {
            this.Idapresentacao = idapresentacao;
            this.Nome = nome;
            this.Descricao = descricao;
        }

        // Método Inserir
        public string Inserir(DApresentacao Apresentacao)
        {
            string resp = "";

            SqlConnection SqlCon = new SqlConnection();
            try
            {
                // código inserção
                SqlCon.ConnectionString = Conexao.con;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spinserir_apresentacao";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdapresentacao = new SqlParameter();
                ParIdapresentacao.ParameterName = "@idapresentacao";
                ParIdapresentacao.SqlDbType = SqlDbType.Int;
                ParIdapresentacao.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParIdapresentacao);

                SqlParameter ParNome = new SqlParameter();
                ParNome.ParameterName = "@nome";
                ParNome.SqlDbType = SqlDbType.VarChar;
                ParNome.Size = 50;
                ParNome.Value = Apresentacao.Nome;
                SqlCmd.Parameters.Add(ParNome);

                SqlParameter ParDescricao = new SqlParameter();
                ParDescricao.ParameterName = "@descricao";
                ParDescricao.SqlDbType = SqlDbType.VarChar;
                ParDescricao.Size = 50;
                ParDescricao.Value = Apresentacao.Descricao;
                SqlCmd.Parameters.Add(ParDescricao);

                // Executar o comando

                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "Registro não foi inserido";

            }
            catch (Exception ex)
            {
                // mensagem de erro
                resp = ex.Message;
            }

            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }

            return resp;
        }

        // Método Editar
        public string Editar(DApresentacao Apresentacao)
        {
            string resp = "";

            SqlConnection SqlCon = new SqlConnection();
            try
            {
                // código edição
                SqlCon.ConnectionString = Conexao.con;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "speditar_apresentacao";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdapresentacao = new SqlParameter();
                ParIdapresentacao.ParameterName = "@idapresentacao";
                ParIdapresentacao.SqlDbType = SqlDbType.Int;
                ParIdapresentacao.Value = Apresentacao.Idapresentacao;
                SqlCmd.Parameters.Add(ParIdapresentacao);

                SqlParameter ParNome = new SqlParameter();
                ParNome.ParameterName = "@nome";
                ParNome.SqlDbType = SqlDbType.VarChar;
                ParNome.Size = 50;
                ParNome.Value = Apresentacao.Nome;
                SqlCmd.Parameters.Add(ParNome);

                SqlParameter ParDescricao = new SqlParameter();
                ParDescricao.ParameterName = "@descricao";
                ParDescricao.SqlDbType = SqlDbType.VarChar;
                ParDescricao.Size = 50;
                ParDescricao.Value = Apresentacao.Descricao;
                SqlCmd.Parameters.Add(ParDescricao);

                // Executar o comando

                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "A edição não foi concluída!";

            }
            catch (Exception ex)
            {
                // mensagem de erro
                resp = ex.Message;
            }

            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return resp;
        }

        // Método Excluir
        public string Excluir(DApresentacao Apresentacao)
        {
            string resp = "";

            SqlConnection SqlCon = new SqlConnection();
            try
            {
                // código exclusão
                SqlCon.ConnectionString = Conexao.con;
                SqlCon.Open();

                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spdeletar_apresentacao";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdapresentacao = new SqlParameter();
                ParIdapresentacao.ParameterName = "@idapresentacao";
                ParIdapresentacao.SqlDbType = SqlDbType.Int;
                ParIdapresentacao.Value = Apresentacao.Idapresentacao;
                SqlCmd.Parameters.Add(ParIdapresentacao);

                // Executar o comando

                resp = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "A exclusão não foi concluída!";

            }
            catch (Exception ex)
            {
                // mensagem de erro
                resp = ex.Message;
            }

            finally
            {
                if (SqlCon.State == ConnectionState.Open)
                    SqlCon.Close();
            }
            return resp;
        }

        // Método Consultar
        public DataTable Consultar()
        {
            DataTable DtResultado = new DataTable("apresentacao");
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon.ConnectionString = Conexao.con;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spconsultar_apresentacao";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                // quando precisar guardar informações de consulta e exibir em tabelas
                // listbox, use um DataAdapter
                SqlDataAdapter SqlData = new SqlDataAdapter(SqlCmd);
                SqlData.Fill(DtResultado);

            }
            catch (Exception)
            {
                DtResultado = null;
            }

            //finally
            //{
            //    if (SqlCon.State == ConnectionState.Open)
            //        SqlCon.Close();
            //}

            return DtResultado;

        }

        public DataTable ConsultarPorNome(DApresentacao Apresentacao)
        {
            DataTable DtResultado = new DataTable("apresentacao");
            SqlConnection SqlCon = new SqlConnection();

            try
            {
                SqlCon.ConnectionString = Conexao.con;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spconsultar_nome_apresentacao";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                // quando precisar guardar informações de consulta e exibir em tabelas
                // listbox, use um DataAdapter

                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 50;
                ParTextoBuscar.Value = Apresentacao.TextoBuscar;
                SqlCmd.Parameters.Add(ParTextoBuscar);

                SqlDataAdapter SqlData = new SqlDataAdapter(SqlCmd);
                SqlData.Fill(DtResultado);

            }
            catch (Exception)
            {
                DtResultado = null;
            }

            //finally
            //{
            //    if (SqlCon.State == ConnectionState.Open)
            //        SqlCon.Close();
            //}

            return DtResultado;

        }

    }
}
