﻿using main.DomainTest.TestTools.Autofixture;
using Main.Domain.WorkflowTemplateDomain;

namespace main.DomainTest.Tests.WorkflowTemplateTests
{   /// <summary>
    /// Класс тестов для создания WorkflowTemplate
    /// </summary>
    public class WorkflowTemplateCreate
    {
        private readonly IFixture _fixture;

        public WorkflowTemplateCreate()
        {
            _fixture = new Fixture();
            _fixture.FixtureCustomization();
        }

        public static IEnumerable<object[]> GetInvalidInputs()
        {
            // Неправильный CompanyId
            yield return new object[]
            {
                "Valid name",
                "Valid description",
                Guid.Empty,
                $"{Guid.Empty} - некорректный идентификатор компании"
            };

            // Пустое имя шаблона
            yield return new object[]
            {
                string.Empty,
                "Valid descriotion",
                Guid.NewGuid(),
                "Наименование шаблона не может быть пустым"
            };

            // Пустое описание шаблона
            yield return new object[]
            {
                "Valid name",
                string.Empty,
                Guid.NewGuid(),
                "Описание процесса не может быть пустым"
            };

            // Длина имени шаблона меньше требуемой
            yield return new object[]
            {
                new string('a', WorkflowTemplate.MinLengthName - 1),
                "Valid descriotion",
                Guid.NewGuid(),
                $"Длина наименование шаблона не может быть меньше {WorkflowTemplate.MinLengthName}"
            };
        }

        /// <summary>
        /// Тестирование создания WorkflowTemplate при валидных данных 
        /// </summary>
        [Fact]
        public void Create_WorkflowTemplate_With_Valid_Data()
        {
            var validName = _fixture.Create<string>().Substring(0, WorkflowTemplate.MinLengthName + 1);
            var validDescription = _fixture.Create<string>();
            var validCompanyId = _fixture.Create<Guid>();

            var workflowTemplateResult = WorkflowTemplate.Create(
                                                        validName,
                                                        validDescription,
                                                        validCompanyId);

            Assert.NotNull(workflowTemplateResult);
            Assert.True(workflowTemplateResult.IsSuccess);
        }

        /// <summary>
        /// Кейс-тест с набором невалидных данных
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="companyId"></param>
        /// <param name="expectedErrorMessage"></param>
        [Theory]
        [MemberData(nameof(GetInvalidInputs))]
        public void Create_ShouldReturnFailure_WhenInvalidInput(
        string name,
        string description,
        Guid companyId,
        string expectedErrorMessage)
        {
            var result = WorkflowTemplate.Create(name, description, companyId);

            Assert.True(result.IsFailure);
            Assert.Equal(expectedErrorMessage, result.Error);
        }
    }
}