namespace Contracts.ResultTypes;

public abstract record WithdrawResult
{
    private WithdrawResult() { }

    public sealed record Success : WithdrawResult;

    public sealed record LowBalanceError : WithdrawResult;
}