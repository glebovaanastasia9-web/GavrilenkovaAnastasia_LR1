using System.Collections.Generic;
using System.IO;
using System.Linq;
using Calculator.Services.Interfaces;

namespace Calculator.Services
{
    /// <summary>
    /// Реализация IHistoryService поверх текстового файла history.log.
    /// Выделена из Calculations.cs, где раньше была перемешана
    /// с арифметикой (нарушение единственной ответственности).
    /// </summary>
    public class HistoryService : IHistoryService
    {
        private readonly string historyFilePath;

        public HistoryService(string historyFilePath = "history.log")
        {
            this.historyFilePath = historyFilePath;
        }

        public void AppendEntry(string line)
        {
            try
            {
                File.AppendAllText(historyFilePath, line + "\n");
            }
            catch
            {
                // Ошибки записи истории не должны прерывать вычисление,
                // поэтому намеренно подавляются (как и в исходной реализации).
            }
        }

        public List<string> LoadHistory()
        {
            if (!File.Exists(historyFilePath)) return new List<string>();
            return File.ReadAllLines(historyFilePath).ToList();
        }

        public void ClearHistory()
        {
            if (File.Exists(historyFilePath)) File.WriteAllText(historyFilePath, "");
        }
    }
}
