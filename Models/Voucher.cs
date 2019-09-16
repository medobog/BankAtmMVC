using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class Voucher
    {
        public int VoucherID { get; set; }
        public string VoucherKey { get; set; }
        public float Amount { get; set; }
        public DateTime Created_at { get; set; } = new DateTime(DateTime.UtcNow.Ticks);
        public float Duration { get; set; }
        public DateTime Until { get; set; }
        public int AccountID { get; set; }
        public Account Account { get; set; }
        public int Used { get; set; }
    }
}
