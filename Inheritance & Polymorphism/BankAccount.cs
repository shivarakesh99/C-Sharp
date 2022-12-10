using System;
using System.Collections.Generic;
using System.Text;

namespace BankApplication
{
    public class BankAccount
    {
        public string Owner { get; set; }
        public string Number { get; }
        
        private static int accountNumberSeed = 1234567890;

        private List<Transaction> allTransactions= new List<Transaction>();

        public decimal Balance 
        { 
            get 
            {
                decimal balance = 0;
                foreach (var transaction in allTransactions) 
                {
                    balance += transaction.Amount;
                }                
                return balance;
            }
            
        }

        private readonly decimal _minimumBalance;

        public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0)
        {

        }

        public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
        {
            Owner = name;
            
            Number = accountNumberSeed.ToString();
            accountNumberSeed++;

            _minimumBalance= minimumBalance;

            if (initialBalance < 0)
            {
                MakeDeposit(initialBalance, DateTime.Now, "initial Balance");
            }
            
            

        }

        
        
        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            #region Exceptions for Deposit
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Deposit must be positive");
            }            
            #endregion
            Transaction deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            #region Exceptions for withdrawal
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal must be positive");
            }
            #endregion
            Transaction? overDraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
            Transaction withdrawal = new(-amount, date, note);
            allTransactions.Add(withdrawal);
            if(overDraftTransaction != null )
            {
                allTransactions.Add(overDraftTransaction);
            }
        }

        protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
        {
            if (isOverdrawn)
            {
                throw new InvalidOperationException("not insufficient funds for this transaction");
            }
            else
            {
                return default;
            }
            
        }

        public void GetTransactionHistory()
        {
            var stringBuilder = new StringBuilder();
            Console.WriteLine("Date\t\t\t\tAmount\t\tNotes");
            foreach (var item in allTransactions)
            {
                stringBuilder.AppendLine(item.Date +"\t\t" +item.Amount + "\t\t"+item.Notes);
            }
            Console.WriteLine(stringBuilder.ToString());           
        }

        public virtual void PerformMonthEndTransactions()
        {

        }


    }
}
