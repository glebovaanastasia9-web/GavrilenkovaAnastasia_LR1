namespace Calculator.Services.Interfaces
{
    /// <summary>
    /// Контракт сервиса вычислений. Отделяет арифметическую логику
    /// от кода представления (Views), чтобы окна не знали, как именно
    /// разбираются и считаются выражения.
    /// </summary>
    public interface ICalculatorService
    {
        string InputDigit(string currentDisplay, string digit);
        string InputComma(string currentDisplay);
        string SetOperation(string currentDisplay, string pressedOp, out string newDisplay);
        string PressEquals(string currentDisplay, out string histLine);
        string PressSci(string currentDisplay, string func, out string histLine);
        string Backspace(string currentDisplay);
        string Negate(string currentDisplay);
        void Clear();
        string FormatNumber(double value);
    }
}
