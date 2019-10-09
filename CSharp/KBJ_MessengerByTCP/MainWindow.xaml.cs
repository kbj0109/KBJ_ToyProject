using KBJ_MessengerByTCP.Client;
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

namespace KBJ_MessengerByTCP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;//첫 UI를 화면 중앙에 띄우기
            Title = LanguageResource.language_res.strTitleMainWindow;
            buttonServer.Content = LanguageResource.language_res.strButtonServer;
            buttonClient.Content = LanguageResource.language_res.strButtonClient;
        }

        private void buttonServer_Click(object sender, RoutedEventArgs e)
        {
            EntryForServer entryForServer = new EntryForServer();
            entryForServer.Show();
            this.Close();
        }

        private void buttonClient_Click(object sender, RoutedEventArgs e)
        {
            EntryForClient entryForClient = new EntryForClient();
            entryForClient.Show();
            this.Close();
        }
    }
}
