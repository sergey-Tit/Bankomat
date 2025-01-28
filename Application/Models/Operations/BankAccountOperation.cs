namespace Models.Operations;

public class BankAccountOperation
{
    public long BankAccountId { get; }

    public DateTime DateTime { get; }

    public decimal? Amount { get; }

    public BankAccountOperationType Type { get; }

    public BankAccountOperation(long bankAccountId, DateTime dateTime, decimal? amount, BankAccountOperationType type)
    {
        BankAccountId = bankAccountId;
        DateTime = dateTime;
        Amount = amount;
        Type = type;
    }

    public string Info()
    {
        string info = $"{DateTime}: Bank account №{BankAccountId}, {Type}";
        if (Type is not BankAccountOperationType.Create)
        {
            info += $" {Amount}";
        }

        return info;
    }
}