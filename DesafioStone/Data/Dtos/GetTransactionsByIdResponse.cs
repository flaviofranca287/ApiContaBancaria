using System;

namespace DesafioStone.Data.Dtos
{
    public class GetTransactionsByIdResponse
    {
        public int Id { get; set; }
        public string TransactionType { get; set; } // a partir do momento que a tabela existe precisa de um construtor público para fazer essas amarrações, se eu deixo sem o getset eu não consigo criar na mão
        public string NameTransactor { get; set; }
        public string NameReceiver { get; set; }
        public DateTime TransactionDate { get; set; }
        public double TransactionValue { get; set; }
        public double IdTransactor { get; set; }
        public double IdReceiver { get; set; }
    }
}