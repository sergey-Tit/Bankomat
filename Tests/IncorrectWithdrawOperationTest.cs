using Abstractions.Repositories;
using Application.Accounts;
using Contracts.ResultTypes;
using Models.Accounts;
using NSubstitute;
using Xunit;

namespace Lab5.Tests;

public class IncorrectWithdrawOperationTest
{
    [Fact]
    public void NotEnoughBalanceTest()
    {
        // Arrange
        IBankAccountRepository bankAccountRepositoryMock = Substitute.For<IBankAccountRepository>();
        IBankAccountOperationRepository bankAccountOperationRepositoryMock =
            Substitute.For<IBankAccountOperationRepository>();
        var bankAccountService = new BankAccountService(
            bankAccountRepositoryMock,
            bankAccountOperationRepositoryMock);
        var account = new BankAccount(0, 30);
        bankAccountRepositoryMock.FindAccountById(0, "123").Returns(new FindBankAccountResult.Success(account));

        // Act
        bankAccountService.Login(0, "123");

        // Assert
        Assert.Equal(new WithdrawResult.LowBalanceError(), bankAccountService.Withdraw(50));
    }
}