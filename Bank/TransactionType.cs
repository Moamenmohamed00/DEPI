using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal enum TransactionType
    {
        deposit =10,//store in DB as 10 not as string "deposit" to save space and for faster processing
        withdraw =20,
        transfer= 30
    }
}
