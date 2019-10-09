using KBJ_MessengerByTCP.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KBJ_MessengerByTCP
{
    /// <summary>
    /// Interaction logic for EntryForServer.xaml
    /// </summary>
    public partial class EntryForServer : Window
    {
        

        public EntryForServer()
        {
            InitializeComponent();
            
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;//첫 UI를 화면 중앙에 띄우기
            textBoxPortNumber.Focus();

            Title = LanguageResource.language_res.strTitleEntryForServer;
            labelPortNumber.Content = LanguageResource.language_res.strlabelPortNumber;
            buttonConfirmPortNumber.Content = LanguageResource.language_res.strButtonConfirmPortNumber;
        }

        private void buttonConfirmPortNumber_Click(object sender, RoutedEventArgs e)
        {
            try {
                string textValue = textBoxPortNumber.Text.Replace(" ", "");
                if (textValue.Length != 0)
                {
                    int portNumber = Int32.Parse(textValue);
                    ServerMessenger serverMessenger = new ServerMessenger(portNumber);
                    serverMessenger.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(LanguageResource.language_res.strMessageToValidatePortNumber);
                }
            }
            catch (PortNumberAlreadyUsedException ex)//입력한 포트넘버가 이미 사용 중일 때 발생하는 에러
            {
                MessageBox.Show(LanguageResource.language_res.strExceptionMessagePortNumberAlreadyUsed);
            }
            catch (Exception ex)
            {
                MessageBox.Show("EntryForServer에서 에러 발생\n"+ex.ToString());
            }
        }

        //port 번호 텍스트 박스에서 Enter 키 누르면 Confirm 버튼이 클릭되기 위함
        private void textBoxPortNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                buttonConfirmPortNumber_Click(sender, e);
        }

        //port번호 텍스트박스에 숫자만 들어가게 하기 위함, xaml 파일의 PreviewTextInput="NumberValidationTextBox" 설정과 연계
        private void PortNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
