using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository(DefaultContext context) : ISaleRepository
{
    /// <summary>
    /// Initializes a new instance of SaleRepository
    /// </summary>
    /// <param name="context">The database context</param>
    private readonly DefaultContext _context = context;

    /// <summary>
    /// Adds a new sale to the database
    /// </summary>
    /// <param name="sale"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task AddAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        await _context.Sales.AddAsync(sale, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Deletes a sale from the database
    /// </summary>
    /// <param name="sale"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task DeleteAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        _context.Sales.Remove(sale);
        await _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves a paginated list of sales based on the provided filters
    /// </summary>
    /// <param name="page"></param>
    /// <param name="pageSize"></param>
    /// <param name="id"></param>
    /// <param name="saleNumber"></param>
    /// <param name="customer"></param>
    /// <param name="branch"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<(int, IEnumerable<Sale>)> GetPaginatedAsync(
        GetSalePaginated getSalePaginated, CancellationToken cancellationToken = default)
    {
        var query = _context.Sales.AsQueryable();

        if (getSalePaginated.Id.HasValue)
            query = query.Where(x => x.Id == getSalePaginated.Id.Value);

        if (!string.IsNullOrEmpty(getSalePaginated.SaleNumber))
            query = query.Where(x => x.SaleNumber.ToLower().Contains(getSalePaginated.SaleNumber.ToLower()));

        if (!string.IsNullOrEmpty(getSalePaginated.Customer))
            query = query.Where(x => x.Customer.ToLower().Contains(getSalePaginated.Customer.ToLower()));

        if (!string.IsNullOrEmpty(getSalePaginated.Branch))
            query = query.Where(x => x.Branch.ToLower().Contains(getSalePaginated.Branch.ToLower()));

        var totalCount = await query.CountAsync(cancellationToken);
        query = query
            .Include(x => x.Itens)
            .Skip((getSalePaginated.Page - 1) * getSalePaginated.PageSize)
            .Take(getSalePaginated.PageSize);

        if (!string.IsNullOrEmpty(getSalePaginated.OrderBy))
        {
            var orderBy = getSalePaginated.OrderBy.Split(',')
                .Select(x => x.Trim())
                .FirstOrDefault();

            var descending = orderBy?.EndsWith(" desc") ?? false;

            query = orderBy.ToLower() switch
            {
                "salenumber" => descending ? query.OrderByDescending(x => x.SaleNumber) : query.OrderBy(x => x.SaleNumber),
                "customer" => descending ? query.OrderByDescending(x => x.Customer) : query.OrderBy(x => x.Customer),
                "branch" => descending ? query.OrderByDescending(x => x.Branch) : query.OrderBy(x => x.Branch),
                _ => descending ? query.OrderByDescending(x => x.SaleDate) : query.OrderBy(x => x.SaleDate)
            };
        }

        var items = await query.ToListAsync(cancellationToken);

        return (totalCount, items);
    }

    /// <summary>
    /// Retrieves a sale by its ID
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<Sale> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Sales
            .Include(x => x.Itens)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    /// <summary>
    /// Updates an existing sale in the database
    /// </summary>
    /// <param name="sale"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken = default)
    {
        _context.Sales.Update(sale);
        await _context.SaveChangesAsync(cancellationToken);
    }
}