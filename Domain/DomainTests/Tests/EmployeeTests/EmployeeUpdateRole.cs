using Main.Domain.EmployeeDomain;

namespace main.DomainTest.Tests.EmployeeTests;

public class EmployeeUpdateName
{
    private readonly IFixture _fixture;
    private Employee _employee;

    public EmployeeUpdateName()
    {
        _fixture = new Fixture();
        _fixture.FixtureCustomization();

        _employee = _fixture.Create<Employee>();
    }

    public static IEnumerable<object[]> GetInvalidData()
    {
        yield return new object[]
        {
                Guid.Empty,
                "Некорректное значение идентификатора должности"
        };
    }

    [Theory]
    [MemberData(nameof(GetInvalidData))]
    public void Failed_UpdateRole_Employee_MethodResult_With_Invalid_Data(
        Guid role,
        string expectError)
    {
        var dateUpdate = _employee.DateUpdate;
        var lastRole = _employee.RoleId;
        var resultUpdate = _employee.UpdateRole(role);

        Assert.True(resultUpdate.IsFailure);
        Assert.Equal(resultUpdate.Error, expectError);
        Assert.Equal(lastRole, _employee.RoleId);
        Assert.NotEqual(role, _employee.RoleId);
        Assert.False(dateUpdate < _employee.DateUpdate);
    }

    [Fact]
    public void Success_UpdateRole_Employee_With_Valid_Data()
    {
        var dateUpdate = _employee.DateUpdate;
        var lastRole = _employee.RoleId;
        var validId = Guid.NewGuid();

        var resultUpdate = _employee.UpdateRole(validId);

        Assert.True(resultUpdate.IsSuccess);
        Assert.NotEqual(_employee.RoleId, lastRole);
        Assert.Equal(_employee.RoleId, validId);
        Assert.True(dateUpdate < _employee.DateUpdate);
    }

    [Fact]
    public void Success_UpdateRole_Employee_With_Same_Data()
    {
        var dateUpdate = _employee.DateUpdate;
        var lastRole = _employee.RoleId;

        var resultUpdate = _employee.UpdateRole(validId);

        Assert.True(resultUpdate.IsSuccess);
        Assert.Equal(_employee.RoleId, lastRole);
        Assert.False(dateUpdate < _employee.DateUpdate);
    }
}

