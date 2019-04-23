namespace ServisPanel
{
    public class News
    {
        public string author;
        public string body;
        public string title;
        public string type;
        public byte[] image;

        public News(string author, string title, string body, string type, byte[] image)
        {
            this.author = author;
            this.title = title;
            this.body = body;
            this.type = type;
            this.image = image;
        }
    }
}