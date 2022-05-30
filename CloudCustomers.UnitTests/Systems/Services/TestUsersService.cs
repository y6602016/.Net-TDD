using CloudCustomers.API.Config;
using CloudCustomers.API.Models;
using CloudCustomers.API.Services;
using CloudCustomers.UnitTests.Fixtures;
using CloudCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace CloudCustomers.UnitTests.Systems.Services;

public class TestUsersService
{
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
    {
        // Arrange
        // we need to create a http client object that can send Http request, then we pass it
        // as the dependency to the service to test
        var ExpectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(ExpectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com/users";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UsersService(httpClient, config);
        
        // Act
        await sut.GetAllUsers();
        
        // Assert
        // Verify Http request is made
        handlerMock
            .Protected()
            .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                ItExpr.IsAny<CancellationToken>());
    }

    [Fact]
    public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
    {
        // Arrange
        var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com/users";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UsersService(httpClient, config);
        
        // Act
        var result = await sut.GetAllUsers();
        
        // Assert
        result.Count.Should().Be(0);
    }
    
    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
    {
        // Arrange
        var ExpectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(ExpectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        var endpoint = "https://example.com/users";
        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        var sut = new UsersService(httpClient, config);
        
        // Act
        var result = await sut.GetAllUsers();
        
        // Assert
        result.Count.Should().Be(ExpectedResponse.Count);
    }
    
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesConfigureExternalUrl()
    {
        // Arrange
        var ExpectedResponse = UsersFixture.GetTestUsers();
        var endpoint = "https://example.com/users";
        var handlerMock = MockHttpMessageHandler<User>
            .SetupBasicGetResourceList(ExpectedResponse, endpoint);
        var httpClient = new HttpClient(handlerMock.Object);

        var config = Options.Create(new UsersApiOptions
        {
            Endpoint = endpoint
        });
        
        var sut = new UsersService(httpClient, config);
        
        // Act
        var result = await sut.GetAllUsers();
        var uri = new Uri(endpoint);
        
        // Assert
        handlerMock
            .Protected()
            .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(
                    req => req.Method == HttpMethod.Get 
                           && req.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>());
    }
    
}