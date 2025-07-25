using Ambev.DeveloperEvaluation.Application.Event;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Bogus;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application;

public class CreateSaleCommandHandlerTests
{
    private readonly Mock<ISaleRepository> _saleRepositoryMock = new();
    private readonly Mock<IMediator> _mediatorMock = new();
    private readonly Mock<ILogger<CreateSaleCommandHandler>> _loggerMock = new();

    private readonly CreateSaleCommandHandler _handler;

    public CreateSaleCommandHandlerTests()
    {
        _handler = new CreateSaleCommandHandler(
            _saleRepositoryMock.Object,
            _mediatorMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldCreateSaleSuccessfully_WhenCommandIsValid()
    {
        // Arrange
        var items = SaleItemTestData.GenerateMultipleItems(3)
            .Select(x => new CreateSaleItemDto
            {
                ProductName = x.ProductName,
                Quantity = x.Quantity,
                UnitPrice = x.UnitPrice
            }).ToList();

        var command = new CreateSaleCommand
        {
            SaleNumber = "S-123",
            Customer = "Cliente Teste",
            Branch = "Filial Teste",
            Items = items
        };

        _saleRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<EventCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.SaleId);

        _saleRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()), Times.Once);
        _mediatorMock.Verify(m => m.Send(It.Is<EventCommand>(e => e.Event == SaleEvent.SaleCreated), It.IsAny<CancellationToken>()), Times.Once);
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("SaleCreated")),
                null,
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenCommandIsInvalid()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            SaleNumber = "", // inválido
            Customer = "Cliente Teste",
            Branch = "Filial Teste",
            Items = []
        };

        // Act & Assert
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }
}