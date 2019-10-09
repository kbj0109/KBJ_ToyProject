using KBJ_Tetris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
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

namespace KBJ_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            try
            {
                new HandleResourceFiles();
                Icon = new BitmapImage(new Uri(HandleResourceFiles.sourceGreenBird));

                InitializeComponent(); //이후 주석해제
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;//첫 UI를 화면 중앙에 띄우기
                textBoxID.Focus(); //커서를 TextBox에 놓기
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void buttonConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxID.Text.Trim() == "")
            {
                MessageBox.Show("아이디를 입력 해주세요.");
                textBoxID.Focus();
            }
            else
            {
                string insertedValue = textBoxID.Text.Trim().Replace("_", "-");
                MainUI mainUI = new MainUI(insertedValue);
                mainUI.Show();
                this.Close();
            }
        }

        private void textBoxID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                buttonConfirm_Click(sender, e);
        }
    }
}