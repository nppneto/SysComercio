using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CamadaDados;
using System.Data;

namespace CamadaNegocio
{
    public class NApresentacao
    {
        // Método Inserir
        public static string Inserir(string nome, string descricao)
        {
            DApresentacao Obj = new CamadaDados.DApresentacao();
            Obj.Nome = nome;
            Obj.Descricao = descricao;

            return Obj.Inserir(Obj);
        }

        // Método Editar
        public static string Editar(int idapresentacao, string nome, string descricao)
        {
            DApresentacao Obj = new CamadaDados.DApresentacao();
            Obj.Idapresentacao = idapresentacao;
            Obj.Nome = nome;
            Obj.Descricao = descricao;

            return Obj.Editar(Obj);
        }

        // Método Excluir
        public static string Excluir(int idapresentacao)
        {
            DApresentacao Obj = new CamadaDados.DApresentacao();
            Obj.Idapresentacao = idapresentacao;

            return Obj.Excluir(Obj);
        }

        // Método Consultar
        public static DataTable Consultar()
        {
            return new DApresentacao().Consultar();
        }

        // Método Buscar Nome
        public static DataTable ConsultarPorNome(string textobuscar)
        {
            DApresentacao Obj = new CamadaDados.DApresentacao();
            Obj.TextoBuscar = textobuscar;

            return Obj.ConsultarPorNome(Obj);
        }
    }
}
