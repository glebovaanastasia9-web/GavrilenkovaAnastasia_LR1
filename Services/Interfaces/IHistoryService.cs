using System.Collections.Generic;

namespace Calculator.Services.Interfaces
{
    /// <summary>
    /// Контракт сервиса хранения истории вычислений.
    /// Позволяет заменить реализацию (файл, БД, память) без изменения кода,
    /// который им пользуется.
    /// </summary>
    public interface IHistoryService
    {
        void AppendEntry(string line);
        List<string> LoadHistory();
        void ClearHistory();
    }
}
