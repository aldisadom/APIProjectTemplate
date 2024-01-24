using Application.DTO.Item;
using Application.Services;
using AutoFixture;
using AutoFixture.Xunit2;
using Domain.Entities;

using Domain.Exceptions;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace ShopV2.UnitTest.Services;

public class ItemServiceTest
{

    private readonly Mock<IItemRepository> _itemRepositoryMock;
    private readonly Mock<IShopRepository> _shopRepositoryMock;
    private readonly ItemService _itemService;
    private readonly ShopService _shopService;

    public ItemServiceTest()
    {
        _itemRepositoryMock = new Mock<IItemRepository>();
        _shopRepositoryMock = new Mock<IShopRepository>();
        _shopService = new ShopService(_shopRepositoryMock.Object);
        _itemService = new ItemService(_itemRepositoryMock.Object, _shopService);
    }

    [Theory]
    [AutoData]
    public async Task GetId_GivenValidId_ReturnsDTO(Guid id)
    {
        //Arrange
        _itemRepositoryMock.Setup(m => m.Get(id))
                        .ReturnsAsync(new ItemEntity { Id = id });

        //Act
        ItemDto result = await _itemService.Get(id);

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
                                (x => x.Name == name && x.Price == price && x.IsDeleted == false)))
                                 .ReturnsAsync(id);

        //Act
        Guid result = await _itemService.Add(new ItemAddDto { Name = name, Price = price });

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
                                { Id = id, Name = name, Price = price, IsDeleted = false });

        //Act
        //Assert
        await _itemService.Invoking(x => x.Update(id, new ItemAddDto
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
        await _itemService.Invoking(x => x.Update(id, new ItemAddDto { Name = name, Price = price }))
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
        await _itemService.Invoking(x => x.Update(id, new ItemAddDto { Name = name, Price = price }))
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

    [Theory]
    [AutoData]
    public async Task AddToBought_NotThrowException(BuyItemEntity buyItemEntity)
    {
        //Arrange
        _itemRepositoryMock.Setup(m => m.AddToBought(It.Is<BuyItemEntity>
                                (x => x.ShopId == buyItemEntity.ShopId && x.UserId == buyItemEntity.UserId
                                && x.ItemId == buyItemEntity.ItemId && x.Price == buyItemEntity.Price && x.Quantity == buyItemEntity.Quantity)));
        //Act
        //Assert
        await _itemService.Invoking(x => x.AddToBought(buyItemEntity))
                                .Should().NotThrowAsync<Exception>();

        _itemRepositoryMock.Verify(m => m.AddToBought(It.IsAny<BuyItemEntity>()), Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task AddToShop_InvalidItemId_ThrowNotFoundException(ItemEntity item, ShopEntity shop)
    {
        //Arrange
        _itemRepositoryMock.Setup(m => m.Get(item.Id))
                        .ReturnsAsync((ItemEntity)null!);

        //Act
        //Assert
        await _itemService.Invoking(x => x.AddToShop(item.Id, shop.Id))
                                .Should().ThrowAsync<NotFoundException>();

        _itemRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
        _shopRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Never());
        _itemRepositoryMock.Verify(m => m.Update(It.IsAny<ItemEntity>()), Times.Never());
    }

    [Theory]
    [AutoData]
    public async Task AddToShop_InvalidShopId_ThrowNotFoundException(ItemEntity item, ShopEntity shop)
    {
        //Arrange
        _itemRepositoryMock.Setup(m => m.Get(item.Id))
                             .ReturnsAsync(new ItemEntity { Id = item.Id });

        _shopRepositoryMock.Setup(m => m.Get(shop.Id))
                        .ReturnsAsync((ShopEntity)null!);
        //Act
        //Assert
        await _itemService.Invoking(x => x.AddToShop(item.Id, shop.Id))
                                .Should().ThrowAsync<NotFoundException>();

        _itemRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
        _shopRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
        _itemRepositoryMock.Verify(m => m.Update(It.IsAny<ItemEntity>()), Times.Never());
    }

    [Theory]
    [AutoData]
    public async Task AddToShop_Valid_EditMultipleLine_ThrowInvalidOperationException(ItemEntity item, ShopEntity shop)
    {
        //Arrange
        _itemRepositoryMock.Setup(m => m.Get(item.Id))
                             .ReturnsAsync(new ItemEntity { Id = item.Id });

        _shopRepositoryMock.Setup(m => m.Get(shop.Id))
                        .ReturnsAsync(new ShopEntity { Id = shop.Id });

        _itemRepositoryMock.Setup(m => m.Update(It.Is<ItemEntity>(x => x.Id == item.Id)))
                             .ReturnsAsync(2);
        //Act
        //Assert
        await _itemService.Invoking(x => x.AddToShop(item.Id, shop.Id))
                                .Should().ThrowAsync<InvalidOperationException>();

        _itemRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
        _shopRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
        _itemRepositoryMock.Verify(m => m.Update(It.IsAny<ItemEntity>()), Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task AddToShop_Valid_NotThrowException(ItemEntity item, ShopEntity shop)
    {
        //Arrange
        _itemRepositoryMock.Setup(m => m.Get(item.Id))
                             .ReturnsAsync(new ItemEntity { Id = item.Id });

        _shopRepositoryMock.Setup(m => m.Get(shop.Id))
                        .ReturnsAsync(new ShopEntity { Id = shop.Id });

        _itemRepositoryMock.Setup(m => m.Update(It.Is<ItemEntity>(x => x.Id == item.Id)))
                             .ReturnsAsync(1);
        //Act
        //Assert
        await _itemService.Invoking(x => x.AddToShop(item.Id, shop.Id))
                                .Should().NotThrowAsync<Exception>();

        _itemRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
        _shopRepositoryMock.Verify(m => m.Get(It.IsAny<Guid>()), Times.Once());
        _itemRepositoryMock.Verify(m => m.Update(It.IsAny<ItemEntity>()), Times.Once());
    }

    [Fact]
    public async Task Buy_InvalidQuantity()
    {
        //Arrange
        uint quantity = 0;
        ItemDto item = new() { Price = 15.0m, Id = new Guid() };

        //Act
        //Assert
        _itemService.Invoking(x => x.GetItemsPrice(It.Is<ItemDto>(x => x.Id == item.Id && x.Price == item.Price), quantity))
                        .Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(10.0, 5, 50.0)]
    public async Task Buy_NoDiscount(decimal price, uint quantity, decimal totalCost)
    {
        //Arrange
        ItemDto item = new() { Price = price, Id = new Guid() };

        //Act
        //Assert
        decimal result = _itemService.GetItemsPrice(item, quantity);

        result.Should().Be(totalCost);
    }

    [Theory]
    [InlineData(10.0, 20, 180.0)]
    public async Task Buy_Discount10Percent(decimal price, uint quantity, decimal totalCost)
    {
        //Arrange
        ItemDto item = new() { Price = price, Id = new Guid() };

        //Act
        //Assert
        decimal result = _itemService.GetItemsPrice(item, quantity);

        result.Should().Be(totalCost);
    }

    [Theory]
    [InlineData(10.0, 40, 320.0)]
    public async Task Buy_Discount20Percent(decimal price, uint quantity, decimal totalCost)
    {
        //Arrange
        ItemDto item = new() { Price = price, Id = new Guid() };

        //Act
        //Assert
        decimal result = _itemService.GetItemsPrice(item, quantity);

        result.Should().Be(totalCost);
    }
}
