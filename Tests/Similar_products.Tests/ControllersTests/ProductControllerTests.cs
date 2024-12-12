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

public class ProductControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ProductController(_mediatorMock.Object);
    }

    //[Fact]
    //public async Task Get_ReturnsListOfProducts()
    //{
    //    // Arrange
    //    var products = new List<ProductDto> { new(), new() };

    //    _mediatorMock
    //        .Setup(m => m.Send(new GetProductsQuery(), CancellationToken.None))
    //        .ReturnsAsync(products);

    //    // Act
    //    var result = await _controller.Get();

    //    // Assert
    //    result.Should().NotBeNull();
    //    result.Should().BeOfType(typeof(OkObjectResult));

    //    var okResult = result as OkObjectResult;
    //    okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

    //    var value = okResult?.Value as List<ProductDto>;
    //    value.Should().HaveCount(2);
    //    value.Should().BeEquivalentTo(products);

    //    _mediatorMock.Verify(m => m.Send(new GetProductsQuery(), CancellationToken.None), Times.Once);
    //}

    [Fact]
    public async Task GetById_ExistingProductId_ReturnsProduct()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new ProductDto { Id = productId };

        _mediatorMock
            .Setup(m => m.Send(new GetProductByIdQuery(productId), CancellationToken.None))
            .ReturnsAsync(product);

        // Act
        var result = await _controller.GetById(productId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as ProductDto).Should().BeEquivalentTo(product);

        _mediatorMock.Verify(m => m.Send(new GetProductByIdQuery(productId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingProductId_ReturnsNotFoundResult()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new ProductDto { Id = productId };

        _mediatorMock
            .Setup(m => m.Send(new GetProductByIdQuery(productId), CancellationToken.None))
            .ReturnsAsync((ProductDto?)null);

        // Act
        var result = await _controller.GetById(productId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetProductByIdQuery(productId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_Product_ReturnsProduct()
    {
        // Arrange
        var product = new ProductForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateProductCommand(product), CancellationToken.None));

        // Act
        var result = await _controller.Create(product);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as ProductForCreationDto).Should().BeEquivalentTo(product);

        _mediatorMock.Verify(m => m.Send(new CreateProductCommand(product), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateProductCommand(It.IsAny<ProductForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingProduct_ReturnsNoContentResult()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new ProductForUpdateDto { Id = productId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateProductCommand(product), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(productId, product);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateProductCommand(product), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingProduct_ReturnsNotFoundResult()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var product = new ProductForUpdateDto { Id = productId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateProductCommand(product), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(productId, product);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateProductCommand(product), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var productId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(productId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateProductCommand(It.IsAny<ProductForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingProductId_ReturnsNoContentResult()
    {
        // Arrange
        var productId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteProductCommand(productId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(productId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteProductCommand(productId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingProductId_ReturnsNotFoundResult()
    {
        // Arrange
        var productId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteProductCommand(productId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(productId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteProductCommand(productId), CancellationToken.None), Times.Once);
    }
}

