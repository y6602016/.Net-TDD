using CloudCustomers.API.Models;

namespace CloudCustomers.UnitTests.Fixtures;

public static class UsersFixture
{
    public static List<User> GetTestUsers() => new()
    {
        new User
        {
            Name = "Test User 1",
            Email = "test1@gamil.com",
            Address = new Address
            {
                Street = "123 street",
                City = "123 city",
                ZipCode = "123"
            }
        },
        new User
        {
            Name = "Test User 2",
            Email = "test2@gamil.com",
            Address = new Address
            {
                Street = "123 street",
                City = "123 city",
                ZipCode = "123"
            }
        },
        new User
        {
            Name = "Test User 3",
            Email = "test3@gamil.com",
            Address = new Address
            {
                Street = "123 street",
                City = "123 city",
                ZipCode = "123"
            }
        }
    };
}