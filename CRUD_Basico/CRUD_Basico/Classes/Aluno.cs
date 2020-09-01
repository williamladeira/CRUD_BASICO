﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Basico.Classes
{
    public class Aluno : AcessoDados
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DtNascimento { get; set; }
        public bool Ativo { get; set; }

        public Aluno()
        {

        }

        public Aluno(string nome, DateTime dtNascimento, bool ativo)
        {
            Nome = nome;
            DtNascimento = dtNascimento;
            Ativo = ativo;
        }
        //[Jsonconstructor]
        public Aluno(int id, string nome, DateTime dtNascimento, bool ativo)
        {
            Id = id;
            Nome = nome;
            DtNascimento = dtNascimento;
            Ativo = ativo;
        }       


        public void Cadastrar() 
        {
            try
            {
                string nomeprocedure = "SP_inserealuno";
                List<SqlParameter> parametros = new List<SqlParameter>

            {
                new SqlParameter("nome",Nome),
                new SqlParameter("dtNasc",DtNascimento),
                new SqlParameter("ativo",Ativo)
            };

                DataSet ds = Consultar(nomeprocedure, parametros);
                if (ds != null && ds.Tables != null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count > 0) 
                {
                    Id =int.Parse (ds.Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    throw new Exception("nao houve retorno do banco");
                }

            }
            catch (Exception)
            {

                throw;
            }
            
        }


        public List<Aluno>obterAlunos()
        {
            try
            {
                string nomeprocedure = "SP_ObterAlunos";

                DataSet ds = Consultar(nomeprocedure, null);

                List<Aluno> alunos = new List<Aluno>();
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow  linha in ds.Tables[0].Rows )
                    {
                        alunos.Add(new Aluno(linha.Field<int>(0), linha.Field<string>(1), linha.Field<DateTime>(2), linha.Field<bool>(3)));
                    }
                    return alunos;
                }
                else
                {
                    throw new Exception("nao houve retorno do banco");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        
    }
}
