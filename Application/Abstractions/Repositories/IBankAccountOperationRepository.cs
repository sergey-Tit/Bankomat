using Models.Operations;

namespace Abstractions.Repositories;

public interface IBankAccountOperationRepository
{
    Task AddOperation(BankAccountOperation operation);

    Task<ICollection<BankAccountOperation>> GetOperations(long bankAccountId);
}