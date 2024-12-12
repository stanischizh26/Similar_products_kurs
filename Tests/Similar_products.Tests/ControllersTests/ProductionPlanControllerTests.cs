using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using Similar_products.Application.Dtos;
using Similar_products.Application.Requests.Queries;
using Similar_products.Application.Requests.Commands;
using Similar_products.Web.Controllers;

namespace Similar_products.Tests.ControllersTests;

public class ProductionPlanControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ProductionPlanController _controller;

    public ProductionPlanControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ProductionPlanController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetById_ExistingProductionPlanId_ReturnsProductionPlan()
    {
        // Arrange
        var productionPlanId = Guid.NewGuid();
        var productionPlan = new ProductionPlanDto { Id = productionPlanId };

        _mediatorMock
            .Setup(m => m.Send(new GetProductionPlanByIdQuery(productionPlanId), CancellationToken.None))
            .ReturnsAsync(productionPlan);

        // Act
        var result = await _controller.GetById(productionPlanId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as ProductionPlanDto).Should().BeEquivalentTo(productionPlan);

        _mediatorMock.Verify(m => m.Send(new GetProductionPlanByIdQuery(productionPlanId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingProductionPlanId_ReturnsNotFoundResult()
    {
        // Arrange
        var productionPlanId = Guid.NewGuid();
        var productionPlan = new ProductionPlanDto { Id = productionPlanId };

        _mediatorMock
            .Setup(m => m.Send(new GetProductionPlanByIdQuery(productionPlanId), CancellationToken.None))
            .ReturnsAsync((ProductionPlanDto?)null);

        // Act
        var result = await _controller.GetById(productionPlanId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetProductionPlanByIdQuery(productionPlanId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_ProductionPlan_ReturnsProductionPlan()
    {
        // Arrange
        var productionPlan = new ProductionPlanForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateProductionPlanCommand(productionPlan), CancellationToken.None));

        // Act
        var result = await _controller.Create(productionPlan);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as ProductionPlanForCreationDto).Should().BeEquivalentTo(productionPlan);

        _mediatorMock.Verify(m => m.Send(new CreateProductionPlanCommand(productionPlan), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_NullValue_ReturnsBadRequest()
    {
        // Arrange and Act
        var result = await _controller.Create(null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new CreateProductionPlanCommand(It.IsAny<ProductionPlanForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingProductionPlan_ReturnsNoContentResult()
    {
        // Arrange
        var productionPlanId = Guid.NewGuid();
        var productionPlan = new ProductionPlanForUpdateDto { Id = productionPlanId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateProductionPlanCommand(productionPlan), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(productionPlanId, productionPlan);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateProductionPlanCommand(productionPlan), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingProductionPlan_ReturnsNotFoundResult()
    {
        // Arrange
        var productionPlanId = Guid.NewGuid();
        var productionPlan = new ProductionPlanForUpdateDto { Id = productionPlanId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateProductionPlanCommand(productionPlan), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(productionPlanId, productionPlan);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateProductionPlanCommand(productionPlan), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var productionPlanId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(productionPlanId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateProductionPlanCommand(It.IsAny<ProductionPlanForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingProductionPlanId_ReturnsNoContentResult()
    {
        // Arrange
        var productionPlanId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteProductionPlanCommand(productionPlanId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(productionPlanId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteProductionPlanCommand(productionPlanId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingProductionPlanId_ReturnsNotFoundResult()
    {
        // Arrange
        var productionPlanId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteProductionPlanCommand(productionPlanId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(productionPlanId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteProductionPlanCommand(productionPlanId), CancellationToken.None), Times.Once);
    }
}

