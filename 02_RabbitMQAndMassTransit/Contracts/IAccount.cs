using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAccount
    {
        string AccountName { get; set; }
        int YearOfAccounting { get; set; }
        int YearOfEstablishment { get; set; }
        int NumberOfEmployees { get; set; }
    }
}
