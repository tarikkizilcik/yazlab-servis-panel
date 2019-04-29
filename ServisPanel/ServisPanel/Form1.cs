using System;
using System.Data;
using System.IO;
using System.Net;
using System.Drawing;
using System.Windows.Forms;

namespace ServisPanel
{
    public partial class Form1 : Form
    {
        private string imageFile;
        public Form1()
        {
            InitializeComponent();

            dateTimePicker.Format = DateTimePickerFormat.Custom;
            dateTimePicker.CustomFormat = "dd MM yyyy - HH:mm";
            dateTimePicker.MaxDate = DateTime.Now;

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
                var type = comboBoxType.Text;
                var publicationDate = ((DateTimeOffset) dateTimePicker.Value).ToUnixTimeMilliseconds();
                var image = File.ReadAllBytes(imageFile);

                var json = $"{{\"news\":{new News(author, title, body, type, publicationDate, image).ToJSON()}}}";

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

        private void linkLabelPicture_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                imageFile = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(imageFile);
            }
        }

        private void toolStripButtonTypes_Click(object sender, EventArgs e)
        {
            var formTypes = new FormTypes();
            formTypes.ShowDialog();
        }
    }
}