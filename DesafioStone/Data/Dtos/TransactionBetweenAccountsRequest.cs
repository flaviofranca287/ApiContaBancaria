using System;
using System.ComponentModel.DataAnnotations;

namespace DesafioStone.Data.Dtos
{
    public class TransactionBetweenAccountsRequest
    {
        public int IdReceiver { get; set; }
        public double ValueOfTransaction { get; set; }
        public int IdTransactor { get; set; }

    }
}