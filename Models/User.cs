﻿using System;
using System.Collections.Generic;

namespace Bank.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserKey { get; set; }
        public DateTime Created_at { get; set; } = new DateTime(DateTime.UtcNow.Ticks);
        public DateTime Updated_at { get; set; } = new DateTime(DateTime.UtcNow.Ticks);
        public List<Account> Accounts { get; set; } = new List<Account>();
    }
}