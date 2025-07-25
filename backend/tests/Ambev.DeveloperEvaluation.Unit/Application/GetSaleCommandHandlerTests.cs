using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ViewModels;
using AutoMapper;
using Bogus;
using FluentValidation;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class GetSaleCommandHandlerTests
{
    private readonly Mock<ISaleRepository> _repositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();

    private readonly GetSaleCommandHandler _handler;

    public GetSaleCommandHandlerTests()
    {
        _handler = new GetSaleCommandHandler(
            _repositoryMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnPaginatedSales_WhenRequestIsValid()
    {
        // Arrange
        var command = new GetSaleCommand
        {
            Page = 1,
            PageSize = 10,
            Customer = null,
            Branch = null,
            Id = null,
            SaleNumber = null
        };

        var fakeSales = new List<Sale>
        {
            new("001", "Cliente 1", "Filial A"),
            new("002", "Cliente 2", "Filial B")
        };

        var mappedResult = new List<GetSaleResult>
        {
            new() { Id = fakeSales[0].Id, Customer = "Cliente 1", SaleNumber = "001", Branch = "Filial A" },
            new() { Id = fakeSales[1].Id, Customer = "Cliente 2", SaleNumber = "002", Branch = "Filial B" }
        };

        _repositoryMock
            .Setup(r => r.GetPaginatedAsync(It.IsAny<GetSalePaginated>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((2, fakeSales));

        _mapperMock
            .Setup(m => m.Map<List<GetSaleResult>>(fakeSales))
            .Returns(mappedResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.TotalCount);
        Assert.Equal(2, result.Sales.Count);
        Assert.Equal("Cliente 1", result.Sales[0].Customer);
        Assert.Equal("Cliente 2", result.Sales[1].Customer);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        // Arrange
        var command = new GetSaleCommand
        {
            Page = 0,       // inválido
            PageSize = -5   // inválido
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }
}