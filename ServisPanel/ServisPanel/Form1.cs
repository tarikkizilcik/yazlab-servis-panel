using System;
using System.Data;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace ServisPanel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            buttonSave.Click += ButtonSave_Click;
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            var httpWebRequest = (HttpWebRequest) WebRequest.Create("http://localhost:3000/api/news/add");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var author = textBoxAuthor.Text;
                var title = textBoxTitle.Text;
                var body = richTextBoxBody.Text;
                var type = textBoxType.Text;

                var json = $"{{\"news\":{new News(author, title, body, type).ToJSON()}}}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
            using (var streamReader =
                new StreamReader(httpResponse.GetResponseStream() ?? throw new NoNullAllowedException()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
    }
}