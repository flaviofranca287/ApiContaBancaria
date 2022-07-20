namespace DesafioStone
{
    public interface IReposity <T> where T : class // Quer dizer que qualquer classe pode implementar a interface
    {
        //Todo mundo que extenda esse repositorio tenha algo em comum
        //Quem implementar essa interface vai ter que colocar um "adicionar" por exemplo
        void Add(T entity);//A classe que eu to extendendo vai servir como objeto de inserção
    }
}
