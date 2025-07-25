using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ViewModels;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Interface for Sale repository operations
/// Provides methods for managing sales data in the repository.
/// </summary>
public interface ISaleRepository
{
    Task<Sale> GetByIdAsync(Guid saleId, CancellationToken cancellationToken = default);
    Task AddAsync(Sale sale, CancellationToken cancellationToken = default);
    Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default);
    Task DeleteAsync(Sale sale, CancellationToken cancellationToken = default);
    Task<(int, IEnumerable<Sale>)> GetPaginatedAsync(GetSalePaginated request, CancellationToken cancellationToken = default);
}