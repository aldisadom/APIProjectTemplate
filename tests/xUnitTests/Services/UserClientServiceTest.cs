using Application.DTO.User;
using Application.Services;
using AutoFixture.Xunit2;
using Domain.Dto;
using Domain.Exceptions;
using Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace xUnitTests.Services;

public class UserClientServiceTest
{
    private readonly Mock<IGeneralClient> _userClientMock;
    private readonly UserClientService _userClientService;

    private readonly Mock<IItemRepository> _itemRepositoryMock;
    private readonly ItemService _itemService;

    private readonly Mock<IShopRepository> _shopRepositoryMock;
    private readonly ShopService _shopService;

    public UserClientServiceTest()
    {
        _itemRepositoryMock = new Mock<IItemRepository>();
        _shopRepositoryMock = new Mock<IShopRepository>();
        _shopService = new ShopService(_shopRepositoryMock.Object);
        _itemService = new ItemService(_itemRepositoryMock.Object, _shopService);
        _userClientMock = new Mock<IGeneralClient>();
        _userClientService = new UserClientService(_userClientMock.Object, _itemService);
    }

    [Theory]
    [AutoData]
    public async Task GetId_GivenValidId_ReturnsDTO(int id)
    {
        //Arrange
        _userClientMock.Setup(m => m.Get<UserDto>(It.IsAny<string>(), id))
                        .ReturnsAsync(new JsonPlaceholderResult<UserDto>
                        {
                            IsSuccessful = true,
                            Data = new UserDto { Id = id },
                        });

        //Act
        UserDto result = await _userClientService.Get(id);

        //Assert
        result.Id.Should().Be(id);

        _userClientMock.Verify(m => m.Get<UserDto>(It.IsAny<string>(), It.IsAny<int>()), Times.Once());
    }

    [Theory]
    [AutoData]
    public async Task GetId_GivenInvalidId_ThrowNotFoundException(int id)
    {
        //Arrange
        _userClientMock.Setup(m => m.Get<UserDto>(It.IsAny<string>(), id))
                        .ReturnsAsync(new JsonPlaceholderResult<UserDto>
                        {
                            IsSuccessful = false,
                            Data = new UserDto { Id = id },
                        });

        // Act Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => await _userClientService.Get(id));

        _userClientMock.Verify(m => m.Get<UserDto>(It.IsAny<string>(), It.IsAny<int>()), Times.Once());
    }
}
