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

public class EnterpriseControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly EnterpriseController _controller;

    public EnterpriseControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new EnterpriseController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetById_ExistingEnterpriseId_ReturnsEnterprise()
    {
        // Arrange
        var enterpriseId = Guid.NewGuid();
        var enterprise = new EnterpriseDto { Id = enterpriseId };

        _mediatorMock
            .Setup(m => m.Send(new GetEnterpriseByIdQuery(enterpriseId), CancellationToken.None))
            .ReturnsAsync(enterprise);

        // Act
        var result = await _controller.GetById(enterpriseId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as EnterpriseDto).Should().BeEquivalentTo(enterprise);

        _mediatorMock.Verify(m => m.Send(new GetEnterpriseByIdQuery(enterpriseId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingEnterpriseId_ReturnsNotFoundResult()
    {
        // Arrange
        var enterpriseId = Guid.NewGuid();
        var enterprise = new EnterpriseDto { Id = enterpriseId };

        _mediatorMock
            .Setup(m => m.Send(new GetEnterpriseByIdQuery(enterpriseId), CancellationToken.None))
            .ReturnsAsync((EnterpriseDto?)null);

        // Act
        var result = await _controller.GetById(enterpriseId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetEnterpriseByIdQuery(enterpriseId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_Enterprise_ReturnsEnterprise()
    {
        // Arrange
        var enterprise = new EnterpriseForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateEnterpriseCommand(enterprise), CancellationToken.None));

        // Act
        var result = await _controller.Create(enterprise);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as EnterpriseForCreationDto).Should().BeEquivalentTo(enterprise);

        _mediatorMock.Verify(m => m.Send(new CreateEnterpriseCommand(enterprise), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateEnterpriseCommand(It.IsAny<EnterpriseForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingEnterprise_ReturnsNoContentResult()
    {
        // Arrange
        var enterpriseId = Guid.NewGuid();
        var enterprise = new EnterpriseForUpdateDto { Id = enterpriseId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateEnterpriseCommand(enterprise), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(enterpriseId, enterprise);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateEnterpriseCommand(enterprise), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingEnterprise_ReturnsNotFoundResult()
    {
        // Arrange
        var enterpriseId = Guid.NewGuid();
        var enterprise = new EnterpriseForUpdateDto { Id = enterpriseId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateEnterpriseCommand(enterprise), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(enterpriseId, enterprise);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateEnterpriseCommand(enterprise), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var enterpriseId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(enterpriseId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateEnterpriseCommand(It.IsAny<EnterpriseForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingEnterpriseId_ReturnsNoContentResult()
    {
        // Arrange
        var enterpriseId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteEnterpriseCommand(enterpriseId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(enterpriseId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteEnterpriseCommand(enterpriseId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingEnterpriseId_ReturnsNotFoundResult()
    {
        // Arrange
        var enterpriseId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteEnterpriseCommand(enterpriseId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(enterpriseId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteEnterpriseCommand(enterpriseId), CancellationToken.None), Times.Once);
    }
}

