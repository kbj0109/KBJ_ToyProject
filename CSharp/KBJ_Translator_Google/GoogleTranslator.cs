using Google.Cloud.Translation.V2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KBJ_Translator_Google
{
    public partial class GoogleTranslator : Form
    {
        public GoogleTranslator()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                TranslationClient client = TranslationClient.Create();
                var response = client.TranslateText("Hello World.", "ko");
                MessageBox.Show(response.TranslatedText);
            }
            catch(Exception ex)
            {
                RTB.Text = ex.ToString();
            }
        }
    }
}
