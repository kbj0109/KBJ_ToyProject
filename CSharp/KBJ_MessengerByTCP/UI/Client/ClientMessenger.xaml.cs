using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KBJ_MessengerByTCP.Client
{
    /// <summary>
    /// Interaction logic for ClientMessenger.xaml
    /// </summary>
    public partial class ClientMessenger : Window
    {
        Socket clientSocket;
        IPEndPoint ipEndPointForClient;
        bool clientAlive = true;

        private string ipAddress;   //접속하려는 IP
        private int portNumber;     //접속하려는 포트번호
        private string userName;    //클라이언트가 지정
        private string userNumber;  //서버로부터 배정 받는 번호 (채팅방의 입장 순서)

        //클라이언트 메신저를 시작하는 생성자
        public ClientMessenger(string textValueIPAddress, int portNumber, string textValueUserName)
        {
            try
            {
                InitializeComponent();
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;//UI를 화면 중앙에 띄우기

                this.ipAddress = textValueIPAddress;
                this.portNumber = portNumber;
                this.userName = textValueUserName;

                //클라이언트용 화면 꾸며주고
                Title = LanguageResource.language_res.strTitleForClientMessenger;
                buttonToSendMessage.Content = LanguageResource.language_res.strButtonToSendMessage;
                textBoxToInsertMessage.Focus();

                //클라이언트 메신저 시작
                startClientMessenger();
            }
            catch(ConnectionRefusedAtEntranceException ex)
            {
                this.Close();
                throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this.Close();
                throw;
            }
        }

        //클라이언트 메신저 시작
        public void startClientMessenger()
        {
            try
            {
                //서버에 접속
                connectToServer();

                Thread threadReceiveMessageFromClient = new Thread(receiveMessageFromServer);
                threadReceiveMessageFromClient.IsBackground = true;
                threadReceiveMessageFromClient.Start();
            }
            catch (ConnectionRefusedAtEntranceException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show("startClientMessenger에서 에러 발생\n" + ex.ToString());
            }
        }

        //서버에 접속하기
        public void connectToServer()
        {
            try
            {
                ipEndPointForClient = new IPEndPoint(IPAddress.Parse(ipAddress), portNumber);
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                clientSocket.Connect(ipEndPointForClient);

                //연결이 되면, 서버에게 사용하는 UserName을 알려준다
                Byte[] messageData = new Byte[1024];
                messageData = Encoding.Default.GetBytes(userName);
                clientSocket.Send(messageData);

                //그리고 서버로부터 UserNumber(입장순서)를 받기 위해 메세지를 한번 기다린다.
                messageData = new Byte[1024];
                clientSocket.Receive(messageData);
                userNumber = Encoding.Default.GetString(messageData);
            }
            catch(SocketException ex)
            {
                throw new ConnectionRefusedAtEntranceException();
                throw;
            }
            catch(Exception ex)
            {
                MessageBox.Show("connectToServer 에서 "+ex.ToString());
            }
        }

        //서버로부터 XML 메세지를 패킷으로 수신
        private void receiveMessageFromServer()
        {
            try
            {
                while (clientAlive)
                {
                    Byte[] messageData = new Byte[1024];
                    clientSocket.Receive(messageData);
                    String messageInXML = Encoding.Default.GetString(messageData);
                    putMessageOnMessageBoard(messageInXML);
                }
            }
            catch (Exception ex)
            {
                MessageInfo messageInfo = new MessageInfo("message", "Server", LanguageResource.language_res.strMessageToNotifyClientIsDisconnectedFromServer, getCurrentTime(), "0");
                string messageInXML = XMLHandlerForMessage.convertStringToXml(messageInfo);
                putMessageOnMessageBoard(messageInXML);
                //MessageBox.Show(LanguageResource.language_res.strMessageToNotifyClientIsDisconnectedFromServer, userName);
            }
        }

        //서버에게 XML 메세지를 패킷으로 송신
        private void sendMessageToServer(string messageInXML)
        {
            try
            {
                Byte[] messageData = Encoding.Default.GetBytes(messageInXML);
                clientSocket.Send(messageData);
            }
            catch (SocketException e)
            {
                MessageInfo messageInfo = new MessageInfo("message", "Server", LanguageResource.language_res.strMessageToNotifyClientIsDisconnectedFromServer, getCurrentTime(), "0");
                messageInXML = XMLHandlerForMessage.convertStringToXml(messageInfo);
                putMessageOnMessageBoard(messageInXML);
            }
        }

        //전송된 메세지를 UI 화면에 띄어주기
        private void putMessageOnMessageBoard(string messageInXML)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {//다른 스레드에서 UI를 컨트롤 하기 위한 Dispatcher.Invoke();
                MessageInfo messageInfo = XMLHandlerForMessage.readXmlToGetMessage(messageInXML);

                TextBox textBoxSender = new TextBox();
                textBoxSender.IsReadOnly = true;
                textBoxSender.Background = new SolidColorBrush(Colors.Transparent);
                textBoxSender.FontWeight = FontWeights.ExtraBold;
                textBoxSender.BorderThickness = new Thickness(0);
                textBoxSender.FontSize = 18;
                textBoxSender.Text = messageInfo.sender + " [" + messageInfo.userNumber + "] - " + messageInfo.time;

                TextBox textBoxMessage = new TextBox();
                textBoxMessage.IsReadOnly = true;
                textBoxMessage.Background = new SolidColorBrush(Colors.Transparent);
                textBoxMessage.TextWrapping = TextWrapping.Wrap;
                textBoxMessage.BorderThickness = new Thickness(0);
                textBoxMessage.FontSize = 16;
                textBoxMessage.Text = messageInfo.content;
                textBoxMessage.Margin = new Thickness(0, 0, 0, 10);

                if (messageInfo.sender.Equals(userName)&& Int32.Parse(messageInfo.userNumber)== Int32.Parse(userNumber))
                {
                    textBoxSender.HorizontalAlignment = HorizontalAlignment.Right;
                    textBoxMessage.HorizontalAlignment = HorizontalAlignment.Right;
                }
                DockPanel.SetDock(textBoxSender, Dock.Top);
                DockPanel.SetDock(textBoxMessage, Dock.Top);
                dockPanelToShowMessage.Children.Add(textBoxSender);
                dockPanelToShowMessage.Children.Add(textBoxMessage);

                scrollViewerToShowMessage.ScrollToEnd();
            }));
        }

        //전송버튼을 누르고 텍스트 상자 안의 메세지를 송신
        private void buttonToSendMessage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxToInsertMessage.Text.Trim() == "")
                {
                    return;
                }
                //입력한 메세지를 XML 값으로 변경
                string message = textBoxToInsertMessage.Text.Trim();
                MessageInfo messageInfo = new MessageInfo("message", userName, message, getCurrentTime(), userNumber);
                string messageInXML = XMLHandlerForMessage.convertStringToXml(messageInfo);

                sendMessageToServer(messageInXML);

                textBoxToInsertMessage.Text = "";
                textBoxToInsertMessage.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("buttonToSendMessage_Click에서\n " + ex.ToString());
            }
        }

        private void textBoxToInsertMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                buttonToSendMessage_Click(sender, e);
        }

        //클라이언트 메신저 종료될 때 실행되는 메소드
        private void Messenger_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            closeClientSocket();
        }

        //서버를 종료하는 메소드
        private void closeClientSocket()
        {
            try
            {
                clientAlive = false; // 나중에 닫기 버튼 누르면 false로 변환하자
                clientSocket.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("closeServerSocket 메소드에서 에러 발생\n" + ex.ToString());
            }
        }

        //현재 시간을 리턴하기, 메세지 보낼 때 현재 시간을 같이 보내기 위해
        private string getCurrentTime()
        {
            return DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss ") + LanguageResource.language_res.strSentTime;
        }
    }
}
