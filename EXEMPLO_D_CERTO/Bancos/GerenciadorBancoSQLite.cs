using EXEMPLO_D_CERTO.Classes;
using EXEMPLO_D_CERTO.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EXEMPLO_D_CERTO.Bancos
{
    public class GerenciadorBancoSQLite<Elemento> : IBanco<Elemento> where Elemento : EntidadeBase
    {
        private const string CAMINHO_BANCO = @"C:/Dados/banco_temp.sqlite";
        private static SQLiteConnection sqliteConnection;
        public GerenciadorBancoSQLite()
        {
            CriarTabelaSQlite();

            var naoExistePastaDados = !File.Exists(@"C:/Dados");
            if (naoExistePastaDados)
            {
                File.Create(@"C:/Dados");
            }

            var bancoNaoExiste = !File.Exists(CAMINHO_BANCO);
            if (bancoNaoExiste)
            {
                SQLiteConnection.CreateFile(CAMINHO_BANCO);
            }
        }

        private string PegaNomeTabela() => typeof(Elemento).Name;

        private static SQLiteConnection DbConnection()
        {
            sqliteConnection = new SQLiteConnection("Data Source=c:\\dados\\Cadastro.sqlite; Version=3;");
            sqliteConnection.Open();
            return sqliteConnection;
        }

        public static void CriarTabelaSQlite()
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Funcionario(id int, Nome Varchar(300), Salario Real)";
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = "CREATE TABLE IF NOT EXISTS Documento(id int, Nome Varchar(300), Descricao VarChar(300))";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Elemento> PegaColecao()
        {
            SQLiteDataAdapter dataAdaptador = null;
            DataTable tabelaRetornada = new DataTable();
            try
            {
                using (var comando = DbConnection().CreateCommand())
                {
                    comando.CommandText = $"SELECT * FROM {PegaNomeTabela()}";
                    dataAdaptador = new SQLiteDataAdapter(comando.CommandText, DbConnection());
                    dataAdaptador.Fill(tabelaRetornada);

                    return ConvertDataTable<Elemento>(tabelaRetornada);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Adiciona(Elemento elemento)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    StringBuilder propriedadesSemArroba = new StringBuilder();
                    propriedadesSemArroba.Append("(");

                    var propriedades = typeof(Elemento).GetProperties().ToList();
                    foreach (var propriedade in propriedades)
                    {
                        var valorCampo = elemento.GetType().GetProperty(propriedade.Name).GetValue(elemento);

                        if (valorCampo == null)
                            continue;

                        if (propriedades.Last() == propriedade)
                        {
                            propriedadesSemArroba.Append($" {valorCampo} ");
                        }
                        else
                        {
                            propriedadesSemArroba.Append($" {valorCampo}, ");
                        }
                    }
                    propriedadesSemArroba.Append(")");

                    StringBuilder propriedadesComArroba = new StringBuilder();
                    propriedadesComArroba.Append("(");

                    foreach (var propriedade in propriedades)
                    {
                        var valorCampo = elemento.GetType().GetProperty(propriedade.Name).GetValue(elemento);

                        if (valorCampo == null)
                            continue;

                        if (propriedades.Last() == propriedade)
                        {
                            propriedadesComArroba.Append($" @{valorCampo} ");
                        }
                        else
                        {
                            propriedadesComArroba.Append($" @{valorCampo}, ");
                        }
                    }
                    propriedadesComArroba.Append(")");

                    cmd.CommandText = $"INSERT INTO {PegaNomeTabela()} {propriedadesSemArroba.ToString()} values ({propriedadesComArroba.ToString()})";
                    var comandoParaExecutar = $"UPDATE {PegaNomeTabela()} SET {propriedadesSemArroba.ToString()}";

                    foreach (var propriedade in typeof(Elemento).GetProperties())
                    {
                        var valorCampo = elemento.GetType().GetProperty(propriedade.Name).GetValue(elemento);

                        if (valorCampo == null)
                            continue;

                        cmd.Parameters.AddWithValue($"@{valorCampo}", valorCampo);
                    }

                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Remove(string id)
        {
            try
            {
                using (var cmd = DbConnection().CreateCommand())
                {
                    cmd.CommandText = $"DELETE FROM {PegaNomeTabela()} WHERE Id = @Id)";
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Atualiza(Elemento elementoAtualizado)
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    foreach (var propriedade in typeof(Elemento).GetProperties())
                    {
                        var valorCampo = elementoAtualizado.GetType().GetProperty(propriedade.Name).GetValue(elementoAtualizado);

                        if (valorCampo == null)
                            continue;

                        stringBuilder.Append($" {valorCampo} = @{valorCampo} ");
                    }

                    var comandoParaExecutar = $"UPDATE {PegaNomeTabela()} SET {stringBuilder.ToString()}";

                    foreach (var propriedade in typeof(Elemento).GetProperties())
                    {
                        var valorCampo = elementoAtualizado.GetType().GetProperty(propriedade.Name).GetValue(elementoAtualizado);

                        if (valorCampo == null)
                            continue;

                        cmd.Parameters.AddWithValue($"@{valorCampo}", valorCampo);
                    }

                    cmd.ExecuteNonQuery();
                    
                };

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void LimpaColecao()
        {
            try
            {
                using (var cmd = new SQLiteCommand(DbConnection()))
                {
                    cmd.CommandText = $"DELETE FROM {PegaNomeTabela()}";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            sqliteConnection.Dispose();
        }

        public IEnumerable<Elemento> PegaElementos()
        {
            SQLiteDataAdapter dataAdaptador = null;
            DataTable tabelaRetornada = new DataTable();
            try
            {
                using (var comando = DbConnection().CreateCommand())
                {
                    comando.CommandText = $"SELECT * FROM {PegaNomeTabela()}";
                    dataAdaptador = new SQLiteDataAdapter(comando.CommandText, DbConnection());
                    dataAdaptador.Fill(tabelaRetornada);

                    return ConvertDataTable<Elemento>(tabelaRetornada);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}

