using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KBJ_MessengerByTCP
{
    class DatabaseHandler
    {
        static Oracle.DataAccess.Client.OracleConnection conn;

        public string connectionInfo =
            "Data Source=(DESCRIPTION="
              + "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=LOCALHOST)(PORT=1521)))"
              + "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));"
              + "User Id=kbj0109;Password=kbj0109;";

        public void insertMessengerRecordIntoDB(string filePath, string startTime, string endTime, int portNumber)
        {
            
            try
            {
                // 오라클 연결
                conn = new Oracle.DataAccess.Client.OracleConnection(connectionInfo);
                conn.Open();

                //명령 실행을 위한 커맨드 객체 생성
                Oracle.DataAccess.Client.OracleCommand cmd = new Oracle.DataAccess.Client.OracleCommand();
                cmd.Connection = conn;

                // SQL문 지정 및 INSERT 실행
                cmd.CommandText = "insert into KBJ_MESSENGER(start_time, end_time, file_path, port_number) " +
                    "values(TO_DATE('" + startTime + "', 'YYYY.MM.DD-HH24:MI:SS')," +
                        " TO_DATE('" + endTime + "', 'YYYY.MM.DD-HH24:MI:SS'), '" +
                        filePath + "', " + portNumber + " )";
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch(Exception)
            {
                MessageBox.Show(LanguageResource.language_res.strExceptionMessageDBInsertFailed);
            }
            finally
            {
                conn.Close();
            }
        }

    }
}
