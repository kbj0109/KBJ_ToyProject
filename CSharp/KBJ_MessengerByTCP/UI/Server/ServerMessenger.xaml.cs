using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
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

namespace KBJ_MessengerByTCP.Server
{
    /// <summary>
    /// Interaction logic for ServerMessenger.xaml
    /// </summary>
    public partial class ServerMessenger : Window
    {
        Socket serverSocket;
        IPEndPoint ipEndPointForServer;
        LinkedList<ClientInfo> listOfClient = new LinkedList<ClientInfo>();
        LinkedList<MessageInfo> listOfMessage = new LinkedList<MessageInfo>();

        int portNumber;
        bool serverAlive = true;
        string startMessengerTime;

        //서버 메신저 시작하는 생성자
        public ServerMessenger(int portNumber)
        {
            try
            {
                InitializeComponent();
                WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;//UI를 화면 중앙에 띄우기

                this.portNumber = portNumber;

                Title = LanguageResource.language_res.strTitleForServerMessenger;
                buttonToSendMessage.Content = LanguageResource.language_res.strButtonToSendMessage;
                textBoxToInsertMessage.Focus();

                startServerMessenger();
            }catch(PortNumberAlreadyUsedException ex)
            {
                this.Close();
                throw;
            }
        }

        //서버 메신저를 시작하는 메소드
        public void startServerMessenger()
        {
            try
            {
                //서버를 열고
                openServerSocket();

                //클라이언트를 지속적으로 받는 스레드를 실행
                Thread threadAcceptClientSocket = new Thread(acceptClientSocket);
                threadAcceptClientSocket.Start();
            }
            catch (PortNumberAlreadyUsedException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                MessageBox.Show("startServerMessenger에서 에러 발생\n" + ex.ToString());
            }
        }

        //서버 소켓을 열어서 준비한다.
        private void openServerSocket()
        {
            try
            {
                //Port 설정 후 Listen 대기
                ipEndPointForServer = new IPEndPoint(IPAddress.Any, portNumber);
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(ipEndPointForServer);
                serverSocket.Listen(20);//연결 수 지정

                startMessengerTime = DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss");

                //서버 준비가 완료되었다는 메세지 표시
                MessageInfo messageInfo = new MessageInfo("message", "Server", LanguageResource.language_res.strMessageToNotifyServerIsOpened, getCurrentTime(), 0 + "");
                string messageInXML = XMLHandlerForMessage.convertStringToXml(messageInfo);
                putMessageOnMessageBoard(messageInXML);
            }
            catch (SocketException ex)
            {
                throw new PortNumberAlreadyUsedException();
            }
            catch (Exception ex)
            {
                MessageBox.Show("openServerSocket 에서 에러 발생\n" + ex.ToString());
            }
        }

        //서버 소켓을 열고, 클라이언트를 기다린다. 스레드를 생성해서 다중 클라이언트 수신을 가능케 한다.
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void acceptClientSocket()
        {
            try
            {
                int userNumber = 1;
                while (serverAlive)
                {
                    //클라이언트가 들어오면, 접속을 받아들인다.
                    Socket clientSocket = serverSocket.Accept();

                    //먼저 클라이언트로부터 userName을 받기 위해 메세지를 한번은 기다린다
                    Byte[] messageData = new Byte[1024];
                    clientSocket.Receive(messageData);
                    string clientUserName = Encoding.Default.GetString(messageData);

                    //그리고 클라이언트에게 배정한 userNumber를 전송한다
                    messageData = new Byte[1024];
                    messageData = Encoding.Default.GetBytes(""+userNumber);
                    clientSocket.Send(messageData);

                    //클라이언트의 정보를 서버에 저장
                    ClientInfo client = new ClientInfo();
                    client.ClientSocket = clientSocket;
                    client.ClientSocketAlive = true;
                    client.UserName = clientUserName;
                    client.UserNumber = userNumber;
                    listOfClient.AddLast(client); //리스트에 클라이언트 정보를 저장
                    userNumber++;

                    //클라이언트 한명이 입장했다는 메세지를 존재하는 모든 클라이언트에게 전송
                    string message = (client.userName + " [" + client.userNumber + "] " + LanguageResource.language_res.strMessageToNotifyClientJoinedServer);
                    MessageInfo messageInfo = new MessageInfo("message", "Server", message, getCurrentTime(), 0 + "");
                    string messageInXML = XMLHandlerForMessage.convertStringToXml(messageInfo);
                    sendMessageToConnectedClients(messageInXML);

                    //클라이언트로부터 메세지를 받기위한 스레드를 각각 생성 
                    createThreadToReceiveMessageFromClient(client);
                }
            }
            catch (SocketException ex)
            {
                //클라이언트가 퇴장할 때마다 한번씩은 에러가 발생되기에 넘긴다.
            }catch(Exception ex)
            {
                MessageBox.Show("acceptClientSocket 에서 에러 발생\n" + ex.ToString());
            }
        }

