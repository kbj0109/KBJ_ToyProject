using KBJ_Game;
using KBJ_Tetris;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

namespace KBJ_Game
{
    /// <summary>
    /// Interaction logic for MainUI.xaml
    /// </summary>
    public partial class MainUI : Window
    {
        public static readonly int rowSizeOfGameBoard = 17;    //게임보드 세로 길이
        public static readonly int columnSizeOfGameBoard = 13; //게임보드 가로 길이
        public readonly int rectangleSizeOfGameBoard;   //게임보드 가로*세로
        public static readonly string scoreDigit = "000,000";   //게임 스코어 표시 단위
        public static readonly int rankingNumberToShow = 10;   //명예의 전당에 보여줄 사람 수

        readonly int left = -1; //키보드 왼쪽키
        readonly int right = 1; //키보드 오른쪽 키

        public static Random random = new Random();
        public bool gameStatus = false;        //게임 진행 상태
        public int gamePlayingScore = 0;   //게임 진행 점수
        public int gameLevel = 0;   //게임 레벨
        public readonly int gameStartSpeed = 170;   //시작시 게임 속도, 짧을수록 빨라짐
        public readonly int gameTimeToIncreaseLevel = 10;//게임 Level 올라가는데 걸리는 시간
        public readonly int gameTimeToChangeCatchingBird = 5;   //CatchingBird 바뀌는 시간, 5초

        //public static readonly string filePath = System.Environment.CurrentDirectory + "/../../";

        string userName;
        Bird bird;
        Dog dogIconForUser;
        HandleGameRanking ranking;

        RowDefinition[] rowGameBoard;
        ColumnDefinition[] columnGameBoard;
        System.Windows.Shapes.Rectangle[] rectangleGameBoard;

        public Label[] labelNameOnRankingBoard;
        public Label[] labelScoreOnRankingBoard;
        public Label[] labelDateOnRankingBoard;

