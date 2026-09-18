using System;
using Calculator.Services.Interfaces;

namespace Calculator.Services
{
    /// <summary>
    /// Бизнес-логика калькулятора: разбор ввода, арифметика, научные функции,
    /// форматирование чисел. Выделена из старого класса Calculations.cs,
    /// который совмещал вычисления и запись в файл истории.
    /// Запись истории делегируется IHistoryService (внедряется через конструктор),
    /// поэтому CalculatorService не знает, куда и как сохраняется история.
    /// </summary>
    public class CalculatorService : ICalculatorService
    {
        private readonly IHistoryService historyService;

        private double firstNumber = 0;
        private string operation = "";
        private bool newInput = true;

        public CalculatorService(IHistoryService historyService)
        {
            this.historyService = historyService;
        }

        public string InputDigit(string currentDisplay, string digit)
        {
            if (currentDisplay == "Ошибка" || newInput)
            {
                newInput = false;
                return digit;
            }
            if (currentDisplay == "0") return digit;

            int digitCount = 0;
            foreach (char c in currentDisplay)
            {
                if (char.IsDigit(c)) digitCount++;
            }
            if (digitCount >= 15) return currentDisplay;

            return currentDisplay + digit;
        }

        public string InputComma(string currentDisplay)
        {
            if (currentDisplay == "Ошибка" || newInput)
            {
                newInput = false;
                return "0,";
            }
            if (!currentDisplay.Contains(",")) return currentDisplay + ",";
            return currentDisplay;
        }

        public string SetOperation(string currentDisplay, string pressedOp, out string newDisplay)
        {
            if (currentDisplay == "Ошибка") currentDisplay = "0";
            if (!newInput && operation != "")
            {
                newDisplay = Calculate(currentDisplay);
            }
            else
            {
                newDisplay = currentDisplay;
            }
            firstNumber = ParseNumber(newDisplay);
            operation = pressedOp;
            newInput = true;
            return FormatNumber(firstNumber) + " " + operation;
        }

        public string PressEquals(string currentDisplay, out string histLine)
        {
            histLine = "";
            if (operation == "" || currentDisplay == "Ошибка") return currentDisplay;
            double oldFirstNumber = firstNumber;
            string result = Calculate(currentDisplay);
            if (result != "Ошибка")
            {
                histLine = FormatNumber(oldFirstNumber) + " " + operation + " " + currentDisplay + " = " + result;
                historyService.AppendEntry(histLine);
            }
            operation = "";
            return result;
        }

        private string Calculate(string currentDisplay)
        {
            double second = ParseNumber(currentDisplay);
            double result = 0;
            if (operation == "+") result = firstNumber + second;
            else if (operation == "-") result = firstNumber - second;
            else if (operation == "*") result = firstNumber * second;
            else if (operation == "/")
            {
                if (second == 0) return "Ошибка";
                result = firstNumber / second;
            }
            else if (operation == "^") result = Math.Pow(firstNumber, second);
            else if (operation == "%") result = firstNumber * (second / 100.0);
            else return currentDisplay;

            firstNumber = result;
            newInput = true;
            return FormatNumber(result);
        }

        public string PressSci(string currentDisplay, string func, out string histLine)
        {
            histLine = "";
            if (currentDisplay == "Ошибка") return "Ошибка";
            double number = ParseNumber(currentDisplay);
            double result = 0;

            if (func == "sin")
            {
                result = Math.Sin(number * Math.PI / 180);
                result = RoundIfTiny(result);
                histLine = "sin(" + number + ")";
            }
            else if (func == "cos")
            {
                result = Math.Cos(number * Math.PI / 180);
                result = RoundIfTiny(result);
                histLine = "cos(" + number + ")";
            }
            else if (func == "tg")
            {
                if (Math.Abs(number % 180) == 90) return "Ошибка";
                result = Math.Tan(number * Math.PI / 180);
                result = RoundIfTiny(result);
                histLine = "tg(" + currentDisplay + ")";
            }
            else if (func == "ctg")
            {
                if (Math.Abs(number % 180) == 0) return "Ошибка";
                result = 1 / Math.Tan(number * Math.PI / 180);
                result = RoundIfTiny(result);
                histLine = "ctg(" + currentDisplay + ")";
            }
            else if (func == "log")
            {
                if (number <= 0) return "Ошибка";
                result = Math.Log10(number);
                histLine = "log(" + number + ")";
            }
            else if (func == "ln")
            {
                if (number <= 0) return "Ошибка";
                result = Math.Log(number);
                histLine = "ln(" + number + ")";
            }
            else if (func == "sqrt")
            {
                if (number < 0) return "Ошибка";
                result = Math.Sqrt(number);
                histLine = "sqrt(" + number + ")";
            }
            else if (func == "1/x")
            {
                if (number == 0) return "Ошибка";
                result = 1 / number;
                histLine = "1/(" + number + ")";
            }
            else if (func == "x!")
            {
                if (number < 0 || number > 170) return "Ошибка";
                if (number != Math.Floor(number)) return "Ошибка";
                result = Factorial((int)number);
                histLine = number + "!";
            }
            else if (func == "x^n" || func == "%")
            {
                string localOp;
                if (func == "x^n")
                {
                    localOp = "^";
                }
                else
                {
                    localOp = "%";
                }

                if (!newInput && operation != "")
                {
                    string intermediateHistory = FormatNumber(firstNumber) + " " + operation + " " + currentDisplay;

                    currentDisplay = Calculate(currentDisplay);
                    number = ParseNumber(currentDisplay);

                    histLine = currentDisplay + " " + localOp;

                    historyService.AppendEntry(intermediateHistory + " = " + currentDisplay);
                }
                else
                {
                    histLine = currentDisplay + " " + localOp;
                }

                firstNumber = number;
                operation = localOp;
                newInput = true;
                return currentDisplay;
            }
            else return currentDisplay;

            string resStr = FormatNumber(result);
            historyService.AppendEntry(histLine + " = " + resStr);
            newInput = true;
            return resStr;
        }

        public string Backspace(string currentDisplay)
        {
            if (newInput || currentDisplay == "Ошибка" || currentDisplay == "0") return "0";
            if (currentDisplay.Length <= 1) return "0";
            string res = currentDisplay.Substring(0, currentDisplay.Length - 1);
            if (res == "-") return "0";
            return res;
        }

        public string Negate(string currentDisplay)
        {
            if (currentDisplay == "0" || currentDisplay == "Ошибка") return currentDisplay;
            if (currentDisplay.StartsWith("-")) return currentDisplay.Substring(1);
            else return "-" + currentDisplay;
        }

        public void Clear()
        {
            firstNumber = 0;
            operation = "";
            newInput = true;
        }

        private double ParseNumber(string text)
        {
            if (text == "Ошибка") return 0;
            try
            {
                return double.Parse(text.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
            }
            catch { return 0; }
        }

        public string FormatNumber(double value)
        {
            if (double.IsInfinity(value) || double.IsNaN(value)) return "Ошибка";

            if (Math.Abs(value) > 1e14 || (Math.Abs(value) < 1e-7 && value != 0))
                return value.ToString("G7").Replace(".", ",");

            return Math.Round(value, 8).ToString().Replace(".", ",");
        }

        private double Factorial(int n)
        {
            double res = 1;
            for (int i = 2; i <= n; i++) res *= i;
            return res;
        }

        private double RoundIfTiny(double value)
        {
            if (Math.Abs(value) < 1e-10) return 0;
            return value;
        }
    }
}
