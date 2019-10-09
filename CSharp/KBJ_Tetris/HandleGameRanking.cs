using KBJ_Tetris;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace KBJ_Game
{
    class HandleGameRanking
    {
        MainUI mainUI = null;

        public HandleGameRanking(MainUI mainUI)
        {
            this.mainUI = mainUI;
        }

        public void renewRankOnRankingBoard(string userName, string score)
        {
            try
            {
                saveGameRanking(userName, score);
                ArrayList listOfRecordedRanking = readGameRanking();
                showGameRankingOnBoard(listOfRecordedRanking);
            }catch(Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public ArrayList readGameRanking()
        {
            string[] gameRankingInfo = File.ReadAllLines(HandleResourceFiles.sourceRankingInfoText);
            ArrayList listOfRecordedRanking = new ArrayList();

            for (int a = 0; a < gameRankingInfo.Length; a++)
            {
                listOfRecordedRanking.Add(gameRankingInfo[a]);
            }
            listOfRecordedRanking.Sort();
            listOfRecordedRanking.Reverse();
            return listOfRecordedRanking;
        }

        public void saveGameRanking(string userName, string score)
        {
            try { 
                using (StreamWriter writer = File.AppendText(HandleResourceFiles.sourceRankingInfoText))
                {
                    string now = DateTime.Now.ToString("yyyy.MM.dd - HH:mm:ss");
                    writer.WriteLine(score + "_"+ userName + "_"+now);
                }
            }catch(Exception e)
            {
                    MessageBox.Show(e.ToString());
            }
        }

        public void showGameRankingOnBoard(ArrayList listOfRecordedRanking)
        {
            try
            {
                for (int a = 0; a < MainUI.rankingNumberToShow; a++)
                {
                    string[] gameRecord = listOfRecordedRanking[a].ToString().Split('_');
                    mainUI.labelScoreOnRankingBoard[a].Content = gameRecord[0];
                    mainUI.labelNameOnRankingBoard[a].Content = gameRecord[1];
                    mainUI.labelDateOnRankingBoard[a].Content = gameRecord[2];
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("showGameRankingOnBoard 메소드 \n" + e.ToString());
            }
        }

    }
}
