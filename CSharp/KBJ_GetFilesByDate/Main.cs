using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KBJ_GetFilesByDate
{
    public partial class Main : Form
    {
        string LogPath = "D:\\Log"; //Default Log Folder Path
        string ModuleResultPath = "D:\\TEST_RESULT"; //Default Result Folder Path

        public Main()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.Mario; //왼쪽 상단의 아이콘을 버섯으로 교체
            this.CenterToScreen(); //Set Form to Center of Screen.
            txtLogPath.Text = LogPath;
            txtResultPath.Text = ModuleResultPath;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                buttonStart.Enabled = false; //버튼을 너무 연속해서 누를 수 없게, 일단 사용 중지

                List<string> listFile = new List<string>(); //복사 될 파일 리스트

                //1. DateTimePicker에서 날짜만 추출한다
                string start = dateTimePickerStart.Value.ToString("yyyy-MM-dd");
                string end = dateTimePickerEnd.Value.ToString("yyyy-MM-dd");
                DateTime dtStart = DateTime.Parse(start);
                DateTime dtEnd = DateTime.Parse(end);

                //Log 파일들 저장
                try
                {
                    //2. Log 폴더가 존재하고, Log 폴더 경로가 제대로 설정 됐을 때
                    if (chkLog.Checked && chkLog.Checked == true)
                    {
                        string[] allFiles = Directory.GetFiles(LogPath, "*.*", SearchOption.AllDirectories); //폴더 내 모든 폴더까지 검색

                        while (dtEnd >= dtStart) //시작 날짜를 하루씩 더해가면서, 파일을 추출하고, 마지막 날짜에 도달하면 멈춘다
                        {
                            foreach (var filePath in allFiles)
                            {
                                //검색된 파일의 이름이 해당 날짜를 포함하지 않으면 넘어감
                                if (filePath.Contains(dtStart.ToString("yyyy-MM-dd")) == false)
                                    continue;
                                listFile.Add(filePath); //기준을 만족하는 파일일 경우, 복사될 리스트에 추가
                            }
                            dtStart = dtStart.AddDays(1); //하루 추가해서 다시 파일이 해당 날짜에 속하는지 확인
                        }

                        //2-a. 가져오려는 파일이 오늘 날짜의 파일이라면, PacTester.log 파일과 Mes.log 파일에 오늘 날짜가 없다. 그 경우 다른 방식으로 
                        if (dtEnd >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd")))
                        {
                            foreach (var filePath in allFiles)
                            {
                                if (filePath.EndsWith("Mes.log"))
                                    listFile.Add(filePath);
                                if (filePath.EndsWith("PackTester.log"))
                                    listFile.Add(filePath);
                            }
                        }
                    };
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Log 파일 분류 중 Exception");
                }

                //Result 파일들 저장
                try
                {
                    //비교될 기준 초기화 - Result 폴더는 Log 폴더와 날짜 표기 방식에 차이가 있다
                    start = dateTimePickerStart.Value.ToString("yyyyMMdd");
                    end = dateTimePickerEnd.Value.ToString("yyyyMMdd");
                    dtStart = dateTimePickerStart.Value;
                    dtEnd = dateTimePickerEnd.Value;

                    //3. Result 폴더가 존재하고, Result 폴더 경로가 제대로 설정 됐을 때
                    if (chkResult.Checked && chkResult.Enabled == true)
                    {
                        string[] allFiles = Directory.GetFiles(ModuleResultPath, "*.*", SearchOption.AllDirectories); //폴더 내 모든 폴더까지 검색

                        while (dtEnd >= dtStart) //시작 날짜를 하루씩 더해가면서, 파일을 추출하고, 마지막 날짜에 도달하면 멈춘다
                        {
                            foreach (var filePath in allFiles)
                            {
                                if (filePath.Contains(dtStart.ToString("yyyyMMdd")) == false)
                                    continue;
                                listFile.Add(filePath); //기준을 만족하는 파일일 경우, 복사될 리스트에 추가
                            }
                            dtStart = dtStart.AddDays(1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Result 파일 분류 중 Exception");
                }

                SaveAllFiles(listFile); //리스트에 포함된 모든 파일을 복사한다
            }
            catch(Exception ex)
            {
                MessageBox.Show("Exception 발생 - " + ex.ToString());
            }
            finally
            {
                buttonStart.Enabled = true; // Disable 된 버튼을 다시 사용 가능하게
            }
        }

        /// <summary>
        /// List에 담긴 모든 FilePath의 파일들을, 현재 폴더에 복사한다
        /// </summary>
        private void SaveAllFiles(List<string> listFile)
        {
            string ExceptionFileList = ""; //혹시 복사하다가 에러가 발생한 파일이 있다면, 그 파일들 이름을 저장해서 알리자.

            try
            {
                string folderPath = DateTime.Now.ToString("yyyy.MM.dd - HH.mm.ss.fff"); //버튼을 누른시간이 폴더 이름으로 지정되고, 이 폴더에 복사된 파일들이 저장된다

                for (int a = 0; a < listFile.Count; a++)
                {
                    try
                    {
                        string path = listFile[a].Substring(2); //기존 경로에서 'D:' 부분만 제거
                        string newPath = Path.GetDirectoryName(Application.ExecutablePath) + "/" + folderPath + path; //새로운 경로로 현재 경로 + 기존 경로

                        //복사되려는 Path의 폴더가 존재하지 않는다면 생성
                        if (Directory.Exists(Path.GetDirectoryName(newPath)) == false)
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(newPath));
                        }

                        File.Copy(listFile[a], newPath); //파일 복사
                    }
                    catch(Exception ex)
                    {
                        ExceptionFileList += listFile[a] + "\n";
                    }
                }
                MessageBox.Show(folderPath + " 시간에 실행된 파일 추출 완료되었습니다");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ExceptionFileList+ "들의 파일 복사에 실패했습니다");
            }
        }

        /// <summary>
        /// 선택된 날짜가 변경될 때마다, 시작 날짜와 마지막 날짜 범위가 침범되지 않게 조정
        /// </summary>
        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePickerStart.Value > dateTimePickerEnd.Value)
            {
                dateTimePickerEnd.Value = dateTimePickerStart.Value;
            }
        }

        /// <summary>
        /// 선택된 날짜가 변경될 때마다, 시작 날짜와 마지막 날짜 범위가 침범되지 않게 조정
        /// </summary>
        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePickerStart.Value > dateTimePickerEnd.Value)
            {
                dateTimePickerStart.Value = dateTimePickerEnd.Value;
            }
        }

        //Log 경로가 변경될 때마다, 해당 경로가 존재하는지 확인
        private void txtLogPath_TextChanged(object sender, EventArgs e)
        {
            //Log 폴더의 경로가 제대로 설정되었는가 판별
            if (Directory.Exists(txtLogPath.Text))
            {
                chkLog.Enabled = true;
                labelLogPath.Text = "O";
                labelLogPath.ForeColor = Color.Green;
            }
            else
            {
                chkLog.Enabled = false;
                labelLogPath.Text = "X";
                labelLogPath.ForeColor = Color.Red;
            }

        }

        //Result 경로가 변경될 때마다, 해당 경로가 존재하는지 확인
        private void txtResultPath_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(txtResultPath.Text))
            {
                chkResult.Enabled = true;
                labelResultPath.Text = "O";
                labelResultPath.ForeColor = Color.Green;
            }
            else
            {
                chkResult.Enabled = false;
                labelResultPath.Text = "X";
                labelResultPath.ForeColor = Color.Red;
            }
        }

        /// <summary>
        ///Log 폴더 경로 변경을 위한 Folder Browser을 실행
        /// </summary>
        private void buttonLogPath_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtLogPath.Text = fbd.SelectedPath;
                }
            }
        }

        /// <summary>
        ///Result 폴더 경로 변경을 위한 Folder Browser을 실행
        /// </summary>
        private void buttonResultPath_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtResultPath.Text = fbd.SelectedPath;
                }
            }
        }
        
    }
}
