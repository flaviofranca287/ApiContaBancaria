using System;

namespace DesafioStone.Data.Dtos
{
    public class DepositResponse
    {
        public double ValueOfTransaction { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public string NameTransactor { get; set; }
        public string NameReceiver { get; set; }
        public string TransactionType { get; set; }
        public int IdTransactor { get; set; }
        public int IdReceiver { get; set; }
        public double ActualBalance { get; set; }
    }
}
