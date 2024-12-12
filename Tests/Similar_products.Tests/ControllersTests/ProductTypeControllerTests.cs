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

public class ProductTypeControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ProductTypeController _controller;

    public ProductTypeControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ProductTypeController(_mediatorMock.Object);
    }


    [Fact]
    public async Task GetById_ExistingProductTypeId_ReturnsProductType()
    {
        // Arrange
        var productTypeId = Guid.NewGuid();
        var productType = new ProductTypeDto { Id = productTypeId };

        _mediatorMock
            .Setup(m => m.Send(new GetProductTypeByIdQuery(productTypeId), CancellationToken.None))
            .ReturnsAsync(productType);

        // Act
        var result = await _controller.GetById(productTypeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as ProductTypeDto).Should().BeEquivalentTo(productType);

        _mediatorMock.Verify(m => m.Send(new GetProductTypeByIdQuery(productTypeId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingProductTypeId_ReturnsNotFoundResult()
    {
        // Arrange
        var productTypeId = Guid.NewGuid();
        var productType = new ProductTypeDto { Id = productTypeId };

        _mediatorMock
            .Setup(m => m.Send(new GetProductTypeByIdQuery(productTypeId), CancellationToken.None))
            .ReturnsAsync((ProductTypeDto?)null);

        // Act
        var result = await _controller.GetById(productTypeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetProductTypeByIdQuery(productTypeId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_ProductType_ReturnsProductType()
    {
        // Arrange
        var productType = new ProductTypeForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateProductTypeCommand(productType), CancellationToken.None));

        // Act
        var result = await _controller.Create(productType);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as ProductTypeForCreationDto).Should().BeEquivalentTo(productType);

        _mediatorMock.Verify(m => m.Send(new CreateProductTypeCommand(productType), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateProductTypeCommand(It.IsAny<ProductTypeForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingProductType_ReturnsNoContentResult()
    {
        // Arrange
        var productTypeId = Guid.NewGuid();
        var productType = new ProductTypeForUpdateDto { Id = productTypeId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateProductTypeCommand(productType), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(productTypeId, productType);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateProductTypeCommand(productType), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingProductType_ReturnsNotFoundResult()
    {
        // Arrange
        var productTypeId = Guid.NewGuid();
        var productType = new ProductTypeForUpdateDto { Id = productTypeId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateProductTypeCommand(productType), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(productTypeId, productType);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateProductTypeCommand(productType), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var productTypeId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(productTypeId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateProductTypeCommand(It.IsAny<ProductTypeForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingProductTypeId_ReturnsNoContentResult()
    {
        // Arrange
        var productTypeId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteProductTypeCommand(productTypeId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(productTypeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteProductTypeCommand(productTypeId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingProductTypeId_ReturnsNotFoundResult()
    {
        // Arrange
        var productTypeId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteProductTypeCommand(productTypeId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(productTypeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteProductTypeCommand(productTypeId), CancellationToken.None), Times.Once);
    }
}

