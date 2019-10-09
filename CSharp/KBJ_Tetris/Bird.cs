using KBJ_Tetris;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace KBJ_Game
{
    class Bird
    {
        private MainUI mainUI;
        private Image eachBird;
        public LinkedList<Image> listFallingBirdsOnGame;

        static readonly BitmapImage RedBird = new BitmapImage(new Uri(HandleResourceFiles.sourceRedBird));
        static readonly BitmapImage BlueBird = new BitmapImage(new Uri(HandleResourceFiles.sourceBlueBird));
        static readonly BitmapImage GreenBird = new BitmapImage(new Uri(HandleResourceFiles.sourceGreenBird));
        static readonly BitmapImage YellowBird = new BitmapImage(new Uri(HandleResourceFiles.sourceYellowBird));

        readonly Thickness MarginNormal = new Thickness(-7, -7, -7, 0);
        readonly Thickness MarginSmall = new Thickness(2, 2, 2, 0);

        public Thread threadForCatchingBird;
        public Thread threadToDropBirds;
        public Thread threadForGameLevel;
        private Thread threadForScoreIcon;

        public Thread threadForMusicCatch;
        public Thread threadForMusicDead;

        private int gameCurrentSpeed;

        public Bird(MainUI mainUI)
        {
            this.mainUI = mainUI;
            eachBird = null;
            gameCurrentSpeed = mainUI.gameStartSpeed;

            threadForMusicCatch = new Thread(MusicToPlay.playCatchSound);
            threadForMusicDead = new Thread(MusicToPlay.playCatchSound);
            threadForMusicCatch.IsBackground = true;
            threadForMusicDead.IsBackground = true;
        }
        
        //새를 생성하는 메소드
        private Image createBird()
        {
            Image birdIcon = new Image();
            birdIcon.Margin = MarginNormal;
            defineColorOfBird(MainUI.random.Next(1, 5), birdIcon);
            Grid.SetColumn(birdIcon, MainUI.random.Next(0, MainUI.columnSizeOfGameBoard));
            mainUI.gridGameBoard.Children.Add(birdIcon);
            return birdIcon;
        }

        //새를 생성할 때, 랜덤으로 생성하는 메소드
        [MethodImpl(MethodImplOptions.Synchronized)]//defineColorOfBird가 확실히 한번에 한 스레드만 접근할 수 있도록
        private void defineColorOfBird(int randomNum, Image birdIcon)
        {
            switch (randomNum)
            {
                case 1:
                    birdIcon.Source = RedBird;
                    break;
                case 2:
                    birdIcon.Source = BlueBird;
                    break;
                case 3:
                    birdIcon.Source = GreenBird;
                    break;
                case 4:
                    birdIcon.Source = YellowBird;
                    break;
            }
        }

        //게임 시작을 알리고 다른 메소드를 호출 시작.
        public void startDropBirds()
        {
            threadToDropBirds = new Thread(dropBirdsOnGameBoard);
            threadForCatchingBird = new Thread(changeCatchingBird);
            threadForGameLevel = new Thread(increaseGameLevel);
            threadToDropBirds.IsBackground = true;
            threadForCatchingBird.IsBackground = true;
            threadForGameLevel.IsBackground = true;

            threadToDropBirds.Start();
            threadForCatchingBird.Start();
            threadForGameLevel.Start();
        }

        //게임 시작하면서, 새를 떨어트리기 시작하는 메소드
        private void dropBirdsOnGameBoard()
        {
            try
            {
                int numberOfFallingBirds = 1; //한번에 떨어지는 새의 수, 1마리
                listFallingBirdsOnGame = new LinkedList<Image>();//떨어지고 있는 새 모음
                while (mainUI.gameStatus)
                {
                    Thread.Sleep(gameCurrentSpeed);
                    mainUI.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {//다른 스레드에서 UI를 컨트롤 하기 위한 Dispatcher.Invoke();
                        for (int a = 0; a < numberOfFallingBirds; a++)
                        {
                            listFallingBirdsOnGame.AddLast(createBird());
                        }
                        for (int a = 0; a < listFallingBirdsOnGame.Count; a++)
                        {
                            eachBird = listFallingBirdsOnGame.ElementAt(a);
                            if (Grid.GetRow(eachBird) == MainUI.rowSizeOfGameBoard - 1)
                            {
                                mainUI.gridGameBoard.Children.Remove(eachBird);
                                listFallingBirdsOnGame.RemoveFirst();
                                renewGameScore();
                            }
                            else
                            {
                                Grid.SetRow(listFallingBirdsOnGame.ElementAt(a), Grid.GetRow(listFallingBirdsOnGame.ElementAt(a)) + 1);
                            }
                        }
                        mainUI.gridGameBoard.Dispatcher.Invoke(
                            (ThreadStart)(() => { }), DispatcherPriority.ApplicationIdle);
                    }));
                }
            }
            catch (Exception ex)
            {
                //게임 끝날 떄, 스레드가 정지되면서 ThreadAbortException 발생됨, 즉 ThreadAbortException은 Exception이 아님
                if(! ex.GetType().IsAssignableFrom(typeof(System.Threading.ThreadAbortException)))
                    MessageBox.Show(ex.ToString() + "");
            }
        }

        //화면 상단 우측에, CatchingBird 판에 잡을 수 있는 새가 바뀌는 메소드
        private void changeCatchingBird()
        {
            try
            {
                while (mainUI.gameStatus)
                {
                    for (int a = mainUI.gameTimeToChangeCatchingBird; a > 0; a--)
                    {
                        mainUI.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                        {//다른 스레드에서 UI를 컨트롤 하기 위한 Dispatcher.Invoke();
                            if (a == mainUI.gameTimeToChangeCatchingBird)
                            {
                                defineColorOfBird(MainUI.random.Next(1, 5), mainUI.imageCatchingBird);
                            }
                            mainUI.labelTimeForCatchingBird.Content = "남은 시간: " + a + " 초";
                            mainUI.catchingBirdBoard.Dispatcher.Invoke(
                                (ThreadStart)(() => { }), DispatcherPriority.ApplicationIdle);
                        }));
                        if(mainUI.gameStatus)//이 조건문이 없으면, 게임 종료 후에도 for문 남은 횟수가 1초 간격으로 실행된다
                            Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                //게임 끝날 떄, 스레드가 정지되면서 ThreadAbortException 발생됨, 즉 ThreadAbortException은 Exception이 아님
                if (!ex.GetType().IsAssignableFrom(typeof(System.Threading.ThreadAbortException)))
                    MessageBox.Show(ex.ToString());
            }

        }

        //시간 지남에 따라, 레벨 올라가면서 새가 떨어지는 속도 증가하는 메소드
        private void increaseGameLevel()
        {
            try { 
                while (mainUI.gameStatus)
                {
                    for (int a = mainUI.gameTimeToIncreaseLevel; a > 0; a--)
                    {
                        if (a == mainUI.gameTimeToIncreaseLevel)
                        {
                            mainUI.gameLevel++;
                            gameCurrentSpeed -= 15;
                            mainUI.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                            {//다른 스레드에서 UI를 컨트롤 하기 위한 Dispatcher.Invoke();
                                mainUI.labelGameLevel.Content = "Level "+ mainUI.gameLevel;
                                mainUI.catchingBirdBoard.Dispatcher.Invoke(
                                    (ThreadStart)(() => { }), DispatcherPriority.ApplicationIdle);
                                if (mainUI.gameLevel != 1)
                                {
                                    mainUI.labelGameStatus.Content = "- Speed Up -";
                                    mainUI.labelGameStatus.Visibility = Visibility.Visible;
                                    mainUI.labelGameStatus.Dispatcher.Invoke(
                                        (ThreadStart)(() => { }), DispatcherPriority.ApplicationIdle);
                                    Thread.Sleep(400);
                                    mainUI.labelGameStatus.Visibility = Visibility.Hidden;
                                }
                            }));
                        }
                        Thread.Sleep(1000);
                    }
                }
            }
            catch (Exception ex)
            {
                //게임 끝날 떄, 스레드가 정지되면서 ThreadAbortException 발생됨, 즉 ThreadAbortException은 Exception이 아님
                if (!ex.GetType().IsAssignableFrom(typeof(System.Threading.ThreadAbortException)))
                    MessageBox.Show(ex.ToString());
            }
        }

        //새가 땅에 닿았을 때, 점수를 올리거나 게임을 끝내기 위한 메소드
        private void renewGameScore()
        {
            if (Grid.GetColumn(eachBird) != Dog.dogColumnPosition)//새를 피했을 때, 10점 증가
            {
                mainUI.gamePlayingScore += 10;
                mainUI.labelPlayingScore.Content = mainUI.gamePlayingScore.ToString(MainUI.scoreDigit);
            }
            else if (mainUI.imageCatchingBird.Source.ToString().Equals(eachBird.Source.ToString()))//새를 잡았을 때, 100점 증가
            {
                try
                {
                    MusicToPlay.playCatchSound();
                    mainUI.gamePlayingScore += 100;
                    mainUI.labelPlayingScore.Content = mainUI.gamePlayingScore.ToString(MainUI.scoreDigit);

                    threadForScoreIcon = new Thread(showScoreIconForMoment);
                    threadForScoreIcon.IsBackground = true;
                    threadForScoreIcon.Start();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else//새에 부딫혔을 때, 게임 종료
            {
                MusicToPlay.playDeadSound();
                mainUI.endGame(listFallingBirdsOnGame);
            }
        }

        //새 잡았을 때, 스코어 아이콘에 애니메이션 효과 주기
        private void showScoreIconForMoment()
        {
            mainUI.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                Dog.scoreIcon.Visibility = Visibility.Visible;
                Dog.scoreIcon.Margin = MarginSmall;
                mainUI.gridGameBoard.Dispatcher.Invoke(
                    (ThreadStart)(() => { }), DispatcherPriority.ApplicationIdle);
            }));
            Thread.Sleep(300);
            mainUI.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                Dog.scoreIcon.Margin = MarginNormal;
                mainUI.gridGameBoard.Dispatcher.Invoke(
                    (ThreadStart)(() => { }), DispatcherPriority.ApplicationIdle);
            }));
            Thread.Sleep(300);
            mainUI.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {//다른 스레드에서 UI를 컨트롤 하기 위한 Dispatcher.Invoke();
                Dog.scoreIcon.Margin = MarginSmall;
                mainUI.gridGameBoard.Dispatcher.Invoke(
                    (ThreadStart)(() => { }), DispatcherPriority.ApplicationIdle);
            }));
            Thread.Sleep(300);
            mainUI.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                Dog.scoreIcon.Margin = MarginNormal;
                mainUI.gridGameBoard.Dispatcher.Invoke(
                    (ThreadStart)(() => { }), DispatcherPriority.ApplicationIdle);
            }));
            Thread.Sleep(200);
            mainUI.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                Dog.scoreIcon.Visibility = Visibility.Hidden;
                mainUI.gridGameBoard.Dispatcher.Invoke(
                    (ThreadStart)(() => { }), DispatcherPriority.ApplicationIdle);
            }));
        }
    }
}
