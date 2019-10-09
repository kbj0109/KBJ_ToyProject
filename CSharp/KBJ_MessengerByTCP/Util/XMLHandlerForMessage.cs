using KBJ_MessengerByTCP.Server;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace KBJ_MessengerByTCP
{
    class XMLHandlerForMessage
    {
        //XML 메세지 샘플
        //<MessageData>
            // <type>message</type>
            // <sender>kbj0109</sender>
            // <content>Hello</content>
            // <time>2017.01.01-01:01.01</time>
            // <userNumber>1</userNumber>
        //</MessageData>

        //XML 메세지를 일반 메세지로 변환
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static MessageInfo readXmlToGetMessage(string xmlMessage)
        {
            MessageInfo messageInfo = null;
            try
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(xmlMessage);   //string xml메세지를 읽기
                XmlNode node = xdoc.SelectSingleNode("/MessageData");

                string type = node.SelectSingleNode("type").InnerText;
                string content = node.SelectSingleNode("content").InnerText;
                string time = node.SelectSingleNode("time").InnerText;
                string userNumber = node.SelectSingleNode("userNumber").InnerText;
                string sender = null;
                if (node.SelectSingleNode("sender").InnerText.Equals("Server"))
                {
                    //sender = StringValues.strServerManager;
                    sender = LanguageResource.language_res.strServerManager;
                }
                else
                {
                    sender = node.SelectSingleNode("sender").InnerText;
                }
                messageInfo = new MessageInfo(type, sender, content, time, userNumber);
            }
            catch(Exception e)
            {
                MessageBox.Show("readXmlToGetMessage 에서\n" + e.ToString());
            }
            return messageInfo;
        }

        //일반 메세지를 XML 메세지로 변환
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string convertStringToXml(MessageInfo messageInfo)
        {
            string xmlMessage = null;
            try
            {
                using (var stringWriter = new StringWriter())
                //using (var stringWriter = new Utf8StringWriter())  //아래에 Utf8StringWriter를 재정의 하면서, UTF 인코딩 타입 변경이 가능하다
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(stringWriter))
                    {
                        xmlWriter.WriteStartDocument();
                        xmlWriter.WriteStartElement("MessageData");

                        xmlWriter.WriteElementString("type", ReplaceHexadecimalSymbols(messageInfo.type));
                        xmlWriter.WriteElementString("sender", ReplaceHexadecimalSymbols(messageInfo.sender));
                        xmlWriter.WriteElementString("content", ReplaceHexadecimalSymbols(messageInfo.content));
                        xmlWriter.WriteElementString("time", ReplaceHexadecimalSymbols(messageInfo.time));
                        xmlWriter.WriteElementString("userNumber", ReplaceHexadecimalSymbols(messageInfo.userNumber));

                        xmlWriter.WriteEndElement();
                        xmlWriter.WriteEndDocument();
                    }
                    xmlMessage = stringWriter.ToString();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("convertStringToXml에서\n" + e.ToString());
            }
            return xmlMessage;
        }

        public static string createXmlFileFromString(LinkedList<MessageInfo> listOfMessage)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            filePath += "/"+DateTime.Now.ToString("yyyy.MM.dd-HH.mm.ss") + ".xml";
            try
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(filePath))
                {
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("AllMessageData");
                    for (int a = 0; a < listOfMessage.Count; a++)
                    {
                        xmlWriter.WriteStartElement("MessageData");

                        xmlWriter.WriteElementString("type", ReplaceHexadecimalSymbols(listOfMessage.ElementAt(a).type));
                        xmlWriter.WriteElementString("sender", ReplaceHexadecimalSymbols(listOfMessage.ElementAt(a).sender));
                        xmlWriter.WriteElementString("content", ReplaceHexadecimalSymbols(listOfMessage.ElementAt(a).content));
                        xmlWriter.WriteElementString("time", ReplaceHexadecimalSymbols(listOfMessage.ElementAt(a).time));
                        xmlWriter.WriteElementString("userNumber", ReplaceHexadecimalSymbols(listOfMessage.ElementAt(a).userNumber));

                        xmlWriter.WriteEndElement();
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndDocument();

                    MessageBox.Show(LanguageResource.language_res.strMessageToNotifyXmlFileIsSavedAt);//파일 저장 위치를 알림
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return filePath;
        }

        //이것 없이 XML을 쓸 때, "16진수 값 0x00은(는) 잘못된 문자입니다"  라는 에러가 발생
        public static string ReplaceHexadecimalSymbols(string txt)
        {
            string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
            return Regex.Replace(txt, r, "", RegexOptions.Compiled);
        }
    }

    //인코딩 타입 설정하려면 쓰자, 현재는 안씀
    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding
        {
            //get { return Encoding.UTF32; }
            get { return Encoding.UTF8; }
        }
    }
}
