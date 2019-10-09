using KBJ_Tetris;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace KBJ_Game
{
    class MusicToPlay
    {
        //private string musicBackgroundFilePath = "C:/Users/kbj01/Desktop/" + "BGM_Tetris_Kalinka.wav";
        //public static string musicCatchFilePath = MainUI.filePath + "Sounds/Ta_Da.wav";
        //private static string musicDeadFilePath = MainUI.filePath + "Sounds/HomerSimpson_doh.wav";

        static MediaPlayer musicPlayerForCatch;
        static MediaPlayer musicPlayerForDead;

        public MusicToPlay()
        {
            musicPlayerForCatch = new System.Windows.Media.MediaPlayer();
            musicPlayerForDead = new System.Windows.Media.MediaPlayer();

            Thread musicThread = new Thread(playBackgroundMusic);
            musicThread.IsBackground = true;
            musicThread.Start();
        }

        private void playBackgroundMusic()
        {
            try
            {
                System.IO.Stream strForMusic = KBJ_Game.Properties.Resources.BGM_Tetris_Kalinka;
                SoundPlayer musicPlayerForBackground = new SoundPlayer(strForMusic);
                for (;;) { 
                    musicPlayerForBackground.PlaySync ();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("음악 재생 실패 했습니다.");
            }
        }

        public static void playCatchSound()
        {
            try
            {
                musicPlayerForCatch.Open(new System.Uri(HandleResourceFiles.sourceCatchSound));
                musicPlayerForCatch.Play();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        public static void playDeadSound()
        {
            try
            {
                musicPlayerForDead.Open(new System.Uri(HandleResourceFiles.sourceDeadSound));
                musicPlayerForDead.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
