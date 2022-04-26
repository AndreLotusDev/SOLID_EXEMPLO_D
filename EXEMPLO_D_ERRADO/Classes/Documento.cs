namespace EXEMPLO_D_ERRADO.Classes
{
    public class Documento: EntidadeBase
    {
        public Documento(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public override string ToString()
        {
            return $"Nome: {Nome} | Descrição: {Descricao} | Id: {Id}";
        }
    }
}
