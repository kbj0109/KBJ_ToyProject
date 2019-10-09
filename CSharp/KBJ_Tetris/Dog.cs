using KBJ_Tetris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace KBJ_Game
{
    class Dog
    {
        private MainUI mainUI;
        private Image userIcon = new Image();
        public static Image scoreIcon = new Image();

        public static int dogColumnPosition;   //강아지 사용자 아이콘의 위치 정보

        public Dog(MainUI mainUI)
        {
            this.mainUI = mainUI;
        }

        public void createUserIcon()
        {
            userIcon.Source = new BitmapImage(new Uri(HandleResourceFiles.sourceDogIcon));
            userIcon.Margin = new System.Windows.Thickness(-7, -7, -7, 0);
            Panel.SetZIndex(userIcon, 2);   //나중에 강아지 아이콘과 Bird 아이콘이 겹치면, 강아지 아이콘이 위에 보이게 한다
            Grid.SetRow(userIcon, MainUI.rowSizeOfGameBoard - 1);
            mainUI.gridGameBoard.Children.Add(userIcon);

            scoreIcon.Source = new BitmapImage(new Uri(HandleResourceFiles.sourceHeartIcon));
            scoreIcon.Margin = new System.Windows.Thickness(-7, -7, -7, 0);
            scoreIcon.Visibility = System.Windows.Visibility.Hidden;
            Panel.SetZIndex(scoreIcon, 2);   //나중에 score 아이콘과 Bird 아이콘이 겹치면, score 아이콘이 위에 보이게 한다
            Grid.SetRow(scoreIcon, MainUI.rowSizeOfGameBoard - 2);
            mainUI.gridGameBoard.Children.Add(scoreIcon);
        }

        public void moveUserIcon(int direction)
        {
            dogColumnPosition += direction;
            putUserIcon(dogColumnPosition);
        }

        public void putUserIcon(int futureColumnPosition)
        {
            Grid.SetColumn(userIcon, futureColumnPosition);
            Grid.SetColumn(scoreIcon, futureColumnPosition);
            userIcon.Dispatcher.Invoke(
                (ThreadStart)(() => { }), DispatcherPriority.ApplicationIdle);
        }
    }
}