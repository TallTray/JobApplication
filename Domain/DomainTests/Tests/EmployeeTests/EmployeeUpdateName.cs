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
                "",
                "Наименование должности не может быть пустым"
        };

        yield return new object[]
        {
                "te",
                $"Длина наименование должности не может быть меньше {Employee.MinLengthName}"
        };
    }

    [Theory]
    [MemberData(nameof(GetInvalidData))]
    public void Failed_UpdateName_Employee_MethodResult_With_Invalid_Data(
        string name,
        string expectError)
    {
        var dateUpdate = _employee.DateUpdate;
        var lastName = _employee.Name;
        var resultUpdate = _employee.UpdateName(name);

        Assert.True(resultUpdate.IsFailure);
        Assert.Equal(resultUpdate.Error, expectError);
        Assert.Equal(lastName, _employee.Name);
        Assert.NotEqual(name, _employee.Name);
        Assert.False(dateUpdate < _employee.DateUpdate);
    }

    [Fact]
    public void Success_UpdateName_Employee_With_Valid_Data()
    {
        var dateUpdate = _employee.DateUpdate;
        var lastName = _employee.Name;
        var validName = _fixture.Create<string>().Substring(0, Role.MinLengthName + 1);

        var resultUpdate = _employee.UpdateName(validName);

        Assert.True(resultUpdate.IsSuccess);
        Assert.NotEqual(_employee.Name, lastName);
        Assert.Equal(_employee.Name, validName);
        Assert.True(dateUpdate < _employee.DateUpdate);
    }

    [Fact]
    public void Success_UpdateName_Employee_With_Same_Data()
    {
        var dateUpdate = _employee.DateUpdate;
        var lastName = _employee.Name;

        var resultUpdate = _employee.UpdateName(_employee.Name);

        Assert.True(resultUpdate.IsSuccess);
        Assert.Equal(_employee.Name, lastName);
        Assert.False(dateUpdate < _employee.DateUpdate);
    }
}

