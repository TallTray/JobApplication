﻿using Main.Domain.Common;
using Main.Domain.EmployeeDomain;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("main.DomainTest")]

namespace Main.Domain.WorkflowTemplateDomain
{
    /// <summary>
    /// Шаг в шаблоне Workflow
    /// </summary>
    public class WorkflowStepTemplate
    {
        protected WorkflowStepTemplate(
            int number, 
            string description, 
            Guid? employeeId, 
            Guid? roleId, 
            DateTime dateCreate, 
            DateTime dateUpdate)
        {
            if (number < 1)
            {
                throw new ArgumentOutOfRangeException("Некорректный номер шага процесса");
            }

            if (employeeId is null && roleId is null)
            {
                throw new ArgumentNullException (("У шага должна быть привязка либо к конкретногому сотруднику либо к должности"));
            }

            if (employeeId is not null && employeeId == Guid.Empty)
            {
                throw new ArgumentNullException($"{employeeId} - некорректное значение для идентификатора сотрудника в шаге");
            }

            if (roleId is not null && roleId == Guid.Empty)
            {
                throw new ArgumentNullException($"{roleId} - некорректное значение для идентификатора должности в шаге");
            }

            if (dateCreate == DateTime.MinValue)
            {
                throw new ArgumentException("Дата создания не может быть дефолтной.");
            }

            if (dateUpdate == DateTime.MinValue)
            {
                throw new ArgumentException("Дата обновления не может быть дефолтной.");
            }

            Number = number;
            Description = description;
            EmployeeId = employeeId;
            RoleId = roleId;
            DateCreate = dateCreate;
            DateUpdate = dateUpdate;
        }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime DateCreate { get; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime DateUpdate { get; private set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        /// Идентификатор сотрудника, который должен будет исполнять шаг
        /// </summary>
        public Guid? EmployeeId { get; private set; }

        /// <summary>
        /// Идентификатор должности, которая должна будет исполнять шаг
        /// </summary>
        public Guid? RoleId { get; private set; }


        /// <summary>
        /// Создание нового шага
        /// </summary>
        /// <param name="number">Порядковый номер</param>
        /// <param name="description">Описание</param>
        /// <param name="employeeId">Идентификатор сотрудника</param>
        /// <param name="roleId">Идентификатор роли</param>
        /// <returns></returns>
        internal static Result<WorkflowStepTemplate> Create(int number, string description, Guid? employeeId, Guid? roleId)
        {
            #region Тестовая ошибка
            if (description == "test-failure")
            {
                return Result<WorkflowStepTemplate>.Failure("Тестовая ошибка при создании шага");
            }
            #endregion

            if (number <= 0)
            {
                return Result<WorkflowStepTemplate>.Failure($"{number} - некорректное значение для номера шага");
            }

            if (string.IsNullOrEmpty(description))
            {
                return Result<WorkflowStepTemplate>.Failure("Описание шаблона процесса не может быть пустым");
            }

            if (employeeId is null && roleId is null)
            {
                return Result<WorkflowStepTemplate>.Failure("У шага должна быть привязка либо к конкретногому сотруднику либо к должности");
            }

            if (employeeId is not null && employeeId == Guid.Empty)
            {
                return Result<WorkflowStepTemplate>.Failure($"{employeeId} - некорректное значение для идентификатора сотрудника в шаге");
            }

            if (roleId is not null && roleId == Guid.Empty)
            {
                return Result<WorkflowStepTemplate>.Failure($"{roleId} - некорректное значение для идентификатора должности в шаге");
            }

            var stepTemplate = new WorkflowStepTemplate(
                number, 
                description, 
                employeeId, 
                roleId, 
                DateTime.UtcNow, 
                DateTime.UtcNow);

            return Result<WorkflowStepTemplate>.Success(stepTemplate);
        }

        /// <summary>
        /// Обновление данных шага
        /// </summary>
        /// <param name="description">Описание</param>
        /// <returns></returns>
        /// 
        public Result<bool> UpdateInfo(string description)
        {
            var isChanged = false;

            if (Description != description.Trim())
            {
                Description = description.Trim();
                isChanged = true;
            }

            if (isChanged)
            {
                DateUpdate = DateTime.UtcNow;
            }

            return Result<bool>.Success(true);
        }

        /// <summary>
        /// Обновление номера
        /// </summary>
        /// <param name="number">Новый номер</param>
        /// <returns></returns>
        public Result<bool> UpdateNumber(int number)
        {
            if (number <= 0)
            {
                return Result<bool>.Failure($"{number} - некорректное значение для номера шага");
            }

            var isChanged = false;

            if (number != Number)
            {
                Number = number;
                isChanged = true;
            }

            if (isChanged)
            {
                DateUpdate = DateTime.UtcNow;
            }

            return Result<bool>.Success(true);
        }

        /// <summary>
        /// Обновление идентификатора должности
        /// </summary>
        /// <param name="roleId">новый идентификатор должности</param>
        /// <returns></returns>
        public Result<bool> UpdateRoleId(Guid roleId)
        {
            if (roleId == Guid.Empty)
            {
                return Result<bool>.Failure($"{roleId} - некорректный идентификатор должности");
            }

            var isChanged = false;

            if (roleId != RoleId)
            {
                RoleId = roleId;
                isChanged = true;
            }

            if (isChanged)
            {
                DateUpdate = DateTime.UtcNow;
            }

            return Result<bool>.Success(true);
        }
        /// <summary>
        /// Обновление идентификатора работника
        /// </summary>
        /// <param name="employeeId">Новый идентификатор работника</param>
        /// <returns></returns>
        public Result<bool> UpdateEmployeeId(Guid employeeId)
        {
            if (employeeId == Guid.Empty)
            {
                return Result<bool>.Failure($"{employeeId} - некорректный идентификатор сотрудника");
            }

            var isChanged = false;

            if (employeeId != EmployeeId) 
            {
                EmployeeId = employeeId;
                isChanged = true;
            }

            if (isChanged)
            {
                DateUpdate = DateTime.UtcNow;
            }

            return Result<bool>.Success(true);
        }
    }
}