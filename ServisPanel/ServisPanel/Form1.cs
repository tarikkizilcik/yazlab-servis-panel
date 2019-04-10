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

        private void ButtonSave_Click(object sender, System.EventArgs e)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:3000/api/news/add");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string author = textBoxAuthor.Text;
                string title = textBoxTitle.Text;
                string body = richTextBoxBody.Text;

                string json = @"{""news"":{""author"":" + $"\"{author}\"," + @"""title"":" + $"\"{title}\"," + @"""body"":" + $"\"{body}\"" + "}}";

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
        }
    }
}
