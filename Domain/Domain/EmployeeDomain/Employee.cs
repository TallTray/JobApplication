﻿using Main.Domain.Common;

namespace Main.Domain.EmployeeDomain;

/// <summary>
/// Класс сотрудника
/// </summary>
public class Employee
{
    /// <summary>
    /// Минимая длина наименования сотрудника
    /// </summary>
    public const int MinLengthName = 5;
    private Employee(
        Guid id, 
        string name, 
        Guid companyId, 
        Guid roleId, 
        DateTime dateCreate, 
        DateTime dateUpdate)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException($"{id} - некорректный идентификатор процесса");
        }

        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException(nameof(name),"ФИО сотрудника не может быть пустым");
        }

        if (companyId == Guid.Empty)
        {
            throw new ArgumentException($"{companyId} - некорректный идентификатор компании");
        }

        if (roleId == Guid.Empty)
        {
            throw new ArgumentException($"{roleId} - некорректный идентификатор должности");
        }

        if (dateCreate == DateTime.MinValue)
        {
            throw new ArgumentException("Дата создания не может быть дефолтной.");
        }

        if (dateUpdate == DateTime.MinValue)
        {
            throw new ArgumentException("Дата обновления не может быть дефолтной.");
        }

        if (name.Trim().Length < MinLengthName)
        {
            throw new ArgumentException($"Длина ФИО сотрудника не может быть меньше {MinLengthName}");
        }

        Id = id;
        Name = name;
        CompanyId = companyId;
        RoleId = roleId;
        DateCreate = dateCreate;
        DateUpdate = dateUpdate;
    }

    /// <summary>
    /// Метод создания сотрудника
    /// </summary>
    /// <param name="name">ФИО сотрудника</param>
    /// <param name="companyId">Идентификатор компании</param>
    /// <param name="roleId">Идентификатор роли</param>
    /// <returns>Сущность сотрудника</returns>
    public static Result<Employee> Create(string name, Guid companyId, Guid roleId)
    {
        if (string.IsNullOrEmpty(name))
        {
            return Result<Employee>.Failure("ФИО сотрудника не может быть пустым");
        }

        if (companyId == Guid.Empty)
        {
            return Result<Employee>.Failure("Идентификатор компании некорректен");
        }

        if (roleId == Guid.Empty)
        {
            return Result<Employee>.Failure("Идентификатор должности некорректен");
        }

        if (name.Trim().Length < MinLengthName)
        {
            return Result<Employee>.Failure($"Длина ФИО сотрудника не может быть меньше {MinLengthName}");
        }

        var employee = new Employee(
            Guid.NewGuid(), 
            name, companyId, 
            roleId, 
            DateTime.UtcNow, 
            DateTime.UtcNow);

        return Result<Employee>.Success(employee);
    }

    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTime DateCreate { get; }

    /// <summary>
    /// Дата изменения
    /// </summary>
    public DateTime DateUpdate { get; private set; }

    /// <summary>
    /// ФИО сотрудника
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Индетификатор компании, в которой работает сотрудник
    /// </summary>
    public Guid CompanyId { get; private set; }

    /// <summary>
    /// Идентификатор долности сотрудника
    /// </summary>
    public Guid RoleId { get; private set; }

    /// <summary>
    /// Обновление ФИО сотрудника
    /// </summary>
    /// <param name="name">ФИО</param>
    /// <returns>Успешность выполнения операции</returns>
    public Result<bool> UpdateName(string name)
    {

        if (string.IsNullOrEmpty(name))
        {
            return Result<bool>.Failure("ФИО не может быть пустым");
        }

        if (name.Trim().Length < MinLengthName)
        {
            return Result<bool>.Failure($"Длина наименование должности не может быть меньше {MinLengthName}");
        }

        var isChanged = false;

        if (name.Trim() != Name)
        {
            Name = name.Trim();
            isChanged = true;
        }

        if (isChanged)
        {
            DateUpdate = DateTime.UtcNow;
        }

        return Result<bool>.Success(true);
    }

    /// <summary>
    /// Обновление должности сотрудника
    /// </summary>
    /// <param name="roleId">Идентификатор роли</param>
    /// <returns>Успешность выполнения операции</returns>
    public Result<bool> UpdateRole(Guid roleId)
    {
        if (roleId == Guid.Empty)
        {
            return Result<bool>.Failure("Некорректное значение идентификатора должности");
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
}