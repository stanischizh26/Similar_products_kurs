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

public class SalesPlanControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly SalesPlanController _controller;

    public SalesPlanControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new SalesPlanController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetById_ExistingSalesPlanId_ReturnsSalesPlan()
    {
        // Arrange
        var salesPlanId = Guid.NewGuid();
        var salesPlan = new SalesPlanDto { Id = salesPlanId };

        _mediatorMock
            .Setup(m => m.Send(new GetSalesPlanByIdQuery(salesPlanId), CancellationToken.None))
            .ReturnsAsync(salesPlan);

        // Act
        var result = await _controller.GetById(salesPlanId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as SalesPlanDto).Should().BeEquivalentTo(salesPlan);

        _mediatorMock.Verify(m => m.Send(new GetSalesPlanByIdQuery(salesPlanId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingSalesPlanId_ReturnsNotFoundResult()
    {
        // Arrange
        var salesPlanId = Guid.NewGuid();
        var salesPlan = new SalesPlanDto { Id = salesPlanId };

        _mediatorMock
            .Setup(m => m.Send(new GetSalesPlanByIdQuery(salesPlanId), CancellationToken.None))
            .ReturnsAsync((SalesPlanDto?)null);

        // Act
        var result = await _controller.GetById(salesPlanId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetSalesPlanByIdQuery(salesPlanId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_SalesPlan_ReturnsSalesPlan()
    {
        // Arrange
        var salesPlan = new SalesPlanForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateSalesPlanCommand(salesPlan), CancellationToken.None));

        // Act
        var result = await _controller.Create(salesPlan);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as SalesPlanForCreationDto).Should().BeEquivalentTo(salesPlan);

        _mediatorMock.Verify(m => m.Send(new CreateSalesPlanCommand(salesPlan), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateSalesPlanCommand(It.IsAny<SalesPlanForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingSalesPlan_ReturnsNoContentResult()
    {
        // Arrange
        var salesPlanId = Guid.NewGuid();
        var salesPlan = new SalesPlanForUpdateDto { Id = salesPlanId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateSalesPlanCommand(salesPlan), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(salesPlanId, salesPlan);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateSalesPlanCommand(salesPlan), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingSalesPlan_ReturnsNotFoundResult()
    {
        // Arrange
        var salesPlanId = Guid.NewGuid();
        var salesPlan = new SalesPlanForUpdateDto { Id = salesPlanId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateSalesPlanCommand(salesPlan), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(salesPlanId, salesPlan);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateSalesPlanCommand(salesPlan), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var salesPlanId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(salesPlanId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateSalesPlanCommand(It.IsAny<SalesPlanForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingSalesPlanId_ReturnsNoContentResult()
    {
        // Arrange
        var salesPlanId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteSalesPlanCommand(salesPlanId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(salesPlanId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteSalesPlanCommand(salesPlanId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingSalesPlanId_ReturnsNotFoundResult()
    {
        // Arrange
        var salesPlanId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteSalesPlanCommand(salesPlanId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(salesPlanId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteSalesPlanCommand(salesPlanId), CancellationToken.None), Times.Once);
    }
}

