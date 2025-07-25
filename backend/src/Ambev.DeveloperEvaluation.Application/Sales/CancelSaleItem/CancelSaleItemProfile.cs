using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

/// <summary>
/// Profile for mapping CancelSaleItemCommand to Sale entity
/// and Sale entity to CancelSaleItemResult
/// </summary>
public class CancelSaleItemProfile : Profile
{
    public CancelSaleItemProfile()
    {
        CreateMap<CancelSaleItemCommand, Sale>();
        CreateMap<Sale, CancelSaleItemResult>();
    }
}
