using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class Account : IAccount
    {
        public string AccountName { get; set; }
        public int YearOfAccounting { get; set; }
        public int YearOfEstablishment { get; set; }
        public int NumberOfEmployees { get; set; }
    }
}
