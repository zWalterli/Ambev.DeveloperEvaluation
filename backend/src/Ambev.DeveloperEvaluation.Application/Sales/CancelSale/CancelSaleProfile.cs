using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Profile for mapping CancelSaleCommand to Sale entity and Sale entity to CancelSaleResult
/// </summary>
public class CancelSaleProfile : Profile
{
    public CancelSaleProfile()
    {
        CreateMap<CancelSaleCommand, Sale>();
        CreateMap<Sale, CancelSaleResult>();
    }
}
