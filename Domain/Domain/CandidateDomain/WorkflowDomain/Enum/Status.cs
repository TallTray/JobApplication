namespace Domain.CandidateDomain.WorkflowDomain.Enum;

/// <summary>
/// Статус объекта
/// </summary>
public enum Status
{
    /// <summary>
    /// Пустное значение, возникающее в случае ошибки
    /// </summary>
    Default = 0,

    /// <summary>
    /// Ожидание
    /// </summary>
    InProgress = 1,

    /// <summary>
    /// Одобренно
    /// </summary>
    Approved = 2,

    /// <summary>
    /// Отказано
    /// </summary>
    Rejected = 3
}