        //각각의 클라이언트로부터 메세지를 계속 받기 위한 각각의 스레드 생성
        private void createThreadToReceiveMessageFromClient(ClientInfo client)
        {
            try
            {
                Thread threadReceiveMessageFromClient = new Thread(() => receiveMessageFromClient(client));
                threadReceiveMessageFromClient.IsBackground = true;
                threadReceiveMessageFromClient.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //클라이언트로부터 메세지를 패킷으로 받기
        private void receiveMessageFromClient(ClientInfo client)
        {
            try
            {
                while (client.ClientSocketAlive)
                {
                    Byte[] messageData = new Byte[1024];
                    client.ClientSocket.Receive(messageData);
                    String messageInXML = Encoding.Default.GetString(messageData);
                    sendMessageToConnectedClients(messageInXML);
                }
            }
            catch (SocketException e)
            {
                removeClientFromServer(client);
            }catch(Exception e)
            {
                MessageBox.Show("receiveMessageFromClient에서\n" + e.ToString());
            }
        }

        //클라이언트에게 메시지를 패킷으로 전송 
        private void sendMessageToConnectedClients(string messageInXML)
        {
            try
            {
                Byte[] messageData = Encoding.Default.GetBytes(messageInXML);
                for (int a = 0; a < listOfClient.Count; a++)
                {
                    listOfClient.ElementAt(a).ClientSocket.Send(messageData);
                }
                putMessageOnMessageBoard(messageInXML);
            }
            catch(Exception ex)
            {
                MessageBox.Show("sendMessageToConnectedClients 에서\n"+ex.ToString());
            }
        }

        //전송된 메세지를 서버 창에 띄어주기
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

                if (messageInfo.sender.Equals(LanguageResource.language_res.strServerManager))
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

            //MessageInfo에서 xml로 변한 string 값을 다시 MessageInfo로 제작해서 넘기는건 비효율적, 하지만 일단 그렇게 하자
            listOfMessage.AddLast(XMLHandlerForMessage.readXmlToGetMessage(messageInXML));
        }

        //서버를 종료하는 메소드
        private void closeServerSocket()
        {
            try
            {
                serverAlive = false; // 나중에 닫기 버튼 누르면 false로 변환하자
                serverSocket.Close();
            }catch(Exception ex)
            {
                MessageBox.Show("closeServerSocket 메소드에서 에러 발생\n" + ex.ToString());
            }
        }

        //클라이언트 서버에서 내보내기
        private void removeClientFromServer(ClientInfo client)
        {
            try
            {
                client.ClientSocketAlive = false;   //내보내려는 클라이언트의 활성상태 false로 변경
                client.ClientSocket.Close();    //내보내려는 클라이언트의 소켓 닫기

                string message = client.userName + " [" + client.userNumber + "] " + LanguageResource.language_res.strMessageToNotifyClientLeftServer;
                MessageInfo messageInfo = new MessageInfo("message", "Server", message, getCurrentTime(), 0 + "");
                string messageInXML = XMLHandlerForMessage.convertStringToXml(messageInfo);
                listOfClient.Remove(client); //내보내려는 클라이언트의 정보 삭제

                sendMessageToConnectedClients(messageInXML);
            }
            catch (Exception ex)
            {
                MessageBox.Show("removeClientFromServer 에서 에러 발생\n" + ex.ToString());
            }
        }

        //서버 메신저 종료될 때 실행되는 메소드
        private void Messenger_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                closeServerSocket();
                Thread thread = new Thread(createThreadForEmailAndDatabase);
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        
        private void createThreadForEmailAndDatabase()
        {
            string filePath = XMLHandlerForMessage.createXmlFileFromString(listOfMessage);
            EmailHandler.sendEmail(filePath);
            new DatabaseHandler().insertMessengerRecordIntoDB(filePath, startMessengerTime, DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss"), portNumber);
            //DB 기능은 프로젝트 속성에서 Target Platform이 32bit 운영체제로 되는걸 전제로 실행된다.
            //DB가 준비되어 있지 않다면, 김범준 컴퓨터에서만 실행 가능.
        }


        //서버 운영자가 클라이언트에게 메세지를 쓸 때, 전송 버튼을 누를 때
        private void buttonToSendMessage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxToInsertMessage.Text.Trim() == "")
                {
                    return;
                }
                //서버가 입력한 메세지를 XML 값으로 변경
                string message = textBoxToInsertMessage.Text.Trim();
                MessageInfo messageInfo = new MessageInfo("message", "Server", message, getCurrentTime(), 0 + "");
                string messageInXML = XMLHandlerForMessage.convertStringToXml(messageInfo);

                sendMessageToConnectedClients(messageInXML);

                textBoxToInsertMessage.Text = "";
                textBoxToInsertMessage.Focus();
            }
            catch(Exception ex)
            {
                MessageBox.Show("buttonToSendMessage_Click에서\n " + ex.ToString());
            }
        }

        private void textBoxToInsertMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                buttonToSendMessage_Click(sender, e);
        }

        //현재 시간을 리턴하기, 메세지 보낼 때 현재 시간을 같이 보내기 위해
        private string getCurrentTime()
        {
            return DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss ") + LanguageResource.language_res.strSentTime;
        }
    }
}
