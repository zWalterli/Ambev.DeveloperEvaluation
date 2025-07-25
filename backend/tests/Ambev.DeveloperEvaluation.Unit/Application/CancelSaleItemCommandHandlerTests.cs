using Ambev.DeveloperEvaluation.Application.Event;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CancelSaleItemCommandHandlerTests
{
    private readonly Mock<ISaleRepository> _saleRepositoryMock = new();
    private readonly Mock<IMediator> _mediatorMock = new();
    private readonly Mock<ILogger<CancelSaleItemCommandHandler>> _loggerMock = new();

    private readonly CancelSaleItemCommandHandler _handler;

    public CancelSaleItemCommandHandlerTests()
    {
        _handler = new CancelSaleItemCommandHandler(
            _saleRepositoryMock.Object,
            _mediatorMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldCancelItemSuccessfully_WhenCommandIsValid()
    {
        // Arrange
        var sale = new Sale("123", "Cliente", "Filial");
        var item = SaleItemTestData.GenerateFixedItem("Produto A", 5, 20m);
        sale.AddItem(item.ProductName, item.Quantity, item.UnitPrice);
        var itemId = sale.Itens.First().Id;

        var command = new CancelSaleItemCommand
        {
            SaleId = sale.Id,
            ItemId = itemId
        };

        _saleRepositoryMock
            .Setup(r => r.GetByIdAsync(command.SaleId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(sale);

        _saleRepositoryMock
            .Setup(r => r.UpdateAsync(sale, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<EventCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.True(sale.Itens.First().IsCancelled);
        Assert.False(sale.IsCancelled); // não cancela venda inteira
    }

    [Fact]
    public async Task Handle_ShouldCancelEntireSale_WhenLastItemIsCancelled()
    {
        // Arrange
        var sale = new Sale("456", "Cliente", "Filial");
        var item = SaleItemTestData.GenerateFixedItem("Produto Único", 10, 15m);
        sale.AddItem(item.ProductName, item.Quantity, item.UnitPrice);
        var itemId = sale.Itens.First().Id;

        var command = new CancelSaleItemCommand
        {
            SaleId = sale.Id,
            ItemId = itemId
        };

        _saleRepositoryMock
            .Setup(r => r.GetByIdAsync(sale.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(sale);

        _saleRepositoryMock
            .Setup(r => r.UpdateAsync(sale, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<EventCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(sale.Itens.First().IsCancelled);
        Assert.True(sale.IsCancelled);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
    {
        // Arrange
        var command = new CancelSaleItemCommand
        {
            SaleId = Guid.Empty,
            ItemId = Guid.Empty
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }
}