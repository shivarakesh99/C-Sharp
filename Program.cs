// See https://aka.ms/new-console-template for more information
using BankApplication;

BankAccount account = new BankAccount("rakesh",1000);
Console.WriteLine($"a Bank account is created for {account.Owner} with initial Balance {account.Balance} and the account number is {account.Number}");
Console.WriteLine(account.Balance);

try
{
    account.MakeDeposit(-200, DateTime.Now, "salary");
}
catch (ArgumentOutOfRangeException e)
{
    Console.WriteLine("Deposit must be positive");
    Console.WriteLine(e.ToString());
}
Console.WriteLine(account.Balance);

try
{
    account.MakeWithdrawal(-200, DateTime.Now, "expense");  
}
catch (ArgumentOutOfRangeException e)
{
    Console.WriteLine("Withdrawal must be positive");
    Console.WriteLine(e.ToString());
}
Console.WriteLine(account.Balance);

try
{
    account.MakeWithdrawal(1200, DateTime.Now, "debt");
}
catch (InvalidOperationException e)
{
    Console.WriteLine("not sufficient funds for this withdrawal");
    Console.WriteLine(e.ToString());
}
Console.WriteLine(account.Balance);

account.MakeWithdrawal(500, DateTime.Now, "expense");
account.MakeDeposit(500, DateTime.Now, "salary");

account.GetTransactionHistory();
Console.WriteLine(account.Balance);
Console.ReadKey();