        public MainUI(string userName)
        {
            try
            {
                Icon = new BitmapImage(new Uri(HandleResourceFiles.sourceGreenBird));

                Dog.dogColumnPosition = columnSizeOfGameBoard / 2;
                rectangleSizeOfGameBoard = rowSizeOfGameBoard * columnSizeOfGameBoard;

                InitializeComponent();
                Title = Title+ " - " + userName + "님 환영합니다~";
                createGameBoard();
                createGuideBoard();
                createRankingBoard();

                dogIconForUser = new Dog(this);
                dogIconForUser.createUserIcon();
                dogIconForUser.putUserIcon(Dog.dogColumnPosition);

                DispatcherTimer dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += new EventHandler(animateColorEffect);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
                dispatcherTimer.Start();

                this.userName = userName;
                new MusicToPlay();//음악 재생 시작
                ranking = new HandleGameRanking(this);  //저장된 점수 랭킹 불러오기
                ArrayList list = ranking.readGameRanking();
                ranking.showGameRankingOnBoard( list );
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void createGameBoard()
        {
            gridGameBoard.IsEnabled = false;    //UI 업데이트를 바로 하기 위해서 필요하다고 하지만, 없어도 되는거 같은데...
            rowGameBoard = new RowDefinition[rowSizeOfGameBoard];
            columnGameBoard = new ColumnDefinition[columnSizeOfGameBoard];
            rectangleGameBoard = new System.Windows.Shapes.Rectangle[rectangleSizeOfGameBoard];

            for (int a = 0; a < rowSizeOfGameBoard; a++)
            {
                rowGameBoard[a] = new RowDefinition();
                gridGameBoard.RowDefinitions.Add(rowGameBoard[a]);
            }
            for (int a = 0; a < columnSizeOfGameBoard; a++)
            {
                columnGameBoard[a] = new ColumnDefinition();
                gridGameBoard.ColumnDefinitions.Add(columnGameBoard[a]);
            }
            for (int a = 0; a < rectangleSizeOfGameBoard; a++)
            {
                rectangleGameBoard[a] = new System.Windows.Shapes.Rectangle();
            }

            int temp = 0;
            for (int a = 0; a < rowSizeOfGameBoard; a++)
            {
                for (int b = 0; b < columnSizeOfGameBoard; b++)
                {
                    Grid.SetRow(rectangleGameBoard[temp], a);
                    Grid.SetColumn(rectangleGameBoard[temp], b);
                    gridGameBoard.Children.Add(rectangleGameBoard[temp]);
                    temp++;
                }
            }
        }
        private void createGuideBoard()
        {
            textBlockGameGuide.Text += "\n";
            textBlockGameGuide.Text += "1. 'F1'을 누르면 게임이 시작 됩니다. \n";
            textBlockGameGuide.Text += "2. 떨어지는 새를 피해야 합니다. \n";
            textBlockGameGuide.Text += "3. Catching Bird 판의 새는 피하기보다, \n   높은 점수로 획득 가능합니다. \n";
            textBlockGameGuide.Text += "4. Catching Bird 판의 새는 "+gameTimeToChangeCatchingBird+"초마다 \n   변하거나, 혹은 변하지 않습니다. \n";
            textBlockGameGuide.Text += "5. 시간이 지날수록 새는 더 빨리 떨어집니다. \n";

        }
        private void createRankingBoard()
        {
            labelNameOnRankingBoard = new Label[rankingNumberToShow+1];
            labelScoreOnRankingBoard = new Label[rankingNumberToShow+1];
            labelDateOnRankingBoard = new Label[rankingNumberToShow+1];

            for (int a = 0; a < rankingNumberToShow+1; a++)
            {
                gridScoreBoard.RowDefinitions.Add(new RowDefinition());

                labelNameOnRankingBoard[a] = new Label();
                labelNameOnRankingBoard[a].Margin = new Thickness(7, 0, 0, 0);
                labelNameOnRankingBoard[a].FontWeight = FontWeights.Bold;
                Grid.SetRow(labelNameOnRankingBoard[a], a + 1);
                Grid.SetColumn(labelNameOnRankingBoard[a], 0);
                gridScoreBoard.Children.Add(labelNameOnRankingBoard[a]);

                labelScoreOnRankingBoard[a] = new Label();
                labelScoreOnRankingBoard[a].Margin = new Thickness(7, 0, 0, 0);
                labelScoreOnRankingBoard[a].FontWeight = FontWeights.Bold;
                Grid.SetRow(labelScoreOnRankingBoard[a], a + 1);
                Grid.SetColumn(labelScoreOnRankingBoard[a], 1);
                gridScoreBoard.Children.Add(labelScoreOnRankingBoard[a]);

                labelDateOnRankingBoard[a] = new Label();
                labelDateOnRankingBoard[a].Margin = new Thickness(7, 0, 0, 0);
                labelDateOnRankingBoard[a].FontWeight = FontWeights.Bold;
                Grid.SetRow(labelDateOnRankingBoard[a], a + 1);
                Grid.SetColumn(labelDateOnRankingBoard[a], 2);
                gridScoreBoard.Children.Add(labelDateOnRankingBoard[a]);
            }
        }
        private void animateColorEffect(object sender, EventArgs e)
        {
            int a = random.Next(1, 6);
            switch (a)
            {
                case 1:
                    labelRankingTitle.Foreground = new SolidColorBrush(Colors.Red);
                    borderRankingChart.Background = new SolidColorBrush(Colors.Green);
                    break;
                case 2:
                    labelRankingTitle.Foreground = new SolidColorBrush(Colors.Blue);
                    borderRankingChart.Background = new SolidColorBrush(Colors.Purple);
                    break;
                case 3:
                    labelRankingTitle.Foreground = new SolidColorBrush(Colors.Green);
                    borderRankingChart.Background = new SolidColorBrush(Colors.Orange);
                    break;
                case 4:
                    labelRankingTitle.Foreground = new SolidColorBrush(Colors.Purple);
                    borderRankingChart.Background = new SolidColorBrush(Colors.Red);
                    break;
                case 5:
                    labelRankingTitle.Foreground = new SolidColorBrush(Colors.Orange);
                    borderRankingChart.Background = new SolidColorBrush(Colors.Blue);
                    break;
            }
        }

        private void Keyboard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Right && gameStatus)
            {
                if (Dog.dogColumnPosition != columnSizeOfGameBoard - 1)
                    dogIconForUser.moveUserIcon(right);
            } else if (e.Key == Key.Left && gameStatus)
            {
                if (Dog.dogColumnPosition != 0)
                    dogIconForUser.moveUserIcon(left);
            } else if (e.Key == Key.F1 && !gameStatus)
            {
                startGame();
            }
        }

        private void startGame()
        {
            gamePlayingScore = 0;
            gameLevel = 0;
            gameStatus = true;

            labelGameLevel.Content = "Level " + gameLevel;
            labelGameStatus.Content = "START !!";
            labelPlayingScore.Content = gamePlayingScore.ToString(scoreDigit);

            gridGameBoard.Dispatcher.Invoke((ThreadStart)(() => { }), DispatcherPriority.ApplicationIdle);
            Thread.Sleep(1000);
            labelGameStatus.Visibility = Visibility.Hidden;

            bird = null;
            bird = new Bird(this);
            bird.startDropBirds();
        }

        public void endGame(LinkedList<System.Windows.Controls.Image> list)
        {
            try {
                //게임 진행 중 필요했던 모든 스레드를 종료시키자.
                bird.threadToDropBirds.Abort();
                bird.threadForCatchingBird.Abort();
                bird.threadForGameLevel.Abort();

                gameStatus = false;
                labelGameStatus.Content = "- DEAD -";
                labelGameStatus.Visibility = Visibility.Visible;

                for (int a = 0; a < list.Count; a++)
                {//다음 게임을 위해 화면의 새를 제거
                    gridGameBoard.Children.Remove(list.ElementAt(a));
                }
                gridGameBoard.Dispatcher.Invoke((ThreadStart)(() => { }), DispatcherPriority.ApplicationIdle);

                ranking.renewRankOnRankingBoard(userName, labelPlayingScore.Content+"");
                System.GC.Collect();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void MainUI_ClosedEvent(object sender, EventArgs e)
        {
            //종료 시에 원하는 코드를 넣어주자.
        }
    }
}
