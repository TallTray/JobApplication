using Main.Domain.EmployeeDomain;

namespace main.DomainTest.Tests.EmployeeTests;

    public class EmployeeConstructor
    {
        public class EmployeeCtor : Employee
        {
            public RoleCtor(
                Guid id,
                string name,
                Guid companyId,
                Guid roleId,
                DateTime dateCreate,
                DateTime dateUpdate)
                : base(id, name, companyId,roleId, dateCreate, dateUpdate)
            { }
        }

        private readonly IFixture _fixture;
        public RoleConstructor()
        {
            _fixture = new Fixture();
        }

        public static IEnumerable<object[]> GetUnvalidData()
        {
            yield return new object[]
            {
                Guid.Empty,
                "Valid Name",
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateTime.UtcNow,
                DateTime.UtcNow,
                typeof(ArgumentNullException),
                $"{Guid.Empty} - некорректный идентификатор должности"
            };

            yield return new object[]
            {
                Guid.NewGuid(),
                "",
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateTime.UtcNow,
                DateTime.UtcNow,
                typeof(ArgumentNullException),
                "Наименование должности не может быть пустым"
            };

            yield return new object[]
            {
                Guid.NewGuid(),
                "Valid Name",
                Guid.Empty,
                Guid.NewGuid(),
                DateTime.UtcNow,
                DateTime.UtcNow,
                typeof(ArgumentNullException),
                $"{Guid.Empty} - некорректный идентификатор компании"
            };
            yield return new object[]
            {
                Guid.NewGuid(),
                "Valid Name",
                Guid.NewGuid(),
                Guid.Empty,
                DateTime.UtcNow,
                DateTime.UtcNow,
                typeof(ArgumentNullException),
                $"{Guid.Empty} - некорректный идентификатор роли"
            };
            yield return new object[]
            {
                Guid.NewGuid(),
                "Valid Name",
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateTime.MinValue,
                DateTime.UtcNow,
                typeof(ArgumentException),
                "Дата создания не может быть дефолтной"
            };

            yield return new object[]
            {
                Guid.NewGuid(),
                "Valid Name",
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateTime.UtcNow,
                DateTime.MinValue,
                typeof(ArgumentException),
                "Дата обновления не может быть дефолтной"
            };

            yield return new object[]
            {
                Guid.NewGuid(),
                "te",
                Guid.NewGuid(),
                Guid.NewGuid(),
                DateTime.UtcNow,
                DateTime.UtcNow,
                typeof(ArgumentException),
                $"Длина наименования должности не может быть меньше {Role.MinLengthName}"
            };
        }

        [Fact]
        public void Success_Ctor_Create_With_Valid_Data()
        {
            var id = _fixture.Create<Guid>();
            var name = _fixture.Create<string>();
            var companyId = _fixture.Create<Guid>();
            var roleId = _fixture.Create<Guid>();
            var dateCreate = _fixture.Create<DateTime>();
            var dateUpdate = _fixture.Create<DateTime>();

            var employee = new EmployeeCtor(
                id,
                name,
                companyId,
                roleId,
                dateCreate,
                dateUpdate);

            Assert.NotNull(employee);
            Assert.Equal(id, employee.Id);
            Assert.Equal(name, employee.Name);
            Assert.Equal(companyId, employee.CompanyId);
            Assert.Equal(roleId, employee.RoleId);
            Assert.Equal(dateCreate, employee.DateCreate);
            Assert.Equal(dateUpdate, employee.DateUpdate);
        }

        [Theory]
        [MemberData(nameof(GetUnvalidData))]
        public void Failed_Ctor_Create_With_UnvalidData(
                Guid id,
                string name,
                Guid companyId,
                Guid roleId,
                DateTime dateCreate,
                DateTime dateUpdate,
                Type exeptionType,
                string exeptionMessage)
        {
            var exeption = Assert.Throws(exeptionType, () =>
                new EmployeeCtor(
                    id,
                    name,
                    companyId,
                    roleId,
                    dateCreate,
                    dateUpdate));

            Assert.NotNull(exeption);
            Assert.Contains(exeptionMessage, exeption.Message);
        }


    }

