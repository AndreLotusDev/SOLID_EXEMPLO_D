using EXEMPLO_D_ERRADO.Classes;
using LiteDB;
using System;
using System.Collections.Generic;

namespace EXEMPLO_D_ERRADO.Banco
{
    public class GerenciadorBanco<Elemento> where Elemento : EntidadeBase
    {
        public LiteDatabase Banco { get; private set; }
        public GerenciadorBanco()
        {
            var banco = new LiteDatabase(@"C:\Temp\MyData.db");
            Banco = banco;
        }

        public IEnumerable<Elemento> PegaColecao()
        {
            var colecao = MapeaTabela();
            return colecao.Query().ToList();
        }

        public ILiteCollection<Elemento> MapeaTabela() => Banco.GetCollection<Elemento>(typeof(Elemento).Name);

        public bool Adiciona(Elemento elemento)
        {
            elemento.Id = ObjectId.NewObjectId().ToString();
            var colecao = MapeaTabela();

            try
            {
                var elementoRetornado = colecao.Insert(elemento);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool Remove(string id)
        {
            var colecao = MapeaTabela();

            var deletado = colecao.Delete(id);

            return deletado;
        }

        public bool Atualiza(Elemento elementoAtualizado)
        {
            var colecao = MapeaTabela();

            var atualizado = colecao.Update(elementoAtualizado);

            return atualizado;
        }

        public void LimpaColecao()
        {
            var colecao = MapeaTabela();

            colecao.DeleteAll();
        }

        public void Dispose()
        {
            Banco.Dispose();
        }

        public IEnumerable<Elemento> PegaElementos() => MapeaTabela().Query().ToEnumerable();   
    }
}
