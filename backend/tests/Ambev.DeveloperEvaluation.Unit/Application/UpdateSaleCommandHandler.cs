using Ambev.DeveloperEvaluation.Application.Event;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
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

public class UpdateSaleCommandHandlerTests
{
    private readonly Mock<ISaleRepository> _repositoryMock = new();
    private readonly Mock<IMediator> _mediatorMock = new();
    private readonly Mock<ILogger<UpdateSaleCommandHandler>> _loggerMock = new();

    private readonly UpdateSaleCommandHandler _handler;

    public UpdateSaleCommandHandlerTests()
    {
        _handler = new UpdateSaleCommandHandler(
            _repositoryMock.Object,
            _mediatorMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldUpdateSaleSuccessfully_WhenValid()
    {
        // Arrange
        var sale = new Sale("001", "Cliente Original", "Filial A");
        var existingItem = SaleItemTestData.GenerateFixedItem("Produto A", 5, 10m);
        sale.AddItem(existingItem.ProductName, existingItem.Quantity, existingItem.UnitPrice);
        var itemId = sale.Itens.First().Id;

        var command = new UpdateSaleCommand
        {
            Id = sale.Id,
            SaleNumber = "002",
            Customer = "Cliente Atualizado",
            Branch = "Filial B",
            Items = new List<UpdateSaleItemDto>
            {
                new() { Id = itemId, ProductName = "Produto Atualizado", Quantity = 7, UnitPrice = 12m },
                new() { ProductName = "Novo Produto", Quantity = 4, UnitPrice = 8m }
            }
        };

        _repositoryMock.Setup(r => r.GetByIdAsync(sale.Id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(sale);

        _repositoryMock.Setup(r => r.UpdateAsync(sale, It.IsAny<CancellationToken>()))
                       .Returns(Task.CompletedTask);

        _mediatorMock.Setup(m => m.Send(It.IsAny<EventCommand>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(Unit.Value);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(sale.Id, result.SaleId);
        Assert.Equal("Cliente Atualizado", sale.Customer);
        Assert.Equal("002", sale.SaleNumber);
        Assert.Equal(2, sale.Itens.Count);
        Assert.Contains(sale.Itens, i => i.ProductName == "Produto Atualizado");
        Assert.Contains(sale.Itens, i => i.ProductName == "Novo Produto");

        _repositoryMock.Verify(r => r.UpdateAsync(sale, It.IsAny<CancellationToken>()), Times.Once);
        _mediatorMock.Verify(m => m.Send(It.Is<EventCommand>(e => e.Event == SaleEvent.SaleUpdated), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenRequestIsInvalid()
    {
        // Arrange
        var command = new UpdateSaleCommand
        {
            Id = Guid.Empty, // inválido
            SaleNumber = "",
            Customer = "",
            Branch = "",
            Items = []
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenSaleNotFound()
    {
        // Arrange
        var command = new UpdateSaleCommand
        {
            Id = Guid.NewGuid(),
            SaleNumber = "001",
            Customer = "Cliente",
            Branch = "Filial",
            Items = []
        };

        _repositoryMock.Setup(r => r.GetByIdAsync(command.Id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync((Sale?)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(() =>
            _handler.Handle(command, CancellationToken.None));

        Assert.Equal($"Sale {command.Id} not found.", ex.Message);
    }
}