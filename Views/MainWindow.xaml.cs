using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Calculator.Services;
using Calculator.Services.Interfaces;

namespace Calculator.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IHistoryService historyService = new HistoryService();
        private readonly ICalculatorService calc;

        public MainWindow()
        {
            InitializeComponent();
            calc = new CalculatorService(historyService);

            App app = (App)Application.Current;

            if (app.MainWindowTheme != -1)
            {
                app.ChangeTheme(app.MainWindowTheme);
                ColorTheme.SelectedIndex = app.MainWindowTheme - 1;
            }
        }
        private void ColorTheme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ColorTheme == null) return;
            int themeIndex = ColorTheme.SelectedIndex + 1;
            App app = (App)Application.Current;
            app.MainWindowTheme = themeIndex;
            app.ChangeTheme(themeIndex);
        }

        private void BtnSwitchToEng_Click(object sender, RoutedEventArgs e)
        {
            Window1 eng = new Window1();
            eng.Show();
            this.Close();
        }

        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            string digit = ((Button)sender).Content.ToString();
            MainOutput.Text = calc.InputDigit(MainOutput.Text, digit);
        }

        private void Comma_Click(object sender, RoutedEventArgs e)
        {
            MainOutput.Text = calc.InputComma(MainOutput.Text);
        }

        private void Op_Click(object sender, RoutedEventArgs e)
        {
            string op = ((Button)sender).Content.ToString();
            string newDisplay;
            HistOutput.Text = calc.SetOperation(MainOutput.Text, op, out newDisplay);
            MainOutput.Text = newDisplay;
        }

        private void Eq_Click(object sender, RoutedEventArgs e)
        {
            string histLine;
            MainOutput.Text = calc.PressEquals(MainOutput.Text, out histLine);
            if (histLine != "") HistOutput.Text = histLine;
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            calc.Clear();
            MainOutput.Text = "0";
            HistOutput.Text = "";
        }

        private void BS_Click(object sender, RoutedEventArgs e)
        {
            MainOutput.Text = calc.Backspace(MainOutput.Text);
        }

        private void Negate_Click(object sender, RoutedEventArgs e)
        {
            MainOutput.Text = calc.Negate(MainOutput.Text);
        }

        private void Hist_Click(object sender, RoutedEventArgs e)
        {
            List<string> lines = historyService.LoadHistory();

            if (lines.Count == 0)
            {
                MessageBox.Show("История пуста", "История");
                return;
            }

            string all = "";
            foreach (string line in lines)
                all = all + line + "\n";

            MessageBoxResult answer = MessageBox.Show(
                all + "\nОчистить историю?",
                "История вычислений",
                MessageBoxButton.YesNo);

            if (answer == MessageBoxResult.Yes)
            {
                historyService.ClearHistory();
                HistOutput.Text = "";
            }
        }
    }
}
