using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace KBJ_LogAnalyzer
{
    public partial class Main : Form
    {
        XmlWriterSettings xmlSetting = new XmlWriterSettings();

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            RTB.WordWrap = false;

            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            textBoxFilePath.Text = filePath;

            xmlSetting.OmitXmlDeclaration = true; //XML이 생성될 때, <?xml~~~ > 이런 declaration을 나타나지 않게 한다.

            //RTB.AllowDrop = true;
            //this.DragDrop += FileDragAndDropped; //드래그 드롭 기능 중지
            //RTB.DragDrop += FileDragAndDropped;  //드래그 드롭 기능 중지
        }

        #region 중지된 메서드
        /// <summary>
        /// 중지 1 - 파일 드래그 드롭으로 실행하기 
        /// </summary>
        private void FileDragAndDropped(object sender, DragEventArgs e)
        {
            try
            {
                //드래그 한 파일의 경로를 읽어온다
                string[] draggedFile = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                string filePath = draggedFile[0];

                //드래그한 파일의 확장자를 체크한다
                string ext = Path.GetExtension(filePath).ToLower();
                if (ext != ".txt" && ext != ".log" && ext != "" && ext != ".xml")
                {
                    MessageBox.Show("로그 파일이 아닙니다.");
                    return;
                }

                List<string> listLine = new List<string>();
                string line;
                StreamReader file = new StreamReader(filePath);
                int lineNumber = 1;
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Trim() != "") ;
                        listLine.Add( ManipulateTextToXML(line, lineNumber++));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }


        /// <summary>
        /// 중지2 - 날짜와 시간을 MESSAGE Xml의 Attribute에 넣는다
        /// </summary>
        private string SetAttributeToXML(string date, string time, string originalXml)
        {
            string manipulatedXML = null;
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(originalXml);   //string xml메세지를 읽기
                XmlNode node = xmlDocument.SelectSingleNode("MESSAGE");

                XmlAttribute attr = xmlDocument.CreateAttribute("a");
                attr.Value = string.Format("{0} {1}", date, time);
                node.Attributes.Append(attr);

                using (StringWriter sw = new StringWriter())
                {
                    using (XmlTextWriter tx = new XmlTextWriter(sw))
                    {
                        xmlDocument.WriteTo(tx);
                        manipulatedXML = sw.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return manipulatedXML;
        }

         /// <summary>
        /// 중지3 - nodeName 으로 노드를 생성
        /// </summary>
        private string CreateXMLWithoutAttribute(string nodeName)
        {
            using (var stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlSetting))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement(nodeName);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                }
                return stringWriter.ToString();
            }
        }
        #endregion


        /// <summary>
        /// text의 값이 XML 인지 아닌지 확인
        /// </summary>
        private bool CheckStringIsXML(string text)
        {
            try
            {
                XDocument.Parse(text);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// nodeName 으로 노드를 생성해서 attrValue를 설정
        /// </summary>
        private string CreateXMLWithAttribute(string nodeName, params XmlAttributeSet [] attributeList)
        {
            using (var stringWriter = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter, xmlSetting))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement(nodeName);

                    for(int a=0; a< attributeList.Length; a++)
                        xmlWriter.WriteAttributeString(attributeList[a].attributeName, attributeList[a].attributeValue);
                    //writer.WriteString("aa");
                    
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();
                }
                return stringWriter.ToString();
            }
        }

        

        /// <summary>
        /// 특정 라인을 우리가 원하는 형식의 XML로 변경
        /// </summary>
        /// <param name="line">라인 내용</param>
        /// <param name="lineNumber">몇번째 라인을 해석하고 있는지</param>
        /// <returns></returns>
        private string ManipulateTextToXML(string line, int lineNumber)
        {
            string CompletedTextLine = null;

            try
            {
                string lineWithoutTime = line.Substring(24).Trim();
                string date = line.Substring(0, 10);
                string time = line.Substring(11, 12);
                string messageType = lineWithoutTime.Substring(0, lineWithoutTime.IndexOf(" ")); //SendMessage인지, MESControl인지, MesLogs인지

                string lineAfterType = lineWithoutTime.Substring(messageType.Length+1).Trim();
                string onlyMessage = null;

                string nodeName = "_" + date;

                if (messageType == "SendMessage")
                {
                    onlyMessage = lineAfterType.Substring(5).Trim();
                    XmlAttributeSet attr1 = new XmlAttributeSet("_", string.Format("{0}  {1}", time, messageType));
                    string xmlOutsideNodeString = CreateXMLWithAttribute(nodeName, attr1);

                    CompletedTextLine = AppendInnerXmlToNode(xmlOutsideNodeString, nodeName, onlyMessage);
                }
                else if (messageType == "MESControl")
                {
                    onlyMessage = lineAfterType.Substring(1).Trim(); // ':' 값을 제거

                    //MESControl 이 XML 메세지라면,
                    if (CheckStringIsXML(onlyMessage))
                    {
                        //1. 날짜와 시간 정보를 가진 XML 노드를 생성
                        XmlAttributeSet attr1 = new XmlAttributeSet("_", string.Format("{0}  {1}", time, messageType));
                        string xmlOutsideNodeString = CreateXMLWithAttribute(nodeName, attr1);

                        CompletedTextLine = AppendInnerXmlToNode(xmlOutsideNodeString, nodeName, onlyMessage);
                    }
                    else  //MESControl이 상태 알림이라면,
                    {
                        XmlAttributeSet attr1 = new XmlAttributeSet("_", string.Format("{0}  {1} {2}", time, messageType, lineAfterType));
                        CompletedTextLine = CreateXMLWithAttribute(nodeName, attr1);
                    }
                }
                else if (messageType == "MesLogs")
                {
                    XmlAttributeSet attr1 = new XmlAttributeSet("_", string.Format("{0}  {1} {2}", time, messageType, lineAfterType));
                    CompletedTextLine = CreateXMLWithAttribute(nodeName, attr1);
                }
                else
                {
                    //MessageBox.Show("김범준이 모르는 무언가가 있었네?");
                    XmlAttributeSet attr = new XmlAttributeSet("error-", lineNumber + "번째 Line 확인 바람");
                    CompletedTextLine = CreateXMLWithAttribute("UnknownMessageType__________", attr); //뭔지 모르는 것도 일단 XML 형식으로 만들어서 표시해주자
                }
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                XmlAttributeSet attr = new XmlAttributeSet("error", lineNumber + "번째 Line 확인 바람");
                CompletedTextLine = CreateXMLWithAttribute("AnalyzeError__________", attr); //해석에 에러가 발생하면, 그 줄을 에러 처리해서 표시
            }
            return CompletedTextLine + "<_/><_/>";
        }

        /// <summary>
        /// xmlOutsideNodeString라는 String 값의 outsideNodeName 이름의 노드 안에 innerXml을 첨부한다
        /// </summary>
        private string AppendInnerXmlToNode(string xmlOutsideNodeString, string outsideNodeName, string innerXml)
        {
            //1. 생성한 XML은 <_2019~~~ /> 이렇게 하나의 태그로만 되어 있으니, 그것을 <_2019></> 로 변경
            int position = xmlOutsideNodeString.IndexOf("/>");
            xmlOutsideNodeString = xmlOutsideNodeString.Insert(position, "><");

            //2. <_2019></> 로 변경된 노드를 <_2019></_2019> 로 변경
            position = xmlOutsideNodeString.IndexOf("/") + 1;
            xmlOutsideNodeString = xmlOutsideNodeString.Insert(position, outsideNodeName);

            //3. <_2019></_2019> 로 변경된 노드 안에, 전체 <MESSAGE> 노드를 넣어준다
            position = xmlOutsideNodeString.IndexOf(">") + 1;
            xmlOutsideNodeString = xmlOutsideNodeString.Insert(position, innerXml);
            return xmlOutsideNodeString;
        }

        private void buttonAlignLog_Click(object sender, EventArgs e)
        {
            string entireText = "";

            for(int a=0; a< RTB.Lines.Length; a++)
            {
                if(RTB.Lines[a].Trim() != "")
                {
                    string line = ManipulateTextToXML(RTB.Lines[a], a+1);
                    entireText += line + Environment.NewLine;
                }
            }

            entireText = "<MessageList>" + entireText + "</MessageList>";

            File.WriteAllText(textBoxFilePath.Text + "/AlignedLog.xml", entireText);

            if(chkOpenIE.Checked)
                System.Diagnostics.Process.Start("iexplore.exe", Path.GetFullPath(textBoxFilePath.Text+"/AlignedLog.xml"));
        }


    }

    class XmlAttributeSet
    {
        public string attributeName;
        public string attributeValue;
        public XmlAttributeSet(string AttributeName, string AttributeValue)
        {
            this.attributeName = AttributeName;
            this.attributeValue = AttributeValue;
        }
    }

}
