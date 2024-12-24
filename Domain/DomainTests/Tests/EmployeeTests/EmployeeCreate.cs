using Main.Domain.EmployeeDomain;

namespace main.DomainTest.Tests.EmployeeTests;

public class EmployeeCreate
{
    private readonly IFixture _fixture;

    public EmployeeCreate()
    {
        _fixture = new Fixture();
    }

    public static IEnumerable<object[]> GetInvalidData()
    {
        yield return new object[]
        {
                "",
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Наименование должности не может быть пустым"
        };

        yield return new object[]
        {
                "Valid Name",
                Guid.Empty,
                $"{Guid.Empty} - некорректный идентификатор компании"
        };

        yield return new object[]
        {
                "Valid Name",
                Guid.NewGuid(),
                Guid.Empty,
                $"{Guid.Empty} - некорректный идентификатор роли"
        };

        yield return new object[]
        {
                "te",
                Guid.NewGuid(),
                Guid.NewGuid(),
                $"Длина наименования должности не может быть меньше {Role.MinLengthName}"
        };
    }

    [Fact]
    public void Success_Role_Create_StaticMethod_With_Valid_Data()
    {
        var name = _fixture.Create<string>().Substring(0, Role.MinLengthName + 1);
        var companyId = _fixture.Create<Guid>();
        var roleId = _fixture.Create<Guid>();
        var resultCreateEmployee = Employee.Create(name, companyId,roleId);
        var employee = resultCreateEmployee.Value;

        Assert.True(resultCreateEmployee.IsSuccess);
        Assert.NotNull(employee);
        Assert.Equal(name, employee.Name);
        Assert.Equal(companyId, employee.CompanyId);
        Assert.Equal(roleId, employee.RoleId);
    }

    [Theory]
    [MemberData(nameof(GetInvalidData))]
    public void Failed_Role_Create_With_Invalid_Data(
        string name,
        Guid companyId,
        Guid roleId,
        string expectedError)
    {
        var resultCreateEmployee = Employee.Create(name, companyId, roleId);
        var employee = resultCreateEmployee.Value;

        Assert.True(resultCreateEmployee.IsFailure);
        Assert.False(resultCreateEmployee.IsSuccess);
        Assert.Null(employee);
        Assert.Equal(resultCreateEmployee.Error, expectedError);
    }
}

