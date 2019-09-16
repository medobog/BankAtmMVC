using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Bank.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public float Amount { get; set; }
        public DateTime Created_at { get; set; } = new DateTime(DateTime.UtcNow.Ticks);
        public int AccountID { get; set; }
        public Account Account { get; set; }
    }
}