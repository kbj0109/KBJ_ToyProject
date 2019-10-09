using KBJ_Game;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;

namespace KBJ_Tetris
{
    class HandleResourceFiles
    {
        //private static string folderPath = "C:/Temp/KBJ_Game";
        private static string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/KBJ_Game";

        public static string sourceRedBird = "/Luma - Red.png";
        public static string sourceBlueBird = "/Luma - Blue.png";
        public static string sourceGreenBird = "/Luma - Green.png";
        public static string sourceYellowBird = "/Luma - Yellow.png";
        public static string sourceDogIcon = "/Dog.png";
        public static string sourceHeartIcon = "/heart-icon.png";
        public static string sourceCatchSound = "/Ta_Da.wav";
        public static string sourceDeadSound = "/HomerSimpson_doh.wav";

        public static string sourceRankingInfoText = "/KBJ_Game/GameRankingInfo.txt";

        public HandleResourceFiles()
        {
            sourceRedBird = folderPath + sourceRedBird;
            sourceBlueBird = folderPath + sourceBlueBird;
            sourceGreenBird = folderPath + sourceGreenBird;
            sourceYellowBird = folderPath + sourceYellowBird;
            sourceDogIcon = folderPath + sourceDogIcon;
            sourceHeartIcon = folderPath + sourceHeartIcon;
            sourceCatchSound = folderPath + sourceCatchSound;
            sourceDeadSound = folderPath + sourceDeadSound;
            sourceRankingInfoText = folderPath +"/.." +sourceRankingInfoText;

            createTempFolder();
            copyFilesFromResources();
            createTextFileForRanking();
        }

        private void createTempFolder()
        {
            bool exists = System.IO.Directory.Exists(folderPath);
            if (!exists)
                System.IO.Directory.CreateDirectory(folderPath);
        }

        private void copyFilesFromResources()
        {
            KBJ_Game.Properties.Resources.Dog.Save(sourceDogIcon);
            KBJ_Game.Properties.Resources.heart_icon.Save(sourceHeartIcon);

            KBJ_Game.Properties.Resources.Luma_Red.Save(sourceRedBird);
            KBJ_Game.Properties.Resources.Luma_Blue.Save(sourceBlueBird);
            KBJ_Game.Properties.Resources.Luma_Green.Save( sourceGreenBird);
            KBJ_Game.Properties.Resources.Luma_Yellow.Save( sourceYellowBird);

            Stream resource = KBJ_Game.Properties.Resources.ResourceManager.GetStream("Ta_Da");
            using (Stream output = File.OpenWrite( sourceCatchSound))
            {
                resource.CopyTo(output);
            }
            resource = KBJ_Game.Properties.Resources.ResourceManager.GetStream("HomerSimpson_doh");
            using (Stream output = File.OpenWrite(sourceDeadSound))
            {
                resource.CopyTo(output);
                resource.Close();
            }
        }

        private void createTextFileForRanking()
        {
            if (!File.Exists(sourceRankingInfoText))
            {
                using (StreamWriter writetext = new StreamWriter(sourceRankingInfoText))
                {
                    int score = 0;
                    for (int a = 0; a < MainUI.rankingNumberToShow; a++)
                    {
                        score += 100;
                        writetext.WriteLine(score.ToString(MainUI.scoreDigit)+"_하쿠나 마타타_2017.01.01 - 12:00:00");
                    }
                }
            }
        }

        public static void deleteFilesFromResourcesToLocal()
        {
            if (Directory.Exists(folderPath))
            {
                Directory.Delete(folderPath, true);
            }
        }
    }
}
