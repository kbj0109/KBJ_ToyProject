using System;
using System.Collections.Generic;
using System.Data;
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

namespace KBJ_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Boolean isParenthesisOpened = false;
        private String formulaToCalculate = "";

        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void buttonClear_Click(object sender, RoutedEventArgs e)
        {
            formulaToCalculate = "";
            textBoxToDisplay.Text = "";
        }

        private void buttonParenthesis_Click(object sender, RoutedEventArgs e)
        {
            if (isParenthesisOpened)
            { //괄호가 열려 있을 때, 괄호 버튼을 클릭한 경우
                formulaToCalculate += ")";
                textBoxToDisplay.Text += ")";
                isParenthesisOpened = false;
                buttonParenthesis.Content = "괄호 열기";
            }
            else
            { //괄호가 닫겨 있을 때, 괄호 버튼을 클릭한 경우
                formulaToCalculate += "(";
                textBoxToDisplay.Text += "(";
                isParenthesisOpened = true;
                buttonParenthesis.Content = "괄호 닫기";
            }
        }

        private void buttonDivide_Click(object sender, RoutedEventArgs e)
        {
            formulaToCalculate += "/";
            textBoxToDisplay.Text += "/";
        }

        private void buttonMultiply_Click(object sender, RoutedEventArgs e)
        {
            formulaToCalculate += "*";
            textBoxToDisplay.Text += "x";
        }

        private void buttonPlus_Click(object sender, RoutedEventArgs e)
        {
            formulaToCalculate += "+";
            textBoxToDisplay.Text += "+";
        }

        private void buttonMinus_Click(object sender, RoutedEventArgs e)
        {
            formulaToCalculate += "-";
            textBoxToDisplay.Text += "-";
        }

        private void buttonResult_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                string result = dt.Compute(formulaToCalculate, "")+"";
                formulaToCalculate = result;
                textBoxToDisplay.Text = result;
            }
            catch(Exception)
            {
                MessageBox.Show("잘못된 공식을 입력하였습니다.");
            }
        }

        private void buttonBackSpace_Click(object sender, RoutedEventArgs e)
        {//예외처리 필요*****************************************
            if (formulaToCalculate == "") return;   //현재 입력된 값이 없다면 그냥 리턴
            formulaToCalculate = formulaToCalculate.Substring(0, formulaToCalculate.Length - 1);
            textBoxToDisplay.Text = textBoxToDisplay.Text.Substring(0, textBoxToDisplay.Text.Length-1);
        }

        private void buttonDecimal_Click(object sender, RoutedEventArgs e)
        {
            formulaToCalculate += ".";
            textBoxToDisplay.Text += ".";
        }

        private void buttonNo0_Click(object sender, RoutedEventArgs e)
        {
            formulaToCalculate += "0";
            textBoxToDisplay.Text += "0";
        }

        private void buttonNo1_Click(object sender, RoutedEventArgs e)
        {
            formulaToCalculate += "1";
            textBoxToDisplay.Text += "1";
        }

        private void buttonNo2_Click(object sender, RoutedEventArgs e)
        {
            formulaToCalculate += "2";
            textBoxToDisplay.Text += "2";
        }

        private void buttonNo3_Click(object sender, RoutedEventArgs e)
        {
            formulaToCalculate += "3";
            textBoxToDisplay.Text += "3";
        }

        private void buttonNo4_Click(object sender, RoutedEventArgs e)
        {
            formulaToCalculate += "4";
            textBoxToDisplay.Text += "4";
        }

        private void buttonNo5_Click(object sender, RoutedEventArgs e)
        {
            formulaToCalculate += "5";
            textBoxToDisplay.Text += "5";
        }

        private void buttonNo6_Click(object sender, RoutedEventArgs e)
        {
            formulaToCalculate += "6";
            textBoxToDisplay.Text += "6";
        }

        private void buttonNo7_Click(object sender, RoutedEventArgs e)
        {
            formulaToCalculate += "7";
            textBoxToDisplay.Text += "7";
        }

        private void buttonNo8_Click(object sender, RoutedEventArgs e)
        {
            formulaToCalculate += "8";
            textBoxToDisplay.Text += "8";
        }

        private void buttonNo9_Click(object sender, RoutedEventArgs e)
        {
            formulaToCalculate += "9";
            textBoxToDisplay.Text += "9";
        }
    }
}
