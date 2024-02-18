using Application.Services;
using AutoFixture;
using AutoFixture.Xunit2;
using Contracts.Requests;
using Contracts.Responses;
using Domain.Entities;

using Domain.Exceptions;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace ShopV2.UnitTest.Services;

public class ItemServiceTest
{
    private readonly Mock<IItemRepository> _itemRepositoryMock;
    private readonly ItemService _itemService;

    public ItemServiceTest()
    {
        _itemRepositoryMock = new Mock<IItemRepository>();
        _itemService = new ItemService(_itemRepositoryMock.Object);
    }

    [Theory]
    [AutoData]
    public async Task GetId_GivenValidId_ReturnsDTO(Guid id)
    {
        //Arrange
        _itemRepositoryMock.Setup(m => m.Get(id))
                        .ReturnsAsync(new ItemEntity { Id = id });

        //Act
        ItemResponce result = await _itemService.Get(id);

        //Assert
        result.Id.Should().Be(id);

        _itemRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
    }

    [Fact]
    public async Task GetId_GivenInvalidId_ThrowNotFoundException()
    {
        // Arrange
        Guid id = new();

        _itemRepositoryMock.Setup(m => m.Get(id))
                        .ReturnsAsync((ItemEntity)null!);

        // Act Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _itemService.Get(id));

        _itemRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
    }

    [Fact]
    public async Task Get_GivenValidId_ReturnsDTO()
    {
        int quantity = 5;

        Fixture _fixture = new();
        List<ItemEntity> itemList = [];
        _fixture.AddManyTo(itemList, quantity);

        //Arrange
        _itemRepositoryMock.Setup(m => m.Get())
                        .ReturnsAsync(itemList);

        //Act
        var result = await _itemService.Get();

        //Assert
        result.Count.Should().Be(quantity);

        _itemRepositoryMock.Verify(m => m.Get(), Times.Once());
    }

    [Fact]
    public async Task Get_GivenEmpty_ShouldReturnEmpty()
    {
        // Arrange
        _itemRepositoryMock.Setup(m => m.Get())
                        .ReturnsAsync(new List<ItemEntity>());

        // Act Assert
        var result = await _itemService.Get();

        result.Count.Should().Be(0);

        _itemRepositoryMock.Verify(m => m.Get(), Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task Add_GivenValidId_ReturnsGuid(Guid id, string name, decimal price)
    {
        //Arrange
        _itemRepositoryMock.Setup(m => m.Add(It.Is<ItemEntity>
                                (x => x.Name == name && x.Price == price)))
                                 .ReturnsAsync(id);

        //Act
        Guid result = await _itemService.Add(new ItemAddRequest { Name = name, Price = price });

        //Assert
        result.Should().Be(id);

        _itemRepositoryMock.Verify(m => m.Add(It.IsAny<ItemEntity>()), Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task Update_ReturnsSuccess(Guid id, string name, decimal price)
    {
        //Arrange
        _itemRepositoryMock.Setup(m => m.Update(It.Is<ItemEntity>
                                (x => x.Id == id && x.Name == name && x.Price == price)))
                                .ReturnsAsync(1);

        _itemRepositoryMock.Setup(m => m.Get(id))
                                .ReturnsAsync(new ItemEntity
                                { Id = id, Name = name, Price = price });

        //Act
        //Assert
        await _itemService.Invoking(x => x.Update(id, new ItemAddRequest
        { Name = name, Price = price }))
                                        .Should().NotThrowAsync<Exception>();

        _itemRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
        _itemRepositoryMock.Verify(m => m.Update(It.IsAny<ItemEntity>()), Times.Once());
    }

    [Fact]
    public async Task Update_Invalid_InvalidOperationException()
    {
        Guid id = new();
        string name = "name";
        decimal price = 5.98m;

        //Arrange
        _itemRepositoryMock.Setup(m => m.Update(It.Is<ItemEntity>
                                (x => x.Id == id && x.Name == name && x.Price == price)))
                                .ReturnsAsync(2);

        _itemRepositoryMock.Setup(m => m.Get(id))
                             .ReturnsAsync(new ItemEntity { Id = id });

        //Act
        //Assert
        await _itemService.Invoking(x => x.Update(id, new ItemAddRequest { Name = name, Price = price }))
                            .Should().ThrowAsync<InvalidOperationException>();

        _itemRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
        _itemRepositoryMock.Verify(m => m.Update(It.IsAny<ItemEntity>()), Times.Once());
    }

    [Fact]
    public async Task Update_InvalidId_InvalidOperationException()
    {
        Guid id = new();
        string name = "name";
        decimal price = 5.98m;

        //Arrange
        _itemRepositoryMock.Setup(m => m.Update(It.Is<ItemEntity>
                                (x => x.Id == id && x.Name == name && x.Price == price)))
                                .ReturnsAsync(1);

        _itemRepositoryMock.Setup(m => m.Get(id))
                        .ReturnsAsync((ItemEntity)null!);

        //Act
        //Assert
        await _itemService.Invoking(x => x.Update(id, new ItemAddRequest { Name = name, Price = price }))
                            .Should().ThrowAsync<NotFoundException>();

        _itemRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
    }

    [Fact]
    public async Task Delete_ValidId()
    {
        Guid id = new();

        //Arrange
        _itemRepositoryMock.Setup(m => m.Delete(id));

        _itemRepositoryMock.Setup(m => m.Get(id))
                        .ReturnsAsync(new ItemEntity { Id = id }!);

        //Act
        //Assert
        await _itemService.Invoking(x => x.Delete(id))
                            .Should().NotThrowAsync<Exception>();

        _itemRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
        _itemRepositoryMock.Verify(m => m.Delete(It.IsAny<Guid>()), Times.Once());
    }

    [Fact]
    public async Task Delete_InvalidId_ThrowNotFoundException()
    {
        Guid id = new();

        //Arrange
        _itemRepositoryMock.Setup(m => m.Delete(id));

        _itemRepositoryMock.Setup(m => m.Get(id))
                        .ReturnsAsync((ItemEntity)null!);

        //Act
        //Assert
        await _itemService.Invoking(x => x.Delete(id))
                            .Should().ThrowAsync<NotFoundException>();

        _itemRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
    }
}