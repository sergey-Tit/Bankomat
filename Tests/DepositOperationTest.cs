using Abstractions.Repositories;
using Application.Accounts;
using Models.Accounts;
using NSubstitute;
using Xunit;

namespace Lab5.Tests;

public class DepositOperationTest
{
    [Fact]
    public void CorrectNewBalance()
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
        bankAccountService.Deposit(10);

        // Assert
        Assert.Equal(40, bankAccountService.CheckBalance());
    }
}