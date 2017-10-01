﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CamadaDados;
using System.Data;

namespace CamadaNegocio
{
    public class NCategoria
    {

        // Método Inserir
        public static string Inserir(string nome, string descricao)
        {
            DCategoria Obj = new CamadaDados.DCategoria();
            Obj.Nome = nome;
            Obj.Descricao = descricao;

            return Obj.Inserir(Obj);
        }

        // Método Editar
        public static string Editar(int idcategoria, string nome, string descricao)
        {
            DCategoria Obj = new CamadaDados.DCategoria();
            Obj.Idcategoria = idcategoria;
            Obj.Nome = nome;
            Obj.Descricao = descricao;

            return Obj.Editar(Obj);
        }

        // Método Excluir
        public static string Exluir(int idcategoria)
        {
            DCategoria Obj = new CamadaDados.DCategoria();
            Obj.Idcategoria = idcategoria;

            return Obj.Inserir(Obj);
        }

        // Método Mostrar
        public static DataTable Mostrar()
        {
            return new DCategoria().Mostrar();
        }

        // Método Buscar Nome
        public static DataTable BuscarNome(string textobuscar)
        {
            DCategoria Obj = new CamadaDados.DCategoria();
            Obj.TextoBuscar = textobuscar;

            return Obj.BuscarNome(Obj);
        }
    }
}
