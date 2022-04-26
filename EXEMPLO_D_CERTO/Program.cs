using EXEMPLO_D_CERTO.Bancos;
using EXEMPLO_D_CERTO.Classes;
using EXEMPLO_D_CERTO.Interface;
using EXEMPLO_D_CERTO.Servicos;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EXEMPLO_D_CERTO
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("BANCO SQLITE 1 - BANCO LITEDB 2");
            var selecioneTipoDeBanco = int.Parse(Console.ReadLine());

            var colecaoDeServico = new ServiceCollection();

            ConfiguraServicos(colecaoDeServico, (ETipoBanco)selecioneTipoDeBanco);
            var servicoBuildado = colecaoDeServico.BuildServiceProvider();

            //---------------------------------------FUNCIONARIO AREA

            Console.ForegroundColor = ConsoleColor.DarkGreen;

            var funcionarioUm = new Funcionario("Andre Soares Gomes", 1_500);
            var funcioarioDois = new Funcionario("Barbara Silveira Souza", 2_700);

            FuncionarioServico funcionarioServico = new(servicoBuildado.GetService<IBanco<Funcionario>>());
            funcionarioServico.Adiciona(funcionarioUm);
            funcionarioServico.Adiciona(funcioarioDois);

            foreach (var funcionario in funcionarioServico.TrazElementos())
            {
                Console.WriteLine(funcionario.ToString());
            }

            funcionarioServico.Dispose();

            Console.WriteLine("-------------------------------------------------------");

            //-------------------------------------DOCUMENTO AREA

            Console.ForegroundColor = ConsoleColor.DarkBlue;

            var documentoUm = new Documento("DOCUMENTO_IDENTIFICACAO", "Serve para identificar o funcionario");
            var documentoDois = new Documento("DOCUMENTO_SALARIO", "Serve para identificar o funcionario");

            DocumentoServico documentoServico = new(servicoBuildado.GetService<IBanco<Documento>>());
            documentoServico.Adiciona(documentoUm);
            documentoServico.Adiciona(documentoDois);

            foreach (var documento in documentoServico.TrazElementos())
            {
                Console.WriteLine(documento.ToString());
            }

            documentoServico.Dispose();

            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ConfiguraServicos(ServiceCollection colecaoDeServicos, ETipoBanco tipoBanco)
        {
            if(tipoBanco == ETipoBanco.SQLite)
            {
                colecaoDeServicos.AddSingleton(typeof(IBanco<>), typeof(GerenciadorBancoSQLite<>));

                var documento = new GerenciadorBancoSQLite<Documento>();
                documento.LimpaColecao();
                documento.Dispose();

                var funcionario = new GerenciadorBancoSQLite<Funcionario>();
                funcionario.LimpaColecao();
                funcionario.Dispose();
            }
            else
            {
                colecaoDeServicos.AddSingleton(typeof(IBanco<>), typeof(GerenciadorBancoDBLite<>));

                var documento = new GerenciadorBancoDBLite<Documento>();
                documento.LimpaColecao();
                documento.Dispose();

                var funcionario = new GerenciadorBancoDBLite<Funcionario>();
                funcionario.LimpaColecao();
                funcionario.Dispose();
            }
        }
    }
}
