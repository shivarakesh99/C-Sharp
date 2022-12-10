using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApplication
{
    class LineOfCreditAccount : BankAccount
    {
        public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit) : base(name, initialBalance, -creditLimit) 
        {

        }

        public override void PerformMonthEndTransactions()
        {
            decimal interest = -Balance * 0.07m;            
            MakeWithdrawal(interest, DateTime.Now, "charge monthly interest");
        }

        protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        {
            if(isOverdrawn)
            {
                return new Transaction(-20, DateTime.Now, "Apply overdraft fee");
            }
            else
            {
                return default;
            }
        }
    }
}
