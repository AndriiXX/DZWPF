using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WPFDZ_1._2
{
    public partial class MainWindow : Window
    {
        private double _previousNumber = 0;
        private string _operator = string.Empty;
        private bool _isOperatorClicked = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Digit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (_isOperatorClicked || CurrentInput.Text == "0")
            {
                CurrentInput.Text = button.Content.ToString();
                _isOperatorClicked = false;
            }
            else
            {
                CurrentInput.Text += button.Content.ToString();
            }
        }

        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (!CurrentInput.Text.Contains("."))
            {
                if (string.IsNullOrEmpty(CurrentInput.Text))
                {
                    CurrentInput.Text = "0.";
                }
                else
                {
                    CurrentInput.Text += ".";
                }
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(CurrentInput.Text, out double currentNumber))
            {
                if (!string.IsNullOrEmpty(_operator))
                {
                    PerformCalculation(currentNumber);
                }
                else
                {
                    _previousNumber = currentNumber;
                }

                _operator = (sender as Button).Content.ToString();
                _isOperatorClicked = true;
                PreviousOperations.Text = $"{_previousNumber} {_operator}";
            }
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(CurrentInput.Text, out double currentNumber) && !string.IsNullOrEmpty(_operator))
            {
                PerformCalculation(currentNumber);
                PreviousOperations.Text = $"{_previousNumber} {_operator} {currentNumber} = {CurrentInput.Text}";
                _operator = string.Empty;
                _previousNumber = double.Parse(CurrentInput.Text);
            }
        }

        private void PerformCalculation(double currentNumber)
        {
            double result = _operator switch
            {
                "+" => _previousNumber + currentNumber,
                "-" => _previousNumber - currentNumber,
                "*" => _previousNumber * currentNumber,
                "/" => currentNumber != 0 ? _previousNumber / currentNumber : double.NaN,
                _ => double.NaN
            };

            if (double.IsNaN(result))
            {
                MessageBox.Show("Помилка: Ділення на нуль.");
                CurrentInput.Text = string.Empty;
            }
            else
            {
                CurrentInput.Text = result.ToString();
            }
        }

        private void ClearEntry_Click(object sender, RoutedEventArgs e)
        {
            CurrentInput.Text = string.Empty;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            CurrentInput.Text = string.Empty;
            PreviousOperations.Text = string.Empty;
            _previousNumber = 0;
            _operator = string.Empty;
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentInput.Text.Length > 0)
            {
                CurrentInput.Text = CurrentInput.Text.Substring(0, CurrentInput.Text.Length - 1);
                if (string.IsNullOrEmpty(CurrentInput.Text))
                {
                    CurrentInput.Text = "0";
                }
            }
        }
    }
}
