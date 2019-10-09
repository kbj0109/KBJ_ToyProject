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
using System.Windows.Shapes;

namespace KBJ_MessengerByTCP.Client
{
    /// <summary>
    /// Interaction logic for EntryForClient.xaml
    /// </summary>
    public partial class EntryForClient : Window
    {
        public EntryForClient()
        {
            InitializeComponent();

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;//첫 UI를 화면 중앙에 띄우기
            textBoxIPAddress.Focus();

            Title = LanguageResource.language_res.strTitleEntryForClient;
            labelIPAddress.Content = LanguageResource.language_res.strLabelIPAddress;
            labelPortNumber.Content = LanguageResource.language_res.strlabelPortNumber;
            labelUserName.Content = LanguageResource.language_res.strLabelUserName;
            buttonConfirm.Content = LanguageResource.language_res.strButtonConfirm;
        }

        private void buttonConfirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string textValueIPAddress = textBoxIPAddress.Text.Replace(" ", "");
                string textValuePortNumber = textBoxPortNumber.Text.Replace(" ", "");
                string textValueUserName = textBoxUserName.Text.Trim();

                if ((textValuePortNumber.Length != 0) && (textValueUserName.Length != 0))
                {
                    int portNumber = Int32.Parse(textValuePortNumber);
                    ClientMessenger clientMessenger = new ClientMessenger(textValueIPAddress, portNumber, textValueUserName);
                    clientMessenger.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(LanguageResource.language_res.strMessageToValidateRequiredInfoToConnectServer);
                }
            }
            catch (ConnectionRefusedAtEntranceException ex)
            {
                MessageBox.Show(LanguageResource.language_res.strExceptionMessageConnectionRefusedAtEntrance);
            }
            catch (Exception ex)
            {
                MessageBox.Show("EntryForClient에서 에러 발생\n" + ex.ToString());
            }
        }

        //텍스트 박스에서 Enter 키 누르면 Confirm 버튼이 클릭되기 위함
        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                buttonConfirm_Click(sender, e);
        }

        //port번호 텍스트박스에 숫자만 들어가게 하기 위함, xaml 파일의 PreviewTextInput="PortNumberValidationTextBox" 설정과 연계
        private void PortNumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        //IP 주소 텍스트박스에 숫자만 들어가게 하기 위함, xaml 파일의 PreviewTextInput="IPAddressValidationTextBox" 설정과 연계
        private void IPAddressValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
