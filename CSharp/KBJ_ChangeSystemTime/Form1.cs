using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Threading;

namespace KBJ_ChangeSystemTime
{
    public partial class Form1 : Form
    {
        #region =========== System Time 변경 Setting ===========
        [DllImport("kernel32.dll", EntryPoint = "GetSystemTime", SetLastError = true)]
        public extern static void Win32GetSystemTime(ref SystemTime sysTime);

        [DllImport("kernel32.dll", EntryPoint = "SetSystemTime", SetLastError = true)]
        public extern static bool Win32SetSystemTime(ref SystemTime sysTime);

        public struct SystemTime
        {
            public short Year;
            public short Month;
            public short DayOfWeek;
            public short Day;
            public short Hour;
            public short Minute;
            public short Second;
            public short Millisecond;
        };
        #endregion

        string TimeFormat = "yyyy-MM-dd";
        bool changeRes = false; //시간 변경 성공 여부
        DateTime actualTime; // 실제 시간

        public Form1()
        {
            InitializeComponent();
            this.CenterToScreen(); //화면 중앙을 기본 위치로
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;//크기 고정
            this.KeyPreview = true; //단축키 설정

            changeDateTimePicker.Format = DateTimePickerFormat.Custom;
            changeDateTimePicker.CustomFormat = TimeFormat;
            labelErrorMessage.Visible = false;

            this.FormClosed += EventForCloseForm;
            this.KeyDown += EventForKeyPressed;

            GetActualTimeFromInternet(); //MS 홈페이지에서 현재 시간 가져오기
        }

        #region ================ Event 모음

        /// <summary>
        /// 단축키 실행시 발생되는 이벤트
        /// </summary>
        private void EventForKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Alt)
                buttonChange_Click(null, null);
            if (e.KeyCode == Keys.X && e.Modifiers == Keys.Alt)
                buttonBacToActualDate_Click(null, null);
        }

        /// <summary>
        /// Form 종료시 발생되는 이벤트
        /// </summary>
        private void EventForCloseForm(object sender, FormClosedEventArgs e)
        {
            if (chkCloseWithOriginalTime.Checked)
                buttonBacToActualDate_Click(null, null);
        }

        /// <summary>
        /// 시간 변경을 위해 Change 버튼이 눌렸을 때 발생하는 이벤트
        /// </summary>
        private void buttonChange_Click(object sender, EventArgs e)
        {
            //선택한 날짜의 0시 0분 0초로 설정되기에, 현재 시간을 설정해준다
            DateTime selectedDateTime = changeDateTimePicker.Value.AddHours(actualTime.Hour).AddMinutes(actualTime.Minute).AddSeconds(actualTime.Second);
            ChangeSystemTime(selectedDateTime);
        }

        /// <summary>
        /// 원래의 시간으로 변경을 위해 Bac 버튼이 눌렸을 때 발생
        /// </summary>
        private void buttonBacToActualDate_Click(object sender, EventArgs e)
        {
            ChangeSystemTime(actualTime);
        }

        /// <summary>
        /// MS 홈페이지에서 가져온 실제 시간을, 초마다 Update 하는 메서드
        /// </summary>
        /// <param name="sender">이벤트의 트리거가 되는 Timer</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                actualTime = actualTime.AddSeconds(1);
                labelActualTime.Text = actualTime.ToString(TimeFormat);
                labelSystemTime.Text = DateTime.Now.ToString(TimeFormat);
            }
            catch
            {
                (sender as System.Windows.Forms.Timer).Stop(); //타이머에서 에러 발생시, 타이머 정지
            }
        }
        #endregion



        /// <summary>
        /// System 시간을 변경한다
        /// </summary>
        /// <param name="updatedTime">변경될 새로운 시간</param>
        private void ChangeSystemTime(DateTime TimeToUpdate)
        {
            DateTime orgTime = DateTime.Now;

            TimeToUpdate = TimeToUpdate.AddHours(-9);
            // Set system date and time
            SystemTime updatedTime = new SystemTime();
            updatedTime.Year = (short)TimeToUpdate.Year;
            updatedTime.Month = (short)TimeToUpdate.Month;
            updatedTime.Day = (short)TimeToUpdate.Day;
            updatedTime.Hour = (short)TimeToUpdate.Hour;
            updatedTime.Minute = (short)TimeToUpdate.Minute;
            updatedTime.Second = (short)TimeToUpdate.Second;

            // Call the unmanaged function that sets the new date and time instantly
            changeRes = Win32SetSystemTime(ref updatedTime);
            ShowResult(changeRes, orgTime, TimeToUpdate.AddHours(9));
        }

        /// <summary>
        /// 시간 변경시 UI에 나타날 성공여부 메세지 Handling
        /// </summary>
        private void ShowResult(bool changeRes, DateTime orgDateTime, DateTime TimeToUpdate)
        {
            string orgTime = orgDateTime.ToString(TimeFormat);
            string newTime = TimeToUpdate.ToString(TimeFormat);

            if (changeRes)
            {
                labelTimeChange.Text = string.Format("{0}  To  {1}", orgTime, newTime);
                labelResult.Text = "Succeed";
            }
            else
            {
                labelTimeChange.Text = string.Format("{0}  To  {1}", orgTime, newTime);
                labelResult.Text = "Failed";
            }
                
        }

        /// <summary>
        /// MS 홈페이지에서 인터넷으로 실제 시간 가져오기
        /// </summary>
        private void GetActualTimeFromInternet()
        {
            DateTime dt;
            try
            {
                var myHttpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create("http://www.microsoft.com");
                var response = myHttpWebRequest.GetResponse();
                string todaysDates = response.Headers["date"];
                dt = DateTime.ParseExact(todaysDates,
                                           "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                           //"yyyy/MM/dd - hh:mm:ss",
                                           System.Globalization.CultureInfo.InvariantCulture.DateTimeFormat,
                                           System.Globalization.DateTimeStyles.AssumeUniversal);
            }
            catch (Exception ex)
            {
                dt = DateTime.Now;
                labelActualTime.Text = DateTime.Now.ToString(TimeFormat);
                labelErrorMessage.Text = "인터넷에서 정확한 현재 시간을 가져오기에 실패 했습니다.\n현재 컴퓨터 시간을 실제 시간으로 인식하겠습니다.";
                labelErrorMessage.Visible = true;
            }
            actualTime = dt;
            labelActualTime.Text = actualTime.AddSeconds(1).ToString(); // Timer가 시작하고 1초 뒤부터 초가 업데이트 되기에, 미리 1초를 더해준다
            StartTimerForActualTime();
        }

        /// <summary>
        /// MS 홈페이지에서 인터넷으로 실제 시간 가져오기 성공했을 시, 발생하는 Timer
        /// </summary>
        private void StartTimerForActualTime()
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        

    }
}
