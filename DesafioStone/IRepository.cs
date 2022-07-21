namespace DesafioStone
{
    public interface IRepository <T> where T : class // Quer dizer que qualquer classe pode implementar a interface
    {
        //Todo mundo que extenda esse repositorio tenha algo em comum
        //Quem implementar essa interface vai ter que colocar um "adicionar" por exemplo
        T Add(T entity);//A classe que eu to extendendo vai servir como objeto de inserção
        //Um T quer dizer que ele é genérico e vai retornar a entidade que eu to inserindo
    }
}
