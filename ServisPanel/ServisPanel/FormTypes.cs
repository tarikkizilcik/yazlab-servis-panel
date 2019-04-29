using System;
using System.Data;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace ServisPanel
{
    public partial class FormTypes : Form
    {
        public FormTypes()
        {
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var httpWebRequest = (HttpWebRequest) WebRequest.Create("http://localhost:3000/api/news-types");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var json = $"{{\"type\":{{\"name\": \"{textBoxType.Text}\"}}}}";

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

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}